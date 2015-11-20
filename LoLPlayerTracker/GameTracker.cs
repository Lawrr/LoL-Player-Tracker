using RiotSharp;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace LoLPlayerTracker {
    public class GameTracker {

        public static string WAITING_FOR_GAME = "Waiting for game";
        public static string LOADING_MATCH = "Loading match";

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

        private void OnClientCheck(object sender, ElapsedEventArgs e) {
            if (Program.MainForm.InvokeRequired) {
                OnClientCheckCallback d = new OnClientCheckCallback(OnClientCheck);
                Program.MainForm.Invoke(d, new object[] { sender, e });
            } else {
                Process[] processes = Process.GetProcessesByName(Program.LeagueProcessName);
                if (processes.Length != 0) {
                    // League not opened
                    if (LeagueOpened) {
                        LeagueOpened = false;
                        Program.MainForm.ChangeStatus(WAITING_FOR_GAME);
                    }
                } else {
                    // League opened
                    if (!LeagueOpened) {
                        LeagueOpened = true;
                        Program.MainForm.ChangeStatus(LOADING_MATCH);
                        Program.MainForm.Open();
                        string summonerName = Program.MainForm.GetSummonerName();
                        Region region = (Region)Enum.Parse(typeof(Region), Program.MainForm.GetRegion().ToLower());
                        Platform platform = Program.MainForm.GetPlatform();
                        LoadCurrentMatch(summonerName, region, platform);
                    }
                }
            }
        }

        public async void LoadCurrentMatch(string summonerName, Region region, Platform platform) {
            RiotApi api = RiotApi.GetInstance(Secrets.RIOT_API_KEY);
            try {
                Task<Summoner> summonerTask = api.GetSummonerAsync(region, "BestPudgeUganda");
                Summoner summoner = await summonerTask;
                Task<CurrentGame> gameTask = api.GetCurrentGameAsync(platform, summoner.Id);
                CurrentGame game = await gameTask;
                foreach (Participant p in game.Participants) {
                    Console.WriteLine(p.SummonerName + " " + p.TeamId);
                }
                CurrentMatchPanel currentMatchPanel = new CurrentMatchPanel(game);
                Program.MainForm.SetCurrentMatchPanel(currentMatchPanel);
            } catch (RiotSharpException e) {
                // TODO stub
                Console.WriteLine("Not ingame");
            }
        }
    }
}