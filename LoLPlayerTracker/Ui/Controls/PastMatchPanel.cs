using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Controls {
    public class PastMatchPanel : Panel {

        public PastMatchPanel(int gameId) {
            InitPanel(gameId);
        }

        private void InitPanel(int gameId) {
            // Set panel properties
            Size = new Size(661, 100);
            BackColor = Color.White;

            Label test = new Label();
            test.Text = gameId.ToString();
            test.Size = new Size(100, 16);
            test.Location = new Point(0, 0);

            Controls.Add(test);
        }

    }
}
