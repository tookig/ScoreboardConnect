using System;
using System.Collections.Generic;
using System.Text;
using TP.VisualXML;

namespace TP.Data {
  public class ClubData : TpDataObject {
    public int ID { get; set; }
    public string Name { get; set; }
    public ClubData() {

    }
    public ClubData(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
    }

    public ClubData(GroupNode clubNode) {
      ID = ((ItemNode<int>)clubNode["ID"]).Value;
      Name = ((ItemNode<string>)clubNode["Name"]).Value;
    }
  }
}
