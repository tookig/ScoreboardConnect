using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Court : TpObject {
    public int ID { get; set; }
    public string Name { get; set; }
    public int LocationID { get; set; }
    public int TpMatchID { get; set; }

    public Location Location { get; set; }

    public Court(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
      LocationID = GetInt(reader, "location");
      TpMatchID = GetInt(reader, "playermatch");
    }

    public override string ToString() {
      return string.Format("{0} ({1})", Name, Location?.Name);
    }
  }
}
