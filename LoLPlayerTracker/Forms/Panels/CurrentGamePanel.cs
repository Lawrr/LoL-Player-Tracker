using LoLPlayerTracker.Forms.Panels;
using RiotSharp.ChampionEndpoint;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.StaticDataEndpoint;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class CurrentGamePanel : Panel {
        public CurrentGamePanel(CurrentGame game) {
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
                CurrentGamePlayerPanel playerPanel = new CurrentGamePlayerPanel(p);
                playerPanel.Location = new Point(155 * teamIndex, 16 * teamNumPlayers[teamIndex]);
                teamNumPlayers[teamIndex]++;

                // Add player panel
                Controls.Add(playerPanel);
            }
        }
    }
}
