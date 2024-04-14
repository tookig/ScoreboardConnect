using ScoreboardLiveSocket.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardLiveWebSockets {
  public enum ClientState {
    NotInitialized,
    Connecting,
    Connected,
    WaitingForReconnect,
    Stopping,
    Stopped
  }

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
}
