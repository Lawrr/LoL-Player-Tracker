using RiotSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public partial class MainForm : Form {

        public delegate void OnOpenCallback();
        public delegate void OnSetCurrentGamePanelCallback(CurrentGamePanel panel);
        public delegate void OnSetPastMatchesCallback(List<PastMatchPanel> panels);
        public delegate void OnChangeStatusCallback(string newStatus);

        public CurrentGamePanel CurrentGamePanel;

        public MainForm() {
            InitializeComponent();
            InitForm();
        }

        public void Open() {
            // Check if we're on the main thread
            if (InvokeRequired) {
                // Set delegate to invoke on main thread
                OnOpenCallback d = new OnOpenCallback(Open);
                Invoke(d, new object[] { });
            } else {
                Show();
                BringToFront();
                Activate();
            }
        }

        public void SetCurrentGamePanel(CurrentGamePanel panel) {
            // Check if we're on the main thread
            if (InvokeRequired) {
                // Set delegate to invoke on main thread
                OnSetCurrentGamePanelCallback d = new OnSetCurrentGamePanelCallback(SetCurrentGamePanel);
                Invoke(d, new object[] { panel });
            } else {
                // Remove old game panel and add new one
                CurrentGameGroupBox.Controls.Remove(CurrentGamePanel);
                if (panel != null) {
                    panel.Location = new System.Drawing.Point(14, 26);
                    CurrentGameGroupBox.Controls.Add(panel);
                }
                CurrentGamePanel = panel;
            }
        }

        public void SetPastMatches(List<PastMatchPanel> panels) {
            // Check if we're on the main thread
            if (InvokeRequired) {
                // Set delegate to invoke on main thread
                OnSetPastMatchesCallback d = new OnSetPastMatchesCallback(SetPastMatches);
                Invoke(d, new object[] { panels });
            } else {
                // Remove currently displayed past matches and add new ones
                PastMatchesPanel.Controls.Clear();
                foreach (PastMatchPanel p in panels) {
                    PastMatchesPanel.Controls.Add(p);
                }
            }
        }

        public void ChangeStatus(string newStatus) {
            // Check if we're on the main thread
            if (InvokeRequired) {
                // Set delegate to invoke on main thread
                OnChangeStatusCallback d = new OnChangeStatusCallback(ChangeStatus);
                Invoke(d, new object[] { newStatus });
            } else {
                StatusLabel.Text = newStatus;
            }
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
            ChangeStatus(GameTracker.WAITING_FOR_GAME);
        }

        private void MainForm_FormLoad(object sender, EventArgs e) {
            CenterToScreen();
            BringToFront();

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

    }
}
