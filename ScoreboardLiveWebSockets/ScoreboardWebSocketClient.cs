using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoreboardLiveWebSockets {
  public class ScoreboardWebSocketClient {
    private ClientWebSocket m_clientWebSocket;

    public class MessageEventArgs : EventArgs {
      public string Message { get; private set; }
      public MessageEventArgs(string message) {
        Message = message;
      }
    }

    public class ErrorEventArgs : EventArgs {
      public Exception Error { get; private set; }
      public ErrorEventArgs(Exception error) {
        Error = error;
      }
    }

    public event EventHandler<MessageEventArgs>? MessageReceived;
    public event EventHandler<ErrorEventArgs>? ErrorOccurred;
    public event EventHandler? ConnectionClosed;
    public event EventHandler? ConnectionOpened;

    public bool IsConnected => m_clientWebSocket.State == WebSocketState.Open;
    public bool IsConnecting => m_clientWebSocket.State == WebSocketState.Connecting;

    public ScoreboardWebSocketClient() {
      m_clientWebSocket = new ClientWebSocket();
      m_clientWebSocket.Options.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
    }

    public async Task ConnectAsync(Uri uri) {
      await m_clientWebSocket.ConnectAsync(uri, CancellationToken.None);
      OnConnectionOpened();
      await Task.Run(() => ReceiveLoop());
    }

    public async Task SendAsync(string message) {
      if (m_clientWebSocket.State != WebSocketState.Open) {
        throw new InvalidOperationException("WebSocket is not open.");
      }

      var buffer = Encoding.UTF8.GetBytes(message);
      await m_clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task ReceiveLoop() {
      var buffer = new byte[1024];
      var message = new StringBuilder();
      while (m_clientWebSocket.State == WebSocketState.Open) {
        var result = await m_clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        if (result.MessageType == WebSocketMessageType.Text) {
          var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
          message.Append(receivedMessage);
          if (result.EndOfMessage) {
            OnMessageReceived(message.ToString());
            message.Clear();
          }
        } else if (result.MessageType == WebSocketMessageType.Close) {
          OnConnectionClosed();
          break;
        }
      }
    }

    public async Task DisconnectAsync() {
      if (m_clientWebSocket.State == WebSocketState.Open) {
        await m_clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client disconnecting", CancellationToken.None);
      }
    }

    private void OnMessageReceived(string message) {
      MessageReceived?.Invoke(this, new MessageEventArgs(message));
    }

    private void OnErrorOccurred(Exception error) {
      ErrorOccurred?.Invoke(this, new ErrorEventArgs(error));
    } 

    private void OnConnectionClosed() {
      ConnectionClosed?.Invoke(this, EventArgs.Empty);
    }

    private void OnConnectionOpened() {
      ConnectionOpened?.Invoke(this, EventArgs.Empty);
    }
  }
}
