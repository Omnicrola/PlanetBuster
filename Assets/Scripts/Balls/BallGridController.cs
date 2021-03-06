using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;
using UnityEngine;
using Random = System.Random;


namespace Assets.Scripts.Balls
{
    public class BallGridController : IDisposable
    {


        private static readonly Random _random = new Random(55);

        private readonly Random random = new Random();
        private readonly IBallGrid _ballGrid;
        private readonly IBallFactory _ballFactory;
        private readonly IBallGridPositionCalculator _gridPositionCalculator;

        public BallGridController(IBallFactory ballFactory, IBallGrid ballGrid, IBallGridPositionCalculator gridPositionCalculator)
        {
            _ballFactory = ballFactory;
            _ballGrid = ballGrid;
            _gridPositionCalculator = gridPositionCalculator;
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallDestroyByGiantLaserEventArgs>(OnBallDestroyed);
            gameEventBus.Subscribe<BallCollisionEventArgs>(OnBallCollision);
        }

        public float LowestBallPosition { get { return _ballGrid.LowestBallPosition; } }
        public int HeightOfActiveGrid { get { return _ballGrid.HeightOfActiveGrid; } }

        public void Dispose()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallDestroyByGiantLaserEventArgs>(OnBallDestroyed);
            gameEventBus.Unsubscribe<BallCollisionEventArgs>(OnBallCollision);
        }


        public void Clear()
        {
            _ballGrid.Clear();
        }

        public void Generate(ILevelDataController currentLevel)
        {
            _ballGrid.Initialize(currentLevel.GetInitialBallData());
        }

        private void OnBallCollision(BallCollisionEventArgs e)
        {
            if (e.IncomingBall.IsProjectile)
            {
                var magnitude = e.BallInGrid.Magnitude;
                var gridPosition = _gridPositionCalculator.FindGridPosition(e.BallInGrid, magnitude, e.AngleOfImpact);
                _ballGrid.Append(e.IncomingBall, gridPosition);
            }
        }

        private void OnBallDestroyed(BallDestroyByGiantLaserEventArgs obj)
        {
            _ballGrid.Remove(obj.BallController.GridPosition);
            if (_ballGrid.ActiveBalls == 0)
            {
                GameManager.Instance.EventBus.Broadcast(new GameOverEventArgs(GameOverCondition.Win));
            }
            else
            {
                _ballGrid.HandleOrphanedBalls();
            }
        }

        public GameObject GenerateBall(BallType type)
        {
            var newBall = _ballFactory.GenerateBall(type);
            return newBall;
        }

        public BallType GetNextBallType()
        {
            var typesActive = _ballGrid.TypesLeftActive;
            return typesActive[random.Next(typesActive.Length - 1)];
        }

        public void StickBallToCeiling(IBallController incomingBall)
        {
            var gridPosition = _ballFactory.GetGridPositionFromWorldPosition(incomingBall.Position);
            Debug.Log("Sticking ball to ceiling at : " + incomingBall.Position + " -> " + gridPosition);
            _ballGrid.Append(incomingBall, gridPosition);
        }
    }
}