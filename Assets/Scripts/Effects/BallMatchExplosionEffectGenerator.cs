using System;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BallMatchExplosionEffectGenerator : UnityBehavior
    {
        public GameObject FloatingScoreGenerator;
        public GameObject PowerGemEffectGenerator;
        public float SequentialDelay = 0.25f;

        private SimpleObjectPool _simpleObjectPool;
        private FloatingScoreEffectGenerator _floatingScoreEffectGenerator;
        private PowerGemParticleEffectGenerator _powerGemParticleEffectGenerator;

        void Start()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallGridMatchArgs>(OnMatchFound);

            _simpleObjectPool = GetComponent<SimpleObjectPool>();
            _floatingScoreEffectGenerator = FloatingScoreGenerator.GetComponent<FloatingScoreEffectGenerator>();
            _powerGemParticleEffectGenerator = PowerGemEffectGenerator.GetComponent<PowerGemParticleEffectGenerator>();
        }


        private void OnMatchFound(BallGridMatchArgs e)
        {
            var balls = e.BallPath.OrderBy(b => b.gameObject.transform.position.y).ToList();

            float delay = 0.1f;
            foreach (var ball in balls)
            {
                DestroyOneBall(ball, delay);

                delay += SequentialDelay;
            }
        }

        private void DestroyOneBall(IBallController ball, float delay)
        {
            var ballPosition = ball.Position;
            var newExplosion = _simpleObjectPool.GetObjectFromPool();

            newExplosion.transform.SetParent(transform);
            newExplosion.transform.position = ballPosition;

            var planetSprite = ball.CurrentBallSprite;

            var ballDestroyEffect = newExplosion.GetComponent<BallDestroyEffect>();
            ballDestroyEffect.RePlayEffect(delay, planetSprite, ball.Model.HasPowerGem);
            _floatingScoreEffectGenerator.ShowScore(GameConstants.ScorePerBall, ballPosition, delay);
            if (ball.Model.HasPowerGem)
            {
                _powerGemParticleEffectGenerator.GenerateParticles(delay, ballPosition);
            }
        }


        protected override void OnDestroy()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallGridMatchArgs>(OnMatchFound);
        }
    }
}