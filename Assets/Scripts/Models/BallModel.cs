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

        public BallController North { get; set; }
        public BallController South { get; set; }
        public BallController East { get; set; }
        public BallController West { get; set; }
    }
}