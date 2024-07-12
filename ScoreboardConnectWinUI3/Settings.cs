using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace ScoreboardConnectWinUI3 {
  [Serializable]
  public class Settings : ISerializable {
    public int UnitID { get; set; }
    public string URL { get; set; }
    public bool UseCountryInsteadOfClub { get; set; }
    public Dictionary<int, int> SelectedTournaments { get; private set; }
    public Dictionary<int, Dictionary<int, string>> CourtSetup { get; private set; }
    internal ConnectLogger.LogLevels LogLevel { get; set; }

    public Settings() {
      SelectedTournaments = new Dictionary<int, int>();
      CourtSetup = new Dictionary<int, Dictionary<int, string>>();
      LogLevel = ConnectLogger.LogLevels.Info;
    }

    public Settings(string url) : this() {
      URL = url;
    }

    public void Save(string filename) {
      FileStream stream = new FileStream(filename, FileMode.Create);
      using (stream) {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
        formatter.Serialize(stream, this);
      }
    }

    public static Settings Load(string filename) {
      if (!File.Exists(filename)) {
        return new Settings("https://www.scoreboardlive.se");
      }
      FileStream stream = new FileStream(filename, FileMode.Open);
      using (stream) {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
        return (Settings)formatter.Deserialize(stream);
      }
    }

    #region Serialization
    private const int c_version = 7;
    private const string c_id_version = "Settings.version";
    private const string c_id_unitid = "Settings.unitid";
    private const string c_id_url = "Settings.url";
    private const string c_id_selected_tournaments = "Settings.selected_tournaments";
    private const string c_id_court_setup = "Settings.court_setup";
    private const string c_id_use_country_instead_of_club = "Settings.use_country_instead_of_club";
    private const string c_id_log_level = "Settings.log_level";

    protected Settings(SerializationInfo info, StreamingContext context) : this() {
      int version = info.GetInt32(c_id_version);
      UnitID = info.GetInt32(c_id_unitid);
      URL = info.GetString(c_id_url);
      if (version > 2) {
        SelectedTournaments = (Dictionary<int, int>)info.GetValue(c_id_selected_tournaments, typeof(Dictionary<int, int>));
      }
      if (version > 4) {
        CourtSetup = (Dictionary<int, Dictionary<int, string>>)info.GetValue(c_id_court_setup, typeof(Dictionary<int, Dictionary<int, string>>));
      }
      if (version > 5) {
        UseCountryInsteadOfClub = info.GetBoolean(c_id_use_country_instead_of_club);
      }
      if (version > 6) {
        LogLevel = (ConnectLogger.LogLevels)info.GetValue(c_id_log_level, typeof(ConnectLogger.LogLevels));
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue(c_id_version, c_version);
      info.AddValue(c_id_unitid, UnitID);
      info.AddValue(c_id_url, URL);
      info.AddValue(c_id_selected_tournaments, SelectedTournaments);
      info.AddValue(c_id_court_setup, CourtSetup);
      info.AddValue(c_id_use_country_instead_of_club, UseCountryInsteadOfClub);
      info.AddValue(c_id_log_level, LogLevel);
    }
    #endregion
  }
}

