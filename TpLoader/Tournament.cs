﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace TP {
  public partial class Tournament {
    public Data.TournamentSettings TournamentSettings { get; private set; }
    public List<Data.LocationData> Locations { get; private set; } = [];
    public List<Court> Courts { get; private set; } = [];
    public List<Entry> Entries { get; private set; } = [];
    public List<Event> Events { get; private set; } = [];

    public Match FindMatchByID(int id) {
      foreach (Event e in Events) {
        foreach (Draw draw in e.Draws) {
          var match = draw.Matches.Where(m => m.ID == id).FirstOrDefault();
          if (match != null) {
            return match;
          }
        }
      }
      return null;
    }

    public Event FindEventByID(int id) {
      return Events.Where(e => e.ID == id).FirstOrDefault();
    }

    public Draw FindDrawByID(int id) {
      foreach (Event e in Events) {
        var draw = e.Draws.Where(d => d.ID == id).FirstOrDefault();
        if (draw != null) {
          return draw;
        }
      }
      return null;
    }

    public Court FindCourtByID(int id) {
      return Courts.Where(c => c.ID == id).FirstOrDefault();
    }

    public override string ToString() {
      StringBuilder sb = new();
      sb.AppendLine("-- TOURNAMENT --");
      sb.AppendLine(TournamentSettings.TournamentName);
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
