using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  /// <summary>
  /// Interface for objects detecting court changes in TP
  /// </summary>
  interface ITPCourtEventProducer {
    /// <summary>
    /// Event triggered when a court has received an update 
    /// </summary>
    /// <returns>
    /// A tuple with the TP court name as first item and the TP match ID as the second.
    /// </returns>
    public abstract event EventHandler<(string, int)> CourtUpdate;
  }
}
