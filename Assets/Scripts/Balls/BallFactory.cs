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
        private readonly float _spacing;

        private readonly Sprite[] _ballTypes;
        private readonly Random _random = new Random(11);

        public BallFactory(SimpleObjectPool simpleObjectPool, GameObject ceiling, Vector2 offset, float spacing,
            Sprite[] ballTypes)
        {
            _simpleObjectPool = simpleObjectPool;
            _ceiling = ceiling;
            _offset = offset;
            _spacing = spacing;
            _ballTypes = ballTypes;
        }

        public IBallController GenerateBall(int gridX, int gridY)
        {
            var newBall = _simpleObjectPool.GetObjectFromPool();
            newBall.transform.position = GetGridPosition(gridX, gridY);
            var ballModel = CreateBallModel(gridX, gridY);

            var ballController = newBall.GetComponent<IBallController>();
            ballController.IsProjectile = false;
            ballController.Model = ballModel;

            Logging.Instance.Log(LogLevel.Debug, "Created ball at grid : " + gridX + ", " + gridY);

            return ballController;
        }

        public virtual Vector3 GetGridPosition(int gridX, int gridY)
        {
            var ceilingOffset = _ceiling.transform.position;
            var x = gridX * _spacing + _offset.x + ceilingOffset.x;
            var y = (gridY * _spacing * -1) + _offset.y + ceilingOffset.y;
            return new Vector3(x, y, 0);
        }

        private BallModel CreateBallModel(int gridX, int gridY)
        {
            var type = _random.Next(_ballTypes.Length);
            var icon = _ballTypes[type];
            var ballModel = new BallModel(gridX, gridY)
            {
                Type = type,
                IconName = icon,
                Hitpoints = 1
            };
            return ballModel;
        }


        public GameObject GenerateBall(int type)
        {
            var ballModel = new BallModel(0, 0)
            {
                Type = type,
                Hitpoints = 1,
                IconName = _ballTypes[type]
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

        public Sprite GetBallSpriteOfType(int type)
        {
            return _ballTypes[type];
        }
    }
}