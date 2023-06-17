using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace TP {
  public class SBUploader {
    public interface ICourtMapper {
      /// <summary>
      /// Map a TP court to a scoreboard court id.
      /// </summary>
      /// <param name="tpCourt">Name of TP court to find matching SB court for</param>
      /// <returns>SB court reference, null if there is no mapping available</returns>
      Task<ScoreboardLiveApi.Court> MapTPCourtToSBCourt(string tpCourtName);
    }

    private ScoreboardLiveApi.ApiHelper m_helper;
    private ScoreboardLiveApi.Device m_device;
    private ScoreboardLiveApi.Tournament m_tournament;
    private ICourtMapper m_mapper;

    public SBUploader(ScoreboardLiveApi.ApiHelper helper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament, ICourtMapper mapper) {
      m_device = device ?? throw new ArgumentNullException("Device reference cannot be null");
      m_helper = helper ?? throw new ArgumentNullException("Helper reference cannot be null");
      m_tournament = tournament ?? throw new ArgumentNullException("Tournament reference cannot be null");
      m_mapper = mapper ?? throw new ArgumentNullException("Mapper reference cannot be null");
    }

    public async Task<(ScoreboardLiveApi.Court, ScoreboardLiveApi.Match)> AssignMatchToCourt(Match tpMatch, Draw tpDraw, Event tpEvent, string tpCourtName) {
      // Get the mapped scoreboard court for this TP-court. If none available we can stop here.
      var sbCourt = await m_mapper.MapTPCourtToSBCourt(tpCourtName);
      if (sbCourt == null) {
        return (null, null);
      }

      // Try and find the match on the server.
      //
      // TODO: Make sure the returned match is in the correct tournament
      // (the tag search is made globally, so if we are really unlucky there might be another equal tag in
      // another tournament).
      string tag = Tournament.CreateMatchTag(m_tournament, tpMatch, tpEvent, tpDraw);
      var sbMatches = await m_helper.FindMatchByTag(m_device, tag);
      if (sbMatches.Count > 1) {
        // More then one match means tags are not unique, and there is no way to know what match to use.
        throw new Exceptions.MatchTagNotUniqueException("Match tag not unique; more than 1 match returned from server.");
      }

      var sbMatch = sbMatches.FirstOrDefault();
      if (sbMatch == null) {
        // No match on the server; create a new on-the-fly match.
        sbMatch = await CreateOnTheFlyMatch(tpEvent, tpMatch, tag);
      }
      // Assign match to court
      await UpdateCourtWithMatch(sbCourt, sbMatch);
      return (sbCourt, sbMatch);
     }

    public async Task<ScoreboardLiveApi.Court> ClearCourt(string tpCourtName) {
      // Get the mapped scoreboard court for this TP-court. If none available we can stop here.
      var sbCourt = await m_mapper.MapTPCourtToSBCourt(tpCourtName);
      if (sbCourt == null) {
        return null;
      }

      await m_helper.ClearCourt(m_device, sbCourt);
      return sbCourt;
    }

    protected async Task<ScoreboardLiveApi.Match> CreateOnTheFlyMatch(Event tpEvent, Match tpMatch, string tag) {
      var localSBMatch = Tournament.ConvertMatch(tpMatch, Tournament.CreateMatchCategoryString(tpEvent));
      return await m_helper.CreateMatch(m_device, m_tournament, null, localSBMatch);
    }

    protected async Task UpdateCourtWithMatch(ScoreboardLiveApi.Court sbCourt, ScoreboardLiveApi.Match sbMatch) {
      await m_helper.AssignMatchToCourt(m_device, sbMatch, sbCourt);
    }
  }
}
