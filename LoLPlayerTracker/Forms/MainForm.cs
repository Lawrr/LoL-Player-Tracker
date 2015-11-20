using RiotSharp;
using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public partial class MainForm : Form {

        public CurrentGamePanel CurrentGamePanel;

        public MainForm() {
            InitializeComponent();
            Init();
        }

        public void Init() {
            Icon = Properties.Resources.Icon;
            CenterToScreen();
            BringToFront();

            // Summoner name
            SummonerNameTextBox.Text = ConfigManager.Get("SummonerName");

            // Region
            string savedRegion = ConfigManager.Get("Region");
            if (RegionComboBox.Items.IndexOf(savedRegion) != -1) {
                RegionComboBox.SelectedIndex = RegionComboBox.Items.IndexOf(savedRegion);
            } else {
                RegionComboBox.SelectedIndex = 0;
            }

            // Status
            ChangeStatus(GameTracker.WAITING_FOR_GAME);

            for (int i = 0; i < 40; i++) {
                Label l = new Label();
                l.Text = "Hello";
                l.Location = new System.Drawing.Point(43, 20 + (25 * i));
                PastMatchesPanel.Controls.Add(l);
            }
        }

        public void Open() {
            Show();
            BringToFront();
            Activate();
        }

        public void SetCurrentMatchPanel(CurrentGamePanel panel) {
            // Set panel location
            panel.Location = new System.Drawing.Point(14, 26);
            
            // Remove old panel and add new one
            CurrentGameGroupBox.Controls.Remove(CurrentGamePanel);
            CurrentGameGroupBox.Controls.Add(panel);
            CurrentGamePanel = panel;
        }

        public void ChangeStatus(string newStatus) {
            StatusLabel.Text = newStatus;
        }

        public string GetSummonerName() {
            return SummonerNameTextBox.Text;
        }

        public string GetRegion() {
            return RegionComboBox.SelectedItem.ToString();
        }

        public Platform GetPlatform() {
            return Platform.OC1;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            Hide();
            e.Cancel = true;
        }

        private void SummonerNameTextBox_TextChanged(object sender, EventArgs e) {
            ConfigManager.Set("SummonerName", SummonerNameTextBox.Text);
        }

        private void PastMatchesPanel_MouseEnter(object sender, System.EventArgs e) {
            PastMatchesPanel.Focus();
        }

        private void RegionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ConfigManager.Set("Region", RegionComboBox.SelectedItem.ToString());
        }
    }
}
