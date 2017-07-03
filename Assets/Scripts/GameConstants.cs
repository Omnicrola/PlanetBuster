using Assets.Scripts.Util;

namespace Assets.Scripts
{
    public class GameConstants
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
            public static readonly string LoadingScene = "LoadingScene";
            public static readonly string LevelEditor = "_LevelEditor";
        }

        public static int ChanceForPowerGems = 20;
        public static float LaserChargePercentPerGem = 0.1f;
        public static float LaserMinimumPercentCharge = 0.3f;
        public static float LaserDrainPercentPerSecond = 0.5f;

        public static class Balls
        {
            public static readonly string PurpleBallSprite = "balls/planet-01";
            public static readonly string RedBallSprite = "balls/planet-04";
            public static readonly string GreenBallSprite = "balls/planet-02";
            public static readonly string BlueBallSprite = "balls/planet-03";

        };

        public class BallHitpoints
        {
            public static float Standard = 1f;
            public static float Medium = 10f;
            public static float Large = 50f;
            public static float Huge = 100f;
        }

        public class Levels
        {
            public static readonly string MetadataFile = "meta.bin";
            public static readonly string ImportExportPath = "Assets/Resources/Levels/";
            public static readonly string ResourcePath = "Levels/";
        }
    }
}