using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class PowerGemParticleEffect : UnityBehavior
    {
        public float speed = 5f;
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
                GetComponent<ParticleAttractorSpherical>().target = _targetPosition;
            }
        }

        public void Reset(float startDelay, Vector3 emitterPosition, Vector3 targetPosition)
        {
            transform.position = emitterPosition;
            _startTime = Time.time + startDelay;
            _targetPosition = targetPosition;
            _shouldReset = true;
        }
    }
}