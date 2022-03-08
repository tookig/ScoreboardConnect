using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Player : TpObject {
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int ClubID { get; set; }
    public Club Club { get; set; }
    public int CountryID { get; set; }

    public Player(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      LastName = GetString(reader, "name");
      FirstName = GetString(reader, "firstname");
      ClubID = GetInt(reader, "club");
      CountryID = GetInt(reader, "country");
    }

    public override string ToString() {
      return string.Format("{0}\t{1} {2}\t{3}", ID, FirstName, LastName, Club != null ? Club.Name : "<no club>");
    }
  }
}
