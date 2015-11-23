﻿using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Forms.Panels {
    public class CurrentGamePlayerPanel : Panel {

        public Participant Player { get; private set; }

        public CurrentGamePlayerPanel(Participant p, string iconLocation, List<League> leagues) {
            // Set variables
            Player = p;

            // Set panel properties
            Size = new Size(150, 32);

            // Create image
            PictureBox iconBox = new PictureBox();
            iconBox.InitialImage = null;
            iconBox.ErrorImage = null;
            iconBox.SizeMode = PictureBoxSizeMode.StretchImage;
            iconBox.ImageLocation = iconLocation;
            iconBox.Size = new Size(32, 32);
            iconBox.Location = new Point(0, 0);

            // Create label
            Label nameLabel = new Label();
            nameLabel.BackColor = Color.White;
            nameLabel.Cursor = Cursors.Hand;
            nameLabel.Size = new Size(118, 16);
            nameLabel.Location = new Point(32, 0);
            
            Label rankLabel = new Label();
            rankLabel.BackColor = Color.LightGray;
            rankLabel.Cursor = Cursors.Hand;
            rankLabel.Size = new Size(118, 16);
            rankLabel.Location = new Point(32, 16);
            try {
                rankLabel.Text = leagues[0].Tier.ToString() + " " + leagues[0].Entries[0].Division;
            } catch (IndexOutOfRangeException e) {
                rankLabel.Text = "Unranked";
            }

            // Check if current player is self
            if (p.SummonerName != Program.MainForm.GetSummonerName()) {
                // Get num times you've played with the player (minus 1 to exclude current game)
                int numGames = Program.DatabaseManager.FindNumResults(DatabaseManager.PLAYERS_TABLE, p.SummonerId.ToString()) - 1;
                nameLabel.Text = p.SummonerName + " " + numGames;
            } else {
                // Player is self
                nameLabel.Text = p.SummonerName;
            }

            // Add label
            Controls.Add(iconBox);
            Controls.Add(nameLabel);
            Controls.Add(rankLabel);

            // Add on click
            nameLabel.Click += new EventHandler(NameLabel_Click);
        }

        private void NameLabel_Click(object sender, EventArgs e) {
            Program.GameTracker.LoadMatches(Player.SummonerId);
        }

    }
}
