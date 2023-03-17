using System;
using System.Collections.Generic;
using System.Text;

namespace TP {
  public class Link : Data.LinkData {
    public enum TargetSlot { Slot1 = 0, Slot2 = 1 }
    public Match Source { get; private set; }
    public Match Target { get; private set; }
    public TargetSlot Slot { get; private set; }
    protected Link(Data.LinkData raw) : base(raw) {}
    public static Link Parse(Data.LinkData raw) {
      return new Link(raw);
    }
  }
}
