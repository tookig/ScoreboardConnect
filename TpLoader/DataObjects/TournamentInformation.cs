using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TP {
  public class TournamentInformation : TpObject {
    public int ID { get; set; }
    public string TournamentID { get; set; }
    public string TournamentName { get; set; }

    public TournamentInformation(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      TournamentID = GetString(reader, "tournamentid");
      TournamentName = GetString(reader, "tournamentname");
    }

    public override string ToString() {
      return string.Format("{0} ({1})", TournamentName, TournamentID);
    }
  }
}
