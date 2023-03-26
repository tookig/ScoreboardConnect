using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace TP {
  public partial class Tournament {
    private static readonly string NAMEFORMAT = "{0} {1}";
    private static Helpers.PlaceMap PlaceMap = new Helpers.PlaceMap();

    public class ExportClassItem {
      public ScoreboardLiveApi.TournamentClass MainClass { get; }
      public Draw MainDraw { get; }
      public List<ExportClassItem> SubClasses { get; } = new List<ExportClassItem>();
      public List<ExportMatchItem> Matches { get; } = new List<ExportMatchItem>();
      public ExportClassItem(ScoreboardLiveApi.TournamentClass mainClass, Draw mainDraw, IEnumerable<ExportClassItem> subClasses = null, IEnumerable<ExportMatchItem> matches = null) {
        MainClass = mainClass;
        MainDraw = mainDraw;
        if (subClasses != null) {
          SubClasses.AddRange(subClasses);
        }
        if (matches != null) {
          Matches.AddRange(matches);
        }
      }
    }

    public class ExportMatchItem {
      public ScoreboardLiveApi.MatchExtended SB { get; set; }
      public Match TP { get; set; }
    }

    public IEnumerable<ExportClassItem> ExportClasses(Func<Event, bool> eventFilter) {
      List<ExportClassItem> exportItems = new List<ExportClassItem>();
      foreach (Event ev in Events.Where(eventFilter)) {
        List<ExportMatchItem> mainClassMatches = new List<ExportMatchItem>();
        var rootDraw = FindMainDraw(ev);
        var rootClass = ConvertDraw(ev, rootDraw);
        mainClassMatches.AddRange(ConvertDrawMatches(rootDraw, rootClass));

        var subDraws = FindSubDraws(ev, rootDraw);
        var subClasses = subDraws.Select(subDraw => {
          var sbClass = ConvertDraw(ev, subDraw);
          sbClass.Category = rootClass.Category;
          return new ExportClassItem(sbClass, subDraw, matches: ConvertDrawMatches(subDraw, sbClass));
        });

        exportItems.Add(new ExportClassItem(rootClass, rootDraw, subClasses, mainClassMatches));
      }
      return exportItems;
    }

    private static ScoreboardLiveApi.TournamentClass ConvertDraw(Event ev, Draw draw) {
      return new ScoreboardLiveApi.TournamentClass() {
        Category = CreateMatchCategoryString(ev),
        Description = draw.Name,
        Size = CalculateDrawSize(draw),
        ClassType = CreateClassType(draw),
      };
    }

    private static IEnumerable<ExportMatchItem> ConvertDrawMatches(Draw draw, ScoreboardLiveApi.TournamentClass sbClass) {
      if (draw.DrawType == Data.DrawData.DrawTypes.RoundRobin) {
        return draw.Matches.Select(tpMatch => {
          return new ExportMatchItem() {
            SB = ConvertMatch(tpMatch, sbClass),
            TP = tpMatch
          };
        });
      }
      return TraverseMatchTree(draw, sbClass);
    }

    private static List<ExportMatchItem> TraverseMatchTree(Draw draw, ScoreboardLiveApi.TournamentClass sbClass) {
      List<ExportMatchItem> matches = new List<ExportMatchItem>();
      Match rootMatch = draw.Matches.FirstOrDefault(match => match.WN == 0);
      if (rootMatch == null) {
        throw new Exception("No root match could be found in draw");
      }
      ParseCupMatch(rootMatch, sbClass, matches);
      return matches;
    }

    private static void ParseCupMatch(TP.Match tpMatch, ScoreboardLiveApi.TournamentClass sbClass, List<ExportMatchItem> items, int column=1, int startRow=1) {
      ScoreboardLiveApi.MatchExtended sbMatch = ConvertMatch(tpMatch, sbClass);
      sbMatch.Place = PlaceMap.GetPlace(column * 1000 + startRow);
      items.Add(new ExportMatchItem() {
        SB = sbMatch,
        TP = tpMatch
      });
      if (tpMatch.Source.Item1 != null) {
        ParseCupMatch(tpMatch.Source.Item1, sbClass, items, column + 1, startRow * 2 - 1);
      }
      if (tpMatch.Source.Item2 != null) {
        ParseCupMatch(tpMatch.Source.Item2, sbClass, items, column + 1, startRow * 2);
      }
    }

    private static ScoreboardLiveApi.MatchExtended ConvertMatch(TP.Match tpMatch, ScoreboardLiveApi.TournamentClass sbClass) {
      var entryTextsTeam1 = tpMatch.Entries.Item1 != null ? ConvertEntry(tpMatch.Entries.Item1) : ("", "", "", "");
      var entryTextsTeam2 = tpMatch.Entries.Item2 != null ? ConvertEntry(tpMatch.Entries.Item2) : ("", "", "", "");

      ScoreboardLiveApi.MatchExtended sbMatch = new ScoreboardLiveApi.MatchExtended() {
        Category = sbClass.Category,
        StartTime = tpMatch.PlanDate,
        TournamentMatchNumber = tpMatch.MatchNr,
        BallCount = tpMatch.Shuttles,
        Team1Set1 = tpMatch.Team1Set1,
        Team1Set2 = tpMatch.Team1Set2,
        Team1Set3 = tpMatch.Team1Set3,
        Team1Set4 = tpMatch.Team1Set4,
        Team1Set5 = tpMatch.Team1Set5,
        Team2Set1 = tpMatch.Team2Set1,
        Team2Set2 = tpMatch.Team2Set2,
        Team2Set3 = tpMatch.Team2Set3,
        Team2Set4 = tpMatch.Team2Set4,
        Team2Set5 = tpMatch.Team2Set5,
        Team1Player1Name = entryTextsTeam1.Item1,
        Team1Player1Team = entryTextsTeam1.Item2,
        Team1Player2Name = entryTextsTeam1.Item3,
        Team1Player2Team = entryTextsTeam1.Item4,
        Team2Player1Name = entryTextsTeam2.Item1,
        Team2Player1Team = entryTextsTeam2.Item2,
        Team2Player2Name = entryTextsTeam2.Item3,
        Team2Player2Team = entryTextsTeam2.Item4,
      };

      if (tpMatch.WalkOver || tpMatch.Retired) {
        sbMatch.Status = tpMatch.Winner switch {
          Data.PlayerMatchData.Winners.Entry1 => "team1won",
          Data.PlayerMatchData.Winners.Entry2 => "team2won",
          _ => "nowinner"
        };
        sbMatch.Special = tpMatch.WalkOver ? "walkover" : "retired";
      }

      return sbMatch;
    }

    private static (string, string, string, string) ConvertEntry(TP.Entry entry) {
      return (
        entry.Player1 == null ? "" : string.Format(NAMEFORMAT, entry.Player1.FirstName, entry.Player1.LastName),
        entry.Player1 == null ? "" : (entry.Player1.Club?.Name ?? ""),
        entry.Player2 == null ? "" : string.Format(NAMEFORMAT, entry.Player2.FirstName, entry.Player2.LastName),
        entry.Player2 == null ? "" : (entry.Player2.Club?.Name ?? "")
      );
    }

    protected static string CreateMatchCategoryString(Event ev) {
      return ev.Gender switch {
        Event.Genders.Men => ev.EventType == Event.EventTypes.Singles ? "ms" : "md",
        Event.Genders.Boys => ev.EventType == Event.EventTypes.Singles ? "ms" : "md",
        Event.Genders.Women => ev.EventType == Event.EventTypes.Singles ? "ws" : "wd",
        Event.Genders.Girls => ev.EventType == Event.EventTypes.Singles ? "ws" : "wd",
        _ => "xd",
      };
    }

    protected static int CalculateDrawSize(Draw draw) {
      return draw.GetEntries().Count() + draw.Links.Count();
    }

    protected static string CreateClassType(Draw draw) {
      return draw.DrawType switch {
        Draw.DrawTypes.RoundRobin => "roundrobin",
        _ => "cup"
      };
    }

    private static Draw FindMainDraw(Event ev) {
      // If there is a playoff cup in the draw list, use that, if not make sure there
      // is only one draw and use that. If there is more then one draw, but no playoff, there
      // is no way of knowing which to use so throw.
      var root = ev.Draws.FirstOrDefault(d => d.DrawType == Data.DrawData.DrawTypes.PlayOffCup);
      if (root != null) {
        return root;
      } else if (ev.Draws.Count() == 1) {
        return ev.Draws.First();
      }
      throw new Exception("Could not determine root draw in event");
    }

    private static IEnumerable<Draw> FindSubDraws(Event ev, Draw root) {
      return ev.Draws.Where(draw => draw.ID != root.ID);
    }
    private static IEnumerable<ScoreboardLiveApi.Link> CreateLinks(IEnumerable<ExportClassItem> ecis) {
      List<ScoreboardLiveApi.Link> sbLinks = new List<ScoreboardLiveApi.Link>();
      foreach (ExportClassItem eci in ecis) {
        var linkTargets = FindLinkTargetsInClass(eci);
        foreach (ExportMatchItem emi in linkTargets) {
          if (emi.TP.Links.Item1 != null) {
            sbLinks.Add(ConvertLink(emi.TP.Links.Item1, FindLinkSource(emi.TP.Links.Item1, ecis), emi.SB, 1));
          }
          if (emi.TP.Links.Item2 != null) {
            sbLinks.Add(ConvertLink(emi.TP.Links.Item2, FindLinkSource(emi.TP.Links.Item2, ecis), emi.SB, 2));
          }
        }
      }
      return sbLinks;
    }

    private static IEnumerable<ExportMatchItem> FindLinkTargetsInClass(ExportClassItem eci) {
      return eci.Matches.Where(emi => (emi.TP.Links.Item1 != null) || (emi.TP.Links.Item2 != null));
    }

    private static ScoreboardLiveApi.TournamentClass FindLinkSource(Data.LinkData link, IEnumerable<ExportClassItem> classes) {
      return classes.Aggregate(new List<ExportClassItem>(), (c, next) => {
        c.Add(next);
        if (next.SubClasses != null) {
          c.AddRange(next.SubClasses);
        }
        return c;
      }).First(c => c.MainDraw.ID == link.SourceDrawID).MainClass;
    }

    private static ScoreboardLiveApi.Link ConvertLink(Data.LinkData tpLink, ScoreboardLiveApi.TournamentClass tpSourceClass, ScoreboardLiveApi.Match sbTargetMatch, int targetTeamIndex) {
      return new ScoreboardLiveApi.Link() {
        SourceClassID = tpSourceClass.ID,
        SourcePlace = tpLink.SourcePosition,
        TargetMatchID = sbTargetMatch.MatchID,
        TargetTeam = targetTeamIndex == 1 ? "team1" : "team2"
      };
    }
  }
}
