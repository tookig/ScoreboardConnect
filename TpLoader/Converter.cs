using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TP;
using ScoreboardLiveApi;
using System.Threading.Tasks;

namespace TP {
  public class Converter {
    static readonly PlaceMap Places = new PlaceMap();

    public static Task<List<Event>> ExtractEvents(System.Data.IDbConnection dbConnection) {
      return Task.Run(() => {
        return Loader.LoadEvents(dbConnection.CreateCommand());
      });
    }
 
    public static Task<List<TournamentClass>> Extract(System.Data.IDbConnection dbConnection) {
      return Task.Run(() => {
        var events = Loader.LoadEvents(dbConnection.CreateCommand());
        return Extract(events);
      });
    }

    public static List<TournamentClass> Extract(List<Event> events) {
      var tpClasses = new List<TournamentClass>();
      var errors = new List<Exception>();
      foreach (Event e in events) {
        tpClasses.Add(ExtractClass(e));
      }
      return tpClasses;
    }

    public static Task<List<TP.Court>> ExtractCourts(System.Data.IDbConnection dbConnection) {
      return Task.Run(() => {
        var errors = new List<Exception>();
        var courts = new List<TP.Court>();
        courts.AddRange(Loader.LoadCourts(dbConnection.CreateCommand()));
        return courts;
      });
    }

    public static TournamentClass ExtractClass(Event e) {
      // Find root draw. If there is a playoff cup in the event, use that,
      // otherwise if there is only one draw, use that.
      var root = e.Draws.Find(d => d.DrawType == Draw.DrawTypes.PlayOffCup);
      if ((root == null) && (e.Draws.Count == 1)) {
        root = e.Draws[0];
      } else if (root == null) {
        throw new Exception("No root draw in event");
      }
      // Create class
      var tpClass = root.DrawType == Draw.DrawTypes.RoundRobin ? new List<TournamentClass>() { MakePoolClass(root, e) } : MakeCupClasses(root, e);
      if (tpClass.Count != 1) {
        throw new Exception(string.Format("Got too many/too few classes from root class; should be 1 but found {0}.", tpClass.Count));
      }
      // Create child classes
      e.Draws.FindAll(d => d.DrawType == Draw.DrawTypes.QualifierCups)
        .ForEach(d => tpClass[0].ChildClasses.AddRange(MakeCupClasses(d, e)));
      e.Draws.FindAll(d => d.DrawType == Draw.DrawTypes.RoundRobin)
       .ForEach(d => tpClass[0].ChildClasses.Add(MakePoolClass(d, e)));
      // Create links
      /* root.Matches.FindAll(m => m.Link != null).Select(m => {
        TpTournamentClass tpLinkSource = tpClass[0].ChildClasses.Find(cc => cc.SourceDraw.ID == m.Link.SourceDrawID);
                
      }); */
      return tpClass[0];
    }

    private static List<TournamentClass> MakeCupClasses(Draw draw, Event e) {
      // Return object
      var newClasses = new List<TournamentClass>();
      // Find the root matches in the database
      var roots = draw.Matches.FindAll(match => match.WN == 0);
      // Create a new class for each root
      roots.ForEach(root => {
        var matches = new List<MatchExtended>();
        AddMatch(draw, root, matches);
        var tournamentClass = draw.MakeScoreboardClass();
        tournamentClass.Category = e.CreateMatchCategoryString();
        matches.ForEach(m => m.Category = tournamentClass.Category);
        newClasses.Add(new TournamentClass(draw, tournamentClass, matches));
      });
      // Return
      return newClasses;
    }

    private static TournamentClass MakePoolClass(Draw draw, Event e) {
      // Return object
      ScoreboardLiveApi.TournamentClass tc = draw.MakeScoreboardClass();
      tc.Category = e.CreateMatchCategoryString();
      // Player entries
      var poolPlayers = draw.Matches.FindAll(m => m.EntryID > 0);
      // Match entries
      var matches = draw.Matches.FindAll(m => (m.Van1 > 0) && (m.Van2 > 0) && (m.Van1 < m.Van2))
        .Select(m => m.MakeScoreboardMatch(poolPlayers.Find(pp => pp.Planning == m.Van1)?.Entry, poolPlayers.Find(pp => pp.Planning == m.Van2)?.Entry)).ToList();
      matches.ForEach(m => m.Category = tc.Category);
      // Return
      return new TournamentClass(draw, tc, matches);
    }

    private static PlayerMatch FindPlayerMatch(Draw draw, int planning) {
      var match = draw.Matches.Find(m => m.Planning == planning);
      if (match == null) {
        throw new Exception(string.Format("Match with planning = {0} not found.", planning));
      }
      return match;
    }

    private static void AddMatch(Draw draw, PlayerMatch match, List<MatchExtended> matches, int column = 1, int startRow = 1) {
      // If this match does not have any 'van1' or 'van2', it's a base item and should be ignored, search can stop here
      if ((match.Van1 == 0) || (match.Van2 == 0)) {
        return;
      }
      // Get the van1 and van2 matches
      var van1 = FindPlayerMatch(draw, match.Van1);
      var van2 = FindPlayerMatch(draw, match.Van2);
      // Create the match
      var scoreboardMatch = match.MakeScoreboardMatch(van1.Entry, van2.Entry);
      // Set the place item
      scoreboardMatch.Place = Places.GetPlace(column * 1000 + startRow);
      matches.Add(scoreboardMatch);
      // Create matches one level below
      AddMatch(draw, van1, matches, column + 1, startRow * 2 - 1);
      AddMatch(draw, van2, matches, column + 1, startRow * 2);
    }
  }
}
