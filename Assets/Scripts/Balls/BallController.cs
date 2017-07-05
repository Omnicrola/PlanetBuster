using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    [ExecuteInEditMode]
    public class BallController : DirtyBehavior<BallModel>, IBallController
    {
        public GameObject PowerGemSprite;
        public GameObject BallSprite;
        public GameObject DamageSprite;

        public BallType BallType;
        public BallMagnitude Magnitude;
        public bool HasPowerGem;


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

        public GridPosition GridPosition { get; private set; }


        public BallMagnitude BallMagnitude
        {
            get { return Model.Magnitude; }
            set
            {
                Model.Magnitude = value;
                gameObject.transform.localScale = _baseScale * value.GetScale();
                Hitpoints = value.GetHitpoints();
            }
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
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
            set { BallSprite.GetComponent<SpriteRenderer>().sprite = value; }
        }

        public BallController()
        {
            Model = new BallModel();
        }

        protected override void Start()
        {
            base.Start();
            _ballSpriteRenderer = BallSprite.GetComponent<SpriteRenderer>();
            _baseScale = gameObject.transform.localScale;
        }

        protected override void Update()
        {
            base.Update();
            _ballSpriteRenderer.sprite = BallType.GetSprite();
            PowerGemSprite.SetActive(HasPowerGem);
            DamageSprite.SetActive(Magnitude != BallMagnitude.Standard);
            gameObject.transform.localScale = _baseScale * Magnitude.GetScale();
        }

        protected override void DirtyUpdate()
        {
            if (_ballSpriteRenderer != null && Model != null)
            {
                _ballSpriteRenderer.sprite = Model.Type.GetSprite();
                PowerGemSprite.SetActive(Model.HasPowerGem);
                DamageSprite.SetActive(Model.Magnitude != BallMagnitude.Standard);

                var scale = Model.Magnitude.GetScale();
                gameObject.transform.localScale = _baseScale * scale;
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


        public void SetActiveInGrid(GridPosition gridPosition, Vector3 worldPosition, Transform parentTransform)
        {
            GridPosition = gridPosition;
            gameObject.transform.SetParent(parentTransform);
            IsProjectile = false;
            gameObject.transform.position = worldPosition;
            gameObject.transform.rotation = Quaternion.identity;
        }


        public void SetInactiveInGrid()
        {
            gameObject.transform.SetParent(null);
        }


        public void ResetBall()
        {
            GridPosition = GridPosition.Invalid;
            IsProjectile = false;
            Active = false;
        }
    }
}