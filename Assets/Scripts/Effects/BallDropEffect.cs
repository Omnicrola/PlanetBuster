using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Effects
{
    public class BallDropEffect : UnityBehavior
    {
        public GameObject BallContainer;
        public float RandomForceMagnitude = 1.0f;

        private SimpleObjectPool _simpleObjectPool;
        private readonly Random random = new Random();

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
                fallingBall.GetComponent<Rigidbody2D>().AddForce(random.RandomVector(RandomForceMagnitude));

                var ballSprite = ballController.CurrentBallSprite;
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