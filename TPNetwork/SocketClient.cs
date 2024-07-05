﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml;
using ICSharpCode.SharpZipLib.GZip;
using TPNetwork.Messages;

namespace TPNetwork {
  public class SocketClient {
    private static string xmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

    private string Password { get; set; }
    private string Unicode { get; set; }
    public int Port { get; set; }

    public event EventHandler<MessageBase> MessageSent;
    public event EventHandler<XmlDocument> MessageReceived;
    public event EventHandler<Exception> Error;

    public SocketClient(int port = 9901) {
      Port = port;
    }

    public async Task<XmlDocument> Login() {
      var preSendXml = await SendRequest(new Login());
      Password = preSendXml.SelectSingleNode("//ITEM[@ID='Password']")?.InnerText;
      return preSendXml;
    }

    public async Task<XmlDocument> GetTournamentInfo() {
      return await Send(new SendTournamentInfo(Password));
    }

    public async Task<XmlDocument> SendUpdate(TP.Match tpMatch) {
      return await Send(new SendUpdate(Password, Unicode, tpMatch));
    }

    private async Task<XmlDocument> Send(MessageBase message) {
      // Check if we have a login password
      if (Password == null) {
        await Login();
        message.SetPassword(Password);
      }
      // Send main message
      var xml = await SendRequest(message);
      // Post send events
      int statusCode;
      if (!int.TryParse(xml.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Result']").InnerText, out statusCode)) {
        Exception e = new Exception("Request failed; coult not parse return data");
        OnError(e);
        throw e;
      } else if (statusCode != 1) {
        Exception e = new Exception(string.Format("Request failed, server reported an error (code {0})", statusCode));
        OnError(e);
        throw e;
      }
      if (xml.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Unicode']") != null) {
        Unicode = xml.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Unicode']").InnerText;
      }
      return xml;
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

    private Task<XmlDocument> SendRequest(MessageBase message) {
      return Task.Run(() => {
        try {
          IPHostEntry host = Dns.GetHostEntry("localhost");
          IPAddress ipAddress = host.AddressList[0];

          var compressedStream = CompressXml(message);

          using (TcpClient client = new TcpClient(ipAddress.ToString(), Port)) {
            OnMessageSent(message);
            var stream = client.GetStream();

            byte[] size = BitConverter.GetBytes((Int32)compressedStream.Length);
            if (BitConverter.IsLittleEndian) {
              Array.Reverse(size);
            }
            stream.Write(size);
            stream.Write(compressedStream.ToArray());

            stream.Read(size, 0, 4);
            var returnXml = DecompressXml(stream);
            OnMessageReceived(returnXml);
            return returnXml;
          }
        } catch (Exception e) {
          OnError(e);
          throw;
        }
      });
    }

    protected void OnError(Exception e) {
      Task.Run(() => Error?.Invoke(this, e));
    }

    protected void OnMessageSent(MessageBase message) {
      Task.Run(() => MessageSent?.Invoke(this, message));
    }

    protected void OnMessageReceived(XmlDocument message) {
      Task.Run(() => MessageReceived?.Invoke(this, message));
    }
  }
}
