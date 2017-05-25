using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using UnityEngine;

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

        public event EventHandler<BallGridMatchArgs> MatchFound;
        public event EventHandler<OrphanedBallsEventArgs> OrphansFound;

        public int GridSize = 10;
        public Vector2 Offset = new Vector2(0, 0);
        public float Spacing = 1;

        private BallGridController _ballGridController;
        private BallFactory _ballFactory;

        protected override void Start()
        {
            var simpleObjectPool = GetComponent<SimpleObjectPool>();
            _ballFactory = new BallFactory(simpleObjectPool, Offset, Spacing);
            _ballGridController = new BallGridController(_ballFactory, new BallGrid(GridSize, _ballFactory));
            _ballGridController.MatchFound += OnMatchFound;
            _ballGridController.OrphansFound += OnOrphansFound;
            GenerateLevel();
        }

        private void OnOrphansFound(object sender, OrphanedBallsEventArgs e)
        {
            if (OrphansFound != null)
            {
                OrphansFound.Invoke(this, e);
            }
        }

        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            if (MatchFound != null)
            {
                MatchFound.Invoke(this, e);
            }
        }

        private void GenerateLevel()
        {
            _ballGridController.Clear();
            _ballGridController.Generate();
        }

        public GameObject GenerateBall()
        {
            return _ballGridController.GenerateBall(0, 0);
        }

        public GameObject GenerateBall(int type)
        {
            return _ballGridController.GenerateBall(type);
        }

        public int GetNextBallType()
        {
            return _ballGridController.GetNextBallType();
        }

        public Sprite GetBallSpriteOfType(int type)
        {
            return _ballFactory.GetBallSpriteOfType(type);
        }
    }
}