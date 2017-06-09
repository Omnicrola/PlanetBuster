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

        const float NORTH_EAST = 45f;
        const float SOUTH_EAST = 135f;
        const float SOUTH_WEST = 225f;
        const float NORTH_WEST = 315f;
        private static readonly Random _random = new Random(55);

        private readonly Random random = new Random();
        private readonly IBallGrid _ballGrid;
        private readonly IBallFactory _ballFactory;

        public BallGridController(IBallFactory ballFactory, IBallGrid ballGrid)
        {
            _ballFactory = ballFactory;
            _ballGrid = ballGrid;
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Subscribe<BallCollisionEventArgs>(OnBallCollision);
            gameEventBus.Subscribe<BallGridMatchArgs>(OnBallMatch);
            gameEventBus.Subscribe<OrphanedBallsEventArgs>(OnBallOrphansFound);
        }

        public float LowestBallPosition { get { return _ballGrid.LowestBallPosition; } }

        public void Dispose()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Unsubscribe<BallCollisionEventArgs>(OnBallCollision);
            gameEventBus.Unsubscribe<BallGridMatchArgs>(OnBallMatch);
            gameEventBus.Unsubscribe<OrphanedBallsEventArgs>(OnBallOrphansFound);
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


                if (e.AngleOfImpact >= NORTH_EAST && e.AngleOfImpact < SOUTH_EAST)
                {
                    offsetX = 1;
                }
                else if (e.AngleOfImpact >= SOUTH_EAST && e.AngleOfImpact < SOUTH_WEST)
                {
                    offsetY = 1;
                }
                else if (e.AngleOfImpact >= SOUTH_WEST && e.AngleOfImpact < NORTH_WEST)
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
        private void OnBallDestroyed(BallDestroyEventArgs obj)
        {
            _ballGrid.Remove(obj.BallController.gameObject);
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