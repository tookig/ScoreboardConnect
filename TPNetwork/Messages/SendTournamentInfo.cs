using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TPNetwork.Messages {
  public class SendTournamentInfo : MessageBase {
    public SendTournamentInfo(string password) : base(password, "SENDTOURNAMENTINFO") {
    }
  }
}
