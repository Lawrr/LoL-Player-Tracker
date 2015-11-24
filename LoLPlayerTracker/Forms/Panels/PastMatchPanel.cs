﻿using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class PastMatchPanel : Panel {
        public PastMatchPanel(int gameId) {
            // Set panel properties
            Size = new Size(100, 16);

            Label test = new Label();
            test.Text = gameId.ToString();
            test.Size = new Size(100, 16);
            test.Location = new Point(0, 0);

            Controls.Add(test);
        }
    }
}
