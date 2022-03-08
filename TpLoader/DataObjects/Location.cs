using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Location : TpObject {
    public int ID { get; set; }
    public string Name { get; set; }

    public Location(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
    }
  }
}
