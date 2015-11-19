using System.Windows.Forms;

namespace LoLPlayerTracker {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            Icon = Properties.Resources.Icon;
            CenterToScreen();
            BringToFront();
        }

        public void Open() {
            Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            Hide();
            e.Cancel = true;
        }
    }
}
