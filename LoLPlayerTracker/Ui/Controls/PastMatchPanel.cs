using RiotSharp.MatchEndpoint;
using RiotSharp.StaticDataEndpoint;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoLPlayerTracker.Ui.Controls {
    public class PastMatchPanel : UserControl {
        public bool Valid { get; private set; } = true;

        public PastMatchPanel(int gameId, RiotSharp.Region region) {
            // Set panel properties
            Size = new Size(661, 200);
            BackColor = Color.White;

            try {
                MatchDetail match = Program.RiotApi.GetMatch(region, gameId);
                List<long> teamIds = new List<long>();
                List<int> teamNumPlayers = new List<int>();

                foreach (Participant p in match.Participants) {
                    // Add new team for the game if it does not exist
                    if (!teamIds.Contains(p.TeamId)) {
                        teamIds.Add(p.TeamId);
                        teamNumPlayers.Add(0);
                    }

                    // Get team index
                    int teamIndex = teamIds.IndexOf(p.TeamId);

                    ChampionStatic championStatic = Program.StaticRiotApi.GetChampion(region,
                                                                                      p.ChampionId,
                                                                                      ChampionData.image);
                    // Get champion icon
                    string iconLocation = "http://ddragon.leagueoflegends.com/cdn/" +
                                          Program.PatchVersion +
                                          "/img/champion/" +
                                          championStatic.Image.Full;

                    PastMatchPlayerPanel playerPanel = new PastMatchPlayerPanel(p, iconLocation);
                    playerPanel.Location = new Point(20 + playerPanel.Width * teamIndex, 20 + playerPanel.Height * teamNumPlayers[teamIndex]);
                    teamNumPlayers[teamIndex]++;

                    Controls.Add(playerPanel);
                }
            } catch (Exception) {
                Valid = false;
            }
        }
    }
}
