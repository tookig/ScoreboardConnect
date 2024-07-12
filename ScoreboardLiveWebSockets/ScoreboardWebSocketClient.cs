using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ScoreboardLiveSocket.Messages;
using System.Data;

namespace ScoreboardLiveWebSockets {
  public sealed class ScoreboardWebSocketClient : IDisposable {
    private static readonly int RECONNECT_CHECK_INTERVAL = 5;
    private static readonly int RECONNECT_TRY_INTERVAL = 30;
    private static readonly ClientState[] RECONNECT_STATES = [ClientState.Stopped, ClientState.WaitingForReconnect];

    public event EventHandler<MessageEventArgs>? MessageReceived;
    public event EventHandler<ErrorEventArgs>? ErrorOccurred;
    public event EventHandler<StateEventArgs>? StateChanged;

    private readonly object m_lock = new();
    private ClientState m_state = ClientState.NotInitialized;
    private readonly Timer m_reconnectTimer;

    #region Thread safe fields
    private readonly SemaphoreSlim m_sendLock = new(0);
    private readonly CancellationTokenSource m_cancelThread = new();
    private readonly Queue<ThreadMessage> m_threadQueue = new();
    #endregion

    private abstract class ThreadMessage {
    }

    private class ThreadInitialize(string url) : ThreadMessage {
      public string Url { get; init; } = url;
    }

    private class ThreadStart : ThreadMessage {
    }

    private class ThreadStop(bool autoReconnect = false) : ThreadMessage {
      public bool AutoReconnect { get; init; } = autoReconnect;
    }

    private class ThreadCheckReconnect : ThreadMessage {
    }

    private class ThreadSend(Message message) : ThreadMessage {
      public Message Message { get; init; } = message;
    }

    private class ThreadReceive(Message message) : ThreadMessage {
      public Message Message { get; init; } = message;
    }

    /**
     * Worker thread.
     */
    public ScoreboardWebSocketClient() {
      // m_pingtimer = new Timer(_ => PingServer(), null, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60));
      m_reconnectTimer = new Timer(_ => CheckReconnect(), null, TimeSpan.FromSeconds(RECONNECT_CHECK_INTERVAL), TimeSpan.FromSeconds(RECONNECT_CHECK_INTERVAL));
      new Task(ThreadLoop).Start();
    }

    public void Dispose() {
      if (m_cancelThread.IsCancellationRequested) return;
      m_cancelThread.Cancel();
      m_reconnectTimer.Dispose();
    }

    public void Initialize(string url) {
      AddThreadMessage(new ThreadInitialize(url));
    }

    public void Start() {
      AddThreadMessage(new ThreadStart());
    }

    public void Stop(bool autoReconnect = false) {
      AddThreadMessage(new ThreadStop(autoReconnect));
    }

    public void Send(Message message) {
      AddThreadMessage(new ThreadSend(message));
    }

    private void Receive(Message message) {
      AddThreadMessage(new ThreadReceive(message));
    }

    private void CheckReconnect() {
      AddThreadMessage(new ThreadCheckReconnect());
    }

    private void AddThreadMessage(ThreadMessage message) {
      lock (m_threadQueue) {
        m_threadQueue.Enqueue(message);
      }
      m_sendLock.Release();
    }

