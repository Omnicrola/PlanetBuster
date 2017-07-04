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
}