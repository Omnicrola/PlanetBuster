using System;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public class BallController : DirtyBehavior<BallModel>, IBallController
    {
        public GameObject PowerGem;

        private SpriteRenderer _spriteRenderer;
        private bool _isProjectile;
        private bool _active;
        private readonly AngleOfImpactCalculator angleOfImpactCalculator = new AngleOfImpactCalculator();
        private Vector3 _baseScale;

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
                var rigidBody = GetComponent<Rigidbody2D>();
                if (value)
                {
                    tag = Tags.Balls;
                    rigidBody.isKinematic = false;
                }
                else
                {
                    tag = Tags.InactiveBall;
                    rigidBody.isKinematic = true;
                }
            }
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        protected override void Start()
        {
            base.Start();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _baseScale = gameObject.transform.localScale;
        }

        protected override void DirtyUpdate()
        {
            if (_spriteRenderer != null && Model != null)
            {
                _spriteRenderer.sprite = Model.IconName;
                PowerGem.SetActive(Model.HasPowerGem);
                var scale = GetScale(Model.Magnitude);
                gameObject.transform.localScale = _baseScale * scale;
                gameObject.transform.position -= new Vector3(scale * -0.5f, scale * 0.5f);
            }
        }

        private float GetScale(BallMagnitude magnitude)
        {
            switch (magnitude)
            {
                case BallMagnitude.Standard:
                    return 1f;
                    break;
                case BallMagnitude.Medium:
                    return 2f;
                    break;
                case BallMagnitude.Large:
                    return 3f;
                    break;
                case BallMagnitude.Huge:
                    return 4f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("magnitude", magnitude, null);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == Tags.Balls)
            {
                var otherObject = collision.gameObject.GetComponent<IBallController>();
                if (IsProjectile && otherObject != null)
                {
                    var projectilePosition = transform.position;
                    var gridBallPosition = collision.transform.position;
                    var angle = angleOfImpactCalculator.Calculate(projectilePosition, gridBallPosition);
                    var ballCollisionEventArgs = new BallCollisionEventArgs(this, otherObject, angle);
                    GameManager.Instance.EventBus.Broadcast(ballCollisionEventArgs);
                }
            }
        }

        public void Launch(Vector3 position, Quaternion orientation, Vector3 trajectory, float projectileSpeed)
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

        public void ResetBall()
        {
            IsProjectile = false;
            Active = false;
            Model = null;
        }
    }
}