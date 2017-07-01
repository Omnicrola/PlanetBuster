﻿using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class PowerGemParticleEffect : UnityBehavior
    {
        public Color particleColor = Color.cyan;

        private ParticleSystem _particleSystem;
        private bool _shouldReset;
        private bool _hasStarted;
        private Vector3 _targetPosition;
        private float _startTime;

        protected override void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            var mainModule = _particleSystem.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(particleColor);
        }

        protected override void Update()
        {
            CheckForReset();
            CheckForDone();
        }

        private void CheckForDone()
        {
            if (_hasStarted)
            {
                if (_particleSystem.particleCount == 0)
                {
                    var pooledObject = GetComponent<PooledObject>();
                    pooledObject.ObjectPool.ReturnObjectToPool(gameObject);
                }
            }
            else
            {
                if (_particleSystem.particleCount > 0)
                {
                    _hasStarted = true;
                }
            }
        }

        private void CheckForReset()
        {
            if (_shouldReset && Time.time >= _startTime)
            {
                _shouldReset = false;
                _particleSystem.Clear();
                _particleSystem.Play();
                _hasStarted = false;
                GetComponent<ParticleAttractorLinear>().TargetPosition = _targetPosition;
            }
        }

        public void Reset(float startDelay, Vector2 emitterPosition, Vector2 targetPosition)
        {
            transform.position = emitterPosition;
            _startTime = Time.time + startDelay + 0.2f;
            _targetPosition = targetPosition;
            _shouldReset = true;
            _hasStarted = false;
        }
    }
}