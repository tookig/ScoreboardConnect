using System;
using System.Collections.Generic;
using System.Text;
using TP.VisualXML;

namespace TP.Data {
  public class TournamentSettings {
    public string TournamentName { get; set; }

    public TournamentSettings() { }

    public TournamentSettings(TPNetwork network) {
      network.GetGroup("Result/Tournament/Settings").Groups.ForEach(setting => {
        if (((ItemNode<int>)setting["ID"]).Value == 1001) {
          TournamentName = ((ItemNode<string>)setting["Value"]).Value;
        }
      });
    }
  }
}
