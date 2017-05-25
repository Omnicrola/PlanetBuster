using Assets.Scripts.Balls;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class BallModel
    {
        public int GridX { get; set; }
        public int GridY { get; set; }

        public BallModel(int gridX, int gridY)
        {
            GridX = gridX;
            GridY = gridY;
        }

        public string IconName { get; set; }
        public int Type { get; set; }

        public IBallController North { get; set; }
        public IBallController South { get; set; }
        public IBallController East { get; set; }
        public IBallController West { get; set; }
    }
}