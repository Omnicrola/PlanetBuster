using System;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class BallLevelData
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int BallType { get; set; }
        public bool HasPowerGem { get; set; }
        public BallMagnitude Magnitude { get; set; }
    }
}