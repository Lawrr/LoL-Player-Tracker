using RiotSharp.CurrentGameEndpoint;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public class CurrentMatchPanel : Panel {
        public CurrentMatchPanel(CurrentGame currentGame) {
            Size = new Size(320, 180);
            List<long> teamIds = new List<long>();
            List<int> teamNumPlayers = new List<int>();
            foreach (Participant p in currentGame.Participants) {
                if (!teamIds.Contains(p.TeamId)) {
                    teamIds.Add(p.TeamId);
                    teamNumPlayers.Add(0);
                }
                int teamIndex = teamIds.IndexOf(p.TeamId);
                Label nameLabel = new Label();
                nameLabel.Text = p.SummonerName;
                nameLabel.Location = new System.Drawing.Point(150 * teamIndex, 25 * teamNumPlayers[teamIndex]++);
                Controls.Add(nameLabel);
            }
        }
    }
}
