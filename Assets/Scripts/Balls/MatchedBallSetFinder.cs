using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class MatchedBallSetFinder
    {
        private List<GridPosition> _positionsAlreadyChecked;

        public List<IBallController> FindPath(GridPosition initialPositionToCheckFrom, IBallController[,] ballArray)
        {
            _positionsAlreadyChecked = new List<GridPosition>();
            _positionsAlreadyChecked.Add(initialPositionToCheckFrom);
            var matchedBalls = new List<IBallController>();

            int targetType = ballArray.GetFromPosition(initialPositionToCheckFrom).Model.Type;
            AddBallAtPosition(matchedBalls, targetType, initialPositionToCheckFrom, ballArray);
            return matchedBalls;
        }

        private void AddBallAtPosition(List<IBallController> matchedBalls, int targetType, GridPosition currentPosition,
            IBallController[,] ballGrid)
        {
            var currentBall = ballGrid[currentPosition.X, currentPosition.Y];
            matchedBalls.Add(currentBall);
            var northPosition = new GridPosition(currentPosition.X, currentPosition.Y + 1);
            var southPosition = new GridPosition(currentPosition.X, currentPosition.Y - 1);
            var eastPosition = new GridPosition(currentPosition.X + 1, currentPosition.Y);
            var westPosition = new GridPosition(currentPosition.X - 1, currentPosition.Y);

            CheckPositionForTypeRecursive(matchedBalls, ballGrid, targetType, northPosition);
            CheckPositionForTypeRecursive(matchedBalls, ballGrid, targetType, southPosition);
            CheckPositionForTypeRecursive(matchedBalls, ballGrid, targetType, eastPosition);
            CheckPositionForTypeRecursive(matchedBalls, ballGrid, targetType, westPosition);
        }

        private void CheckPositionForTypeRecursive(List<IBallController> matchedBalls, IBallController[,] ballGrid,
            int targetType, GridPosition potentialNextPosition)
        {
            if (_positionsAlreadyChecked.Contains(potentialNextPosition))
            {
                return;
            }
            else
            {
                _positionsAlreadyChecked.Add(potentialNextPosition);
            }

            var potentialNextBall = ballGrid.GetFromPosition(potentialNextPosition);
            if (potentialNextBall == null)
            {
                return;
            }
            else
            {
                var typesAreAMatch = targetType == potentialNextBall.Model.Type;
                var isNotOversized = potentialNextBall.Model.Magnitude == BallMagnitude.Standard;
                if (typesAreAMatch && isNotOversized)
                {
                    AddBallAtPosition(matchedBalls, targetType, potentialNextPosition, ballGrid);
                }
            }

        }

    }
}