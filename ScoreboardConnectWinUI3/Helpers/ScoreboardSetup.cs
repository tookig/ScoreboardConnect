using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreboardConnectWinUI3 {
  class ScoreboardSetup {
    public ScoreboardLiveApi.ApiHelper Helper { get; set; }
    public ScoreboardLiveApi.Device Device { get; set; }
    public ScoreboardLiveApi.Tournament Tournament { get; set; }
  }
}
