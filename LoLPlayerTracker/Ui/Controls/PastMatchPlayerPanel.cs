using RiotSharp.MatchEndpoint;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Controls {
    public class PastMatchPlayerPanel : UserControl {
        public static Color PLAYED_WITH_BACK_COLOR = Color.FromArgb(0xFF, 0x91, 0xD5, 0xFF);
        public static Color PLAYED_AGAINST_BACK_COLOR = Color.FromArgb(0xFF, 0xE6, 0xAD, 0xAD);

        public PastMatchPlayerPanel(Participant p, string iconLocation) {
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

            // Create labels
            Label nameLabel = new Label();
            nameLabel.BackColor = Color.White;
            nameLabel.Size = new Size(118, 16);
            nameLabel.Location = new Point(32, 0);

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

            Controls.Add(iconBox);
            Controls.Add(nameLabel);
            Controls.Add(playedWithLabel);
            Controls.Add(playedAgainstLabel);
        }
    }
}
