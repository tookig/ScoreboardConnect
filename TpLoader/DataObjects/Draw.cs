using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ScoreboardLiveApi;
using System.Linq;

namespace TP {
  public class Draw : TpObject {
    public enum DrawTypes { PlayOffCup = 1, RoundRobin = 2, QualifierCups = 6}
    public int ID { get; set; }
    public string Name { get; set; }
    public DrawTypes DrawType { get; set; }
    public int EventID { get; set; }

    public List<PlayerMatch> Matches { get; private set; } = new List<PlayerMatch>();
    public int EntryCount { get; set; }

    public Draw(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
      DrawType = (DrawTypes)GetInt(reader, "drawType");
      EventID = GetInt(reader, "event");
    }

    public Draw(XmlReader reader, List<Entry> entries) {
      ID = GetInt(reader, "ID");
      DrawType = (DrawTypes)GetInt(reader, "DRAWTYPE");
      Name = GetString(reader, "DRAWNAME");
      EntryCount = entries.Count;
      while (reader.ReadToFollowing("MATCH")) {
        Matches.Add(new PlayerMatch(reader));
      }
      Matches.ForEach(match => match.Entry = match.EntryID == 0 ? null : entries.Find(entry => entry.ID == match.EntryID));
    }

    public ScoreboardLiveApi.TournamentClass MakeScoreboardClass() {
      return new ScoreboardLiveApi.TournamentClass {
        Description = Name,
        Size = DrawSize(),
        ClassType = DrawType switch {
          DrawTypes.RoundRobin => "roundrobin",
          _ => "cup"
        }
      };
    }

    public int DrawSize() {
      return Matches.FindAll(match => match.Entry != null).Select(match => match.Entry).Distinct().Count();
    }

    public override string ToString() {
      return string.Format("DRAW {0}\t{1}\t{2}\t({3} matches)", ID, Name, DrawType, Matches.Count);
    }
  }
}
