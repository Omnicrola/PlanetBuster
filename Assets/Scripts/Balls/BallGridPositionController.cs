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

        protected override void Start()
        {
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
        }
    }
}