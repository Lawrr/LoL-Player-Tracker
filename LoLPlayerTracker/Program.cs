using LoLPlayerTracker.Exceptions;
using LoLPlayerTracker.Ui.Forms;
using RiotSharp;
using System;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    static class Program {
        // Constants
        public const string ProgramName = "LoL Player Tracker";
        public const string ProgramVersion = "0.9.0";
        public const string LeagueProcessName = "League of Legends";
        public const string ClientProcessName = "LolClient";

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
            DatabaseManager = new DatabaseManager("db.sqlite", 3);
            GameTracker = new GameTracker();
            MainForm = new MainForm();
            Tray = new Tray();

            RiotApi = RiotApi.GetInstance(Secrets.RIOT_API_KEY);
            StaticRiotApi = StaticRiotApi.GetInstance(Secrets.RIOT_API_KEY);

            // Get patch version for the region
            Region region;
            try {
                region = RegionParser.Parse(ConfigManager.Get("Region"));
            } catch (RegionNotFoundException e) {
                region = Region.na;
                ConfigManager.Set("Region", region.ToString());
            }
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
