using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TP.VisualXML;

namespace TP.Data {
  public class LocationData : TpDataObject {
    public int ID { get; set; }
    public string Name { get; set; }

    public LocationData(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
    }

    public LocationData(GroupNode locationNode) {
      ID = ((ItemNode<int>)locationNode["ID"]).Value;
      Name = ((ItemNode<string>)locationNode["Name"]).Value;
    }
  }
}
