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
        public int XPos { get; private set; }
        public int YPos { get; private set; }
        public int BallType { get; private set; }
        public bool HasPowerGem { get; private set; }

        public BallLevelData(int xPos, int yPos, int ballType, bool hasPowerGem)
        {
            XPos = xPos;
            YPos = yPos;
            BallType = ballType;
            HasPowerGem = hasPowerGem;
        }
    }
}