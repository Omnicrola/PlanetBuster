using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGrid : IBallGrid
    {
        private const int GRID_WIDTH = 11;
        private const int GRID_HEIGHT = 100;

        private readonly IBallFactory _ballFactory;

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
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    for (int y = GRID_HEIGHT - 1; y <= 0; y--)
                    {
                        if (_ballArray[x, y] != null)
                        {
                            return _ballArray[x, y].Position.y;
                        }
                    }
                }
                return 0f;
            }
        }

        private readonly IBallController[,] _ballArray = new IBallController[GRID_WIDTH, GRID_HEIGHT];
        private List<IBallController> _activeBalls;
        private readonly BallGridPositionHandler _ballPositionHandler;

        public BallGrid(IBallFactory ballFactory, OrphanedBallFinder orphanedBallFinder,
            GameObject ballContainer)
        {
            _ballFactory = ballFactory;
            _activeBalls = new List<IBallController>(GRID_WIDTH * GRID_HEIGHT);
            _matchedBallSetFinder = new MatchedBallSetFinder();
            _ballPositionHandler = new BallGridPositionHandler();
            _orphanedBallFinder = orphanedBallFinder;
            _ballContainer = ballContainer;
        }


        public void Initialize(List<BallLevelData> ballsToCreate)
        {
            foreach (var ballData in ballsToCreate)
            {
                var newBall = _ballFactory.GenerateBall(ballData);

                AddBallToGrid(newBall, new GridPosition(ballData.XPos, ballData.YPos));
            }
        }

        public void Append(IBallController newBall, GridPosition gridPosition)
        {
            AddBallToGrid(newBall, gridPosition);
            HandleMatches(gridPosition);
            HandleOrphanedBalls();
            CheckForWin();
        }

        private void AddBallToGrid(IBallController newBall, GridPosition gridPosition)
        {
            if (_ballArray[gridPosition.X, gridPosition.Y] != null)
            {
                Debug.Log("**** Overlapping at position : " + gridPosition.X + ", " + gridPosition.Y);
            }

            _ballPositionHandler.AppendAt(_ballArray, newBall, gridPosition);

            _activeBalls.Add(newBall);

            Logging.Instance.Log(LogLevel.Debug,
                string.Format("Appending to grid : {0},{1} type: {2}", gridPosition.X, gridPosition.Y,
                    newBall.Model.Type));

            var worldPosition = _ballFactory.GetWorldPositionFromGrid(gridPosition);
            newBall.SetActiveInGrid(gridPosition, worldPosition, _ballContainer.transform);
        }



        private void CheckForWin()
        {
            if (_activeBalls.Count == 0)
            {
                GameManager.Instance.EventBus.Broadcast(new GameOverEventArgs(GameOverCondition.Win));
            }
        }


        private void HandleMatches(GridPosition gridPosition)
        {
            var ballPath = _matchedBallSetFinder.FindPath(gridPosition, _ballArray);
            if (ballPath.Count >= GameConstants.MinimumMatchNumber)
            {
                GameManager.Instance.EventBus.Broadcast(new BallGridMatchArgs(ballPath));
                foreach (var ball in ballPath)
                {
                    Remove(ball.GridPosition);
                }
            }
        }

        public void HandleOrphanedBalls()
        {
            var orphanedBallsPositions = _orphanedBallFinder.Find(_ballArray);

            if (orphanedBallsPositions.Count > 0)
            {
                var orphanedBalls = orphanedBallsPositions.Select(p => _ballArray.GetFromPosition(p)).ToList();
                GameManager.Instance.EventBus.Broadcast(new OrphanedBallsEventArgs(orphanedBalls));
            }
            foreach (var orphanedBall in orphanedBallsPositions)
            {
                Remove(orphanedBall);
            }
        }


        public void Clear()
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                for (int y = 0; y < GRID_HEIGHT; y++)
                {
                    Remove(new GridPosition(x, y));
                }
            }
            _activeBalls.Clear();
        }

        public void Remove(GridPosition gridPosition)
        {
            var controller = _ballArray[gridPosition.X, gridPosition.Y];
            if (controller != null)
            {
                var logMessage = "Removing ball from grid: " + gridPosition.X + ", " + gridPosition.Y + " type:" +
                                 controller.Model.Type;
                Logging.Instance.Log(LogLevel.Debug, logMessage);

                _activeBalls.Remove(controller);
                controller.SetInactiveInGrid();
                _ballFactory.Recycle(controller.gameObject);
                _ballArray[gridPosition.X, gridPosition.Y] = null;
            }
        }
    }
}