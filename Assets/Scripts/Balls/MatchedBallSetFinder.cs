using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
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
            var nextSteps = ballController.AllNeighbors;
            foreach (var singleStep in nextSteps)
            {
                CheckForType(ballPath, targetType, singleStep);
            }
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
            if (ballsAlreadyChecked[row].Contains((IBallController)potentialStep))
            {
                return;
            }

            ballsAlreadyChecked[row].Add(potentialStep);

            var ballTypesMatch = potentialStep.Model.Type == targetType;
            var isNotOversized = potentialStep.Model.Magnitude == BallMagnitude.Standard;
            if (ballTypesMatch && isNotOversized)
            {
                ballPath.Append(potentialStep);
                WalkPath(ballPath, potentialStep);
            }
        }
    }
}