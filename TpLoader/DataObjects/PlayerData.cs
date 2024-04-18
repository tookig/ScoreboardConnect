using System;
using System.Collections.Generic;
using System.Text;
using TP.VisualXML;

namespace TP.Data {
  public class PlayerData : TpDataObject {
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int ClubID { get; set; }
    public int CountryID { get; set; }
    public string CountryString { get; set; }
    public PlayerData() { }
    public PlayerData(PlayerData cpy) {
      ID = cpy.ID;
      LastName = cpy.LastName;
      FirstName = cpy.FirstName;
      ClubID = cpy.ClubID;
      CountryID = cpy.CountryID;
      CountryString = cpy.CountryString;
    }

    public PlayerData(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      LastName = GetString(reader, "name");
      FirstName = GetString(reader, "firstname");
      ClubID = GetInt(reader, "club");
      CountryID = GetInt(reader, "country");
    }

    public PlayerData(GroupNode playerNode) {
      ID = ((ItemNode<int>)playerNode["ID"]).Value;
      LastName = ((ItemNode<string>)playerNode["Lastname"]).Value;
      FirstName = ((ItemNode<string>)playerNode["Firstname"]).Value;
      ClubID = ((ItemNode<int>)playerNode["ClubID"]).Value;
      CountryString = ((ItemNode<string>)playerNode["Country"]).Value;
    }
  }
}
