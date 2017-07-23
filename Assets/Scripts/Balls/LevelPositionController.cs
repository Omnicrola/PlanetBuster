using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class LevelPositionController : UnityBehavior
    {
        public float DelayBeforeFirstDrop = 30f;
        public float DecentInterval = 30;
        public float DecentDistance = 1f;
        public float MinimumDistanceThreshold = 5f;
        public float DefaultVerticalSpacing = 10f;
        public float MinimumDistance = 1f;

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
            else if (lowestBallPosition > MinimumDistanceThreshold)
            {
                _nextDropTime = Time.time + DecentInterval;
                var currentPosition = transform.position;
                var verticalDistanceToTravel = currentPosition.y - MinimumDistance;
                var newPosition = new Vector2(currentPosition.x, verticalDistanceToTravel);
                iTween.MoveTo(gameObject, newPosition, 1f);
            }
        }

        public void StartNewLevel(int totalGridHeight)
        {
            var offset = totalGridHeight + DefaultVerticalSpacing;
            transform.position = new Vector2(0, offset);
        }
    }
}