using RiotSharp;
using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    static class Program {
        // Constants
        public static string ProgramName { get; private set; } = "LoL Player Tracker";
        public static string ProgramVersion { get; private set; } = "0.9.0";
        public static string LeagueProcessName { get; private set; } = "League of Legends";
        public static string ClientProcessName { get; private set; } = "LolClient";

        // Static objects
        public static MainForm MainForm { get; private set; }
        public static Tray Tray { get; private set; }
        public static DatabaseManager DatabaseManager { get; private set; }
        public static GameTracker GameTracker { get; private set; }
        public static RiotApi RiotApi { get; private set; }
        public static StaticRiotApi StaticRiotApi { get; private set; }
        public static string PatchVersion { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Init static objects
            MainForm = new MainForm();
            Tray = new Tray();
            DatabaseManager = new DatabaseManager("db.sqlite", 3);
            GameTracker = new GameTracker();

            RiotApi = RiotApi.GetInstance(Secrets.RIOT_API_KEY);
            StaticRiotApi = StaticRiotApi.GetInstance(Secrets.RIOT_API_KEY);

            Region region = RegionParser.Parse(ConfigManager.Get("Region"));
            PatchVersion = StaticRiotApi.GetVersions(region)[0];

            // Event handlers
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            // Start application
            Application.Run(MainForm);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e) {
            // Make it so the icon doesnt stay when exiting the program
            Tray.TrayIcon.Visible = false;
        }

    }
}
