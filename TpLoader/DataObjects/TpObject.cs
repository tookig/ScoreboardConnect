using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TP {
  public abstract class TpObject {
    protected static int GetInt(IDataReader reader, string fieldName) {
      int fieldIndex = reader.GetOrdinal(fieldName);
      if (reader.IsDBNull(fieldIndex)) return 0;
      return reader.GetInt32(fieldIndex);
    }

    protected static string GetString(IDataReader reader, string fieldName) {
      return reader.GetString(reader.GetOrdinal(fieldName));
    }

    protected static DateTime GetDateTime(IDataReader reader, string fieldName) {
      int fieldIndex = reader.GetOrdinal(fieldName);
      if (reader.IsDBNull(fieldIndex)) return new DateTime(1899, 12, 30);
      return reader.GetDateTime(fieldIndex);
    }

    protected static bool GetBool(IDataReader reader, string fieldName) {
      int fieldIndex = reader.GetOrdinal(fieldName);
      if (reader.IsDBNull(fieldIndex)) return false;
      return reader.GetBoolean(fieldIndex);
    }
  }
}
