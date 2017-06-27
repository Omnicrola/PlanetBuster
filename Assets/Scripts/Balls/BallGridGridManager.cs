using System;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGridGridManager : UnityBehavior, IBallGridManager
    {
        public float DefaultVerticalSpacing = 10f;
        public GameObject Ceiling;
        public Sprite[] BallTypes;

        public int GridSize = 10;
        public Vector2 Offset = new Vector2(0, 0);
        public float Spacing = 1;

        private BallFactory _ballFactory;
        private BallGridController _ballGridController;

        public float LowestBallPosition { get { return _ballGridController.LowestBallPosition; } }

        protected override void Start()
        {
            var simpleObjectPool = GetComponent<SimpleObjectPool>();
            _ballFactory = new BallFactory(simpleObjectPool, Ceiling, Offset, Spacing, BallTypes);
            var orphanedBallFinder = new OrphanedBallFinder();
            var ballGrid = new BallGrid(_ballFactory, orphanedBallFinder, gameObject);
            _ballGridController = new BallGridController(_ballFactory, ballGrid, new BallGridPositionCalculator());
        }

        public void StartNewLevel()
        {
            GameManager.Instance.EventBus.Broadcast(new GamePrestartEventArgs());
            _ballGridController.Clear();

            var currentLevel = GameManager.Instance.CurrentLevel;
            _ballGridController.Generate(currentLevel);
            var gridSize = currentLevel.BallData.Max(b => b.YPos);
            var offset = gridSize + DefaultVerticalSpacing;
            transform.position = new Vector2(0, offset);
            GameManager.Instance.EventBus.Broadcast(new GameStartEventArgs());
        }


        public IBallController GenerateBall()
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

        protected override void OnDestroy()
        {
            _ballGridController.Dispose();
        }
    }
}