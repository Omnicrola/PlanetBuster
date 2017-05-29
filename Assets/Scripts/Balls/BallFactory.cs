using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Balls
{
    public class BallFactory : IBallFactory
    {
        private readonly SimpleObjectPool _simpleObjectPool;
        private readonly Vector2 _offset;
        private readonly float _spacing;
        private readonly Random _random = new Random(11);


        private static readonly string[] Icons = new[]
        {
            "PlanetIcons/planet_001",
            "PlanetIcons/planet_002",
            "PlanetIcons/planet_003",
            "PlanetIcons/planet_004",
        };

        public BallFactory(SimpleObjectPool simpleObjectPool, Vector2 offset, float spacing)
        {
            _simpleObjectPool = simpleObjectPool;
            _offset = offset;
            _spacing = spacing;
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
            var x = gridX * _spacing + _offset.x;
            var y = gridY * _spacing + _offset.y;
            return new Vector3(x, y, 0);
        }

        private BallModel CreateBallModel(int gridX, int gridY)
        {
            var type = _random.Next(Icons.Length);
            var icon = Icons[type];
            var ballModel = new BallModel(gridX, gridY)
            {
                Type = type,
                IconName = icon
            };
            return ballModel;
        }


        public GameObject GenerateBall(int type)
        {
            var ballModel = new BallModel(0, 0)
            {
                Type = type,
                IconName = Icons[type]
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
            return Resources.Load<Sprite>(Icons[type]);
        }
    }
}