using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TP {
  public class Event : TP.Data.EventData {
    public Data.TournamentInformation TournamentInformation { get; private set; }
    public IEnumerable<Draw> Draws { get; private set; } = new List<Draw>();

    public Event(Data.EventData raw) : base(raw) { }

    public static Event Parse(Data.EventData raw, IEnumerable<Draw> draws, IEnumerable<Data.TournamentInformation> tournamentInformation) {
      Event tpEvent = new Event(raw);
      tpEvent.Draws = draws.Where(draw => draw.EventID == tpEvent.ID);
      tpEvent.TournamentInformation = tournamentInformation.FirstOrDefault(ti => ti.ID == tpEvent.TournamentInformationID);
      return tpEvent;
    }

    /*
    public Event(XmlReader reader) {
      ID = GetInt(reader, "ID");
      Name = GetString(reader, "NAME");
      List<Entry> entries = new List<Entry>();
      if (reader.ReadToDescendant("ENTRIES")) {
        using (var subtree = reader.ReadSubtree()) {
          while (subtree.ReadToFollowing("ENTRY")) {
            entries.Add(new Entry(subtree));
          }
        }
      }
      while (reader.ReadToFollowing("DRAW")) {
        using (var subtree = reader.ReadSubtree()) {
          Draws.Add(new Draw(subtree, entries));
        }
      }
    }

    public string CreateMatchCategoryString() {
      return Gender switch {
        Genders.Men => EventType == EventTypes.Singles ? "ms" : "md",
        Genders.Boys => EventType == EventTypes.Singles ? "ms" : "md",
        Genders.Women => EventType == EventTypes.Singles ? "ws" : "wd",
        Genders.Girls => EventType == EventTypes.Singles ? "ws" : "wd",
        _ => "xd",
      };
    }

    
    public List<PlayerMatch> ExtractMatches() {
      List<PlayerMatch> matches = new List<PlayerMatch>();
      foreach (Draw draw in Draws) {
        matches.AddRange(draw.Matches);
      }
      return matches;
    }

    public (PlayerMatch, Entry, Entry) FindMatchAndEntries(Predicate<PlayerMatch> finder) {
      foreach (Draw draw in Draws) {
        var found = draw.Matches.Find(finder);
        if (found != null) {
          var entry1 = draw.Matches.Find(match => match.Planning == found.Van1);
          var entry2 = draw.Matches.Find(match => match.Planning == found.Van2);
          return (found, entry1?.Entry, entry2?.Entry);
        }
      }
      return (null, null, null);
     }
    */

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("EVENT {0}\t{1}\t{2}\t{3}{4}", ID, Name, Gender, EventType, Environment.NewLine);
      foreach (Draw draw in Draws) {
        sb.AppendFormat("\t{0}{1}", draw.ToString(), Environment.NewLine);
      }
      return sb.ToString();
    }
  }
}
