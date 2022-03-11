using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestTp {
  class Program {
    static async Task Main(string[] args) {

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
