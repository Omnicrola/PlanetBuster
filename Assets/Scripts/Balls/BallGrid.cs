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
        private readonly IBallFactory _ballFactory;

        private readonly List<IBallController> _activeBalls;
        private readonly MatchedBallSetFinder _matchedBallSetFinder;
        private readonly OrphanedBallFinder _orphanedBallFinder;
        private readonly GameObject _ballContainer;
        private readonly BallNeighborLocator _ballNeighborLocator;

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

        public BallGrid(int gridSize, IBallFactory ballFactory, OrphanedBallFinder orphanedBallFinder,
            GameObject ballContainer, BallNeighborLocator ballNeighborLocator)
        {
            _ballFactory = ballFactory;
            Size = gridSize;
            _activeBalls = new List<IBallController>(gridSize * gridSize);
            _matchedBallSetFinder = new MatchedBallSetFinder();
            _orphanedBallFinder = orphanedBallFinder;
            _ballContainer = ballContainer;
            _ballNeighborLocator = ballNeighborLocator;
        }

        public void Append(IBallController newBall, GridPosition gridPosition)
        {
            var ballModel = SetBallPosition(newBall, gridPosition);
            LogAppend(ballModel);

            AddBallToGrid(newBall);
            HandleMatches(newBall);
            HandleOrphanedBalls();
            CheckForWin();
        }

        private BallModel SetBallPosition(IBallController newBall, GridPosition gridPosition)
        {
            if (_activeBalls.Any(b => b.Model.GridX == gridPosition.X && b.Model.GridY == gridPosition.Y))
            {
                Debug.Log("**** Overlapping at position : " + gridPosition.X + ", " + gridPosition.Y);
            }

            var ballModel = newBall.Model;
            ballModel.GridX = gridPosition.X;
            ballModel.GridY = gridPosition.Y;
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
            newBall.Rotation = Quaternion.identity;

            UpdateGrid(newBall, ballModel.GridX, ballModel.GridY);
        }

        private void HandleMatches(IBallController ballController)
        {
            var ballPath = _matchedBallSetFinder.FindPath(ballController);
            if (ballPath.Count >= GameConstants.MinimumMatchNumber)
            {
                GameManager.Instance.EventBus.Broadcast(new BallGridMatchArgs(ballPath));
                foreach (var ball in ballPath)
                {
                    Remove(ball.gameObject);
                }
            }
        }

        public void HandleOrphanedBalls()
        {
            if (_activeBalls.Any())
            {
                List<IBallController> orphanedBalls = _orphanedBallFinder.Find(_activeBalls.ToList());

                if (orphanedBalls.Count > 0)
                {
                    GameManager.Instance.EventBus.Broadcast(new OrphanedBallsEventArgs(orphanedBalls));
                }
                foreach (var orphanedBall in orphanedBalls)
                {
                    Remove(orphanedBall.gameObject);
                }
            }
        }

        private void UpdateGrid(IBallController ballController, int gridX, int gridY)
        {
            _ballNeighborLocator.SetNeighbors(gridX, gridY, ballController, _activeBalls);
        }


        public void Clear()
        {
            var activeCopy = _activeBalls.ToList();
            foreach (var ballController in activeCopy)
            {
                Remove(ballController.gameObject);
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

            _activeBalls.Remove(ballController);
            _ballFactory.Recycle(gameObject);
        }
    }
}