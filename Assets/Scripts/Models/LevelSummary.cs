using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class LevelSummary
    {
        public LevelSummary(int ordinalNumber, string levelNumber)
        {
            OrdinalNumber = ordinalNumber;
            LevelNumber = levelNumber;
            BallData = new List<BallLevelData>();
        }

        public bool IsLocked { get; set; }
        public int OrdinalNumber { get; private set; }
        public string LevelNumber { get; private set; }
        public List<BallLevelData> BallData { get; set; }
    }

    [Serializable]
    public class BallLevelData
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int BallType { get; set; }
        public bool HasPowerGem { get; set; }
        public BallMagnitude Magnitude { get; set; }
    }

    [Serializable]
    public enum BallType
    {
        Purple,
        Red,
        Green,
        Blue
    }

    [Serializable]
    public enum BallMagnitude
    {
        Standard,
        Medium,
        Large,
        Huge
    }
}