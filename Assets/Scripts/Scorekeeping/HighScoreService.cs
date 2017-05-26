using System.Collections.Generic;

namespace Assets.Scripts.Scorekeeping
{
    public class HighScoreService
    {
        public List<HighScore> GetScores()
        {
            return new List<HighScore>
            {
                new HighScore("Alomax", 13893),
                new HighScore("ESCH", 567456),
                new HighScore("BOB", 35432),
                new HighScore("DOLE", 7465),
            };
        }
    }
}