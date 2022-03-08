using System;
using System.Collections.Generic;
using System.Text;
using ScoreboardLiveApi;

namespace TP {
  public class Draw : TpObject {
    public enum DrawTypes { PlayOffCup = 1, RoundRobin = 2, QualifierCups = 6}
    public int ID { get; set; }
    public string Name { get; set; }
    public DrawTypes DrawType { get; set; }
    public int EventID { get; set; }

    public List<PlayerMatch> Matches { get; private set; } 

    public Draw(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
      DrawType = (DrawTypes)GetInt(reader, "drawType");
      EventID = GetInt(reader, "event");
      Matches = new List<PlayerMatch>();
    }

    public ScoreboardLiveApi.TournamentClass MakeScoreboardClass() {
      return new ScoreboardLiveApi.TournamentClass {
        Description = Name,
        Size = Matches.Count + 1,
        ClassType = DrawType switch {
          DrawTypes.RoundRobin => "roundrobin",
          _ => "cup"
        }
      };
    }

    public override string ToString() {
      return string.Format("DRAW {0}\t{1}\t{2}\t({3} matches)", ID, Name, DrawType, Matches.Count);
    }
  }
}
