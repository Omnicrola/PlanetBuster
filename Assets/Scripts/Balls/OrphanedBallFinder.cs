using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Balls
{
    public class OrphanedBallFinder
    {
        private static readonly int CEILING = 0;

        public List<GridPosition> Find(IBallController[,] allActiveBalls)
        {
            var ballsLeft = Listify(allActiveBalls);
            var gridWidth = allActiveBalls.GetLength(0);
            for (int x = 0; x < gridWidth; x++)
            {
                MarkConnected(new GridPosition(x, 0), allActiveBalls, ballsLeft);
            }

            return ballsLeft;
        }

        private List<GridPosition> Listify(IBallController[,] allActiveBalls)
        {
            var ballControllers = new List<GridPosition>(allActiveBalls.Length);
            for (int x = 0; x < allActiveBalls.GetLength(0); x++)
            {
                for (int y = 0; y < allActiveBalls.GetLength(1); y++)
                {
                    if (allActiveBalls[x, y] != null)
                    {
                        ballControllers.Add(new GridPosition(x, y));
                    }
                }
            }
            return ballControllers;
        }

        private void MarkConnected(GridPosition connectedBall, IBallController[,] allActiveBalls, List<GridPosition> remainingBalls)
        {
            if (allActiveBalls.GetFromPosition(connectedBall) == null)
            {
                return;
            }

            if (remainingBalls.Contains(connectedBall))
            {
                remainingBalls.Remove(connectedBall);
                var north = new GridPosition(connectedBall.X, connectedBall.Y + 1);
                var south = new GridPosition(connectedBall.X, connectedBall.Y - 1);
                var east = new GridPosition(connectedBall.X + 1, connectedBall.Y);
                var west = new GridPosition(connectedBall.X - 1, connectedBall.Y);

                MarkConnected(north, allActiveBalls, remainingBalls);
                MarkConnected(south, allActiveBalls, remainingBalls);
                MarkConnected(east, allActiveBalls, remainingBalls);
                MarkConnected(west, allActiveBalls, remainingBalls);
            }
        }
    }
}