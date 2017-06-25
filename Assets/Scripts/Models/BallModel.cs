using System;
using Assets.Scripts.Balls;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class BallModel
    {
        public int GridX { get; set; }
        public int GridY { get; set; }
        public float MaxHitpoints { get; set; }

        public BallModel(int gridX, int gridY)
        {
            GridX = gridX;
            GridY = gridY;
        }

        public Sprite IconName { get; set; }
        public int Type { get; set; }
        public bool HasPowerGem { get; set; }
        public float Hitpoints { get; set; }
        public BallMagnitude Magnitude { get; set; }


        public IBallController North { get; set; }
        public IBallController South { get; set; }
        public IBallController East { get; set; }
        public IBallController West { get; set; }

        public void ClearNeighbors()
        {
            if (North != null && North.Model != null) North.Model.South = null;
            if (South != null && South.Model != null) South.Model.North = null;
            if (East != null && East.Model != null) East.Model.West = null;
            if (West != null && West.Model != null) West.Model.East = null;

            North = null;
            South = null;
            East = null;
            West = null;
        }
    }
}