using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class MatchedBallSetFinder
    {
        private Dictionary<int, List<IBallController>> ballsAlreadyChecked;

        public IBallPath FindPath(IBallController ballController)
        {
            ballsAlreadyChecked = new Dictionary<int, List<IBallController>>();
            var ballpath = new BallPath();
            ballpath.Append(ballController);
            WalkPath(ballpath, ballController);
            return ballpath;
        }

        private void WalkPath(BallPath ballPath, IBallController ballController)
        {
            var targetType = ballController.Model.Type;
            CheckForType(ballPath, targetType, ballController.Model.North);
            CheckForType(ballPath, targetType, ballController.Model.South);
            CheckForType(ballPath, targetType, ballController.Model.East);
            CheckForType(ballPath, targetType, ballController.Model.West);
        }

        private void CheckForType(BallPath ballPath, int targetType, IBallController potentialStep)
        {
            if (potentialStep == null)
            {
                return;
            }
            var row = potentialStep.Model.GridY;

            if (!ballsAlreadyChecked.ContainsKey(row))
            {
                ballsAlreadyChecked[row] = new List<IBallController>();
            }
            if (ballsAlreadyChecked[row].Contains(potentialStep))
            {
                return;
            }

            ballsAlreadyChecked[row].Add(potentialStep);

            if (potentialStep.Model.Type == targetType)
            {
                ballPath.Append(potentialStep);
                WalkPath(ballPath, potentialStep);
            }
        }
    }
}