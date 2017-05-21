using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class GameManager : UnityBehavior, IGameManager
    {
        #region Singleton
        private static GameManager _instance = null;

        public static IGameManager Instance
        {
            get { return _instance; }
        }


        protected override void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }


        public GameManager()
        {
        }
        #endregion

        protected override void Update()
        {
        }

        public GameObject Container;

        public int GridSize = 10;
        public Vector2 Offset = new Vector2(0, 0);
        public float Spacing = 1;

        private SimpleObjectPool _simpleObjectPool;
        private List<GameObject> _activeBalls;


        protected override void Start()
        {
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
            GenerateLevel();
        }

        private int count;

        private void GenerateLevel()
        {
            _activeBalls = new List<GameObject>(GridSize * GridSize);
            for (int gridX = 0; gridX < GridSize; gridX++)
            {
                for (int gridY = 0; gridY < GridSize; gridY++)
                {
                    var newBall = GenerateBall(gridX, gridY);

                    _activeBalls.Add(newBall);
                }
            }
        }

        public GameObject GenerateBall(int gridX, int gridY)
        {
            var newBall = _simpleObjectPool.GetObjectFromPool();
            newBall.name = "grid ball " + (count++);
            newBall.transform.SetParent(Container.transform);
            newBall.transform.position = GetGridPosition(gridX, gridY);
            var ballModel = CreateBallModel(gridX, gridY);

            var ballController = newBall.GetComponent<BallController>();
            ballController.IsProjectile = false;
            ballController.Model = ballModel;
            ballController.OnHit += BallHit;

            _activeBalls.Add(newBall);
            return newBall;
        }

        private Vector3 GetGridPosition(int gridX, int gridY)
        {
            var x = gridX * Spacing + Offset.x;
            var y = gridY * Spacing + Offset.y;
            return new Vector3(x, y, 0);
        }

        private void BallHit(object sender, BallCollisionEventArgs e)
        {
            if (e.ThisObject.IsProjectile)
            {
                var ballToAddToGrid = e.ThisObject;
                var existingBall = e.OtherObject.Model;

                ballToAddToGrid.IsProjectile = false;
                ballToAddToGrid.transform.SetParent(Container.transform, true);
                ballToAddToGrid.transform.position = GetGridPosition(existingBall.GridX, existingBall.GridY - 1);
                ballToAddToGrid.Model.GridX = existingBall.GridX;
                ballToAddToGrid.Model.GridY = existingBall.GridY - 1;
            }
        }

        private BallModel CreateBallModel(int gridX, int gridY)
        {
            var type = Random.Next(Icons.Length);
            var icon = Icons[type];
            var ballModel = new BallModel(gridX, gridY)
            {
                Type = type,
                IconName = icon
            };
            return ballModel;
        }

        private static readonly string[] Icons = new[]
        {
            "PlanetIcons/planet_001",
            "PlanetIcons/planet_002",
            "PlanetIcons/planet_003",
            "PlanetIcons/planet_004",
        };

        private static readonly Random Random = new Random(11);
    }
}