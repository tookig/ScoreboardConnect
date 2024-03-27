using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardConnectWinUI3 {
  internal interface ICourtCorrelator {
    void SetTPCourts(List<TP.Court> tpCourts);
    void SetSBCourts(List<ScoreboardLiveApi.Court> sbCourts);
    Dictionary<ScoreboardLiveApi.Court, TP.Court> GetSnapshot();
  }
}
