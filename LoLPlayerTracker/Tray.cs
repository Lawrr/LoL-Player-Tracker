using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class Tray {
        public NotifyIcon TrayIcon { get; private set; }

        private ContextMenu ContextMenu;

        public Tray() {
            InitTray();
        }

        private void InitTray() {
            // Context menu
            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add("Open " + Program.ProgramName, TrayIcon_OpenClick);
            ContextMenu.MenuItems.Add("-");
            ContextMenu.MenuItems.Add("Exit", TrayIcon_ExitClick);

            // Tray icon
            TrayIcon = new NotifyIcon();
            TrayIcon.Text = Program.ProgramName;
            TrayIcon.Icon = Properties.Resources.Icon;
            TrayIcon.ContextMenu = ContextMenu;
            TrayIcon.Visible = true;

            // Event handlers
            TrayIcon.DoubleClick += new EventHandler(TrayIcon_DoubleClick);
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e) {
            Program.MainForm.ShowActivate();
        }
        
        private void TrayIcon_OpenClick(object sender, EventArgs e) {
            Program.MainForm.ShowActivate();
        }

        private void TrayIcon_ExitClick(object sender, EventArgs e) {
            Application.ExitThread();
        }
    }
}
