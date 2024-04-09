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

namespace ScoreboardLiveWebSockets {
  public class ScoreboardWebSocketClient {
    public enum ClientState {
      NotInitialized,
      Connecting,
      Connected,
      WaitingForReconnect
    }

    private object m_lock = new object();
    private CancellationTokenSource? m_cancellationTokenSource;
    private CancellationToken m_cancellationToken;
    private ClientState m_state = ClientState.NotInitialized;
    private ClientWebSocket? m_clientWebSocket;
    private string m_url = string.Empty;
    private bool m_started = false;
    private Queue<DateTime> m_connectionAttempts = new();
    private Timer m_pingtimer;
    private Queue<Message> m_sendQueue = new();
    private Mutex m_sendLock = new();

    public class StateEventArgs : EventArgs {
      public ClientState State { get; private set; }
      public StateEventArgs(ClientState state) {
        State = state;
      }
    }

    public class MessageEventArgs : EventArgs {
      public Message Message { get; private set; }
      public MessageEventArgs(Message message) {
        Message = message;
      }
    }

    public class ErrorEventArgs : EventArgs {
      public Exception Error { get; private set; }
      public ErrorEventArgs(Exception error) {
        Error = error;
      }
    }

    public class InfoEventArgs : EventArgs {
      public string Info { get; private set; }
      public InfoEventArgs(string info) {
        Info = info;
      }
    }

    public event EventHandler<MessageEventArgs>? MessageReceived;
    public event EventHandler<ErrorEventArgs>? ErrorOccurred;
    public event EventHandler<StateEventArgs>? StateChanged;
    public event EventHandler<InfoEventArgs>? Info;

    public bool IsConnected {
      get {
        return m_clientWebSocket?.State == WebSocketState.Open && !m_cancellationToken.IsCancellationRequested;
      }
    }

    public ScoreboardWebSocketClient() {
      m_pingtimer = new Timer(_ => PingServer(), null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
    }

    public void Start(string url) {
      OnInfo("Starting WebSocket client.");
      lock (m_lock) {
        m_started = true;
      }
      Initialize(url);
    }

    public void Stop() {
      OnInfo("Stopping WebSocket client.");
      lock (m_lock) {
        m_started = false;
      }
      Disconnect();
    }

    public void Send(Message message) {
      OnInfo($"Queueing message: {message}");
      if (!IsConnected || m_clientWebSocket == null) {
        throw new InvalidOperationException("Client is not connected.");
      }
      lock (m_sendQueue) {
        m_sendQueue.Enqueue(message);
      }
      CheckSendQueue();
    }

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
        Send(new Ping());
        Send(new Ping());
        Send(new Ping());
      }
    }

    private void OnMessageReceived(string message) {
      Message parsedMessage;
      try {
        parsedMessage = Message.Deserialize(message);
      } catch (Exception e) {
        OnErrorOccurred(e);
        return;
      }

      MessageReceived?.Invoke(this, new MessageEventArgs(parsedMessage));
    }

    private void OnErrorOccurred(Exception error) {
      ErrorOccurred?.Invoke(this, new ErrorEventArgs(error));
      _ = CheckForReconnect();
    } 

    private void OnConnecting() {
      m_state = ClientState.Connecting;
      StateChanged?.Invoke(this, new StateEventArgs(m_state));
    }

    private void OnConnectionClosed() {
      m_state = ClientState.WaitingForReconnect;
      StateChanged?.Invoke(this, new StateEventArgs(m_state));
    }

    private void OnConnectionOpened() {
      m_state = ClientState.Connected;
      StateChanged?.Invoke(this, new StateEventArgs(m_state));
    }

    private void OnInfo(string message) {
      Info?.Invoke(this, new InfoEventArgs(message));
    }
  }
}
