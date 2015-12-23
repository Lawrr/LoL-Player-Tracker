using RiotSharp;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace LoLPlayerTracker {
    public enum GameStatus {
        Idle,
        Loading,
        Loaded,
        NotFound
    }

    public class GameStatusChangedEventArgs : EventArgs {
        public GameStatus Status { get; private set; }
        public CurrentGame CurrentGame { get; private set; }

        public GameStatusChangedEventArgs(GameStatus status, CurrentGame currentGame = null) {
            Status = status;
            CurrentGame = currentGame;
        }
    }

    public class GameStatusTracker {
        public event EventHandler<GameStatusChangedEventArgs> GameStatusChanged;

        private bool LeagueOpened;
        private Timer StatusCheckTimer;

        public GameStatusTracker() {
            LeagueOpened = false;

            // Init timer
            StatusCheckTimer = new Timer();
            StatusCheckTimer.Elapsed += new ElapsedEventHandler(StatusCheckTimer_Elapsed);
            StatusCheckTimer.Interval = 1000;
            StatusCheckTimer.Enabled = true;
        }

        private async void StatusCheckTimer_Elapsed(object sender, ElapsedEventArgs e) {
            // Find league process
            Process[] processes = Process.GetProcessesByName(Program.LeagueProcessName);

            // Check if league opened
            if (processes.Length == 0) {
                // League not opened
                if (LeagueOpened) {
                    LeagueOpened = false;
                    OnGameStatusChanged(GameStatus.Idle);
                }
            } else {
                // League opened
                if (!LeagueOpened) {
                    LeagueOpened = true;
                    OnGameStatusChanged(GameStatus.Loading);
                    CurrentGame currentGame = await GameFetcher.LoadCurrentGameAsync();
                    if (currentGame != null) {
                        OnGameStatusChanged(GameStatus.Loaded, currentGame);
                    } else {
                        OnGameStatusChanged(GameStatus.NotFound);
                    }
                }
            }
        }

        private void OnGameStatusChanged(GameStatus status, CurrentGame currentGame = null) {
            if (GameStatusChanged != null) {
                GameStatusChanged(this, new GameStatusChangedEventArgs(status, currentGame));
            }
        }
    }
}