using System.Collections.Generic;
using System.Text;

namespace TP.VisualXML {
  public class ItemNode<T> : IItemNode {
    public string ID { get; set; }
    public string Type { get; set; }
    public T Value { get; set; }

    override public string ToString() {
      return $"{ID}={Value}";
    }
  }
}
