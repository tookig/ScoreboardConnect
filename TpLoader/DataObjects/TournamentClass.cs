using System;
using System.Collections.Generic;
using System.Text;
using ScoreboardLiveApi;
using TP;

namespace TP {
  public class TournamentClass {
    public Draw SourceDraw { get; private set; }
    public ScoreboardLiveApi.TournamentClass ScoreboardTournamentClass { get; private set; }
    public List<TournamentClass> ChildClasses { get; private set; }
    public List<MatchExtended> Matches { get; private set; }
    public List<Link> Links { get; private set; }

    public TournamentClass(Draw source, ScoreboardLiveApi.TournamentClass tournamentClass, IEnumerable<MatchExtended> matches = null) {
      SourceDraw = source;
      ScoreboardTournamentClass = tournamentClass;
      ChildClasses = new List<TournamentClass>();
      Matches = new List<MatchExtended>();
      Links = new List<Link>();

      if (matches != null) {
        Matches.AddRange(matches);
      }
    }

    public override string ToString() {
      return ScoreboardTournamentClass.ToString();
    }
  }
}
