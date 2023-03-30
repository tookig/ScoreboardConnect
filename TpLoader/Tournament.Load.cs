using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace TP {
  public partial class Tournament {
    public async static Task<Tournament> LoadFromTP(TPFile file) {
      IEnumerable<Data.TournamentInformation> tournamentInformation = await file.LoadTournamentInformation();
      IEnumerable<Data.LocationData> locations = await file.LoadLocations();
      IEnumerable<Court> courts = (await file.LoadCourts()).Select(cd => Court.Parse(cd, locations));
      IEnumerable<Data.ClubData> clubs = await file.LoadClubs();
      IEnumerable<Player> players = (await file.LoadPlayers()).Select(pd => Player.Parse(pd, clubs));
      IEnumerable<Entry> entries = (await file.LoadEntries()).Select(ed => Entry.Parse(ed, players));
      IEnumerable<Data.PlayerMatchData> playerMatchData = await file.LoadPlayerMatches();

      IEnumerable<Data.DrawData> rawDraws = await file.LoadDraws();
      IEnumerable<Link> links = (await file.LoadLinks()).Select(link => Link.Parse(link, rawDraws));
      IEnumerable<Draw> draws = rawDraws.Select(draw => Draw.Parse(draw, playerMatchData, entries, links));


      IEnumerable<Event> events = (await file.LoadEvents()).Select(ev => Event.Parse(ev, draws, tournamentInformation));

      Tournament tournament = new Tournament() {
        TournamentInformation = tournamentInformation,
        Locations = locations,
        Courts = courts,
        Entries = entries,
        Events = events
      };

      return tournament;
    }
  }
}
