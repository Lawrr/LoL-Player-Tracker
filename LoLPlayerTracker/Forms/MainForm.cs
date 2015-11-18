using System.Windows.Forms;

namespace LoLPlayerTracker {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            Icon = Properties.Resources.TrayIcon;
            CenterToScreen();
            BringToFront();
        }

        public void Open() {
            Show();
        }
    }
}
