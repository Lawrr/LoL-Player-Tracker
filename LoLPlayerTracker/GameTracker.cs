using RiotSharp;
using RiotSharp.ChampionEndpoint;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace LoLPlayerTracker {
    public class GameTracker {

        public static string WAITING_FOR_GAME = "Waiting for game";
        public static string LOADING_MATCH = "Loading match";
        public static string MATCH_LOADED = "Match loaded";
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
                        Program.MainForm.SetCurrentMatchPanel(null);
                    }
                } else {
                    // League opened
                    if (!LeagueOpened) {
                        LeagueOpened = true;

                        // Get current game
                        LoadCurrentMatch();

                        // Change GUI
                        Program.MainForm.ChangeStatus(LOADING_MATCH);
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

        public async void LoadCurrentMatch() {
            // Load api
            RiotApi api = RiotApi.GetInstance(Secrets.RIOT_API_KEY);

            // Get data on what game to load
            string summonerName = Program.MainForm.GetSummonerName();
            Region region = (Region)Enum.Parse(typeof(Region), Program.MainForm.GetRegion().ToLower());
            Platform platform = Program.MainForm.GetPlatform();

            try {
                // Load game data
                Summoner summoner = await api.GetSummonerAsync(region, summonerName);
                CurrentGame game = await api.GetCurrentGameAsync(platform, summoner.Id);

                // Change GUI for current game
                CurrentGamePanel currentMatchPanel = new CurrentGamePanel(game);
                Program.MainForm.SetCurrentMatchPanel(currentMatchPanel);
                Program.MainForm.ChangeStatus(MATCH_LOADED);

                // Add current game to database
                Program.DatabaseManager.AddGame(summoner, game);

            } catch (RiotSharpException e) {
                // TODO stub
                Program.MainForm.ChangeStatus(GAME_NOT_FOUND);
            }

        }
    }
}