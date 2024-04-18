using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace TPNetwork.Messages {
  public class SendUpdate : MessageBase {
    public SendUpdate(string password, TP.Match tpMatch) : base(password, "SENDUPDATE") {
      var update = CreateGroup("Update");
      VisualXMLRoot.AppendChild(update);

      var tournament = CreateGroup("Tournament");
      update.AppendChild(tournament);

      var matches = CreateGroup("Matches");
      tournament.AppendChild(matches);

      var match = CreateGroup("Match");
      match.AppendChild(CreateItem("ID", "Integer", tpMatch.ID.ToString()));
      match.AppendChild(CreateItem("DrawID", "Integer", tpMatch.DrawID.ToString()));
      match.AppendChild(CreateItem("PlanningID", "Integer", tpMatch.Planning.ToString()));
      matches.AppendChild(match);

      var sets = CreateGroup("Sets");
      for (int setIndex = 1; setIndex <= 5; setIndex++) {
        int team1Score = (int)typeof(TP.Match).GetProperty(@"Team1Set{setIndex}").GetValue(tpMatch);
        int team2Score = (int)typeof(TP.Match).GetProperty(@"Team2Set{setIndex}").GetValue(tpMatch);
        if (team1Score == 0 && team2Score == 0) {
          break;
        }
        var set = CreateGroup("Set");
        set.AppendChild(CreateItem("T1", "Integer", team1Score.ToString()));
        set.AppendChild(CreateItem("T2", "Integer", team2Score.ToString()));
        sets.AppendChild(set);
      }
      match.AppendChild(sets);

      int winner = tpMatch.Winner switch {
        TP.Data.PlayerMatchData.Winners.Entry1 => 1,
        TP.Data.PlayerMatchData.Winners.Entry2 => 2,
        _ => 0,
      };
      match.AppendChild(CreateItem("Winner", "Integer", winner.ToString()));

      match.AppendChild(CreateItem("Shuttles", "Integer", tpMatch.Shuttles.ToString()));
    }
  }
}
