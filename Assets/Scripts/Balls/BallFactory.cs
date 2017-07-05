using System.Collections.Generic;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Balls
{
    public class BallFactory : IBallFactory
    {
        private readonly SimpleObjectPool _simpleObjectPool;
        private readonly HashSet<GameObject> _allBallsCreated = new HashSet<GameObject>();
        private readonly GameObject _ceiling;
        private readonly Vector2 _offset;
        private readonly float _spacing;

        private readonly Random _random = new Random(11);

        public BallFactory(SimpleObjectPool simpleObjectPool, GameObject ceiling, Vector2 offset, float spacing)
        {
            _simpleObjectPool = simpleObjectPool;
            _ceiling = ceiling;
            _offset = offset;
            _spacing = spacing;
        }

        public IBallController GenerateBall(BallLevelData ballData)
        {
            var ballController = GenerateBall(new GridPosition(ballData.XPos, ballData.YPos), ballData.BallType,
                ballData.HasPowerGem,
                ballData.Magnitude);
            return ballController;
        }

        public IBallController GenerateBall(GridPosition gridPosition)
        {
            return GenerateBall(gridPosition, BallType.Blue, false, BallMagnitude.Standard);
        }

        private IBallController GenerateBall(GridPosition gridPosition, BallType type, bool hasPowerGem,
            BallMagnitude magnitude)
        {
            var newBall = _simpleObjectPool.GetObjectFromPool();
            newBall.transform.position = GetWorldPositionFromGrid(gridPosition);
            var hitpoints = magnitude.GetHitpoints();
            var ballModel = new BallModel()
            {
                Type = type,
                Hitpoints = hitpoints,
                MaxHitpoints = hitpoints,
                HasPowerGem = hasPowerGem,
                Magnitude = magnitude
            };

            var ballController = newBall.GetComponent<IBallController>();
            ballController.IsProjectile = false;
            ballController.Model = ballModel;

            Logging.Instance.Log(LogLevel.Debug, "Created ball at grid : " + gridPosition.X + ", " + gridPosition.Y);
            if (!_allBallsCreated.Contains(newBall.gameObject))
            {
                _allBallsCreated.Add(newBall.gameObject);
            }
            return ballController;
        }


        public virtual Vector3 GetWorldPositionFromGrid(GridPosition gridPosition)
        {
            var ceilingOffset = _ceiling.transform.position;
            var x = gridPosition.X * _spacing + _offset.x + ceilingOffset.x;
            var y = (gridPosition.Y * _spacing * -1) + _offset.y + ceilingOffset.y;
            return new Vector3(x, y, 0);
        }

        public GridPosition GetGridPositionFromWorldPosition(Vector2 worldPosition)
        {
            var ceilingOffset = _ceiling.transform.position;
            int x = Mathf.RoundToInt((worldPosition.x - ceilingOffset.x - _offset.x) / _spacing);
            int y = Mathf.RoundToInt((worldPosition.y - ceilingOffset.y - _offset.y) / _spacing);
            return new GridPosition(x, y);
        }


        public GameObject GenerateBall(BallType type)
        {
            var ballModel = new BallModel()
            {
                Type = type,
                Hitpoints = 1,
            };
            var newBall = _simpleObjectPool.GetObjectFromPool();
            var ballController = newBall.GetComponent<IBallController>();
            ballController.Model = ballModel;

            Logging.Instance.Log(LogLevel.Debug, "Created ball outside of grid, of type : " + type);
            return newBall;
        }

        public void Recycle(GameObject gameObject)
        {
            gameObject.GetComponent<IBallController>().ResetBall();
            _simpleObjectPool.ReturnObjectToPool(gameObject);
        }

    }
}