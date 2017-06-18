using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(SimpleObjectPool))]
    public class PowerGemParticleEffectGenerator : UnityBehavior
    {
        public GameObject ParticleTarget;
        public GameObject ParticleContainer;
        public GameObject Camera;

        private SimpleObjectPool _simpleObjectPool;
        private Camera _camera;
        private Vector3 _targetPosition;

        protected override void Start()
        {
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
            _camera = Camera.GetComponent<Camera>();
            _targetPosition = _camera.ScreenToWorldPoint(ParticleTarget.transform.position);

        }

        public void GenerateParticles(float delay, Vector2 position)
        {
            var newParticleEffect = _simpleObjectPool.GetObjectFromPool();
            newParticleEffect.transform.SetParent(ParticleContainer.transform);
            var particleEffect = newParticleEffect.GetComponent<PowerGemParticleEffect>();

            particleEffect.Reset(delay, position, _targetPosition);

        }
    }
}