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