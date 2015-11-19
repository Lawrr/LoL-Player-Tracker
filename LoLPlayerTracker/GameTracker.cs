using System;
using System.Diagnostics;
using System.Timers;

namespace LoLPlayerTracker {
    public class GameTracker {

        public static string WAITING_FOR_GAME = "Waiting for game";
        public static string LOADING_MATCH = "Loading match";

        public bool LeagueOpened;

        public GameTracker() {
            // Init variables
            LeagueOpened = false;

            // Initialise timer
            Timer clientCheckTimer = new Timer();
            clientCheckTimer.Elapsed += new ElapsedEventHandler(OnClientCheck);
            clientCheckTimer.Interval = 1000;
            clientCheckTimer.Enabled = true;
        }

        private void OnClientCheck(object sender, ElapsedEventArgs e) {
            Process[] processes = Process.GetProcessesByName(Program.LeagueProcessName);
            if (processes.Length == 0) {
                if (LeagueOpened) {
                    LeagueOpened = false;
                    Program.MainForm.ChangeStatus(WAITING_FOR_GAME);
                }
            } else {
                if (!LeagueOpened) {
                    LeagueOpened = true;
                    Program.MainForm.ChangeStatus(LOADING_MATCH);
                    Program.MainForm.Open();
                }
            }
        }
    }
}