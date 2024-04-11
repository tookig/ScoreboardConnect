using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP {
  public class LPFile(string filename) : FileBase(filename) {
    public async Task<List<Data.ClubData>> LoadTeams() {
      return await LoadStuff("SELECT * FROM Teams", reader => new Data.ClubData(reader));
    }
  }
}
