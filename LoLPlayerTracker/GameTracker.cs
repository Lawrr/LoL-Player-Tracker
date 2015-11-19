using System;
using System.Diagnostics;
using System.Timers;

namespace LoLPlayerTracker {
    public class GameTracker {

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
                }
            } else {
                if (!LeagueOpened) {
                    LeagueOpened = true;
                    Program.MainForm.Open();
                }
            }
        }
    }
}