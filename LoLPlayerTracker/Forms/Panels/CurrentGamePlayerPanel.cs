﻿using RiotSharp.CurrentGameEndpoint;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Forms.Panels {
    public class CurrentGamePlayerPanel : Panel {
        
        public CurrentGamePlayerPanel(Participant p) {
            // Set panel properties
            Size = new Size(150, 14);

            // Create label
            Label nameLabel = new Label();
            nameLabel.BackColor = Color.White;
            nameLabel.Cursor = Cursors.Hand;
            nameLabel.Size = new Size(150, 14);
            nameLabel.Location = new Point(0, 0);

            // Check if current player is self
            if (p.SummonerName != Program.MainForm.GetSummonerName()) {
                // Get num times you've played with the player (minus 1 to exclude current game)
                int numGames = Program.DatabaseManager.FindNumResults("Players", p.SummonerId.ToString()) - 1;
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