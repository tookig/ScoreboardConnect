using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Club : TpObject {
    public int ID { get; set; }
    public string Name { get; set; }

    public Club (string name) {
      Name = name;
    }

    public Club(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
    }

    public override string ToString() {
      return string.Format("{0}\t{1}", ID, Name);
    }
  }
}
