using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Link : TpObject {
    public int ID { get; set; }
    public int SourceDrawID { get; set; }
    public int SourcePosition { get; set; }

    public Link(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      SourceDrawID = GetInt(reader, "src_draw");
      SourcePosition = GetInt(reader, "src_pos");
    }
  }
}
