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
                var neighbors = connectedBall.AllNeighbors;
                foreach (var neighbor in neighbors)
                {
                    MarkConnected(neighbor, remainingBalls);
                }
            }
        }
    }
}