using System;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallController : DirtyBehavior<BallModel>
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isProjectile;
        private bool _active;
        private AngleOfImpactCalculator angleOfImpactCalculator
             = new AngleOfImpactCalculator();

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

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (value)
                {
                    tag = Tags.Balls;
                    GetComponent<Rigidbody2D>().isKinematic = false;
                }
                else
                {
                    tag = Tags.InactiveBall;
                    GetComponent<Rigidbody2D>().isKinematic = true;
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
            if (OnHit != null && collision.gameObject.tag == Tags.Balls)
            {
                var otherObject = collision.gameObject.GetComponent<BallController>();
                if (IsProjectile && otherObject != null)
                {
                    var angle = angleOfImpactCalculator.Calculate(transform.position, collision.transform.position);
                    var ballCollisionEventArgs = new BallCollisionEventArgs(otherObject, this, angle);
                    OnHit.Invoke(this, ballCollisionEventArgs);
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


        public bool IsAtGrid(int gridX, int gridY)
        {
            return Model.GridX == gridX && Model.GridY == gridY;
        }
    }

}
