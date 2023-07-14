using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


namespace ScoreboardConnectUpdate {
  class Version {
    [JsonPropertyName("latest_version")]
    public string LatestVersion { get; set; }

    [JsonPropertyName("min_supported_version")]
    public string MinSupportedVersion { get; set; }
  }
}
