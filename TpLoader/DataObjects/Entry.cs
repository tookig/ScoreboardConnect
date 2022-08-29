﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TP {
  public class Entry : TpObject {
    public int ID { get; set; }
    public int Player1ID { get; set; }
    public int Player2ID { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }

    public Entry(System.Data.IDataReader reader) {
      ID = GetInt(reader, "id");
      Player1ID = GetInt(reader, "player1");
      Player2ID = GetInt(reader, "player2");
    }

    public Entry(XmlReader reader) {
      ID = GetInt(reader, "ID");
      string name1 = GetString(reader, "NAME1");
      string club1 = GetString(reader, "CLUB1");
      Player1 = new Player(name1, club1);
      string name2 = GetString(reader, "NAME2");
      string club2 = GetString(reader, "CLUB2");
      if (!string.IsNullOrWhiteSpace(name2)) {
        Player2 = new Player(name2, club2);
      }
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("ENTRY {0}:{1}", ID, Environment.NewLine);
      sb.AppendFormat("\t{0}{1}", Player1 == null ? "<no player found>" : Player1.ToString(), Environment.NewLine);
      if (Player2 != null) {
        sb.AppendFormat("\t{0}{1}", Player2.ToString(), Environment.NewLine);
      }
      return sb.ToString();
    }
  }
}
