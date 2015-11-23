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
    public class GameTracker {

        public static string WAITING_FOR_GAME = "Waiting for game";
        public static string LOADING_GAME = "Loading game";
        public static string GAME_LOADED = "Game loaded";
        public static string GAME_NOT_FOUND = "Game not found";

        delegate void OnClientCheckCallback(object sender, ElapsedEventArgs e);

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

        private async void OnClientCheck(object sender, ElapsedEventArgs e) {
            // Check if we're on the main thread
            if (Program.MainForm.InvokeRequired) {
                // Set delegate to invoke on main thread
                OnClientCheckCallback d = new OnClientCheckCallback(OnClientCheck);
                Program.MainForm.Invoke(d, new object[] { sender, e });
            } else {
                // Find league process
                Process[] processes = Process.GetProcessesByName(Program.LeagueProcessName);

                // Check if league opened
                if (processes.Length == 0) {
                    // League not opened
                    if (LeagueOpened) {
                        LeagueOpened = false;

                        // Change GUI
                        Program.MainForm.ChangeStatus(WAITING_FOR_GAME);
                        Program.MainForm.SetCurrentGamePanel(null);
                    }
                } else {
                    // League opened
                    if (!LeagueOpened) {
                        LeagueOpened = true;

                        // Get current game
                        LoadCurrentGame();

                        // Open form
                        await PopupDelay();
                        Program.MainForm.Open();
                    }
                }
            }
        }

        public async Task PopupDelay() {
            // Delay for when league opens and goes fullscreen
            // Wait 3 seconds for it to go into fullscreen mode before popping up the form
            await Task.Delay(3000);
        }

        public async void LoadCurrentGame() {
            // Change status
            Program.MainForm.ChangeStatus(LOADING_GAME);

            // Get data on what game to load
            string summonerName = Program.MainForm.GetSummonerName();
            Region region = RegionParser.Parse(Program.MainForm.GetRegion());
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
                Dictionary<long, List<League>> leagues = await Program.RiotApi.GetLeaguesAsync(RegionParser.Parse(Program.MainForm.GetRegion()), summonerIds);

                // Add current game to database
                Program.DatabaseManager.AddGame(summoner, game);

                // Change GUI for current game
                CurrentGamePanel currentGamePanel = new CurrentGamePanel(game, championStatics, leagues);
                Program.MainForm.SetCurrentGamePanel(currentGamePanel);
                Program.MainForm.ChangeStatus(GAME_LOADED);

            } catch (RiotSharpException e) {
                // TODO stub
                Program.MainForm.ChangeStatus(GAME_NOT_FOUND);
            }
        }

        public void LoadMatches(long summonerId) {
            SQLiteDataReader reader = Program.DatabaseManager.FindKey(DatabaseManager.PLAYERS_TABLE, summonerId.ToString());
            while (reader.Read()) {
                Console.WriteLine("Key: " + reader["Key"] + "\tValue: " + reader["Value"]);
            }
        }

        public async void LoadMatches(string summonerName, Region region) {
            Summoner summoner = await Program.RiotApi.GetSummonerAsync(region, summonerName);
            LoadMatches(summoner.Id);
        }
    }
}