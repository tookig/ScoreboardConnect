using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TPNetwork.Messages {
  public class MessageBase : XmlDocument {
    public XmlElement VisualXMLRoot { get; private set; }
    public string ActionID { get; private set; }

    public MessageBase(string password, string actionID, string ip = "127.0.0.1", string unicode = null) : base() {
      ActionID = actionID;
      
      VisualXMLRoot = CreateElement("VISUALXML");
      VisualXMLRoot.SetAttribute("VERSION", "1.0");

      var header = CreateGroup("Header");
      VisualXMLRoot.AppendChild(header);

      var version = CreateGroup("Version");
      version.AppendChild(CreateItem("Hi", "Integer", "1"));
      version.AppendChild(CreateItem("Lo", "Integer", "1"));
      header.AppendChild(version);


      var _action = CreateGroup("Action");
      _action.AppendChild(CreateItem("ID", "String", actionID));
      _action.AppendChild(CreateItem("Password", "String", String.IsNullOrEmpty(password) ? "" : password));
      if (!string.IsNullOrEmpty(unicode)) {
        _action.AppendChild(CreateItem("Unicode", "String", unicode));
      }
      VisualXMLRoot.AppendChild(_action);

      var client = CreateGroup("Client");
      client.AppendChild(CreateItem("IP", "String", ip));
      VisualXMLRoot.AppendChild(client);

      AppendChild(VisualXMLRoot);
    }

    public void SetPassword(string password) {
      VisualXMLRoot.SelectSingleNode("//GROUP[@ID='Action']/ITEM[@ID='Password']").InnerText = password;
    }

    protected virtual XmlElement CreateGroup(string id) {
      var element = CreateElement("GROUP");
      element.SetAttribute("ID", id);
      return element;
    }

    protected virtual XmlElement CreateItem(string id, string itemType, string text) {
      var element = CreateElement("ITEM");
      element.SetAttribute("ID", id);
      element.SetAttribute("TYPE", itemType);
      element.AppendChild(CreateTextNode(text));
      return element;
    }
  }
}
