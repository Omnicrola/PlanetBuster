using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class GiantLaserBeamController : UnityBehavior
    {
        public float Radius = 1.0f;
        public float DamagePerSecond = 1.0f;

        protected override void Start()
        {
        }

        protected override void Update()
        {
            var raycastHit2D = Physics2D.CircleCast(gameObject.transform.position, Radius, gameObject.transform.up);
            if (raycastHit2D.collider != null)
            {
                var ballController = raycastHit2D.collider.gameObject.GetComponent<IBallController>();
                if (ballController != null)
                {
                    ballController.Model.Hitpoints -= DamagePerSecond * Time.deltaTime;
                    if (ballController.Model.Hitpoints <= 0)
                    {
                        GameManager.Instance.EventBus.Broadcast(this,
                            new BallDestroyEventArgs(ballController));
                    }
                }
            }
        }
    }
}