using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using TP.Data;

namespace TestTp {
  class Program {

    static void Main(string[] args) {
      TPNetwork.SocketClient client = new TPNetwork.SocketClient();
     
      var visualXml = new TP.VisualXML.TPNetwork(client.GetTournamentInfo().Result);
      
      var tournament = TP.Tournament.LoadFromVisualXML(visualXml);
      PrintTournament(tournament);
    }


    static void PrintTournament(TP.Tournament tournament) {
      foreach (TP.Event ev in tournament.Events) {
        Console.WriteLine("EVENT: {0}", ev.Name);
        foreach (TP.Draw draw in ev.Draws) {
          Console.WriteLine("DRAW: {0}", draw.Name);
          foreach (TP.Match match in draw.Matches) {
            Console.WriteLine("MATCH: {0}", match);
          }
        }
        Console.WriteLine();
      }
    }

    static async Task MainXX(string[] args) {
      string filename = "D:\\Tmp\\lergok.tp";
      TP.TPFile file = new TP.TPFile(filename);
      TP.Tournament tournament = await TP.Tournament.LoadFromTP(file);

      /*
      foreach (TP.Event ev in tournament.Events) {
        Console.WriteLine("EVENT: {0}", ev.Name);
        foreach (TP.Draw draw in ev.Draws) {
          Console.WriteLine("DRAW: {0}", draw.Name);
          foreach (TP.Data.LinkData link in draw.Links) {
            Console.WriteLine("LINK: {0} {1} {2}", link.ID, link.SourceDrawID, link.SourcePosition);
          }
        }
        Console.WriteLine();
      }

      */
      /*
      foreach (TP.Tournament.ExportClassItem eci in tournament.ExportClasses((x) => true)) {
        Console.WriteLine("-- MAIN DRAW --");
        Console.WriteLine(eci.TPDraw?.ToString());
        Console.WriteLine("-- SUB DRAWS --");
        Console.WriteLine("-- MATCHES --");
        foreach (var m in eci.Matches) {
          //          Console.WriteLine("Match {0} ({1}), Class {2}", m.MatchData.TournamentMatchNumber, m.MatchData.ClassDescription, m.BelongsTo?.Description);
        }
        Console.WriteLine("--   END     --");
      }

      /*
      Console.WriteLine(tournament);


      while (true) {
        Console.Write("Select draw to show (0 to exit): ");
        int drawid = int.Parse(Console.ReadLine().Trim());
        if (drawid == 0) break;

        TP.Draw draw = null;
        foreach (TP.Event ev in tournament.Events) {
          draw = ev.Draws.FirstOrDefault(d => d.ID == drawid);
          if (draw != null) break;
        }
        if (draw == null) {
          Console.WriteLine("Draw not found");
        } else {
          Console.WriteLine("Matches:");
          foreach (TP.Match match in draw.Matches) {
            Console.WriteLine(match);
          }
        }
      }
      */

      /*

      TP.TPStream stream = new TP.TPStream("abc123") {
        IP = IPAddress.Parse("192.168.56.1")
      };
      await stream.GetTournamentInfo();
      Console.ReadKey();

      /*
      string inFile = "D:\\Tmp\\login.gz";
      string outFile = "D:\\Tmp\\login.xml";

      string xml;
      using (var fs = new FileStream(inFile, FileMode.Open)) {
        using (var gz = new System.IO.Compression.GZipStream(fs, System.IO.Compression.CompressionMode.Decompress)) {
          using (var reader = new StreamReader(gz, Encoding.UTF8)) {
            xml = reader.ReadToEnd();
          }
        }
      }

      Console.WriteLine(xml);

      if (File.Exists(outFile)) {
        File.Delete(outFile);
      }

      using (var fs = new FileStream(outFile, FileMode.CreateNew)) {
        using (var sw = new StreamWriter(fs, Encoding.UTF8)) {
          sw.Write(xml);
        }
      }
      */
    }
  }
}
