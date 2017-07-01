using System;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleAttractorLinear : MonoBehaviour
    {
        public Transform Target;
        public float speed = 5f;
        public float attractionDelay = 1f;

        public Vector3 TargetPosition { get; set; }

        private ParticleSystem _particleSystem;
        private ParticleSystem.Particle[] _particles;
        private float _lifetimeThreshold;

        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
            _lifetimeThreshold = _particleSystem.main.startLifetimeMultiplier - attractionDelay;
        }

        void Update()
        {
            var totalParticles = _particleSystem.GetParticles(_particles);
            float step = speed * Time.deltaTime;
            var currentTargetPosition = Target == null ? TargetPosition : Target.position;
            for (int i = 0; i < totalParticles; i++)
            {
                if (_particles[i].remainingLifetime < _lifetimeThreshold)
                {
                    var currentPosition = _particles[i].position;

                    var directionToTarget = currentTargetPosition - currentPosition;
                    directionToTarget.Normalize();
                    directionToTarget.Scale(new Vector3(step, step, 0));
                    _particles[i].position += directionToTarget;
                }
            }
            _particleSystem.SetParticles(_particles, totalParticles);
        }
    }
}