using System;
using System.Collections.Generic;
using System.Text;
using TP.VisualXML;

namespace TP.Data.League {
  public class TeamData : TpDataObject {
    public int ID { get; set; }
    public string Name { get; set; }
    public int CountryID { get; set; }
    public int ClubID { get; set; }

    public TeamData() {

    }
    public TeamData(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
      CountryID = GetInt(reader, "country");
      ClubID = GetInt(reader, "club");
    }

    public TeamData(GroupNode clubNode) {
      ID = ((ItemNode<int>)clubNode["ID"]).Value;
      Name = ((ItemNode<string>)clubNode["Name"]).Value;
      CountryID = ((ItemNode<int>)clubNode["CountryID"]).Value;
      ClubID = ((ItemNode<int>)clubNode["ClubID"]).Value;
    }
  }
}
