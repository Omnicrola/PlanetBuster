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
    public class BallController : UnityBehavior, IBallController
    {
        public GameObject PowerGemSprite;
        public GameObject BallSprite;
        public GameObject DamageSprite;

        public BallType i_BallType;
        public BallMagnitude i_Magnitude;
        public bool i_HasPowerGem;


        private SpriteRenderer _ballSpriteRenderer;
        private bool _isProjectile;
        private bool _active;
        private readonly AngleOfImpactCalculator angleOfImpactCalculator = new AngleOfImpactCalculator();
        private Vector3 _baseScale;
        private float _maxHitpoints;
        private float _hitpoints;
        private bool _isDirty;

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

        public BallType BallType
        {
            get { return i_BallType; }
            set
            {
                i_BallType = value;
                _isDirty = true;
            }
        }

        public BallMagnitude Magnitude
        {
            get { return i_Magnitude; }
            set
            {
                i_Magnitude = value;
                MaxHitpoints = value.GetHitpoints();
                Hitpoints = MaxHitpoints;
                _isDirty = true;
            }
        }

        public bool HasPowerGem
        {
            get { return i_HasPowerGem; }
            set
            {
                i_HasPowerGem = value;
                _isDirty = true;
            }
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public float MaxHitpoints
        {
            get { return _maxHitpoints; }
            set { _maxHitpoints = value; }
        }

        public float Hitpoints
        {
            get { return _hitpoints; }
            set
            {
                _hitpoints = value;
                var percentAlive = 1 - (value / _maxHitpoints);
                DamageSprite.GetComponent<BallDamageController>().PercentDamaged = percentAlive;
            }
        }

        public Sprite CurrentBallSprite
        {
            get { return BallSprite.GetComponent<SpriteRenderer>().sprite; }
            set { BallSprite.GetComponent<SpriteRenderer>().sprite = value; }
        }

        protected override void Start()
        {
            base.Start();
            _ballSpriteRenderer = BallSprite.GetComponent<SpriteRenderer>();
            _baseScale = gameObject.transform.localScale;
        }

        protected override void Update()
        {
            if (_isDirty && isActiveAndEnabled)
            {
                _isDirty = false;
                _ballSpriteRenderer.sprite = BallType.GetSprite();
                Hitpoints = Magnitude.GetHitpoints();
                MaxHitpoints = Magnitude.GetHitpoints(); PowerGemSprite.SetActive(HasPowerGem);
                DamageSprite.SetActive(Magnitude != BallMagnitude.Standard);
                gameObject.transform.localScale = _baseScale * Magnitude.GetScale();
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

        public void MarkDirty()
        {
            _isDirty = true;
        }
    }
}