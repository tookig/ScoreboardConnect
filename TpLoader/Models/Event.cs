using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;
using TP.Data;

namespace TP {
  public class Event : TP.Data.EventData {
    public TournamentSettings TournamentSettings { get; private set; }
    public List<Draw> Draws { get; private set; } = new List<Draw>();

    public Event(Data.EventData raw) : base(raw) { }

    public static Event Parse(Data.EventData raw, IEnumerable<Draw> draws, TournamentSettings tournamentSettings) {
      Event tpEvent = new Event(raw);
      tpEvent.Draws = draws.Where(draw => draw.EventID == tpEvent.ID).ToList();
      tpEvent.TournamentSettings = tournamentSettings;
      return tpEvent;
    }

    public static Event Parse(XmlReader reader) {
      reader.Read();
      Event newEvent = new Event(new Data.EventData(reader));

      List<Entry> entries = new List<Entry>();
      reader.ReadToFollowing("ENTRIES");
      using (var entriesXml = reader.ReadSubtree()) {
        entriesXml.Read();
        while (entriesXml.ReadToFollowing("ENTRY")) {
          entries.Add(Entry.Parse(entriesXml));
        }
      }

      List<Draw> draws = new List<Draw>();
      reader.ReadToFollowing("DRAWS");
      using (var drawsXml = reader.ReadSubtree()) {
        while (drawsXml.ReadToFollowing("DRAW")) {
          using (var subtree = drawsXml.ReadSubtree()) {
            subtree.Read();
            draws.Add(Draw.Parse(subtree, entries));
          }
        }
      }
      newEvent.Draws = draws;


      return newEvent;
    }


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
