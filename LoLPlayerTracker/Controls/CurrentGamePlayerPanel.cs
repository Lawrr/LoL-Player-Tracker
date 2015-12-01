using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class CurrentGamePlayerPanel : Panel {

        public static Color WIN_BACK_COLOR = Color.FromArgb(0xFF, 0xB2, 0xE6, 0xAD);
        public static Color LOSS_BACK_COLOR = Color.FromArgb(0xFF, 0xE6, 0xAD, 0xAD);

        public Participant Player { get; private set; }

        public CurrentGamePlayerPanel(Participant p, string iconLocation, List<League> leagues) {
            // Set variables
            Player = p;

            // Set panel properties
            Size = new Size(150, 32);
            Cursor = Cursors.Hand;

            // Create image
            PictureBox iconBox = new PictureBox();
            iconBox.InitialImage = null;
            iconBox.ErrorImage = null;
            iconBox.SizeMode = PictureBoxSizeMode.StretchImage;
            iconBox.ImageLocation = iconLocation;
            iconBox.Size = new Size(32, 32);
            iconBox.Location = new Point(0, 0);

            // Create labels
            Label nameLabel = new Label();
            nameLabel.BackColor = Color.White;
            nameLabel.Size = new Size(118, 16);
            nameLabel.Location = new Point(32, 0);
            
            Label rankLabel = new Label();
            rankLabel.BackColor = Color.LightGray;
            rankLabel.Size = new Size(66, 16);
            rankLabel.Location = new Point(32, 16);

            Label winLabel = new Label();
            winLabel.BackColor = WIN_BACK_COLOR;
            winLabel.Size = new Size(26, 16);
            winLabel.Location = new Point(98, 16);
            winLabel.TextAlign = ContentAlignment.MiddleCenter;

            Label lossLabel = new Label();
            lossLabel.BackColor = LOSS_BACK_COLOR;
            lossLabel.Size = new Size(26, 16);
            lossLabel.Location = new Point(124, 16);
            lossLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Set name text
            nameLabel.Text = p.SummonerName;

            // Set rank text
            try {
                // Try to get ranked stats
                String rankedTier = leagues[0].Tier.ToString();
                String rankedDivision = leagues[0].Entries[0].Division;
                rankLabel.Text = String.Format("{0} {1}", rankedTier, rankedDivision);
            } catch (Exception e) {
                rankLabel.Text = "Unranked";
            }

            // Set win/loss text if not self
            if (p.SummonerName.Replace(" ", "").ToLower() != Program.MainForm.GetSummonerName().Replace(" ", "").ToLower()) {
                // Get num times you've played with the player (minus 1 to exclude current game)
                int numGames = Program.DatabaseManager.FindNumResults(DatabaseManager.PLAYERS_TABLE, p.SummonerId.ToString()) - 1;
                winLabel.Text = numGames.ToString();
                lossLabel.Text = "0";
            }

            // Add label
            Controls.Add(iconBox);
            Controls.Add(nameLabel);
            Controls.Add(rankLabel);
            Controls.Add(winLabel);
            Controls.Add(lossLabel);

            // Add on click
            iconBox.Click += new EventHandler(Panel_Click);
            nameLabel.Click += new EventHandler(Panel_Click);
            rankLabel.Click += new EventHandler(Panel_Click);
            winLabel.Click += new EventHandler(Panel_Click);
            lossLabel.Click += new EventHandler(Panel_Click);
        }

        private void Panel_Click(object sender, EventArgs e) {
            Program.GameTracker.LoadMatches(Player.SummonerId);
        }

    }
}
