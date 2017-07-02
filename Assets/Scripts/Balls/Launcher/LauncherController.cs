using System;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Balls.Launcher
{
    public class LauncherController : UnityBehavior
    {
        public float ProjectileSpeed = 1;
        public float RotationSpeed = 4;
        public float ShotDelay = .5f;
        public Vector3 BallPositionOffset;

        public GameObject Camera;
        public GameObject BallGridManager;
        public GameObject BallContainer;
        public GameObject NextProjectile;
        public GameObject ParticleEmitter;


        private BallType _nextProjectileType;
        private ParticleSystem _particleSystem;
        private LauncherFireControlCenter _launcherFireControlCenter;
        private Camera _mainCamera;
        private double _lastShotTime;
        private IBallGridManager _ballGridManager;
        private GiantLaserController _giantLaserControl;

        protected override void Start()
        {
            _mainCamera = Camera.GetComponent<Camera>();
            _giantLaserControl = GetComponent<GiantLaserController>();
            _ballGridManager = BallGridManager.GetComponent<IBallGridManager>();
            _particleSystem = ParticleEmitter.GetComponent<ParticleSystem>();
            _particleSystem.Stop();

            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<GameStartEventArgs>(OnGameStart);
            gameEventBus.Subscribe<GameInputEventArgs>(OnInputEvent);

            _launcherFireControlCenter = new LauncherFireControlCenter(transform, _mainCamera, _ballGridManager,
                ProjectileSpeed, BallPositionOffset);
        }

        private void OnGameStart(IGameEvent e)
        {
            GenerateNextBall();
        }

        private void OnInputEvent(GameInputEventArgs eventArgs)
        {
            if (eventArgs.EventType == InputEventType.Release)
            {
                var enoughTimeHasPassed = Time.time - _lastShotTime >= ShotDelay;
                if (enoughTimeHasPassed && !_giantLaserControl.IsCurrentlyActive)
                {
                    _lastShotTime = Time.time;
                    _launcherFireControlCenter.Fire(_nextProjectileType, eventArgs.MousePosition);
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
            _nextProjectileType = _ballGridManager.GetNextBallType();
            var ballSprite = _nextProjectileType.GetSprite();

            NextProjectile.GetComponent<NextBallController>().SetNextBall(ballSprite);
        }


        protected override void OnDestroy()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<GameStartEventArgs>(OnGameStart);
            gameEventBus.Unsubscribe<GameInputEventArgs>(OnInputEvent);

        }
    }
}