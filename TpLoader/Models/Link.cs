using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Link : Data.LinkData {
    public enum TargetSlot { Slot1 = 0, Slot2 = 1 }
    public Draw Source { get; set; }
    public Match Target { get; set; }
    public TargetSlot Slot { get; set; }
    public Link(Data.LinkData raw) : base(raw) {}
  }
}
