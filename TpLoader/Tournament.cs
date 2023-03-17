using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public partial class Tournament {
    public IEnumerable<Data.TournamentInformation> TournamentInformation { get; private set; }
    public IEnumerable<Data.LocationData> Locations { get; private set; }
    public IEnumerable<Court> Courts { get; private set; }
    public IEnumerable<Entry> Entries { get; private set; }
    public IEnumerable<Event> Events { get; private set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("-- TOURNAMENT --");
      sb.AppendLine("Tournament information:");
      foreach (var ti  in TournamentInformation) {
        sb.AppendLine(ti.ToString());
      }
      sb.AppendLine("Courts:");
      foreach (var court in Courts) {
        sb.AppendLine(court.ToString());
      }
      sb.AppendLine("Entries:");
      foreach (var entry in Entries) {
        sb.AppendLine(entry.ToString());
      }
      sb.AppendLine("Events:");
      foreach (var ev in Events) {
        sb.AppendLine(ev.ToString());
      }
      return sb.ToString();
    }
  }
}
