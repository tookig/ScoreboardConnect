using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using ScoreboardLiveApi;

namespace TP {
  public class PlayerMatch : TpObject {
    private static readonly string NAMEFORMAT = "{0} {1}";

    public enum Winners { None = 0, Entry1 = 1, Entry2 = 2 }
    
    public int ID { get; set; }
    public int DrawID { get; set; }
    public int Planning { get; set; }
    public int EntryID { get; set; }
    public Winners Winner { get; set; }
    public int LinkID { get; set; }
    public DateTime PlanDate { get; set; }
    public int Van1 { get; set; }
    public int Van2 { get; set; }
    public int WN { get; set; }
    public int Team1Set1 { get; set; }
    public int Team1Set2 { get; set; }
    public int Team1Set3 { get; set; }
    public int Team1Set4 { get; set; }
    public int Team1Set5 { get; set; }
    public int Team2Set1 { get; set; }
    public int Team2Set2 { get; set; }
    public int Team2Set3 { get; set; }
    public int Team2Set4 { get; set; }
    public int Team2Set5 { get; set; }
    public int Shuttles { get; set; }
    public int MatchNr { get; set; }

    public bool WalkOver { get; set; }
    public bool Retired { get; set; }

    public Entry Entry { get; set; }
    public Link Link { get; set; }

    public PlayerMatch(IDataReader reader) {
      ID = GetInt(reader, "id");
      DrawID = GetInt(reader, "draw");
      Planning = GetInt(reader, "planning");
      EntryID = GetInt(reader, "entry");
      Winner = (Winners)GetInt(reader, "winner");
      LinkID = GetInt(reader, "link");
      PlanDate = GetDateTime(reader, "plandate");
      Van1 = GetInt(reader, "van1");
      Van2 = GetInt(reader, "van2");
      WN = GetInt(reader, "wn");
      Team1Set1 = GetInt(reader, "team1set1");
      Team1Set2 = GetInt(reader, "team1set2");
      Team1Set3 = GetInt(reader, "team1set3");
      Team1Set4 = GetInt(reader, "team1set4");
      Team1Set5 = GetInt(reader, "team1set5");
      Team2Set1 = GetInt(reader, "team2set1");
      Team2Set2 = GetInt(reader, "team2set2");
      Team2Set3 = GetInt(reader, "team2set3");
      Team2Set4 = GetInt(reader, "team2set4");
      Team2Set5 = GetInt(reader, "team2set5");
      Shuttles = GetInt(reader, "shuttles");
      MatchNr = GetInt(reader, "id");
      WalkOver = GetBool(reader, "walkover");
      Retired = GetBool(reader, "retired");
    }

    public PlayerMatch(XmlReader reader) {
      ID = GetInt(reader, "id");
      Planning = GetInt(reader, "PLANNING");
      EntryID = GetInt(reader, "ENTRY");
      PlanDate = GetDateTime(reader, "PLAYTIME");
      MatchNr = ID;
    }

    public MatchExtended MakeScoreboardMatch(Entry team1, Entry team2) {
      MatchExtended match = new MatchExtended();
      match.StartTime = PlanDate;
      match.TournamentMatchNumber = MatchNr;
      match.BallCount = Shuttles;
      match.Team1Set1 = Team1Set1;
      match.Team1Set2 = Team1Set2;
      match.Team1Set3 = Team1Set3;
      match.Team1Set4 = Team1Set4;
      match.Team1Set5 = Team1Set5;
      match.Team2Set1 = Team2Set1;
      match.Team2Set2 = Team2Set2;
      match.Team2Set3 = Team2Set3;
      match.Team2Set4 = Team2Set4;
      match.Team2Set5 = Team2Set5;
      if (team1 != null) {
        if (team1.Player1 != null) {
          match.Team1Player1Name = string.Format(NAMEFORMAT, team1.Player1.FirstName, team1.Player1.LastName);
          match.Team1Player1Team = team1.Player1.Club != null ? team1.Player1.Club.Name : "";
        }
        if (team1.Player2 != null) {
          match.Team1Player2Name = string.Format(NAMEFORMAT, team1.Player2.FirstName, team1.Player2.LastName);
          match.Team1Player2Team = team1.Player2.Club != null ? team1.Player2.Club.Name : "";
        }
      }
      if (team2 != null) {
        if (team2.Player1 != null) {
          match.Team2Player1Name = string.Format(NAMEFORMAT, team2.Player1.FirstName, team2.Player1.LastName);
          match.Team2Player1Team = team2.Player1.Club != null ? team2.Player1.Club.Name : "";
        }
        if (team2.Player2 != null) {
          match.Team2Player2Name = string.Format(NAMEFORMAT, team2.Player2.FirstName, team2.Player2.LastName);
          match.Team2Player2Team = team2.Player2.Club != null ? team2.Player2.Club.Name : "";
        }
      }
      if (WalkOver || Retired) {
        if (Winner == Winners.Entry1) {
          match.Status = "team1won";
        } else if (Winner == Winners.Entry2) {
          match.Status = "team2won";
        }
        if (WalkOver) {
          match.Special = "walkover";
        } else {
          match.Special = "retired";
        }
      }
      return match;
    }
  }
}
