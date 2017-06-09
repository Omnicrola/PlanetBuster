using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGrid : IBallGrid
    {
        private readonly BallFactory _ballFactory;

        private readonly List<IBallController> _activeBalls;
        private readonly MatchedBallSetFinder _matchedBallSetFinder;
        private readonly OrphanedBallFinder _orphanedBallFinder;
        private readonly GameObject _ballContainer;

        public int Size { private set; get; }

        public int[] TypesLeftActive
        {
            get { return _activeBalls.Select(b => b.Model.Type).Distinct().ToArray(); }
        }

        public int ActiveBalls
        {
            get { return _activeBalls.Count; }
        }

        public float LowestBallPosition
        {
            get
            {
                if (_activeBalls.Any())
                {
                    return _activeBalls.OrderBy(b => b.Position.y).First().Position.y;
                }
                return 0f;
            }
        }

        public BallGrid(int gridSize, BallFactory ballFactory, OrphanedBallFinder orphanedBallFinder,
            GameObject ballContainer)
        {
            _ballFactory = ballFactory;
            Size = gridSize;
            _activeBalls = new List<IBallController>(gridSize * gridSize);
            _matchedBallSetFinder = new MatchedBallSetFinder();
            _orphanedBallFinder = orphanedBallFinder;
            _ballContainer = ballContainer;

        }

        public void Append(IBallController newBall, int gridX, int gridY)
        {
            var ballModel = SetBallPosition(newBall, gridX, gridY);
            LogAppend(ballModel);

            AddBallToGrid(newBall);
            HandleMatches(newBall);
            HandleOrphanedBalls();
            CheckForWin();
        }

        private BallModel SetBallPosition(IBallController newBall, int gridX, int gridY)
        {
            if (_activeBalls.Any(b => b.Model.GridX == gridX && b.Model.GridY == gridY))
            {
                Debug.Log("**** Overlapping at position : " + gridX + ", " + gridY);
            }

            var ballModel = newBall.Model;
            ballModel.GridX = gridX;
            ballModel.GridY = gridY;
            return ballModel;
        }

        private void LogAppend(BallModel ballModel)
        {
            Logging.Instance.Log(LogLevel.Debug,
                string.Format("Appending to grid : {0},{1} type: {2}", ballModel.GridX, ballModel.GridY, ballModel.Type));
        }

        private void CheckForWin()
        {
            if (_activeBalls.Count == 0)
            {
                GameManager.Instance.EventBus.Broadcast(new GameOverEventArgs(GameOverCondition.Win));
            }
        }

        public void Initialize(List<IBallController> newBalls)
        {
            foreach (var newBall in newBalls)
            {
                AddBallToGrid(newBall);
            }
        }


        private void AddBallToGrid(IBallController newBall)
        {
            var ballModel = newBall.Model;
            _activeBalls.Add(newBall);

            newBall.gameObject.transform.SetParent(_ballContainer.transform);
            newBall.IsProjectile = false;
            newBall.Position = _ballFactory.GetGridPosition(ballModel.GridX, ballModel.GridY);

            UpdateGrid(newBall, ballModel.GridX, ballModel.GridY);
        }

        private void HandleMatches(IBallController ballController)
        {
            var ballPath = _matchedBallSetFinder.FindPath(ballController);
            if (ballPath.Count >= GameConstants.MinimumMatchNumber)
            {
                GameManager.Instance.EventBus.Broadcast(new BallGridMatchArgs(ballPath));
            }
        }

        private void HandleOrphanedBalls()
        {
            if (_activeBalls.Any())
            {
                List<IBallController> orphanedBalls = _orphanedBallFinder.Find(_activeBalls.ToList());

                if (orphanedBalls.Count > 0)
                {
                    GameManager.Instance.EventBus.Broadcast(new OrphanedBallsEventArgs(orphanedBalls));
                }
            }
        }

        private void UpdateGrid(IBallController ballController, int gridX, int gridY)
        {
            var center = ballController;

            var north = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY + 1));
            var south = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX, gridY - 1));
            var east = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX + 1, gridY));
            var west = _activeBalls.FirstOrDefault(b => b.IsAtGrid(gridX - 1, gridY));

            center.Model.East = east;
            center.Model.West = west;
            center.Model.North = north;
            center.Model.South = south;

            if (east != null) east.Model.West = center;
            if (west != null) west.Model.East = center;
            if (north != null) north.Model.South = center;
            if (south != null) south.Model.North = center;
        }


        public void Clear()
        {
            var activeCopy = _activeBalls.ToList();
            foreach (var ballController in activeCopy)
            {
                Remove(ballController.gameObject);
            }
        }

        private static void ClearNeighbors(IBallController ballController)
        {
            var ballModel = ballController.Model;
            if (ballModel != null)
            {
                ballModel.ClearNeighbors();
                ballController.Model = null;
            }
        }

        public void Remove(GameObject gameObject)
        {
            var ballController = gameObject.GetComponent<BallController>();
            ballController.gameObject.transform.SetParent(null);
            var ballModel = ballController.Model;
            var logMessage = "Removing ball from grid: " + ballModel.GridX + ", " + ballModel.GridY + " type:" +
                             ballModel.Type;
            Logging.Instance.Log(LogLevel.Debug, logMessage);

            ClearNeighbors(ballController);

            _activeBalls.Remove(ballController);
            _ballFactory.Recycle(gameObject);
        }

    }
}