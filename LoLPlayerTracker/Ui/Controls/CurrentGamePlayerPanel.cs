using RiotSharp;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.LeagueEndpoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Controls {
    public class CurrentGamePlayerPanel : UserControl {
        public static Color PLAYED_WITH_BACK_COLOR = Color.FromArgb(0xFF, 0x91, 0xD5, 0xFF);
        public static Color PLAYED_AGAINST_BACK_COLOR = Color.FromArgb(0xFF, 0xE6, 0xAD, 0xAD);

        private Participant Player;

        public CurrentGamePlayerPanel(Participant p, RiotSharp.Region region, bool isSelf, string iconLocation, List<League> leagues) {
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
            rankLabel.Size = new Size(118, 16);
            rankLabel.Location = new Point(32, 16);

            Label playedWithLabel = new Label();
            playedWithLabel.BackColor = PLAYED_WITH_BACK_COLOR;
            playedWithLabel.Size = new Size(26, 16);
            playedWithLabel.Location = new Point(98, 16);
            playedWithLabel.TextAlign = ContentAlignment.MiddleCenter;

            Label playedAgainstLabel = new Label();
            playedAgainstLabel.BackColor = PLAYED_AGAINST_BACK_COLOR;
            playedAgainstLabel.Size = new Size(26, 16);
            playedAgainstLabel.Location = new Point(124, 16);
            playedAgainstLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Set name text
            nameLabel.Text = p.SummonerName;

            // Set rank text
            bool rankFound = false;
            foreach (League l in leagues) {
                if (l.Queue == RiotSharp.Queue.RankedSolo5x5) {
                    String rankedTier = l.Tier.ToString();
                    String rankedDivision = l.Entries[0].Division;
                    rankLabel.Text = String.Format("{0} {1}", rankedTier, rankedDivision);
                    rankFound = true;
                    break;
                }
            }
            if (!rankFound) {
                rankLabel.Text = "Unranked";
            }

            // Set win/loss text if not self
            if (!isSelf) {
                // Get num times you've played with the player (minus 1 to exclude current game)
                int numGames = Program.DatabaseManager.FindNumResults(DatabaseManager.PLAYERS_TABLE, p.SummonerId.ToString()) - 1;
                playedWithLabel.Text = numGames.ToString();
                playedAgainstLabel.Text = "0";

                Controls.Add(playedWithLabel);
                Controls.Add(playedAgainstLabel);
            }

            // Add other controls
            Controls.Add(iconBox);
            Controls.Add(nameLabel);
            Controls.Add(rankLabel);

            // Add on click
            iconBox.Click += new EventHandler(Panel_Click);
            nameLabel.Click += new EventHandler(Panel_Click);
            rankLabel.Click += new EventHandler(Panel_Click);
            playedWithLabel.Click += new EventHandler(Panel_Click);
            playedAgainstLabel.Click += new EventHandler(Panel_Click);
        }

        private void Panel_Click(object sender, EventArgs e) {
            Program.MainForm.SetPastMatches(GameFetcher.GetPastMatchPanels(Program.MainForm.GetRegion(), Player.SummonerId));
        }
    }
}
