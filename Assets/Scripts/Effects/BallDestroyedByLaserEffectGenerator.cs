using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BallDestroyedByLaserEffectGenerator : UnityBehavior
    {
        public GameObject FloatingScoreGenerator;


        private SimpleObjectPool _simpleObjectPool;
        private FloatingScoreEffectGenerator _floatingScoreEffectGenerator;

        protected override void Start()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallDestroyByGiantLaserEventArgs>(OnBallDestroyed);
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
            _floatingScoreEffectGenerator = FloatingScoreGenerator.GetComponent<FloatingScoreEffectGenerator>();
        }

        private void OnBallDestroyed(BallDestroyByGiantLaserEventArgs obj)
        {
            var ball = obj.BallController;
            var ballPosition = ball.Position;
            var newExplosion = _simpleObjectPool.GetObjectFromPool();

            newExplosion.transform.SetParent(transform);
            newExplosion.transform.position = ballPosition;

            var planetSprite = ball.CurrentBallSprite;
            var magnitudeScale = ball.Magnitude.GetScale();

            var ballDestroyEffect = newExplosion.GetComponent<BallDestroyedByLaserEffect>();
            ballDestroyEffect.RePlayEffect(planetSprite, magnitudeScale);
            _floatingScoreEffectGenerator.ShowScore(GameConstants.ScorePerBall, ballPosition, 0);
        }

        protected override void OnDestroy()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallDestroyByGiantLaserEventArgs>(OnBallDestroyed);
        }
    }
}