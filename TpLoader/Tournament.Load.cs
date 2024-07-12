using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using TP.Data;

namespace TP {
  public partial class Tournament {
    public async static Task<Tournament> LoadFromTP(TPFile file) {
      TournamentSettings tournamentSettings = new(await file.LoadTournamentSettings());
      List<Data.LocationData> locations = await file.LoadLocations();
      List<Court> courts = (await file.LoadCourts()).Select(cd => Court.Parse(cd, locations)).ToList();

      List<Data.ClubData> clubs = await file.LoadClubs();
      List<Player> players = (await file.LoadPlayers()).Select(pd => Player.Parse(pd, clubs)).ToList();
      List<Entry> entries = (await file.LoadEntries()).Select(ed => Entry.Parse(ed, players)).ToList();
      List<Data.PlayerMatchData> playerMatchData = await file.LoadPlayerMatches();

      List<Data.DrawData> rawDraws = await file.LoadDraws();
      List<Link> links = (await file.LoadLinks()).Select(link => Link.Parse(link, rawDraws)).ToList();
      List<Draw> draws = rawDraws.Select(draw => Draw.Parse(draw, playerMatchData, entries, links)).ToList();


      List<Event> events = (await file.LoadEvents()).Select(ev => Event.Parse(ev, draws, tournamentSettings)).ToList();

      Tournament tournament = new() {
        TournamentSettings = tournamentSettings,
        Locations = locations,
        Courts = courts,
        Entries = entries,
        Events = events
      };

      return tournament;
    }

    public static Tournament LoadFromXML(XmlReader reader) {
      List<Event> events = [];

      while (reader.ReadToFollowing("EVENT")) {
        events.Add(Event.Parse(reader.ReadSubtree()));
      }

      Tournament tournament = new() {
        Events = events
      };

      return tournament;
    }

    public static async Task<Tournament> LoadFromVisualXMLAsync(VisualXML.TPNetworkDocument visualXml) { 
      return await Task.Run(() => LoadFromVisualXML(visualXml));
    }

    public static Tournament LoadFromVisualXML(VisualXML.TPNetworkDocument visualXml) {
      List<ClubData> clubs = [];
      foreach (var club in visualXml.GetGroup("Result/Tournament/Clubs").Groups) {
        clubs.Add(new ClubData(club));
      }

      List<Player> players = [];
      foreach (var player in visualXml.GetGroup("Result/Tournament/Players").Groups) {
        players.Add(Player.Parse(new PlayerData(player), clubs));
      }

      List<Entry> entries = [];
      foreach (var entry in visualXml.GetGroup("Result/Tournament/Entries").Groups) {
        entries.Add(Entry.Parse(new EntryData(entry), players));
      }

      List<PlayerMatchData> matches = [];
      foreach (var match in visualXml.GetGroup("Result/Tournament/Matches").Groups) {
        matches.Add(new PlayerMatchData(match));
      }

      List<DrawData> rawDraws = [];
      foreach (var draw in visualXml.GetGroup("Result/Tournament/Draws").Groups) {
        rawDraws.Add(new DrawData(draw));
      }

      List<Link> links = [];
      foreach (var link in visualXml.GetGroup("Result/Tournament/Links").Groups) {
        links.Add(Link.Parse(new LinkData(link), rawDraws));
      }

      List<Draw> draws = [];
      foreach (var draw in rawDraws) {
        draws.Add(Draw.Parse(draw, matches, entries, links));
      }

      TournamentSettings tournamentSettings = new(visualXml);

      List<Event> events = []; 
      foreach (var ev in visualXml.GetGroup("Result/Tournament/Events").Groups) {
        events.Add(Event.Parse(new EventData(ev), draws, tournamentSettings));
      }

      List<LocationData> locations = [];
      foreach (var location in visualXml.GetGroup("Result/Tournament/Locations").Groups) {
        locations.Add(new LocationData(location));
      }

      List<Court> courts = [];
      foreach (var court in visualXml.GetGroup("Result/Tournament/Courts").Groups) {
        courts.Add(Court.Parse(new CourtData(court), locations));
      }

      Tournament tournament = new() {
        TournamentSettings = tournamentSettings,
        Events = events,
        Entries = entries,
        Courts = courts,
      };
      
      return tournament;
    }
  }
}
