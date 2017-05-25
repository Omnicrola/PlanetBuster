using System;
using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;
using Random = System.Random;


namespace Assets.Scripts.Balls
{
    public class BallGridController
    {
        public event EventHandler<BallGridMatchArgs> MatchFound;
        public event EventHandler<OrphanedBallsEventArgs> OrphansFound;

        private readonly Random random = new Random();
        private readonly BallGrid _ballGrid;
        private readonly BallFactory _ballFactory;

        public BallGridController(BallFactory ballFactory, BallGrid ballGrid)
        {
            _ballFactory = ballFactory;
            _ballGrid = ballGrid;
            _ballGrid.MatchFound += OnMatchFound;
            _ballGrid.OrphansFound += OnOrphansFound;
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
                Debug.Log(
                    string.Format("Impacted: ({0}, {1}) angleOfImpact: {2} projectilePosition: {3} gridPosition: {4}",
                        existingBall.GridX, existingBall.GridY, e.AngleOfImpact, ballToAddToGrid.transform.position,
                        e.BallInGrid.transform.position));

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


        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            if (e.BallPath.Count > GameConstants.MinimumMatchNumber)
            {
                if (MatchFound != null)
                {
                    MatchFound.Invoke(this, e);
                }
                foreach (var ballController in e.BallPath)
                {
                    ballController.OnHit -= OnBallCollision;
                    _ballGrid.Remove(ballController.gameObject);
                }
            }
        }
        private void OnOrphansFound(object sender, OrphanedBallsEventArgs e)
        {
            if (OrphansFound != null)
            {
                OrphansFound.Invoke(this, e);
            }
        }

        public GameObject GenerateBall(int gridX, int gridY)
        {
            var newBall = _ballFactory.GenerateBall(gridX, gridY);
            newBall.GetComponent<BallController>().OnHit += OnBallCollision;
            return newBall;
        }

        public GameObject GenerateBall(int type)
        {
            var newBall = _ballFactory.GenerateBall(type);
            newBall.GetComponent<BallController>().OnHit += OnBallCollision;
            return newBall;
        }

        public int GetNextBallType()
        {
            var typesActive = _ballGrid.TypesLeftActive;
            return typesActive[random.Next(typesActive.Length - 1)];
        }
    }
}