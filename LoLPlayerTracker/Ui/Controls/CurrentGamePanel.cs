using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using RiotSharp.StaticDataEndpoint;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Controls {
    public class CurrentGamePanel : Panel {
        public CurrentGamePanel(CurrentGame currentGame, string summonerName, List<ChampionStatic> championStatics, Dictionary<long, List<League>> leagues) {
            // Set panel properties
            Size = new Size(320, 180);

            // Set team info
            List<long> teamIds = new List<long>();
            List<int> teamNumPlayers = new List<int>();

            foreach (Participant p in currentGame.Participants) {
                // Add new team for the game if it does not exist
                if (!teamIds.Contains(p.TeamId)) {
                    teamIds.Add(p.TeamId);
                    teamNumPlayers.Add(0);
                }

                // Get team index
                int teamIndex = teamIds.IndexOf(p.TeamId);

                // Get champion icon
                string iconLocation = "http://ddragon.leagueoflegends.com/cdn/" +
                                      Program.PatchVersion +
                                      "/img/champion/" +
                                      championStatics[currentGame.Participants.IndexOf(p)].Image.Full;

                // Check if participant is self
                bool isSelf = false;
                if (p.SummonerName.Replace(" ", "").ToLower() == Program.MainForm.GetSummonerName().Replace(" ", "").ToLower()) {
                    isSelf = true;
                }

                // Get ranked stats
                List<League> rankedStats = new List<League>();
                if (leagues.ContainsKey(p.SummonerId)) {
                    rankedStats = leagues[p.SummonerId];
                }

                // Create player panel
                CurrentGamePlayerPanel playerPanel = new CurrentGamePlayerPanel(p, isSelf, iconLocation, rankedStats);
                playerPanel.Location = new Point(155 * teamIndex, 36 * teamNumPlayers[teamIndex]);
                teamNumPlayers[teamIndex]++;

                // Add player panel
                Controls.Add(playerPanel);
            }
        }
    }
}
