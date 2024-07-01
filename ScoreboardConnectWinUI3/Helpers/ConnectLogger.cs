using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardConnectWinUI3 {
  internal class ConnectLogger {
    public enum LogLevels {
      Verbose,
      Info,
      Warning,
      Error
    }

    public class LogEntry {
      public LogLevels Level { get; init; }
      public string Message { get; init; }
      public Exception Exception { get; init; }
      public DateTime Timestamp { get; init; }
    }

    private static ConnectLogger s_singletonLogger;

    public event EventHandler<LogEntry> Message;

    public static ConnectLogger Singleton {
      get {
        if (s_singletonLogger == null) {
          s_singletonLogger = new ConnectLogger();
        }
        return s_singletonLogger;
      }
    }

    public void Log(LogLevels level, string message, Exception exception = null) {
      var entry = new LogEntry {
        Level = level,
        Message = message,
        Exception = exception,
        Timestamp = DateTime.Now
      };
      Task.Run(() => Message?.Invoke(this, entry));
    }

    private ConnectLogger() { }
  }
}
