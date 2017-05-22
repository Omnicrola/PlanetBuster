using System;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallController : DirtyBehavior<BallModel>
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isProjectile;

        public EventHandler<BallCollisionEventArgs> OnHit { get; set; }

        public bool IsProjectile
        {
            get { return _isProjectile; }
            set
            {
                _isProjectile = value;
                var rigidBody = GetComponent<Rigidbody2D>();
                if (value)
                {
                    rigidBody.bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                    rigidBody.bodyType = RigidbodyType2D.Static;
                }
            }
        }

        protected override void Start()
        {
            base.Start();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void DirtyUpdate()
        {
            if (_spriteRenderer != null && Model != null)
            {
                _spriteRenderer.sprite = Resources.Load<Sprite>(Model.IconName);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (OnHit != null)
            {
                var otherObject = collision.gameObject.GetComponent<BallController>();
                if (IsProjectile && otherObject != null)
                {
                    OnHit.Invoke(this, new BallCollisionEventArgs(otherObject, this));
                }
            }
        }

        public void Fire(Vector3 position, Quaternion orientation, Vector3 trajectory, float projectileSpeed)
        {
            transform.position = position;
            transform.rotation = orientation;
            var rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            trajectory.Normalize();
            rigidBody.velocity = trajectory * projectileSpeed;
        }


    }

}
