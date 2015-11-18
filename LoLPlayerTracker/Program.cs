﻿using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    static class Program {
        // The form which is opened by clicking the tray icon
        public static MainForm MainForm { get; private set; }
        public static DatabaseManager DatabaseManager { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new MainForm();
            DatabaseManager = new DatabaseManager("db.sqlite", 3);

            Application.Run(MainForm);
        }
    }
}
