using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TPNetwork.Messages {
  public class Login : MessageBase {
    public Login() : base("dummy", "LOGIN") {
    }
  }
}
