using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class PowerGemParticleEffect : UnityBehavior
    {
        public Color particleColor = Color.cyan;

        private ParticleSystem _particleSystem;
        private bool _shouldReset;
        private bool _hasStarted;
        private float _startTime;
        private PowerbarCollisionTargetController _particleTarget;
        private ParticleAttractorLinear _particleAttractorLinear;

        protected override void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleAttractorLinear = GetComponent<ParticleAttractorLinear>();
            var mainModule = _particleSystem.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(particleColor);
        }

        protected override void Update()
        {
            CheckForReset();
            MovePosition();
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

        private void MovePosition()
        {
            if (_hasStarted && _particleAttractorLinear.IsAttracting)
            {
                var calculateIncrementalMovement = _particleAttractorLinear.CalculateIncrementalMovement(transform.position);
                transform.position += calculateIncrementalMovement;
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
                _particleAttractorLinear.TargetProvider = _particleTarget;
            }
        }

        public void Reset(float startDelay, Vector2 emitterPosition, PowerbarCollisionTargetController target)
        {
            transform.position = emitterPosition;
            _particleTarget = target;
            _startTime = Time.time + startDelay + 0.2f;
            _shouldReset = true;
            _hasStarted = false;
        }
    }
}