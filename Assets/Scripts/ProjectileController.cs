using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileController : UnityBehavior
    {
        private Rigidbody2D _rigidbody2D;

        protected override void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected override void Update()
        {
            //            transform.position
        }

        public void Fire(Vector3 position, Quaternion orientation, Vector3 trajectory, float projectileSpeed)
        {
            transform.position = position;
            transform.rotation = orientation;
            _rigidbody2D.velocity = trajectory * projectileSpeed;
        }
    }
}