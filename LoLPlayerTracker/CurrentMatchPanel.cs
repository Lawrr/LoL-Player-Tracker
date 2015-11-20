using RiotSharp.CurrentGameEndpoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class CurrentMatchPanel : Panel {
        public CurrentMatchPanel(CurrentGame currentGame) {
            // Set panel size
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

                // Create label
                Label nameLabel = new Label();
                nameLabel.Size = new Size(150, 25);
                nameLabel.Location = new System.Drawing.Point(150 * teamIndex, 25 * teamNumPlayers[teamIndex]++);

                // Check if current player is self
                if (p.SummonerName != Program.MainForm.GetSummonerName()) {
                    // Get num times you've played with the player
                    int numGames = Program.DatabaseManager.FindNumResults("Players", p.SummonerId.ToString());
                    nameLabel.Text = p.SummonerName + " " + numGames;
                } else {
                    // Player is self
                    nameLabel.Text = p.SummonerName;
                }

                // Add label
                Controls.Add(nameLabel);
            }
        }
    }
}
