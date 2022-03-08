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
      (List<Court> courts, List<Exception> errors) p = await Converter.ExtractCourts(Connection);
      if (p.errors.Count > 0) {
        throw new AggregateException(p.errors);
      }
      return p.courts;
    }

    public async Task<List<TP.TournamentClass>> GetTournamentClasses() {
      (List<TP.TournamentClass> tournamentClasses, List<Exception> errors) p = await Converter.Extract(Connection);
      if (p.errors.Count > 0) {
        throw new AggregateException(p.errors);
      }
      return p.tournamentClasses;
    }
  }
}