    private async void ThreadLoop() {
      string url = string.Empty;
      ClientWebSocket? clientWebSocket = null;
      CancellationToken cancellationToken = m_cancelThread.Token;
      bool autoReconnect = true;
      DateTime lastReconnectAttempt = DateTime.MinValue;

      while (!cancellationToken.IsCancellationRequested) {
        ThreadMessage? message = null;
        lock (m_threadQueue) {
          if (m_threadQueue.Count > 0) {
            message = m_threadQueue.Dequeue();
          }
        }

        if (message == null) {
          m_sendLock.Wait(cancellationToken);
          continue;
        }

        switch (message) {
          case ThreadInitialize init:
            url = init.Url;
            ChangeState(ClientState.Stopped);
            break;
          case ThreadStart _:
            autoReconnect = true;
            ChangeState(ClientState.Connecting);
            clientWebSocket = await ThreadDoStart(url, clientWebSocket, cancellationToken);
            if (clientWebSocket != null) {
              _ = Task.Run(() => ThreadReceiveLoop(clientWebSocket, cancellationToken));
              ChangeState(ClientState.Connected);
            } else {
              ChangeState(ClientState.Stopped);
            }
            break;
          case ThreadStop threadStop:
            autoReconnect = threadStop.AutoReconnect;
            ChangeState(ClientState.Stopping);
            await ThreadDoStop(clientWebSocket, cancellationToken);
            clientWebSocket = null;
            ChangeState(ClientState.Stopped);
            break;
          case ThreadSend send:
            await ThreadDoSend(clientWebSocket!, send.Message, cancellationToken);
            break;
          case ThreadReceive receive:
            _ = Task.Run(() => OnMessageReceived(receive.Message));
            break;
          case ThreadCheckReconnect _:
            lock (m_lock) {
              if (!autoReconnect || !RECONNECT_STATES.Contains(m_state) || DateTime.Now - lastReconnectAttempt < TimeSpan.FromSeconds(RECONNECT_TRY_INTERVAL)) {
                break;
              }
            }
            lastReconnectAttempt = DateTime.Now;
            Start();
            break;
        }
      } 
    }

    private async Task<ClientWebSocket?> ThreadDoStart(string url, ClientWebSocket? clientWebSocket, CancellationToken cancellationToken) {
      if (clientWebSocket != null) {
        OnErrorOccurred(new InvalidOperationException("Cannot start socket: already started."));
        return null;
      }
      clientWebSocket = new ClientWebSocket();
      clientWebSocket.Options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      try {
        await clientWebSocket.ConnectAsync(new Uri(url), cancellationToken);
      } catch (Exception e) {
        OnErrorOccurred(e);
        return null;
      }
      return clientWebSocket;
    }

    private async Task<bool> ThreadDoStop(ClientWebSocket? clientWebSocket, CancellationToken cancellationToken) {
      if (clientWebSocket == null) {
        OnErrorOccurred(new InvalidOperationException("Cannot stop socket: not started."));
        return false;
      }
      try {
        await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client requested close.", cancellationToken);
        clientWebSocket.Dispose();
      } catch (Exception e) {
         OnErrorOccurred(e);
      }
      return true;
    }

    private async Task ThreadDoSend(ClientWebSocket clientWebSocket, Message message, CancellationToken cancellationToken) {
      if (clientWebSocket == null) {
        OnErrorOccurred(new InvalidOperationException("Cannot send message: not started."));
        return;
      }
      var buffer = Encoding.UTF8.GetBytes(message.ToJson());
      try {
        await clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
      } catch (Exception e) {
        OnErrorOccurred(e);
      }
    }

    private async Task ThreadReceiveLoop(ClientWebSocket clientWebSocket, CancellationToken cancellationToken) {
      var buffer = new byte[1024];
      var message = new StringBuilder();
      try {
        while (!cancellationToken.IsCancellationRequested) {
          var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
          if (result.MessageType == WebSocketMessageType.Text) {
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            message.Append(receivedMessage);
            if (result.EndOfMessage) {
              Receive(Message.Deserialize(message.ToString()));
              message.Clear();
            }
          } else if (result.MessageType == WebSocketMessageType.Close) {
            break;
          }
        }
      } catch (Exception e) {
        OnErrorOccurred(e);
      }
      Stop(true);
    }

    private void ChangeState(ClientState state) {
      lock (m_lock) {
        m_state = state;
      }
      OnStateChange(state);
    }

