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
      /// <returns>ID of SB court, null if there is no mapping available</returns>
      Task<int?> MapTPCourtToSBCourt(string tpCourtName);
    }

    private ScoreboardLiveApi.ApiHelper m_helper;
    private ScoreboardLiveApi.Device m_device;
    private ScoreboardLiveApi.Tournament m_tournament;
    private ICourtMapper m_mapper;

    public SBUploader(ScoreboardLiveApi.ApiHelper helper, ScoreboardLiveApi.Device device, ScoreboardLiveApi.Tournament tournament, ICourtMapper mapper) {
      m_device = device ?? throw new ArgumentNullException("Device reference cannot be null");
      m_helper = helper ?? throw new ArgumentNullException("Helper reference cannot be null");
      m_tournament = tournament ?? throw new ArgumentNullException("Tournament reference cannot be null");
      m_mapper = m_mapper ?? throw new ArgumentNullException("Mapper reference cannot be null");
    }

    public async Task AssignMatchToCourt(Match tpMatch, string tpCourtName) {
      // Get the mapped scoreboard court for this TP-court. If none available we can stop here.
      int? sbCourtID = await m_mapper.MapTPCourtToSBCourt(tpCourtName);
      if (sbCourtID == null) {
        return;
      }
      // Try and find the match on the server
      // var sbMatches = m_helper.FindMatchByTag(m_device, TP.Tournament.CreateMatchTag()
    }
  }
}
