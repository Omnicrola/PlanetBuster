using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGrid
    {
        private readonly BallFactory _ballFactory;
        public EventHandler<BallGridMatchArgs> MatchFound;

        private readonly List<BallController> _activeBalls;
        private readonly MatchedBallSetFinder _matchedBallSetFinder;

        public int Size { private set; get; }
        public int[] TypesLeftActive
        {
            get { return _activeBalls.Select(b => b.Model.Type).Distinct().ToArray(); }
        }

        public BallGrid(int gridSize, BallFactory ballFactory)
        {
            _ballFactory = ballFactory;
            Size = gridSize;
            _activeBalls = new List<BallController>(gridSize * gridSize);
            _matchedBallSetFinder = new MatchedBallSetFinder();
        }

        public void Append(GameObject newBall, int gridX, int gridY)
        {
            var ballController = newBall.GetComponent<BallController>();
            _activeBalls.Add(ballController);

            ballController.IsProjectile = false;
            ballController.transform.position = _ballFactory.GetGridPosition(gridX, gridY);
            ballController.Model.GridX = gridX;
            ballController.Model.GridY = gridY;

            UpdateGrid(gridX, gridY);

            var ballPath = _matchedBallSetFinder.FindPath(ballController);
            if (MatchFound != null)
            {
                MatchFound.Invoke(this, new BallGridMatchArgs(ballPath));
            }
        }

        private void UpdateGrid(int gridX, int gridY)
        {
            var center = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY));

            var north = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY + 1));
            var south = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY - 1));
            var east = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX + 1, gridY));
            var west = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX - 1, gridY));

            if (center != null)
            {
                center.Model.East = east;
                center.Model.West = west;
                center.Model.North = north;
                center.Model.South = south;
            }

            if (east != null) east.Model.West = center;
            if (west != null) west.Model.East = center;
            if (north != null) north.Model.South = center;
            if (south != null) south.Model.North = center;
        }


        public void Clear()
        {
            foreach (var ballController in _activeBalls)
            {
                ClearNeighbors(ballController);
            }
            _activeBalls.Clear();
        }

        private static void ClearNeighbors(BallController ballController)
        {
            if (ballController.Model != null)
            {
                ballController.Model.North = null;
                ballController.Model.South = null;
                ballController.Model.East = null;
                ballController.Model.West = null;
                ballController.Model = null;
            }
        }

        public void Remove(GameObject gameObject)
        {
            var ballController = gameObject.GetComponent<BallController>();
            ClearNeighbors(ballController);
            _activeBalls.Remove(ballController);
            _ballFactory.Recycle(gameObject);
        }
    }
}