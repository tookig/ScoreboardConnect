using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Event : TpObject {
    public enum EventTypes { Singles = 1, Doubles = 2}
    public enum Genders { Men = 1, Women = 2, Mixed = 3, Boys = 4, Girls = 5 }

    public int ID { get; set; }
    public string Name { get; set; }
    public EventTypes EventType { get; set; }
    public Genders Gender { get; set; }
    public List<Draw> Draws { get; private set; }

    public Event(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Name = GetString(reader, "name");
      Gender = (Genders)GetInt(reader, "gender");
      EventType = (EventTypes)GetInt(reader, "eventtype");
      Draws = new List<Draw>();
    }

    public string CreateMatchCategoryString() {
      return Gender switch {
        Genders.Men => EventType == EventTypes.Singles ? "ms" : "md",
        Genders.Boys => EventType == EventTypes.Singles ? "ms" : "md",
        Genders.Women => EventType == EventTypes.Singles ? "ws" : "wd",
        Genders.Girls => EventType == EventTypes.Singles ? "ws" : "wd",
        _ => "xd",
      };
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("EVENT {0}\t{1}\t{2}\t{3}{4}", ID, Name, Gender, EventType, Environment.NewLine);
      Draws.ForEach(draw => sb.AppendFormat("\t{0}{1}", draw.ToString(), Environment.NewLine));
      return sb.ToString();
    }
  }
}
