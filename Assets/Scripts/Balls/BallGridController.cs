using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
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

        public BallGridController(IBallFactory ballFactory, IBallGrid ballGrid)
        {
            _ballFactory = ballFactory;
            _ballGrid = ballGrid;
            GameManager.Instance.EventBus.Subscribe<BallCollisionEventArgs>(OnBallCollision);
            GameManager.Instance.EventBus.Subscribe<BallGridMatchArgs>(OnBallMatch);
            GameManager.Instance.EventBus.Subscribe<OrphanedBallsEventArgs>(OnBallOrphansFound);
        }

        public void Dispose()
        {
            GameManager.Instance.EventBus.Unsubscribe<BallCollisionEventArgs>(OnBallCollision);
            GameManager.Instance.EventBus.Unsubscribe<BallGridMatchArgs>(OnBallMatch);
            GameManager.Instance.EventBus.Unsubscribe<OrphanedBallsEventArgs>(OnBallOrphansFound);
        }

        public void Clear()
        {
            _ballGrid.Clear();
        }

        public void Generate()
        {
            var newBalls = new List<IBallController>();
            for (int gridX = 0; gridX < _ballGrid.Size; gridX++)
            {
                for (int gridY = 0; gridY < _ballGrid.Size; gridY++)
                {
                    var newBall = GenerateBall(gridX, gridY);
                    newBalls.Add(newBall);
                }
            }
            _ballGrid.Initialize(newBalls);
        }

        private void OnBallCollision(BallCollisionEventArgs e)
        {
            if (e.IncomingBall.IsProjectile)
            {
                var ballToAddToGrid = e.IncomingBall;
                var existingBall = e.BallInGrid.Model;
                int offsetX = 0;
                int offsetY = 0;

                if (e.AngleOfImpact >= 45 && e.AngleOfImpact < 135)
                {
                    offsetX = 1;
                }
                else if (e.AngleOfImpact >= 135 && e.AngleOfImpact < 225)
                {
                    offsetY = -1;
                }
                else if (e.AngleOfImpact >= 225 && e.AngleOfImpact < 315)
                {
                    offsetX = -1;
                }
                _ballGrid.Append(ballToAddToGrid, existingBall.GridX + offsetX, existingBall.GridY + offsetY);
            }
        }

        private void OnBallMatch(BallGridMatchArgs e)
        {
            foreach (var ballController in e.BallPath)
            {
                _ballGrid.Remove(ballController.gameObject);
            }
        }

        private void OnBallOrphansFound(OrphanedBallsEventArgs e)
        {
            foreach (var ballController in e.OrphanedBalls)
            {
                _ballGrid.Remove(ballController.gameObject);
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