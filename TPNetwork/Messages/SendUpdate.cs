using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace TPNetwork.Messages {
  public class SendUpdate : MessageBase {
    public SendUpdate(string password, string unicode, TP.Match tpMatch) : base(password, "SENDUPDATE", unicode: unicode) {
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
        int team1Score = tpMatch.GetScore(setIndex, 1); // (int)typeof(TP.Match).GetProperty(@"Team1Set{setIndex}").GetValue(tpMatch);
        int team2Score = tpMatch.GetScore(setIndex, 2); // (int)typeof(TP.Match).GetProperty(@"Team2Set{setIndex}").GetValue(tpMatch);
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
      match.AppendChild(CreateItem("ScoreStatus", "Integer", ((int)tpMatch.ScoreStatus).ToString()));
      match.AppendChild(CreateItem("Duration", "Integer", "12"));
      match.AppendChild(CreateItem("Shuttles", "Integer", tpMatch.Shuttles.ToString()));

      var players = CreateGroup("Players");
      DateTime lastTimeOnCourt = DateTime.Now;
      if (tpMatch.Entries.Item1?.Player1 != null) {
        players.AppendChild(CreatePlayer(tpMatch.Entries.Item1.Player1, lastTimeOnCourt));
      }
      if (tpMatch.Entries.Item1?.Player2 != null) {
        players.AppendChild(CreatePlayer(tpMatch.Entries.Item1.Player2, lastTimeOnCourt));
      }
      if (tpMatch.Entries.Item2?.Player1 != null) {
        players.AppendChild(CreatePlayer(tpMatch.Entries.Item2.Player1, lastTimeOnCourt));
      }
      if (tpMatch.Entries.Item2?.Player2 != null) {
        players.AppendChild(CreatePlayer(tpMatch.Entries.Item2.Player2, lastTimeOnCourt));
      }
      tournament.AppendChild(players);
    }

    private XmlElement CreatePlayer(TP.Player player, DateTime lastTimeOnCourt) {
      var xmlPlayer = CreateGroup("Player");
      xmlPlayer.AppendChild(CreateItem("ID", "Integer", player.ID.ToString()));
      xmlPlayer.AppendChild(CreateItem("Discount", player.Discount));
      xmlPlayer.AppendChild(CreateItem("WeightChecked", player.WeightChecked));
      xmlPlayer.AppendChild(CreateItem("Weight", player.Weight));
      xmlPlayer.AppendChild(CreateItem("Weight2", player.Weight2));
      xmlPlayer.AppendChild(CreateItem("CheckedIn", player.CheckedIn));
      xmlPlayer.AppendChild(CreateItem("FirstCheckIn", player.FirstCheckIn));
      xmlPlayer.AppendChild(CreateItem("LastTimeOnCourt", lastTimeOnCourt));
      return xmlPlayer;
    }
  }
}
