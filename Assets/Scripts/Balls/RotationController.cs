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
            if (RotationX != 0) transform.Rotate(Vector3.left, RotationX * Time.deltaTime);
            if (RotationY != 0) transform.Rotate(Vector3.up, RotationY * Time.deltaTime);
            if (RotationZ != 0) transform.Rotate(Vector3.forward, RotationZ * Time.deltaTime);
        }
    }
}