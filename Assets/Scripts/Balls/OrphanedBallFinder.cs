using System.Collections.Generic;

namespace Assets.Scripts.Balls
{
    public class OrphanedBallFinder
    {
        public List<IBallController> Find(int ceiling, List<IBallController> activeBalls)
        {
            var orphanedBalls = new List<IBallController>();
            var ballsExamined = new List<IBallController>();
            //            var ballsStillConnected = new List<BallController>();
            foreach (var activeBall in activeBalls)
            {
                if (IsConnected(ceiling, activeBall, ballsExamined))
                {
                    //                    ballsStillConnected.Add(activeBall);
                }
                else
                {
                    orphanedBalls.Add(activeBall);
                }
            }
            return orphanedBalls;
        }

        private bool IsConnected(int ceiling, IBallController activeBall, List<IBallController> ballsExamined)
        {
            if (activeBall == null)
            {
                return false;
            }
            if (ballsExamined.Contains(activeBall))
            {
                return false;
            }
            ballsExamined.Add(activeBall);
            if (activeBall.Model.GridY == ceiling)
            {
                return true;
            }
            if (IsConnected(ceiling, activeBall.Model.East, ballsExamined))
            {
                return true;
            }
            if (IsConnected(ceiling, activeBall.Model.West, ballsExamined))
            {
                return true;
            }
            if (IsConnected(ceiling, activeBall.Model.North, ballsExamined))
            {
                return true;
            }
            if (IsConnected(ceiling, activeBall.Model.South, ballsExamined))
            {
                return true;
            }
            return false;
        }
    }
}