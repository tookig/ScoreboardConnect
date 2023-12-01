﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;
using ScoreboardLiveApi;
using TP.VisualXML;

namespace TP.Data {
  public class PlayerMatchData : TpDataObject {
    public enum Winners { None = 0, Entry1 = 1, Entry2 = 2 }
    
    public int ID { get; set; }
    public int EventID { get; set; }
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

    public PlayerMatchData() { }
    public PlayerMatchData(PlayerMatchData cpy) {
      ID = cpy.ID;
      EventID = cpy.EventID;
      DrawID = cpy.DrawID;
      Planning = cpy.Planning;
      EntryID = cpy.EntryID;
      Winner = cpy.Winner;
      LinkID = cpy.LinkID;
      PlanDate = cpy.PlanDate;
      Van1 = cpy.Van1;
      Van2 = cpy.Van2;
      WN = cpy.WN;
      Team1Set1 = cpy.Team1Set1;
      Team1Set2 = cpy.Team1Set2;
      Team1Set3 = cpy.Team1Set3;
      Team1Set4 = cpy.Team1Set4;
      Team1Set5 = cpy.Team1Set5;
      Team2Set1 = cpy.Team2Set1;
      Team2Set2 = cpy.Team2Set2;
      Team2Set3 = cpy.Team2Set3;
      Team2Set4 = cpy.Team2Set4;
      Team2Set5 = cpy.Team2Set5;
      Shuttles = cpy.Shuttles;
      MatchNr = cpy.MatchNr;
      WalkOver = cpy.WalkOver;
      Retired = cpy.Retired;
    }

    public PlayerMatchData(IDataReader reader) {
      ID = GetInt(reader, "id");
      DrawID = GetInt(reader, "draw");
      EventID = GetInt(reader, "event");
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

    public PlayerMatchData(XmlReader reader) {
      ID = GetInt(reader, "ID");
      EventID = GetInt(reader, "EID");
      Planning = GetInt(reader, "PLANNING");
      EntryID = GetInt(reader, "ENTRY");
      MatchNr = GetInt(reader, "MATCHNR");
      PlanDate = GetDateTime(reader, "PLAYTIME");
      List<(int, int)> sets = new List<(int, int)>();
      if (reader.ReadToFollowing("SETS")) {
        using (XmlReader s = reader.ReadSubtree()) {
          while (s.ReadToFollowing("SET")) {
            sets.Add((GetInt(s, "S1"), GetInt(s, "S2")));
          }
        }
      }
      for (int i = 1; i <= 5; i++) {
        if (sets.Count >= i) {
          GetType().GetProperty(string.Format("Team1Set{0}", i)).SetValue(this, sets[i - 1].Item1);
          GetType().GetProperty(string.Format("Team2Set{0}", i)).SetValue(this, sets[i - 1].Item2);
        } else {
          GetType().GetProperty(string.Format("Team1Set{0}", i)).SetValue(this, 0);
          GetType().GetProperty(string.Format("Team2Set{0}", i)).SetValue(this, 0);
        }
      }
    }

    public PlayerMatchData(GroupNode match) {
      ID = ((ItemNode<int>)match["ID"]).Value;
      Planning = ((ItemNode<int>)match["PlanningID"]).Value;
      EntryID = ((ItemNode<int>)match["EntryID"]).Value;
      MatchNr = ((ItemNode<int>)match["MatchNr"]).Value;
      PlanDate = ((ItemNode<DateTime>)match["PlannedTime"]).Value;
      DrawID = ((ItemNode<int>)match["DrawID"]).Value;
      Winner = (Winners)((ItemNode<int>)match["WinnerID"]).Value;
      Van1 = ((ItemNode<int>)match["From1"]).Value;
      Van2 = ((ItemNode<int>)match["From2"]).Value;
      WN = ((ItemNode<int>)match["WinnerTo"]).Value;
      Shuttles = ((ItemNode<int>)match["Shuttles"]).Value;
      // TODO : Walkover and Retired
      var sets = match.GetGroup("Sets");
      if (sets != null) {
        for (int setIndex = 1; setIndex <= sets.Groups.Count; setIndex++) {
          var scoreTeam1 = ((ItemNode<int>)sets.Groups[setIndex - 1]["T1"]).Value;
          var scoreTeam2 = ((ItemNode<int>)sets.Groups[setIndex - 1]["T2"]).Value;
          GetType().GetProperty(string.Format("Team1Set{0}", setIndex)).SetValue(this, scoreTeam1);
          GetType().GetProperty(string.Format("Team2Set{0}", setIndex)).SetValue(this, scoreTeam2);
        }
      }
    }
  }
}
