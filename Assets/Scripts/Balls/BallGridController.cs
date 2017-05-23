using System;
using Assets.Scripts.Models;
using UnityEngine;
using Random = System.Random;


namespace Assets.Scripts.Balls
{
    public class BallGridController
    {
        public EventHandler<BallGridMatchArgs> MatchFound;

        private readonly Random random = new Random();
        private readonly BallGrid _ballGrid;
        private readonly BallFactory _ballFactory;

        public BallGridController(BallFactory ballFactory, BallGrid ballGrid)
        {
            _ballFactory = ballFactory;
            _ballGrid = ballGrid;
            _ballGrid.MatchFound += OnMatchFound;
        }

        public void Clear()
        {
            _ballGrid.Clear();
        }

        public void Generate()
        {
            _ballGrid.MatchFound -= OnMatchFound;
            for (int gridX = 0; gridX < _ballGrid.Size; gridX++)
            {
                for (int gridY = 0; gridY < _ballGrid.Size; gridY++)
                {
                    GenerateBall(gridX, gridY);
                }
            }
            _ballGrid.MatchFound += OnMatchFound;
        }

        private void OnBallCollision(object sender, BallCollisionEventArgs e)
        {
            if (e.BallInGrid.IsProjectile)
            {
                var ballToAddToGrid = e.BallInGrid;
                var existingBall = e.IncomingBall.Model;
                int offsetX = 0;
                int offsetY = 0;
                Debug.Log("angleOfImpact: " + e.AngleOfImpact + " " + e.BallInGrid.transform.position + " <- " + e.IncomingBall.transform.position);

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

        public GameObject GenerateBall(int gridX, int gridY)
        {
            var newBall = _ballFactory.GenerateBall(gridX, gridY);
            newBall.GetComponent<BallController>().OnHit += OnBallCollision;
            _ballGrid.Append(newBall, gridX, gridY);
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