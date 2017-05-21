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

        public GameObject North { get; set; }
        public GameObject South { get; set; }
        public GameObject East { get; set; }
        public GameObject West { get; set; }
    }
}