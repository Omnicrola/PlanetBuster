using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class LevelSummary
    {
        public LevelSummary(string levelNumber)
        {
            LevelNumber = levelNumber;
            BallData = new List<BallLevelData>();
        }

        public bool IsLocked { get; set; }
        public string LevelNumber { get; private set; }
        public List<BallLevelData> BallData { get; set; }
    }

    public class BallLevelData
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int BallType { get; set; }
        public bool HasPowerGem { get; set; }
        public BallMagnitude Magnitude { get; set; }

    }

    public enum BallMagnitude
    {
        Standard, Medium, Large, Huge
    }
}