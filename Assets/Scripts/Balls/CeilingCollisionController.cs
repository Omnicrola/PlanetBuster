using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class CeilingCollisionController : UnityBehavior
    {
        public GameObject BallGridManager;
        private IBallGridManager _ballGridManager;

        protected override void Start()
        {
            _ballGridManager = BallGridManager.GetComponent<IBallGridManager>();
        }

        protected override void Update()
        {
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == Tags.Balls)
            {
                var ballController = collision.gameObject.GetComponent<IBallController>();
                if (ballController != null)
                {
                    _ballGridManager.StickBallToCeiling(ballController);
                }
            }
        }
    }
}