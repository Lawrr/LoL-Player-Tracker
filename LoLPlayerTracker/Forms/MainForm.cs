using System.Configuration;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public partial class MainForm : Form {

        delegate void OpenFormCallback();
        delegate void SetStatusTextCallback(string newStatus);

        public MainForm() {
            InitializeComponent();
            Init();
        }

        public void Init() {
            Icon = Properties.Resources.Icon;
            CenterToScreen();
            BringToFront();

            // Set initial values
            SummonerNameTextBox.Text = ConfigurationManager.AppSettings["SummonerName"];
            UpdateButton.Enabled = false;
            RegionComboBox.SelectedIndex = 3;
            ChangeStatus(GameTracker.WAITING_FOR_GAME);

            for (int i = 0; i < 40; i++) {
                Label l = new Label();
                l.Text = "Hello";
                l.Location = new System.Drawing.Point(43, 20 + (25 * i));
                PastMatchesPanel.Controls.Add(l);
            }
        }

        public void Open() {
            if (InvokeRequired) {
                OpenFormCallback del = new OpenFormCallback(Open);
                Invoke(del, new object[] { });
            } else {
                Show();
                BringToFront();
                Activate();
            }
        }

        public void ChangeStatus(string newStatus) {
            if (StatusLabel.InvokeRequired) {
                SetStatusTextCallback del = new SetStatusTextCallback(ChangeStatus);
                Invoke(del, new object[] { newStatus });
            } else {
                StatusLabel.Text = "Status: ";
                StatusLabel.Text += newStatus;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            Hide();
            e.Cancel = true;
        }

        private void UpdateButton_Click(object sender, System.EventArgs e) {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("SummonerName");
            config.AppSettings.Settings.Add("SummonerName", SummonerNameTextBox.Text);
            config.Save(ConfigurationSaveMode.Modified);
            UpdateButton.Enabled = false;
        }

        private void SummonerNameTextBox_TextChanged(object sender, System.EventArgs e) {
            UpdateButton.Enabled = true;
        }

        private void PastMatchesPanel_MouseEnter(object sender, System.EventArgs e) {
            PastMatchesPanel.Focus();
        }
    }
}
