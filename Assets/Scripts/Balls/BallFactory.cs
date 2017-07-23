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
        private readonly GameObject _ceiling;
        private readonly Vector2 _offset;

        private readonly BallGridPositionCalculator _ballPositionCalculator;

        public BallFactory(SimpleObjectPool simpleObjectPool, GameObject ceiling, Vector2 offset)
        {
            _simpleObjectPool = simpleObjectPool;
            _ballPositionCalculator = new BallGridPositionCalculator();
            _ceiling = ceiling;
            _offset = offset;
        }

        public virtual Vector3 GetWorldPositionFromGrid(GridPosition gridPosition)
        {
            return _ballPositionCalculator.GetWorldPosition(gridPosition, _ceiling.transform.position, _offset);
        }

        public GridPosition GetGridPositionFromWorldPosition(Vector2 worldPosition)
        {
            return _ballPositionCalculator.GetGridPosition(worldPosition, _ceiling.transform.position, _offset);
        }

        public GameObject GenerateBall(BallType type)
        {
            var newBall = _simpleObjectPool.GetObjectFromPool();
            var ballController = newBall.GetComponent<IBallController>();
            ballController.BallType = type;
            ballController.Magnitude = BallMagnitude.Standard;

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