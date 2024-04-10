using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Threading.Tasks;
using TP.Data;
using System.Linq;

namespace TP {
  public class TPFile(string filename) : File(filename) {
    public async Task<TournamentSettings> LoadTournamentSettings() {
      return (await LoadStuff("SELECT * FROM Settings WHERE name='Tournament'", reader => new Data.TournamentSettings(reader)))[0];
    }

    public async Task<List<Data.LinkData>> LoadLinks() {
      return await LoadStuff("SELECT * FROM Link", reader => new Data.LinkData(reader));
    }

    public async Task<List<Data.LocationData>> LoadLocations() {
      return await LoadStuff("SELECT * FROM Location", reader => new Data.LocationData(reader));
    }

    public async Task<List<Data.CourtData>> LoadCourts() {
      return await LoadStuff("SELECT * FROM Court", reader => new Data.CourtData(reader));
    }

    public async Task<List<Data.EventData>> LoadEvents() {
      return await LoadStuff("SELECT * FROM Event", reader => new Data.EventData(reader));
    }

    public async Task<List<Data.DrawData>> LoadDraws() {
      return await LoadStuff("SELECT * FROM Draw", reader => new Data.DrawData(reader));
    }

    public async Task<List<Data.EntryData>> LoadEntries() {
      return await LoadStuff("SELECT * FROM Entry", reader => new Data.EntryData(reader));
    }

    public async Task<List<Data.PlayerData>> LoadPlayers() {
      return await LoadStuff("SELECT * FROM Player", (reader) => new Data.PlayerData(reader));
    }

    public async Task<List<Data.ClubData>> LoadClubs() {
      return await LoadStuff("SELECT * FROM Club", reader => new Data.ClubData(reader));
    }

    public async Task<List<Data.PlayerMatchData>> LoadPlayerMatches() {
      return await LoadStuff("SELECT * FROM PlayerMatch", reader => new Data.PlayerMatchData(reader));
    }
  }
}
