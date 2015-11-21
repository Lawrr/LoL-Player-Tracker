using LoLPlayerTracker.Forms.Panels;
using RiotSharp;
using RiotSharp.ChampionEndpoint;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.StaticDataEndpoint;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class CurrentGamePanel : Panel {
        public CurrentGamePanel(CurrentGame game, List<ChampionStatic> championStatics) {
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
                                      Program.GameVersion +
                                      "/img/champion/" +
                                      championStatics[game.Participants.IndexOf(p)].Image.Full;
                CurrentGamePlayerPanel playerPanel = new CurrentGamePlayerPanel(p, iconLocation);
                playerPanel.Location = new Point(155 * teamIndex, 36 * teamNumPlayers[teamIndex]);
                teamNumPlayers[teamIndex]++;

                // Add player panel
                Controls.Add(playerPanel);
            }
        }
    }
}
