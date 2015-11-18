using System;
using System.Diagnostics;
using System.Timers;

namespace LoLPlayerTracker {
    public class GameTracker {
        public GameTracker() {
            // Initialise timer
            Timer clientCheckTimer = new Timer();
            clientCheckTimer.Elapsed += new ElapsedEventHandler(OnClientCheck);
            clientCheckTimer.Interval = 1000;
            clientCheckTimer.Enabled = true;
        }

        private void OnClientCheck(object sender, ElapsedEventArgs e) {
            Process[] processes = Process.GetProcessesByName(Program.LeagueProcessName);
            if (processes.Length == 0) {
                Console.WriteLine("League not open");
            } else {
                Console.WriteLine("League open");
            }
        }
    }
}