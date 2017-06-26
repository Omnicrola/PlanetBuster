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
        public GameObject PowerGemSprite;
        public GameObject BallSprite;
        public GameObject DamageSprite;

        private SpriteRenderer _ballSpriteRenderer;
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

        public Quaternion Rotation
        {
            get { return transform.rotation; }
            set { transform.rotation = value; }
        }

        public float Hitpoints
        {
            get { return Model.Hitpoints; }
            set
            {
                Model.Hitpoints = value;
                var percentAlive = 1 - (value / Model.MaxHitpoints);
                DamageSprite.GetComponent<BallDamageController>().PercentDamaged = percentAlive;
            }
        }

        public Sprite CurrentBallSprite
        {
            get { return BallSprite.GetComponent<SpriteRenderer>().sprite; }
        }

        protected override void Start()
        {
            base.Start();
            _ballSpriteRenderer = BallSprite.GetComponent<SpriteRenderer>();
            _baseScale = gameObject.transform.localScale;
        }

        protected override void DirtyUpdate()
        {
            if (_ballSpriteRenderer != null && Model != null)
            {
                _ballSpriteRenderer.sprite = Model.IconName;
                PowerGemSprite.SetActive(Model.HasPowerGem);
                DamageSprite.SetActive(Model.Magnitude != BallMagnitude.Standard);

                var scale = GetScale(Model.Magnitude);
                if (scale > 1)
                {
                    gameObject.transform.localScale = _baseScale * scale;
                }
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