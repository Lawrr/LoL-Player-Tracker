﻿using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class Tray {

        public NotifyIcon TrayIcon { get; private set; }
        private ContextMenu ContextMenu;

        public Tray() {
            InitializeTray();
        }

        private void InitializeTray() {
            // Context menu
            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add("Open " + Program.ProgramName, TrayIcon_OnOpenClicked);
            ContextMenu.MenuItems.Add("-");
            ContextMenu.MenuItems.Add("Exit", TrayIcon_OnExitClicked);

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
            Program.MainForm.Open();
        }
        
        private void TrayIcon_OnOpenClicked(object sender, EventArgs e) {
            Program.MainForm.Open();
        }

        private void TrayIcon_OnExitClicked(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
