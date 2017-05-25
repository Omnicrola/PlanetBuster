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
            Debug.Log("Appending (" + gridX + "," + gridY + ") occupied: " + IsOccupied(gridX, gridY));
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

        private bool IsOccupied(int x, int y)
        {
            return _activeBalls.Any(b => b.Model.GridX == x && b.Model.GridY == y);
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
            var ballModel = ballController.Model;
            if (ballModel != null)
            {
                if (ballModel.North != null) ballModel.North.Model.South = null;
                if (ballModel.South != null) ballModel.South.Model.North = null;
                if (ballModel.East != null) ballModel.East.Model.West = null;
                if (ballModel.West != null) ballModel.West.Model.East = null;

                ballModel.North = null;
                ballModel.South = null;
                ballModel.East = null;
                ballModel.West = null;
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