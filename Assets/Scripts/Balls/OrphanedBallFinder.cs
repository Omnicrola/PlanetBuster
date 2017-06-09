using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Balls
{
    public class OrphanedBallFinder
    {
        private static readonly int CEILING = 0;
        public List<IBallController> Find(List<IBallController> allActiveBalls)
        {
            var ballsOnCeiling = allActiveBalls.Where(b => b.Model.GridY == CEILING).ToList();
            foreach (var ballOnCeiling in ballsOnCeiling)
            {
                MarkConnected(ballOnCeiling, allActiveBalls);
            }

            return allActiveBalls;
        }

        private void MarkConnected(IBallController connectedBall, List<IBallController> remainingBalls)
        {
            if (remainingBalls.Contains(connectedBall))
            {
                remainingBalls.Remove(connectedBall);
                MarkConnected(connectedBall.Model.North, remainingBalls);
                MarkConnected(connectedBall.Model.South, remainingBalls);
                MarkConnected(connectedBall.Model.East, remainingBalls);
                MarkConnected(connectedBall.Model.West, remainingBalls);
            }
        }
    }
}