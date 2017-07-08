using System;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGridManager : UnityBehavior, IBallGridManager
    {
        public GameObject LevelForTesting;

        public float Spacing = 1;
        public GameObject Ceiling;

        public Vector2 Offset = new Vector2(0, 0);

        private BallFactory _ballFactory;
        private BallGridController _ballGridController;

        public float LowestBallPosition { get { return _ballGridController.LowestBallPosition; } }

        protected override void Start()
        {
            var simpleObjectPool = GetComponent<SimpleObjectPool>();
            _ballFactory = new BallFactory(simpleObjectPool, Ceiling, Offset, Spacing);
            var orphanedBallFinder = new OrphanedBallFinder();
            var ballGrid = new BallGrid(_ballFactory, orphanedBallFinder, gameObject);
            _ballGridController = new BallGridController(_ballFactory, ballGrid, new BallGridPositionCalculator());
        }

        public void StartNewLevel()
        {
            GameManager.Instance.EventBus.Broadcast(new GamePrestartEventArgs());
            _ballGridController.Clear();

            ILevelDataController currentLevel = null;
            if (LevelForTesting != null && Application.isEditor)
            {
                currentLevel = LevelForTesting.GetComponent<ILevelDataController>();
            }
            else
            {
                currentLevel = GameManager.Instance.CurrentLevel;
            }

            _ballGridController.Generate(currentLevel);
            GetComponent<BallGridPositionController>().StartNewLevel(_ballGridController.HeightOfActiveGrid);
            GameManager.Instance.EventBus.Broadcast(new GameStartEventArgs());
        }

        public void StickBallToCeiling(IBallController incomingBall)
        {
            _ballGridController.StickBallToCeiling(incomingBall);
        }

        public GameObject GenerateBall(BallType type)
        {
            return _ballGridController.GenerateBall(type);
        }

        public BallType GetNextBallType()
        {
            return _ballGridController.GetNextBallType();
        }


        protected override void OnDestroy()
        {
            _ballGridController.Dispose();
        }
    }
}