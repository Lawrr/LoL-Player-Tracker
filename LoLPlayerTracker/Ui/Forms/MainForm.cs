using LoLPlayerTracker.Ui.Controls;
using RiotSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Forms {
    public partial class MainForm : Form {

        public CurrentGamePanel CurrentGamePanel;

        public MainForm() {
            InitializeComponent();
            InitForm();

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
                    case GameStatus.WaitingForGame:
                        statusString = "Waiting for game";
                        break;
                    case GameStatus.LoadingGame:
                        statusString = "Loading game";
                        break;
                    case GameStatus.GameLoaded:
                        statusString = "Game loaded";
                        break;
                    case GameStatus.GameNotFound:
                        statusString = "Game not found";
                        break;
                }
                StatusLabel.Text = statusString;
            });
        }

        public string GetSummonerName() {
            return SummonerNameTextBox.Text;
        }

        public Region GetRegion() {
            return RegionParser.Parse(RegionComboBox.SelectedItem.ToString());
        }

        private void InitForm() {
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

            // Status
            SetStatus(GameStatus.WaitingForGame);
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

        private void LoadGameButton_Click(object sender, EventArgs e) {
            Program.GameTracker.OnGameStart();
        }

        private void SearchButton_Click(object sender, EventArgs e) {
            Program.GameTracker.LoadMatches(SearchTextBox.Text, GetRegion());
        }

        private void GameTracker_GameStatusChanged(object sender, GameStatusChangedEventArgs e) {
            SetStatus(e.Status);
        }

    }
}
