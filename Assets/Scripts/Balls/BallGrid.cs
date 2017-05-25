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
        public event EventHandler<BallGridMatchArgs> MatchFound;
        public event EventHandler<OrphanedBallsEventArgs> OrphansFound;

        private readonly List<IBallController> _activeBalls;
        private readonly MatchedBallSetFinder _matchedBallSetFinder;
        private readonly OrphanedBallFinder _orphanedBallFinder;

        public int Size { private set; get; }
        public int[] TypesLeftActive
        {
            get { return _activeBalls.Select(b => b.Model.Type).Distinct().ToArray(); }
        }

        public BallGrid(int gridSize, BallFactory ballFactory)
        {
            _ballFactory = ballFactory;
            Size = gridSize;
            _activeBalls = new List<IBallController>(gridSize * gridSize);
            _matchedBallSetFinder = new MatchedBallSetFinder();
            _orphanedBallFinder = new OrphanedBallFinder();
        }

        public void Append(GameObject newBall, int gridX, int gridY)
        {
            var ballModel = newBall.GetComponent<IBallController>().Model;
            ballModel.GridX = gridX;
            ballModel.GridY = gridY;
            var ballController = AddBallToGrid(newBall);
            HandleMatches(ballController);
            HandleOrphanedBalls();
        }

        public void Initialize(List<GameObject> newBalls)
        {
            foreach (var newBall in newBalls)
            {
                AddBallToGrid(newBall);
            }
        }


        private IBallController AddBallToGrid(GameObject newBall)
        {
            var ballModel = newBall.GetComponent<IBallController>().Model;
            var ballController = newBall.GetComponent<IBallController>();
            _activeBalls.Add(ballController);


            ballController.IsProjectile = false;
            ballController.transform.position = _ballFactory.GetGridPosition(ballModel.GridX, ballModel.GridY);

            UpdateGrid(ballModel.GridX, ballModel.GridY);
            return ballController;
        }

        private void HandleMatches(IBallController ballController)
        {
            var ballPath = _matchedBallSetFinder.FindPath(ballController);
            if (MatchFound != null)
            {
                MatchFound.Invoke(this, new BallGridMatchArgs(ballPath));
            }
        }

        private void HandleOrphanedBalls()
        {
            int ceiling = _activeBalls.Max(b => b.Model.GridY);
            List<IBallController> orphanedBalls = _orphanedBallFinder.Find(ceiling, _activeBalls.ToList());
            foreach (var ballController in orphanedBalls)
            {
                ballController.IsFalling = true;
            }

            if (orphanedBalls.Count > 0 && OrphansFound != null)
            {
                OrphansFound.Invoke(this, new OrphanedBallsEventArgs(orphanedBalls));
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

        private static void ClearNeighbors(IBallController ballController)
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