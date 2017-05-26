using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BallDropEffect : UnityBehavior
    {
        public GameObject BallContainer;

        private SimpleObjectPool _simpleObjectPool;

        protected override void Start()
        {
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
            GameManager.Instance.EventBus.BallOrphansFound += OnOrphansFound;
            GameManager.Instance.EventBus.BallOutOfBounds += OnBallOutOfBounds;
        }

        private void OnBallOutOfBounds(object sender, BallOutOfBoundsEventArgs e)
        {
            _simpleObjectPool.ReturnObjectToPool(e.Ball);
        }

        private void OnOrphansFound(object sender, OrphanedBallsEventArgs e)
        {
            foreach (var ballController in e.OrphanedBalls)
            {
                var fallingBall = _simpleObjectPool.GetObjectFromPool();
                fallingBall.transform.SetParent(BallContainer.transform);

                var orphanedBall = ballController.gameObject;
                var ballPosition = orphanedBall.GetComponent<Rigidbody2D>().position;
                fallingBall.GetComponent<Rigidbody2D>().position = ballPosition;

                var ballSprite = orphanedBall.GetComponent<SpriteRenderer>().sprite;
                fallingBall.GetComponent<SpriteRenderer>().sprite = ballSprite;

            }
        }
    }
}