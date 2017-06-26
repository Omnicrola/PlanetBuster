using System;
using System.Collections.Generic;
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

    }
}