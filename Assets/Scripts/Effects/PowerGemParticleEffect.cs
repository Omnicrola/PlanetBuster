using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class PowerGemParticleEffect : UnityBehavior
    {
        public float speed = 5f;
        public Color particleColor = Color.cyan;

        private ParticleSystem _particleSystem;
        private ParticleSystem.Particle[] _particles;
        private int _particlesAlive;
        private Vector3 _targetPosition;
        private bool _shouldReset;

        protected override void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            var mainModule = _particleSystem.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(particleColor);
        }

        protected override void Update()
        {
            if (_shouldReset)
            {
                _shouldReset = false;
                _particleSystem.Clear();
                _particleSystem.Play();
            }
            _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
            _particlesAlive = _particleSystem.GetParticles(_particles);
            float step = speed * Time.deltaTime;
            for (int i = 0; i < _particlesAlive; i++)
            {
                _particles[i].position = Vector3.SlerpUnclamped(_particles[i].position, _targetPosition, step);
            }
            _particleSystem.SetParticles(_particles, _particlesAlive);
        }

        public void Reset(Vector3 emitterPosition, Vector3 targetPosition)
        {
            transform.position = emitterPosition;
            _targetPosition = targetPosition;
            _shouldReset = true;
        }
    }
}