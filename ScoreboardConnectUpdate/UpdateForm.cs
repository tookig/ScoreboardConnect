using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Globalization;

namespace ScoreboardConnectUpdate {
  public partial class UpdateForm : Form {
    private enum UpdateStatus { UpToDate, ShouldUpdate, MustUpdate };

    private readonly HttpClient m_client = new HttpClient();
    private readonly string m_url = "https://www.scoreboardlive.se/assets/connect/version.json";
    private readonly double m_currentVersion = 1.1;

    private CancellationTokenSource m_cancellationToken;
    private object m_cancelLock = new object();

    public UpdateForm() {
      InitializeComponent();
    }

    private void buttonCancel_Click(object sender, EventArgs e) {
      lock (m_cancelLock) {
        if ((m_cancellationToken != null) && !m_cancellationToken.IsCancellationRequested) {
          m_cancellationToken.Cancel();
        }
      }
    }

    private async Task<Version> GetVersion(string url) {
      lock (m_cancelLock) {
        if (m_cancellationToken != null) {
          return null;
        }
        m_cancellationToken = new CancellationTokenSource();
      }

      try {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        HttpResponseMessage response = await m_client.SendAsync(request, m_cancellationToken.Token);

        JsonSerializerOptions options = new JsonSerializerOptions {
          IgnoreNullValues = true
        };
        return await JsonSerializer.DeserializeAsync<Version>(await response.Content.ReadAsStreamAsync(), options) as Version;
      } catch (TaskCanceledException error) {
        return null;
      } catch (Exception error) {
        return null;
      } finally {
        lock (m_cancelLock) {
          m_cancellationToken = null;
        }
      }
    }

    private UpdateStatus ApplicationNeedsUpdate(Version remoteVersion) {
      double remoteRequiredVersion;
      if (double.TryParse(remoteVersion.LatestVersion, NumberStyles.Any, CultureInfo.InvariantCulture, out remoteRequiredVersion) && (remoteRequiredVersion > m_currentVersion)) {
        return UpdateStatus.MustUpdate;
      }

      double remoteCurrentVersion;
      if (double.TryParse(remoteVersion.LatestVersion, NumberStyles.Any, CultureInfo.InvariantCulture, out remoteCurrentVersion) && (remoteCurrentVersion > m_currentVersion)) {
        return UpdateStatus.ShouldUpdate;
      }

      return UpdateStatus.UpToDate;
    }

    private void MustUpdate() {
      UpdateMessageBox umb = new UpdateMessageBox(true);
      umb.ShowDialog(this);
      Close();
    }

    private void ShouldUpdate() {
      UpdateMessageBox umb = new UpdateMessageBox(false);
      umb.ShowDialog(this);
      Close();
    }

    private void NoAction() {
      Close();
    }

    private async void UpdateForm_Shown(object sender, EventArgs e) {
      Version version = await GetVersion(m_url);
      if (version == null) {
        NoAction();
        return;
      }

      UpdateStatus updateStatus = ApplicationNeedsUpdate(version);
      switch (updateStatus) {
        case UpdateStatus.MustUpdate:
          MustUpdate();
          break;
        case UpdateStatus.ShouldUpdate:
          ShouldUpdate();
          break;
        case UpdateStatus.UpToDate:
          NoAction();
          break;
      }
    }
  }
}
