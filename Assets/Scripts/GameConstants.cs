using Assets.Scripts.Util;

namespace Assets.Scripts
{
    internal class GameConstants
    {
        public static readonly LogLevel LoggingLevel = LogLevel.Debug;

        public static readonly int MinimumMatchNumber = 3;

        public static readonly int ScorePerBall = 10;

        public static readonly int BallBonus_4 = 25;
        public static readonly int BallBonus_5 = 50;
        public static readonly int BallBonus_6 = 100;
        public static readonly int BallBonus_7 = 250;
        public static readonly int BallBonus_8 = 500;
        public static readonly int BallBonus_9 = 1000;
        public static readonly int BallBonus_10 = 5000;

        public class SceneNames
        {
            public static readonly string MainPlay = "PlayScene";
            public static readonly string HighScores = "HighScores";
            public static readonly string MainMenu = "MainMenu";
            public static readonly string LevelBrowser = "LevelBrowser";
        }

        public static int ChanceForPowerGems = 20;
        public static float LaserChargePercentPerGem = 0.1f;
        public static float LaserMinimumPercentCharge = 0.3f;
        public static float LaserDrainPercentPerSecond = 0.5f;
    }
}