using System.Configuration;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            Init();
        }

        public void Init() {
            Icon = Properties.Resources.Icon;
            CenterToScreen();
            BringToFront();

            SummonerNameTextBox.Text = ConfigurationManager.AppSettings["SummonerName"];
            UpdateButton.Enabled = false;
        }

        public void Open() {
            Show();
            Activate();
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
    }
}
