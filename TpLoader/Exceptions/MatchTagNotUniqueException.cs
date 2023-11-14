using System;
using System.Collections.Generic;
using System.Text;

namespace TP.Exceptions {
  public class MatchTagNotUniqueException : Exception {
    public MatchTagNotUniqueException(string? message) : base(message) {

    }
  }
}
