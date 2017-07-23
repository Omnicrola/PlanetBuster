using Assets.Scripts.Models;

namespace Assets.Scripts.Balls
{
    public class BallGridPositionHandler
    {
        public void AppendAt(IBallController[,] ballArray, IBallController newBall, GridPosition gridPosition)
        {
            ballArray[gridPosition.X, gridPosition.Y] = newBall;
            var ballMagnitude = newBall.Magnitude;
            if (ballMagnitude == BallMagnitude.Medium)
            {
                AddExtraPositionsForMediumMagnitude(ballArray, newBall, gridPosition);
            }
            else if (ballMagnitude == BallMagnitude.Large)
            {
                AddExtraPositionsForMediumMagnitude(ballArray, newBall, gridPosition);
                AddExtraPositionsForLargeMagnitude(ballArray, newBall, gridPosition);
            }
            else if (ballMagnitude == BallMagnitude.Huge)
            {
                AddExtraPositionsForMediumMagnitude(ballArray, newBall, gridPosition);
                AddExtraPositionsForLargeMagnitude(ballArray, newBall, gridPosition);
                AddExtraPositionsForHugeMagnitude(ballArray, newBall, gridPosition);
            }
        }

        private void AddExtraPositionsForHugeMagnitude(IBallController[,] ballArray, IBallController newBall,
            GridPosition gridPosition)
        {
            ballArray[gridPosition.X + 3, gridPosition.Y] = newBall;
            ballArray[gridPosition.X + 3, gridPosition.Y + 1] = newBall;
            ballArray[gridPosition.X + 3, gridPosition.Y + 2] = newBall;
            ballArray[gridPosition.X + 3, gridPosition.Y + 3] = newBall;
            ballArray[gridPosition.X + 2, gridPosition.Y + 3] = newBall;
            ballArray[gridPosition.X + 1, gridPosition.Y + 3] = newBall;
            ballArray[gridPosition.X + 1, gridPosition.Y + 3] = newBall;
        }

        private void AddExtraPositionsForLargeMagnitude(IBallController[,] ballArray, IBallController newBall,
            GridPosition gridPosition)
        {
            ballArray[gridPosition.X + 2, gridPosition.Y] = newBall;
            ballArray[gridPosition.X + 2, gridPosition.Y + 1] = newBall;
            ballArray[gridPosition.X + 2, gridPosition.Y + 2] = newBall;
            ballArray[gridPosition.X + 1, gridPosition.Y + 2] = newBall;
            ballArray[gridPosition.X, gridPosition.Y + 2] = newBall;
        }

        private static void AddExtraPositionsForMediumMagnitude(IBallController[,] ballArray, IBallController newBall,
            GridPosition gridPosition)
        {
            ballArray[gridPosition.X + 1, gridPosition.Y] = newBall;
            ballArray[gridPosition.X, gridPosition.Y + 1] = newBall;
            ballArray[gridPosition.X + 1, gridPosition.Y + 1] = newBall;
        }
    }
}