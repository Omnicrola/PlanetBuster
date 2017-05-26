using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
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
            EventBus = new EventBus();
        }
        #endregion

        protected override void Update()
        {
        }

        public EventBus EventBus { get; private set; }

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
        }

        public void StartNewLevel()
        {
            EventBus.BroadcastGamePrestart(this, EventArgs.Empty);
            _ballGridController.Clear();
            _ballGridController.Generate();
            EventBus.BroadcastGameStart(this, EventArgs.Empty);
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