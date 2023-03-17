using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TP {
  public class Match : TP.Data.PlayerMatchData {
    public (Entry, Entry) Entries { get; set; }
    public (Data.LinkData, Data.LinkData) Links { get; set; }
    public Match Parent { get; set; }
    public (Match, Match) Source { get; set; }

    protected Match(Data.PlayerMatchData raw, Entry entry1, Entry entry2) : base(raw) {
      Entries = (entry1, entry2);
    }

    public IEnumerable<Match> Flatten() {
      List<Match> flatTree = new List<Match>();
      flatTree.Add(this);
      if (Source.Item1 != null) {
        flatTree.AddRange(Source.Item1.Flatten());
      }
      if (Source.Item2 != null) {
        flatTree.AddRange(Source.Item2.Flatten());
      }
      return flatTree;
    }

    public static Match Parse(Data.PlayerMatchData raw, IEnumerable<Data.PlayerMatchData> playerMatches, IEnumerable<Entry> entries, IEnumerable<Data.LinkData> links) {
      // If this match does not have any 'van1' or 'van2', it's a base item and should be ignored, search can stop here
      if ((raw.Van1 == 0) || (raw.Van2 == 0)) {
        return null;
      }
      // Find the van's
      Data.PlayerMatchData van1 = playerMatches.First(pm => (pm.Planning == raw.Van1) && (pm.DrawID == raw.DrawID));
      Data.PlayerMatchData van2 = playerMatches.First(pm => (pm.Planning == raw.Van2) && (pm.DrawID == raw.DrawID));
      // Create match
      Match match = new Match(raw, entries.FirstOrDefault(entry => entry.ID == van1?.EntryID), entries.FirstOrDefault(entry => entry.ID == van2?.EntryID));
      // Find any links
      Data.LinkData link1 = (van1?.LinkID > 0) ? links.FirstOrDefault(link => link.ID == van1.LinkID) : null;
      Data.LinkData link2 = (van2?.LinkID > 0) ? links.FirstOrDefault(link => link.ID == van2.LinkID) : null;
      match.Links = (link1, link2);
      return match;
    }

    public static IEnumerable<Match> ParsePoolDraw(Draw draw, IEnumerable<Data.PlayerMatchData> playerMatches, IEnumerable<Entry> entries, IEnumerable<Data.LinkData> links) {
      return playerMatches.Where(pm => (pm.DrawID == draw.ID) && (pm.Van1 > 0) && (pm.Van2 > 0) && (pm.Van1 < pm.Van2))
                          .Select(pm => Parse(pm, playerMatches, entries, links));
    }

    public static IEnumerable<Match> TraverseCupDraw(Draw draw, IEnumerable<Data.PlayerMatchData> playerMatches, IEnumerable<Entry> entries, IEnumerable<Data.LinkData> links) {
      // Find the root matches (a TP draw can have many roots, used for example in qualifiers)
      IEnumerable<Data.PlayerMatchData> roots = playerMatches.Where(pm => (pm.DrawID == draw.ID) && (pm.WN == 0));
      List<Match> matches = new List<Match>();
      // Go through all roots and parse the corresponding trees
      foreach (Data.PlayerMatchData root in roots) {
        Match parsedRoot = ParseMatchNode(root, null, playerMatches, entries, links);
        if (parsedRoot != null) {
          matches.AddRange(parsedRoot.Flatten());
        }
      }
      return matches;
    }

    private static Match ParseMatchNode(Data.PlayerMatchData node, Match parent, IEnumerable<Data.PlayerMatchData> playerMatches, IEnumerable<Entry> entries, IEnumerable<Data.LinkData> links) {
      Match match = Parse(node, playerMatches, entries, links);
      if (match == null) {
        return null;
      }

      Data.PlayerMatchData van1 = playerMatches.First(pm => (pm.Planning == node.Van1) && (pm.DrawID == node.DrawID));
      Data.PlayerMatchData van2 = playerMatches.First(pm => (pm.Planning == node.Van2) && (pm.DrawID == node.DrawID));

      match.Parent = parent;
      match.Source = (ParseMatchNode(van1, match, playerMatches, entries, links), ParseMatchNode(van2, match, playerMatches, entries, links));

      return match;
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(string.Format("Match {0}", ID));
      sb.AppendLine(Entries.Item1?.ToString() ?? "<empty>");
      sb.AppendLine(Entries.Item2?.ToString() ?? "<empty>");
      return sb.ToString();
    }
  }
}
