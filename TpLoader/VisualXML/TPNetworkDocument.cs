using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TP.VisualXML {
  public class TPNetworkDocument : GroupNode {
    public XmlDocument XMLSource { get; }

    public TPNetworkDocument(XmlDocument doc) {
      XMLSource = doc;
      Parse();
    }

    protected void Parse() {
      var result = XMLSource.SelectSingleNode("//VISUALXML");
      XmlReader reader = new XmlNodeReader(result);

      var root = ParseGroup(reader);
      ID = root.ID;
      Groups = root.Groups;
      Items = root.Items;
    }

    protected GroupNode ParseGroup(XmlReader reader) {
      reader.Read();

      GroupNode group = new GroupNode();
      group.ID = reader.GetAttribute("ID");

      while (reader.Read()) {
        if (reader.NodeType != XmlNodeType.Element) {
          continue;
        }

        if (reader.Name == "GROUP") {
          group.Groups.Add(ParseGroup(reader.ReadSubtree()));
        } else if (reader.Name == "ITEM") {
          group.Items.Add(ParseItem(reader.ReadSubtree()));
        }
      }

      return group;

    }

    protected IItemNode ParseItem(XmlReader reader) {
      reader.Read();
      string itemType = reader.GetAttribute("TYPE");
      if (itemType == "String") {
        return ParseStringItem(reader);
      } else if (itemType == "Integer") {
        return ParseIntItem(reader);
      } else if (itemType == "Float") {
        return ParseFloatItem(reader);
      } else if (itemType == "Bool") {
        return ParseBoolItem(reader);
      } else if (itemType == "DateTime") {
        return ParseDateTimeItem(reader);
      }
      throw new Exception("Unknown item type");
    }

    protected ItemNode<bool> ParseBoolItem(XmlReader reader) {
      ItemNode<bool> item = new ItemNode<bool>();
      item.ID = reader.GetAttribute("ID");
      item.Type = reader.GetAttribute("TYPE");
      item.Value = reader.ReadElementContentAsBoolean();
      return item;
    }

    protected ItemNode<string> ParseStringItem(XmlReader reader) {
      ItemNode<string> item = new ItemNode<string>();
      item.ID = reader.GetAttribute("ID");
      item.Type = reader.GetAttribute("TYPE");
      item.Value = reader.ReadElementContentAsString();
      return item;
    }

    protected ItemNode<int> ParseIntItem(XmlReader reader) {
      ItemNode<int> item = new ItemNode<int>();
      item.ID = reader.GetAttribute("ID");
      item.Type = reader.GetAttribute("TYPE");
      item.Value = reader.ReadElementContentAsInt();
      return item;
    }

    protected ItemNode<float> ParseFloatItem(XmlReader reader) {
      ItemNode<float> item = new ItemNode<float>();
      item.ID = reader.GetAttribute("ID");
      item.Type = reader.GetAttribute("TYPE");
      item.Value = reader.ReadElementContentAsFloat();
      return item;
    }

    protected ItemNode<DateTime> ParseDateTimeItem(XmlReader reader) {
      ItemNode<DateTime> item = new ItemNode<DateTime>();
      item.ID = reader.GetAttribute("ID");
      item.Type = reader.GetAttribute("TYPE");

      var subReader = reader.ReadSubtree();
      subReader.ReadToFollowing("DATETIME");
      int year = int.Parse(subReader.GetAttribute("Y"));
      int month = int.Parse(subReader.GetAttribute("MM"));
      int day = int.Parse(subReader.GetAttribute("D"));
      int hour = int.Parse(subReader.GetAttribute("H"));
      int minute = int.Parse(subReader.GetAttribute("M"));
      int second = int.Parse(subReader.GetAttribute("S"));
      int ms = int.Parse(subReader.GetAttribute("MS"));
      item.Value = new DateTime(year, month, day, hour, minute, second, ms);

      return item;
    }
  }
}
