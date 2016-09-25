using LoLPlayerTracker.Ui.Controls;
using RiotSharp;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using RiotSharp.StaticDataEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace LoLPlayerTracker {
    public static class GameFetcher {
        public static async Task<CurrentGame> LoadCurrentGameAsync() {
            CurrentGame currentGame;
            
            // Get data on game to load
            try {
                string summonerName = Program.MainForm.GetSummonerName();
                Region region = Program.MainForm.GetRegion();
                Platform platform = PlatformParser.Parse(region);

                // Load current game data
                Summoner summoner = await Program.RiotApi.GetSummonerAsync(region, summonerName);
                currentGame = await Program.RiotApi.GetCurrentGameAsync(platform, summoner.Id);
            } catch (RiotSharpException) {
                // Current game not found
                currentGame = null;
            }
            return currentGame;
        }

        public static async Task<CurrentGamePanel> GetCurrentGamePanelAsync(CurrentGame currentGame, string summonerName, Region region) {
            CurrentGamePanel currentGamePanel;

            if (currentGame != null) {
                // Get champion data
                List<ChampionStatic> championStatics = new List<ChampionStatic>();
                List<int> summonerIds = new List<int>();
                foreach (Participant p in currentGame.Participants) {
                    summonerIds.Add((int)p.SummonerId);
                    ChampionStatic championStatic = await Program.StaticRiotApi.GetChampionAsync(region,
                                                                                                 (int)p.ChampionId,
                                                                                                 ChampionData.image);
                    championStatics.Add(championStatic);
                }
                // Get league data
                Dictionary<long, List<League>> leagues = await Program.RiotApi.GetLeaguesAsync(region, summonerIds);

                currentGamePanel = new CurrentGamePanel(currentGame, summonerName, region, championStatics, leagues);
            } else {
                currentGamePanel = null;
            }
            return currentGamePanel;
        }

        public static List<PastMatchPanel> GetPastMatchPanels(Region region, long summonerId) {
            List<PastMatchPanel> panels = new List<PastMatchPanel>();
            SQLiteDataReader reader = Program.DatabaseManager.FindKey(DatabaseManager.PLAYERS_TABLE, summonerId.ToString());
            while (reader.Read()) {
                Console.WriteLine("Key: " + reader["Key"] + "\tValue: " + reader["Value"]);
                PastMatchPanel pastMatchPanel = new PastMatchPanel((int) reader["Value"], Program.MainForm.GetRegion());
                pastMatchPanel.Location = new System.Drawing.Point(0, pastMatchPanel.Height * panels.Count);

                if (pastMatchPanel.Valid) {
                    panels.Add(pastMatchPanel);
                }
            }
            return panels;
        }
    }
}
