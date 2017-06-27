using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class MatchedBallSetFinder
    {
        private Dictionary<int, List<IBallController>> ballsAlreadyChecked;

        public List<IBallController> FindPath(GridPosition gridPosition, IBallController[,] ballArray)
        {
            ballsAlreadyChecked = new Dictionary<int, List<IBallController>>();
            var ballpath = new List<IBallController>();

            WalkPath(ballpath, gridPosition, ballArray);
            return ballpath;
        }

        private void WalkPath(List<IBallController> ballPath, GridPosition currentPosition, IBallController[,] ballArray)
        {
            var currentBall = ballArray[currentPosition.X, currentPosition.Y];
            ballPath.Add(currentBall);
            var targetType = currentBall.Model.Type;
            var north = ballArray.GetFromPosition(new GridPosition(currentPosition.X, currentPosition.Y + 1));
            var south = ballArray.GetFromPosition(new GridPosition(currentPosition.X, currentPosition.Y - 1));
            var east = ballArray.GetFromPosition(new GridPosition(currentPosition.X + 1, currentPosition.Y));
            var west = ballArray.GetFromPosition(new GridPosition(currentPosition.X - 1, currentPosition.Y));

            CheckForType(ballPath, targetType, currentPosition, north, ballArray);
            CheckForType(ballPath, targetType, currentPosition, south, ballArray);
            CheckForType(ballPath, targetType, currentPosition, east, ballArray);
            CheckForType(ballPath, targetType, currentPosition, west, ballArray);

        }

        private void CheckForType(List<IBallController> ballPath, int targetType, GridPosition currentPosition, IBallController potentialStep, IBallController[,] ballArray)
        {
            if (potentialStep == null)
            {
                return;
            }

            var row = currentPosition.Y;

            if (!ballsAlreadyChecked.ContainsKey(row))
            {
                ballsAlreadyChecked[row] = new List<IBallController>();
            }
            if (ballsAlreadyChecked[row].Contains(potentialStep))
            {
                return;
            }

            ballsAlreadyChecked[row].Add(potentialStep);

            var ballTypesMatch = potentialStep.Model.Type == targetType;
            var isNotOversized = potentialStep.Model.Magnitude == BallMagnitude.Standard;
            if (ballTypesMatch && isNotOversized)
            {
                WalkPath(ballPath, currentPosition, ballArray);
            }
        }
    }
}