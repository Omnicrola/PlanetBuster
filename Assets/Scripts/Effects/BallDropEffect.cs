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
            GameManager.Instance.EventBus.Subscribe<OrphanedBallsEventArgs>(OnOrphansFound);
            GameManager.Instance.EventBus.Subscribe<BallOutOfBoundsEventArgs>(OnBallOutOfBounds);
        }

        private void OnBallOutOfBounds(BallOutOfBoundsEventArgs e)
        {
            _simpleObjectPool.ReturnObjectToPool(e.Ball);
        }

        private void OnOrphansFound(OrphanedBallsEventArgs e)
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

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<OrphanedBallsEventArgs>(OnOrphansFound);
            GameManager.Instance.EventBus.Unsubscribe<BallOutOfBoundsEventArgs>(OnBallOutOfBounds);
        }
    }
}