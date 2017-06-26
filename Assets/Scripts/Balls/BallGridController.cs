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
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Subscribe<BallCollisionEventArgs>(OnBallCollision);
        }

        public float LowestBallPosition { get { return _ballGrid.LowestBallPosition; } }

        public void Dispose()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Unsubscribe<BallCollisionEventArgs>(OnBallCollision);
        }


        public void Clear()
        {
            _ballGrid.Clear();
        }

        public void Generate(LevelSummary currentLevel)
        {
            var newBalls = new List<IBallController>();
            foreach (var ballData in currentLevel.BallData)
            {
                var newBall = _ballFactory.GenerateBall(ballData);
                newBalls.Add(newBall);
            }

            _ballGrid.Initialize(newBalls);
        }

        private void OnBallCollision(BallCollisionEventArgs e)
        {
            if (e.IncomingBall.IsProjectile)
            {
                var gridPosition = _gridPositionCalculator.FindGridPosition(e.BallInGrid.Model, e.AngleOfImpact);
                _ballGrid.Append(e.IncomingBall, gridPosition);
            }
        }

        private void OnBallDestroyed(BallDestroyEventArgs obj)
        {
            _ballGrid.Remove(obj.BallController.gameObject);
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