using System;
using System.Collections.Generic;
using System.Text;

using System.Data;


namespace TP {
  public class Loader {
    private static List<T> LoadStuff<T>(IDbCommand cmd, Func<IDataReader, T> parseDbData) {
      List<T> result = new List<T>();
      using (IDataReader reader = cmd.ExecuteReader()) {
        while (reader.Read()) {
          T item = parseDbData(reader);
          result.Add(item);
        }
      }
      return result;
    }

    public static List<Link> LoadLinks(IDbCommand cmd) {
      // Get links
      cmd.CommandText = "SELECT * FROM Link";
      List<Link> links = LoadStuff<Link>(cmd, reader => new Link(reader));
      // Return
      return links;
    }

    public static List<Location> LoadLocations(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM Location";
      return LoadStuff(cmd, reader => new Location(reader));
    }

    public static List<Court> LoadCourts(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM Court";
      var courts = LoadStuff(cmd, reader => new Court(reader));
      var locations = LoadLocations(cmd);
      courts.ForEach(c => c.Location = locations.Find(l => l.ID == c.LocationID));
      return courts;
    }

    public static List<Event> LoadEvents(IDbCommand cmd) {
      // Get draws
      var draws = LoadDraws(cmd);
      // Get players
      var players = LoadPlayers(cmd);
      // Get events
      cmd.CommandText = "SELECT * FROM Event";
      List<Event> events = LoadStuff<Event>(cmd, reader => new Event(reader));
      events.ForEach(e => e.Draws.AddRange(draws.FindAll(d => d.EventID == e.ID)));

      return events;
    }

    private static List<Draw> LoadDraws(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM Draw";
      var draws = LoadStuff<Draw>(cmd, reader => new Draw(reader));
      var matches = LoadMatches(cmd);
      draws.ForEach(draw => draw.Matches.AddRange(matches.FindAll(match => match.DrawID == draw.ID)));
      return draws;
    }

    public static List<Entry> LoadEntries(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM Entry";
      var entries = LoadStuff<Entry>(cmd, reader => new Entry(reader));
      var players = LoadPlayers(cmd);
      entries.ForEach(entry => {
        if (entry.Player1ID > 0) {
          entry.Player1 = players.Find(p => p.ID == entry.Player1ID);
        }
        if (entry.Player2ID > 0) {
          entry.Player2 = players.Find(p => p.ID == entry.Player2ID);
        }
      });
      return entries;
    }

    public static List<Player> LoadPlayers(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM Player";
      var players = LoadStuff<Player>(cmd, (reader) => new Player(reader));
      var clubs = LoadClubs(cmd);
      players.ForEach(player => player.Club = clubs.Find(club => club.ID == player.ClubID));
      return players;
    }

    private static List<Club> LoadClubs(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM Club";
      return LoadStuff<Club>(cmd, reader => new Club(reader));
    }

    private static List<PlayerMatch> LoadMatches(IDbCommand cmd) {
      cmd.CommandText = "SELECT * FROM PlayerMatch";
      var matches = LoadStuff<PlayerMatch>(cmd, reader => new PlayerMatch(reader));
      var entries = LoadEntries(cmd);
      var links = LoadLinks(cmd);
      matches.ForEach(match => match.Entry = match.EntryID == 0 ? null : entries.Find(entry => entry.ID == match.EntryID));
      matches.ForEach(match => match.Link = match.LinkID == 0 ? null : links.Find(link => link.ID == match.LinkID));
      return matches;
    }
  }
}
