using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallGridPositionController : UnityBehavior
    {
        public float DelayBeforeFirstDrop = 30f;
        public float DecentInterval = 30;
        public float DecentDistance = 1f;

        private float _nextDropTime;
        private IBallGridManager _ballGridManager;
        private float GameOverHeight = -3f;

        protected override void Start()
        {
            _ballGridManager = GetComponent<IBallGridManager>();
            _nextDropTime = Time.time + DelayBeforeFirstDrop;
        }

        protected override void Update()
        {
            if (Time.time >= _nextDropTime)
            {
                _nextDropTime = Time.time + DecentInterval;
                var currentPosition = transform.position;
                var newPosition = new Vector2(currentPosition.x, currentPosition.y - DecentDistance);
                iTween.MoveTo(gameObject, newPosition, 1f);
            }

            float lowestBallPosition = _ballGridManager.LowestBallPosition;
            if (lowestBallPosition <= GameOverHeight)
            {
                GameManager.Instance.EventBus.Broadcast(new GameOverEventArgs(GameOverCondition.LossByDropHeight));
            }
        }
    }
}