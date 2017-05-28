using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;


namespace Assets.Scripts.Balls
{
    public class BallGridController
    {
        private readonly Random random = new Random();
        private readonly BallGrid _ballGrid;
        private readonly BallFactory _ballFactory;

        public BallGridController(BallFactory ballFactory, BallGrid ballGrid)
        {
            _ballFactory = ballFactory;
            _ballGrid = ballGrid;
            GameManager.Instance.EventBus.BallCollision += OnBallCollision;
            GameManager.Instance.EventBus.BallMatchFound += OnBallMatch;
            GameManager.Instance.EventBus.BallOrphansFound += OnBallOrphansFound;
        }


        public void Clear()
        {
            _ballGrid.Clear();
        }

        public void Generate()
        {
            var newBalls = new List<GameObject>();
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

        private void OnBallCollision(object sender, BallCollisionEventArgs e)
        {
            if (e.BallInGrid.IsProjectile)
            {
                var ballToAddToGrid = e.BallInGrid;
                var existingBall = e.IncomingBall.Model;
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
                _ballGrid.Append(ballToAddToGrid.gameObject, existingBall.GridX + offsetX, existingBall.GridY + offsetY);
            }
        }

        private void OnBallMatch(object sender, BallGridMatchArgs e)
        {
            foreach (var ballController in e.BallPath)
            {
                _ballGrid.Remove(ballController.gameObject);
            }
        }

        private void OnBallOrphansFound(object sender, OrphanedBallsEventArgs e)
        {
            foreach (var ballController in e.OrphanedBalls)
            {
                _ballGrid.Remove(ballController.gameObject);
            }
        }

        public GameObject GenerateBall(int gridX, int gridY)
        {
            var newBall = _ballFactory.GenerateBall(gridX, gridY);
            return newBall;
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