    /*
    private void CheckSendQueue() {
      new Task(async () => {
        m_sendLock.WaitOne();
        try {
          while (m_clientWebSocket != null) {
            Message? message;
            lock (m_sendQueue) {
              if (!m_sendQueue.TryDequeue(out message)) break;
            }
            if (message == null) {
              continue;
            }
            OnInfo($"Sending message: {message}");
            var buffer = Encoding.UTF8.GetBytes(message.ToJson());
            await m_clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, m_cancellationToken);
          }
        } catch (Exception e) {
          OnErrorOccurred(e);
        } finally {
          m_sendLock.ReleaseMutex();
        }
      }).Start();
    }

    private async Task CheckForReconnect() {
      lock (m_lock) {
        if (IsConnected || !m_started) {
          return;
        }
      }

      lock (m_connectionAttempts) {
        // Trim the list of connection attempts to the last 3
        while (m_connectionAttempts.Count > 3) {
          m_connectionAttempts.Dequeue();
        }
        // Check if the last 3 connection attempts were within the last 30 seconds
        var now = DateTime.Now;
        if (m_connectionAttempts.Count >= 3 && m_connectionAttempts.All(attempt => now - attempt < TimeSpan.FromSeconds(30))) {
          // Call the reconnect method after 30 seconds
          OnInfo("3 connection attempts failed, waiting 30 seconds before reconnecting.");
          Task.Run(async () => {
            await Task.Delay(TimeSpan.FromSeconds(30));
            _ = CheckForReconnect();
          });
          return;
        }
      }

      // Connect
      await Connect();
    }

    private void Initialize(string url) {
      OnInfo($"Initializing WebSocket client with url {url}");
      Disconnect();
      lock (m_lock) {
        m_url = url;
        m_clientWebSocket = null;
      }
      _ = CheckForReconnect();
    }

    private async Task Connect() {
      OnInfo("Connecting to WebSocket server.");

      lock (m_connectionAttempts) {
        m_connectionAttempts.Enqueue(DateTime.Now);
      }

      if (m_url == string.Empty) {
        OnErrorOccurred(new InvalidOperationException("URL is not set."));
        return;
      }

      lock (m_lock) {
        m_cancellationTokenSource = new CancellationTokenSource();
        m_cancellationToken = m_cancellationTokenSource.Token;
        if (m_clientWebSocket == null
          || m_clientWebSocket.State == WebSocketState.Closed
          || m_clientWebSocket.State == WebSocketState.Aborted) {
          m_clientWebSocket = new ClientWebSocket();
          m_clientWebSocket.Options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }
      }
      
      OnConnecting();

      try {
        await m_clientWebSocket.ConnectAsync(new Uri(m_url), m_cancellationToken);
      } catch (Exception e) {
        OnErrorOccurred(e);
        return;
      }

      OnConnectionOpened();
      OnInfo("Connected to WebSocket server.");
      
      await Task.Run(() => ReceiveLoop());
    }

    private async Task ReceiveLoop() {
      var buffer = new byte[1024];
      var message = new StringBuilder();
      try {
        while (m_clientWebSocket != null) { // Stupid hack to avoid null reference warnings
          var result = await m_clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), m_cancellationToken);
          if (result.MessageType == WebSocketMessageType.Text) {
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            message.Append(receivedMessage);
            if (result.EndOfMessage) {
              OnMessageReceived(message.ToString());
              message.Clear();
            }
          } else if (result.MessageType == WebSocketMessageType.Close) {
            break;
          }
        }
      } catch (Exception e) {
        OnErrorOccurred(e);
      }
      OnConnectionClosed();
    }

    private void Disconnect() {
      OnInfo("Disconnecting from WebSocket server.");

      if (!IsConnected) return;
      m_cancellationTokenSource?.Cancel();
    }

    private void PingServer() {
      if (IsConnected) {
        Send(new Ping());
      }
    }
    */

    private void OnMessageReceived(Message message) {
      MessageReceived?.Invoke(this, new MessageEventArgs(message));
    }

    private void OnErrorOccurred(Exception error) {
      ErrorOccurred?.Invoke(this, new ErrorEventArgs(error));
    } 

    private void OnStateChange(ClientState state) {
      StateChanged?.Invoke(this, new StateEventArgs(state));
    }
  }
}
