using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DestroyOnParticleFinish : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private bool _hasStartedEmitting;

        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _hasStartedEmitting = false;
        }

        void Update()
        {
            var particleCount = _particleSystem.particleCount;
            if (!_hasStartedEmitting && particleCount > 0)
            {
                _hasStartedEmitting = true;
            }
            if (_hasStartedEmitting && particleCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
