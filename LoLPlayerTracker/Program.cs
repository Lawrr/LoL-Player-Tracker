using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    static class Program {
        // Constants
        public static string ProgramName { get; private set; } = "LoL Player Tracker";
        public static string LeagueProcessName { get; private set; } = "League of Legends";
        public static string ClientProcessName { get; private set; } = "LolClient";

        // Global Objects
        public static MainForm MainForm { get; private set; }
        public static Tray Tray { get; private set; }
        public static DatabaseManager DatabaseManager { get; private set; }
        public static GameTracker GameTracker { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Init objects
            MainForm = new MainForm();
            Tray = new Tray();
            DatabaseManager = new DatabaseManager("db.sqlite", 3);
            GameTracker = new GameTracker();

            // Start application
            Application.Run(MainForm);
        }
    }
}
