using System;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class LauncherController : UnityBehavior
    {
        public float ProjectileSpeed = 1;
        public float RotationSpeed = 4;
        public float ShotDelay = .5f;

        public GameObject Camera;
        public GameObject BallContainer;
        public GameObject NextProjectile;
        public GameObject ParticleEmitter;


        private int _nextProjectileType;
        private ParticleSystem _particleSystem;
        private LauncherFireControlCenter _launcherFireControlCenter;
        private Camera _mainCamera;
        private double _lastShotTime;

        protected override void Start()
        {
            _mainCamera = Camera.GetComponent<Camera>();
            _particleSystem = ParticleEmitter.GetComponent<ParticleSystem>();
            _particleSystem.Stop();

            GameManager.Instance.EventBus.GameStart += OnGameStart;
            _launcherFireControlCenter = new LauncherFireControlCenter(transform, _mainCamera,
                ProjectileSpeed);
        }

        private void OnGameStart(object sender, EventArgs e)
        {
            GenerateNextBall();
        }


        protected override void Update()
        {
            if (TouchWasReleased() || Input.GetMouseButtonUp(0))
            {
                if (Time.time - _lastShotTime >= ShotDelay)
                {
                    _lastShotTime = Time.time;
                    _launcherFireControlCenter.Fire(_nextProjectileType);
                    EmitParticles();
                    GenerateNextBall();
                }
            }
        }

        private void EmitParticles()
        {
            _particleSystem.Play();
            WaitForSeconds(0.1f, () => _particleSystem.Stop());
        }

        private void GenerateNextBall()
        {
            _nextProjectileType = GameManager.Instance.GetNextBallType();
            var ballSprite = GameManager.Instance.GetBallSpriteOfType(_nextProjectileType);

            NextProjectile.GetComponent<NextBallController>().SetNextBall(ballSprite);
        }


        private bool TouchWasReleased()
        {
            return Input.touches.Any(t => t.phase == TouchPhase.Ended);
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.GameStart -= OnGameStart;
        }
    }
}