using System;
using System.Collections.Generic;
using System.Text;
using TP.VisualXML;

namespace TP.Data {
  public class TournamentSettings : TpDataObject {
    public string TournamentName { get; set; }

    public TournamentSettings() { }

    public TournamentSettings(TournamentSettings cpy) {
      TournamentName = cpy.TournamentName;
    }

    public TournamentSettings(System.Data.IDataReader reader) {
      TournamentName = GetString(reader, "value");
    }

    public TournamentSettings(TPNetworkDocument network) {
      network.GetGroup("Result/Tournament/Settings").Groups.ForEach(setting => {
        if (((ItemNode<int>)setting["ID"]).Value == 1001) {
          TournamentName = ((ItemNode<string>)setting["Value"]).Value;
        }
      });
    }
  }
}
