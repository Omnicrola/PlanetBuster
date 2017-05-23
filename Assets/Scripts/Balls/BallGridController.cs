using Assets.Scripts.Models;
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
            if (e.ThisObject.IsProjectile)
            {
                var ballToAddToGrid = e.ThisObject;
                var existingBall = e.OtherObject.Model;
                _ballGrid.Append(ballToAddToGrid.gameObject, existingBall.GridX, existingBall.GridY - 1);
            }
        }

        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            if (e.BallPath.Count > 3)
            {
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