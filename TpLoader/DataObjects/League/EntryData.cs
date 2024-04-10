using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TP.VisualXML;

namespace TP.Data.League {
  public class EntryData : TpDataObject {
    public int ID { get; set; }
    public int EventID { get; set;}
    public int TeamID { get; set; }

    public EntryData() { }
    
    public EntryData(EntryData cpy) {
      ID = cpy.ID;
      EventID = cpy.EventID;
      TeamID = cpy.TeamID;
    }
    public EntryData(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      EventID = GetInt(reader, "Event");
      TeamID = GetInt(reader, "Team");
    }


    public EntryData(GroupNode entryNode) {
      ID = ((ItemNode<int>)entryNode["ID"]).Value;
      EventID = ((ItemNode<int>)entryNode["EventID"]).Value;
      TeamID = ((ItemNode<int>)entryNode["Team"]).Value;
    }
  }
}
