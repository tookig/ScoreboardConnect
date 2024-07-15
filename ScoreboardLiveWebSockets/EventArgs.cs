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

  public class StateEventArgs(ClientState state) : EventArgs {
    public ClientState State { get; private set; } = state;
  }

  public class MessageEventArgs(Message message) : EventArgs {
    public Message Message { get; private set; } = message;
  }

  public class ErrorEventArgs(Exception error) : EventArgs {
    public Exception Error { get; private set; } = error;
  }

  public class InfoEventArgs(string info) : EventArgs {
    public string Info { get; private set; } = info;
  }
}
