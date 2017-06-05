using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BallMatchExplosionEffect : UnityBehavior
    {
        private SimpleObjectPool _simpleObjectPool;

        void Start()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallGridMatchArgs>(OnMatchFound);
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
        }


        private void OnMatchFound(BallGridMatchArgs e)
        {
            var balls = e.BallPath.OrderBy(b => b.gameObject.transform.position.y).ToList();

            float delay = 0;
            float baseOffset = balls.First().gameObject.transform.position.y;
            foreach (var ball in balls)
            {
                DestroyOneBall(ball, delay);

                delay += (ball.gameObject.transform.position.y - baseOffset) * 0.05f;
            }
        }

        private void OnBallDestroyed(BallDestroyEventArgs obj)
        {
            DestroyOneBall(obj.BallController, 0);
        }

        private void DestroyOneBall(IBallController ball, float delay)
        {
            var ballPosition = ball.Position;
            var newExplosion = _simpleObjectPool.GetObjectFromPool();

            newExplosion.transform.SetParent(transform);
            newExplosion.transform.position = ballPosition;

            var planetSprite = ball.gameObject.GetComponent<SpriteRenderer>().sprite;

            newExplosion.GetComponent<BallDestroyEffect>().RePlayEffect(delay, planetSprite);
        }


        protected override void OnDestroy()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Unsubscribe<BallGridMatchArgs>(OnMatchFound);
        }
    }
}