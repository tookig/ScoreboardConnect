using System;
using System.IO;
using System.IO.Compression;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ICSharpCode.SharpZipLib.GZip;

namespace TPNetwork {
  public class SocketClient {
    private static string xmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

    private string Password { get; set; }
    private string Unicode { get; set; }
    public int Port { get; set; }

    public SocketClient(int port = 9901) {
      Port = port;
    }

    public async Task<XmlDocument> GetTournamentInfo() {
      await PreSend();
      var xml = await SendRequest(new Messages.SendTournamentInfo(Password));
      PostSend(xml);
      return xml;
    }

    public async Task<XmlDocument> SendUpdate(TP.Match tpMatch) {
      await PreSend();
      var xml = await SendRequest(new Messages.SendUpdate(Password, Unicode, tpMatch));
      PostSend(xml);
      return xml;
    }

    private async Task PreSend() {
      // Send the request
      var xml = await SendRequest(new Messages.Login());
      // Find the ITEM element with attribute ID="Password" and get its value
      Password = xml.SelectSingleNode("//ITEM[@ID='Password']")?.InnerText;
    }

    private void PostSend(XmlDocument xml) {
      int statusCode;
      if (!int.TryParse(xml.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Result']").InnerText, out statusCode)
        || (statusCode != 1)) { 
        throw new Exception(string.Format("Request failed, server reported an error (code {0})", statusCode));
      }
      if (xml.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Unicode']") != null) {
        Unicode = xml.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Unicode']").InnerText;
      }
    }

    private MemoryStream CompressXml(XmlDocument xml)
    {
      MemoryStream output = new MemoryStream();

      using MemoryStream xmlInput = new MemoryStream();
      using StreamWriter xmlWriter = new StreamWriter(xmlInput);

      xmlWriter.WriteLine(xmlHeader);
      xmlWriter.Write(xml.OuterXml);
      xmlWriter.Flush();
      xmlInput.Position = 0;

      GZip.Compress(xmlInput, output, false, 1024, 8);
      output.Position = 0;

      return output;
    }

    private XmlDocument DecompressXml(Stream input)
    {
      MemoryStream output = new MemoryStream();
      GZip.Decompress(input, output, false);
      output.Position = 0;

      using StreamReader reader = new StreamReader(output);
      XmlDocument xml = new XmlDocument();
      xml.LoadXml(reader.ReadToEnd());
      return xml;
    }

    private Task<XmlDocument> SendRequest(XmlDocument xml) {
      return Task.Run(() => {

        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];

        var compressedStream = CompressXml(xml);

        using (TcpClient client = new TcpClient(ipAddress.ToString(), Port)) {
          var stream = client.GetStream();

          byte[] size = BitConverter.GetBytes((Int32)compressedStream.Length);
          if (BitConverter.IsLittleEndian) {
            Array.Reverse(size);
          }
          stream.Write(size);
          stream.Write(compressedStream.ToArray());

          stream.Read(size, 0, 4);
          return DecompressXml(stream);
        }
      });
    }
  }
}
