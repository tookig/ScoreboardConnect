using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Data;

namespace TP {
  public abstract class File {
    public OdbcConnection Connection { get; private set; }
    protected object connectionLock = new object();

    public File(string filename) {
      string password = "d4R2GY76w2qzZ";
      OdbcConnectionStringBuilder connectionStringBuilder = new OdbcConnectionStringBuilder();
      connectionStringBuilder.Driver = "Microsoft Access Driver (*.mdb, *.accdb)";
      connectionStringBuilder.Add("Dbq", filename);
      connectionStringBuilder.Add("Uid", "Admin");
      connectionStringBuilder.Add("Pwd", password);
      lock (connectionLock) {
        Connection = new OdbcConnection(connectionStringBuilder.ConnectionString);
        Connection.Open();
      }
    }

    public void Close() {
      lock (connectionLock) {
        if ((Connection != null) && (Connection.State == System.Data.ConnectionState.Open)) {
          Connection.Close();
        }
      }
    }
    protected virtual Task<List<T>> LoadStuff<T>(string sql, Func<IDataReader, T> parseDbData) {
      lock (connectionLock) {
        if (Connection?.State != ConnectionState.Open) {
          throw new Exception("Could not read file; connection closed");
        }
      }
      return Task.Run(() => {
        List<T> result = new List<T>();
        lock (connectionLock) {
          using (IDbCommand cmd = Connection.CreateCommand()) {
            cmd.CommandText = sql;
            using (IDataReader reader = cmd.ExecuteReader()) {
              while (reader.Read()) {
                T item = parseDbData(reader);
                result.Add(item);
              }
            }
          }
        }
        return result;
      });
    }
  }
}
