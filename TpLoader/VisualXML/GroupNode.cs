using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP.VisualXML {
  public class GroupNode {
    public string ID { get; set; }
    public List<GroupNode> Groups { get; set; } = new List<GroupNode>();
    public List<IItemNode> Items { get; set; } = new List<IItemNode>();

    public IItemNode this[string id] => GetItem(id);

    public IItemNode GetItem(string id) {
      foreach (IItemNode item in Items) {
        if (item.ID == id) {
          return item;
        }
      }
      throw new Exception($"Item {id} not found");
    }

    public bool HasItem(string id) {
      foreach (IItemNode item in Items) {
        if (item.ID == id) {
          return true;
        }
      }
      return false;
    }

    public GroupNode GetGroup(string id) {
      int separator = id.IndexOf('/');
      if (separator > 0) {
        string groupID = id.Substring(0, separator);
        string itemID = id.Substring(separator + 1);
        GroupNode group = GetGroup(groupID);
        return group.GetGroup(itemID);
      }
      
      foreach (GroupNode group in Groups) {
        if (group.ID == id) {
          return group;
        }
      }
      return null;
    }

    override public string ToString() {
      return ToString("");
    }

    protected string ToString(string indent) {
      StringBuilder sb = new StringBuilder();
      sb.Append($"{indent}-- {ID}");
      foreach (IItemNode item in Items) {
        sb.Append($" {item}");
      }
      sb.AppendLine();

      foreach (GroupNode group in Groups) {
        sb.Append(group.ToString(indent + "  "));
      }

      return sb.ToString();
    }
  }
}
