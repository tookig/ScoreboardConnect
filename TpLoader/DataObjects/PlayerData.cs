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

    // The following fields are not used by the application, 
    // but needs to be included for compatibility with the Tournament Planner
    // software, else they will be reset in Tournament Planner when Scoreboard
    // Connect sends match updates.
    public float Discount { get; set; }
    public bool WeightChecked { get; set; }
    public float EntryWeight { get; set; }
    public float Weight { get; set; }
    public float Weight2 { get; set; }
    public bool CheckedIn { get; set; }
    public bool FirstCheckIn { get; set; }
    
    
    public PlayerData() { }
    public PlayerData(PlayerData cpy) {
      ID = cpy.ID;
      LastName = cpy.LastName;
      FirstName = cpy.FirstName;
      ClubID = cpy.ClubID;
      CountryID = cpy.CountryID;
      CountryString = cpy.CountryString;
      Discount = cpy.Discount;
      WeightChecked = cpy.WeightChecked;
      EntryWeight = cpy.EntryWeight;
      Weight = cpy.Weight;
      Weight2 = cpy.Weight2;
      CheckedIn = cpy.CheckedIn;
      FirstCheckIn = cpy.FirstCheckIn;
    }

    public PlayerData(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      LastName = GetString(reader, "name");
      FirstName = GetString(reader, "firstname");
      ClubID = GetInt(reader, "club");
      CountryID = GetInt(reader, "country");
      Discount = GetFloat(reader, "discount");
      WeightChecked = GetBool(reader, "weightchecked");
      EntryWeight = GetFloat(reader, "entryweight");
      Weight = GetFloat(reader, "weight");
      Weight2 = GetFloat(reader, "weight2");
      CheckedIn = GetBool(reader, "checkedin");
      FirstCheckIn = GetBool(reader, "firstcheckin");
    }

    public PlayerData(GroupNode playerNode) {
      ID = ((ItemNode<int>)playerNode["ID"]).Value;
      LastName = ((ItemNode<string>)playerNode["Lastname"]).Value;
      FirstName = ((ItemNode<string>)playerNode["Firstname"]).Value;
      ClubID = ((ItemNode<int>)playerNode["ClubID"]).Value;
      CountryString = ((ItemNode<string>)playerNode["Country"]).Value;
      Discount = ((ItemNode<float>)playerNode["Discount"]).Value;
      WeightChecked = ((ItemNode<bool>)playerNode["WeightChecked"]).Value;
      EntryWeight = ((ItemNode<float>)playerNode["EntryWeight"]).Value;
      Weight = ((ItemNode<float>)playerNode["Weight"]).Value;
      Weight2 = ((ItemNode<float>)playerNode["Weight2"]).Value;
      CheckedIn = ((ItemNode<bool>)playerNode["CheckedIn"]).Value;
      FirstCheckIn = ((ItemNode<bool>)playerNode["FirstCheckIn"]).Value;
    }
  }
}
