using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class RotationController : UnityBehavior
    {
        public float RotationX = 0;
        public float RotationY = 0;
        public float RotationZ = 0;

        protected override void Update()
        {
            transform.Rotate(Vector3.forward, RotationZ);
        }
    }
}