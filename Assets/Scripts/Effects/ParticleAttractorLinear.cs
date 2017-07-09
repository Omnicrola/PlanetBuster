using System;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleAttractorLinear : MonoBehaviour
    {
        public float speed = 5f;
        public float attractionDelay = 1f;

        public IParticleTargetProvider TargetProvider { get; set; }

        private ParticleSystem _particleSystem;
        private ParticleSystem.Particle[] _particles;
        private float _lifetimeThreshold;
        private bool _isAttracting;

        void Start()
        {
            TargetProvider = new EmptyTargetProvider();
            _particleSystem = GetComponent<ParticleSystem>();
            _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
            _lifetimeThreshold = _particleSystem.main.startLifetimeMultiplier - attractionDelay;
        }

        void Update()
        {
            var totalParticles = _particleSystem.GetParticles(_particles);
            var currentTargetPosition = TargetProvider.TargetPosition;

            for (int i = 0; i < totalParticles; i++)
            {
                if (_particles[i].remainingLifetime < _lifetimeThreshold)
                {
                    var currentPosition = _particles[i].position;

                    var incrementalMovement = CalculateIncrementalMovement(currentPosition, currentTargetPosition);
                    _particles[i].position += incrementalMovement;
                    _isAttracting = true;
                }
                else
                {
                    _isAttracting = false;
                }
            }
            _particleSystem.SetParticles(_particles, totalParticles);
        }

        private Vector3 CalculateIncrementalMovement(Vector3 currentPosition, Vector3 targetPosition)
        {
            float step = speed * Time.deltaTime;
            var incrementalMovement = targetPosition - currentPosition;
            incrementalMovement.Normalize();
            incrementalMovement.Scale(new Vector3(step, step, 0));
            return incrementalMovement;
        }

        public Vector3 CalculateIncrementalMovement(Vector3 currentPosition)
        {
            var currentTargetPosition = TargetProvider.TargetPosition;
            return CalculateIncrementalMovement(currentPosition, currentTargetPosition);
        }
        public bool IsAttracting { get { return _isAttracting; } }
    }

    internal class EmptyTargetProvider : IParticleTargetProvider
    {
        public Vector3 TargetPosition { get { return Vector3.zero; } }
    }
}