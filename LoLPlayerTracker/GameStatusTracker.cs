using LoLPlayerTracker.Ui.Controls;
using RiotSharp;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using RiotSharp.StaticDataEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace LoLPlayerTracker {
    public enum GameStatus {
        WaitingForGame,
        LoadingGame,
        GameLoaded,
        GameNotFound
    }

    public class GameStatusChangedEventArgs : EventArgs {
        public GameStatus Status { get; private set; }

        public GameStatusChangedEventArgs(GameStatus status) {
            Status = status;
        }
    }

    public class GameStatusTracker {
        public event EventHandler<GameStatusChangedEventArgs> GameStatusChanged;

        public bool LeagueOpened;

        public GameStatusTracker() {
            // Init variables
            LeagueOpened = false;

            // Init timer
            Timer clientCheckTimer = new Timer();
            clientCheckTimer.Elapsed += new ElapsedEventHandler(OnClientCheck);
            clientCheckTimer.Interval = 1000;
            clientCheckTimer.Enabled = true;
        }

        public async void OnGameStart() {
            // Change status
            OnGameStatusChanged(GameStatus.LoadingGame);

            // Get data on what game to load
            string summonerName = Program.MainForm.GetSummonerName();
            Region region = Program.MainForm.GetRegion();
            Platform platform = PlatformParser.Parse(region);

            try {
                // Load game data
                Summoner summoner = await Program.RiotApi.GetSummonerAsync(region, summonerName);
                CurrentGame game = await Program.RiotApi.GetCurrentGameAsync(platform, summoner.Id);

                List<ChampionStatic> championStatics = new List<ChampionStatic>();
                List<int> summonerIds = new List<int>();
                foreach (Participant p in game.Participants) {
                    summonerIds.Add((int) p.SummonerId);
                    ChampionStatic championStatic = await Program.StaticRiotApi.GetChampionAsync(region,
                                                                                                 (int) p.ChampionId,
                                                                                                 ChampionData.image);
                    championStatics.Add(championStatic);
                }
                Dictionary<long, List<League>> leagues = await Program.RiotApi.GetLeaguesAsync(region, summonerIds);

                // Add current game to database
                Program.DatabaseManager.AddGame(summoner, game);

                // Change GUI for current game
                CurrentGamePanel currentGamePanel = new CurrentGamePanel(game, championStatics, leagues);
                Program.MainForm.SetCurrentGamePanel(currentGamePanel);
                OnGameStatusChanged(GameStatus.GameLoaded);
            } catch (RiotSharpException e) {
                // TODO stub
                OnGameStatusChanged(GameStatus.GameNotFound);
            }
        }

        public void LoadMatches(long summonerId) {
            List<PastMatchPanel> panels = new List<PastMatchPanel>();
            SQLiteDataReader reader = Program.DatabaseManager.FindKey(DatabaseManager.PLAYERS_TABLE, summonerId.ToString());
            while (reader.Read()) {
                Console.WriteLine("Key: " + reader["Key"] + "\tValue: " + reader["Value"]);
                PastMatchPanel pastMatchPanel = new PastMatchPanel((int) reader["Value"]);
                pastMatchPanel.Location = new System.Drawing.Point(0, pastMatchPanel.Height * panels.Count);
                panels.Add(pastMatchPanel);
            }
            Program.MainForm.SetPastMatches(panels);
        }

        public async void LoadMatches(string summonerName, Region region) {
            Summoner summoner = await Program.RiotApi.GetSummonerAsync(region, summonerName);
            LoadMatches(summoner.Id);
        }

        private async void OnClientCheck(object sender, ElapsedEventArgs e) {
            // Find league process
            Process[] processes = Process.GetProcessesByName(Program.LeagueProcessName);

            // Check if league opened
            if (processes.Length == 0) {
                // League not opened
                if (LeagueOpened) {
                    LeagueOpened = false;

                    // Change GUI
                    OnGameStatusChanged(GameStatus.WaitingForGame);
                    Program.MainForm.SetCurrentGamePanel(null);
                }
            } else {
                // League opened
                if (!LeagueOpened) {
                    LeagueOpened = true;

                    // Get current game
                    OnGameStart();

                    // Open form
                    await PopupDelay();
                    Program.MainForm.ShowActivate();
                }
            }
        }

        private async Task PopupDelay() {
            // Delay for when league opens and goes fullscreen
            // Wait 3 seconds for it to go into fullscreen mode before popping up the form
            await Task.Delay(3000);
        }

        private void OnGameStatusChanged(GameStatus status) {
            if (GameStatusChanged != null) {
                GameStatusChanged(this, new GameStatusChangedEventArgs(status));
            }
        }
    }
}