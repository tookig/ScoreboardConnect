using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Threading.Tasks;

namespace TP {
  public class TPFile {
    public OdbcConnection Connection { get; private set; }

    public TPFile(string filename) {
      string password = "d4R2GY76w2qzZ";
      OdbcConnectionStringBuilder connectionStringBuilder = new OdbcConnectionStringBuilder();
      connectionStringBuilder.Driver = "Microsoft Access Driver (*.mdb, *.accdb)";
      connectionStringBuilder.Add("Dbq", filename);
      connectionStringBuilder.Add("Uid", "Admin");
      connectionStringBuilder.Add("Pwd", password);
      Connection = new OdbcConnection(connectionStringBuilder.ConnectionString);
      Connection.Open();
    }

    public void Close() {
      if ((Connection != null) && (Connection.State == System.Data.ConnectionState.Open)) {
        Connection.Close();
      }
    }

    public async Task<List<Court>> GetCourts() {
      return await Converter.ExtractCourts(Connection);
    }

    public async Task<List<TP.TournamentClass>> GetTournamentClasses() {
      return await Converter.Extract(Connection);
    }

    public async Task<List<TP.Event>> GetEvents() {
      return await Converter.ExtractEvents(Connection);
    }

    public async Task<List<TournamentInformation>> GetTournamentInformation() {
      return await Converter.ExtractTournamentInformation(Connection);
    }
  }
}
