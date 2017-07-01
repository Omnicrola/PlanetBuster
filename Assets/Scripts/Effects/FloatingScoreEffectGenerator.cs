using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class FloatingScoreEffectGenerator : UnityBehavior
    {
        public GameObject Container;
        public GameObject Camera;
        public float PositionJitter;
        public Vector3 PositionOffset;

        private SimpleObjectPool _scoreTextPool;
        private Camera _camera;

        protected override void Start()
        {
            _scoreTextPool = GetComponent<SimpleObjectPool>();
            _camera = Camera.GetComponent<Camera>();
        }


        public void ShowScore(int score, Vector3 ballPosition, float delay)
        {
            var newScoreText = _scoreTextPool.GetObjectFromPool();
            var floatingScoreEffect = newScoreText.GetComponent<FloatingScoreEffect>();

            var textPosition = _camera.WorldToScreenPoint(ballPosition + PositionOffset);
            textPosition = textPosition.RandomOffset(PositionJitter);
            floatingScoreEffect.Reset(delay, score, textPosition);
            newScoreText.transform.SetParent(Container.transform);
        }
    }
}