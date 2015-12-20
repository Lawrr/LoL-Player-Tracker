using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using RiotSharp.StaticDataEndpoint;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Controls {
    public class CurrentGamePanel : Panel {

        public CurrentGamePanel(CurrentGame game, List<ChampionStatic> championStatics, Dictionary<long, List<League>> leagues) {
            InitPanel(game, championStatics, leagues);
        }

        private void InitPanel(CurrentGame game, List<ChampionStatic> championStatics, Dictionary<long, List<League>> leagues) {
            // Set panel properties
            Size = new Size(320, 180);

            // Set team info
            List<long> teamIds = new List<long>();
            List<int> teamNumPlayers = new List<int>();

            foreach (Participant p in game.Participants) {
                // Add new team for the game if it does not exist
                if (!teamIds.Contains(p.TeamId)) {
                    teamIds.Add(p.TeamId);
                    teamNumPlayers.Add(0);
                }

                // Get team index
                int teamIndex = teamIds.IndexOf(p.TeamId);

                // Create player panel
                string iconLocation = "http://ddragon.leagueoflegends.com/cdn/" +
                                      Program.PatchVersion +
                                      "/img/champion/" +
                                      championStatics[game.Participants.IndexOf(p)].Image.Full;
                CurrentGamePlayerPanel playerPanel;
                try {
                    playerPanel = new CurrentGamePlayerPanel(p, iconLocation, leagues[p.SummonerId]);
                } catch (KeyNotFoundException e) {
                    // No ranked stats
                    playerPanel = new CurrentGamePlayerPanel(p, iconLocation, new List<League>());
                }
                playerPanel.Location = new Point(155 * teamIndex, 36 * teamNumPlayers[teamIndex]);
                teamNumPlayers[teamIndex]++;

                // Add player panel
                Controls.Add(playerPanel);
            }
        }

    }
}
