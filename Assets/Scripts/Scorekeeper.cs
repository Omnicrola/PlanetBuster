using System.Collections.Generic;
using Assets.Scripts.Balls;

namespace Assets.Scripts
{
    public class Scorekeeper
    {
        public string CurrentScore { get; private set; }

        public void ScoreOrphans(List<IBallController> orphanedBalls)
        {
        }

        public void ScoreMatch(IBallPath ballPath)
        {

        }

    }
}