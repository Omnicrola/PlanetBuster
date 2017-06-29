using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
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

        public void Generate(LevelSummary currentLevel)
        {
            _ballGrid.Initialize(currentLevel.BallData);
        }

        private void OnBallCollision(BallCollisionEventArgs e)
        {
            if (e.IncomingBall.IsProjectile)
            {
                var magnitude = e.BallInGrid.Model.Magnitude;
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

        public IBallController GenerateBall(int gridX, int gridY)
        {
            var ballController = _ballFactory.GenerateBall(gridX, gridY);
            bool chanceForPower = _random.Next(100) < GameConstants.ChanceForPowerGems;
            if (chanceForPower)
            {
                ballController.Model.HasPowerGem = true;
            }
            return ballController;
        }

        public GameObject GenerateBall(int type)
        {
            var newBall = _ballFactory.GenerateBall(type);
            return newBall;
        }

        public int GetNextBallType()
        {
            var typesActive = _ballGrid.TypesLeftActive;
            return typesActive[random.Next(typesActive.Length - 1)];
        }
    }
}