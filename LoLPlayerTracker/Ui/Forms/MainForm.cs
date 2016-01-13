using LoLPlayerTracker.Ui.Controls;
using RiotSharp;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Forms {
    public partial class MainForm : Form {
        private CurrentGamePanel CurrentGamePanel;
        // Name which is compared to when loading a game
        private string BufferedSummonerName;
        // Region which is compared to when loading a game
        private Region BufferedRegion;

        public MainForm() {
            InitializeComponent();

            // Icon
            Icon = Properties.Resources.Icon;

            // Summoner name
            SummonerNameTextBox.Text = ConfigManager.Get("SummonerName");

            // Region
            string savedRegion = ConfigManager.Get("Region");
            if (RegionComboBox.Items.IndexOf(savedRegion.ToUpper()) != -1) {
                RegionComboBox.SelectedIndex = RegionComboBox.Items.IndexOf(savedRegion.ToUpper());
            } else {
                RegionComboBox.SelectedIndex = 0;
            }

            // Set buffered details
            BufferedSummonerName = GetSummonerName();
            BufferedRegion = RegionParser.Parse(savedRegion);

            // Status
            SetStatus(GameStatus.Idle);

            // Event handlers
            Program.GameTracker.GameStatusChanged += new EventHandler<GameStatusChangedEventArgs>(GameTracker_GameStatusChanged);
        }

        public void SetCurrentGamePanel(CurrentGamePanel panel) {
            this.InvokeSafe(() => {
                // Remove old game panel and add new one
                CurrentGameGroupBox.Controls.Remove(CurrentGamePanel);
                if (panel != null) {
                    panel.Location = new System.Drawing.Point(14, 26);
                    CurrentGameGroupBox.Controls.Add(panel);
                }
                CurrentGamePanel = panel;
            });
        }

        public void SetPastMatches(List<PastMatchPanel> panels) {
            this.InvokeSafe(() => {
                // Remove currently displayed past matches and add new ones
                PastMatchesPanel.Controls.Clear();
                foreach (PastMatchPanel p in panels) {
                    PastMatchesPanel.Controls.Add(p);
                }
            });
        }

        public void SetStatus(GameStatus status) {
            this.InvokeSafe(() => {
                // Set status text
                string statusString = "";
                switch (status) {
                    case GameStatus.Idle:
                        statusString = "Waiting for game";
                        break;
                    case GameStatus.Loading:
                        statusString = "Loading game";
                        break;
                    case GameStatus.Loaded:
                        statusString = "Game loaded";
                        break;
                    case GameStatus.NotFound:
                        statusString = "Game not found";
                        break;
                }
                StatusLabel.Text = statusString;
            });
        }

        public string GetSummonerName() {
            if (InvokeRequired) {
                return (string)Invoke(new Func<string>(GetSummonerName));
            } else {
                return SummonerNameTextBox.Text;
            }
        }

        public Region GetRegion() {
            if (InvokeRequired) {
                return (Region)Invoke(new Func<Region>(GetRegion));
            } else {
                return RegionParser.Parse(RegionComboBox.SelectedItem.ToString());
            }
        }

        private void MainForm_FormLoad(object sender, EventArgs e) {
            CenterToScreen();
            BringToFront();
            Activate();

            // Set focus to the past matches panel
            ActiveControl = PastMatchesPanel;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            Hide();
            e.Cancel = true;
        }

        private void SummonerNameTextBox_TextChanged(object sender, EventArgs e) {
            ConfigManager.Set("SummonerName", GetSummonerName());
        }

        private void PastMatchPanel_Click(object sender, EventArgs e) {
            PastMatchesPanel.Focus();
        }

        private void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ConfigManager.Set("Region", GetRegion().ToString());
        }

        private async void LoadGameButton_Click(object sender, EventArgs e) {
            GameTracker_GameStatusChanged(this, new GameStatusChangedEventArgs(GameStatus.Loading));
            CurrentGame currentGame = await GameFetcher.LoadCurrentGameAsync();
            if (currentGame != null) {
                GameTracker_GameStatusChanged(this, new GameStatusChangedEventArgs(GameStatus.Loaded, currentGame));
            } else {
                GameTracker_GameStatusChanged(this, new GameStatusChangedEventArgs(GameStatus.NotFound));   
            }
        }

        private async void SearchButton_Click(object sender, EventArgs e) {
            Summoner summoner = await Program.RiotApi.GetSummonerAsync(GetRegion(), SearchTextBox.Text);
            SetPastMatches(GameFetcher.GetPastMatchPanels(GetRegion(), summoner.Id));
        }

        private async void GameTracker_GameStatusChanged(object sender, GameStatusChangedEventArgs e) {
            SetStatus(e.Status);

            // Additional things
            switch (e.Status) {
                case GameStatus.Idle:
                    SetCurrentGamePanel(null);
                    break;
                case GameStatus.Loading:
                    BufferedSummonerName = GetSummonerName();
                    BufferedRegion = GetRegion();
                    await Delay(3000);
                    this.ShowActivate();
                    break;
                case GameStatus.Loaded:
                    if (e.CurrentGame != null) {
                        Task<CurrentGamePanel> currentGamePanelTask = GameFetcher.GetCurrentGamePanelAsync(e.CurrentGame, BufferedSummonerName, BufferedRegion);
                        Program.DatabaseManager.AddGame(e.CurrentGame, BufferedSummonerName, BufferedRegion);
                        SetCurrentGamePanel(await currentGamePanelTask);
                    }
                    break;
                case GameStatus.NotFound:
                    SetCurrentGamePanel(null);
                    break;
            }
        }

        private async Task Delay(int ms) {
            await Task.Delay(ms);
        }
    }
}
