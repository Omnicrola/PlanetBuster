using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class BottomWallCollisionDetector : UnityBehavior
    {
        protected override void Start()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            GameManager.Instance.EventBus.Broadcast(new BallOutOfBoundsEventArgs(other.gameObject));
        }
    }
}
