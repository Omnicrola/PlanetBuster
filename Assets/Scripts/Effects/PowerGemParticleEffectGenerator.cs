using Assets.Scripts.Core;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [RequireComponent(typeof(SimpleObjectPool))]
    public class PowerGemParticleEffectGenerator : UnityBehavior
    {
        public PowerbarCollisionTargetController ParticleTarget;
        public GameObject ParticleContainer;

        private SimpleObjectPool _simpleObjectPool;

        protected override void Start()
        {
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
        }

        public void GenerateParticles(float delay, Vector2 position)
        {
            var newParticleEffect = _simpleObjectPool.GetObjectFromPool();
            newParticleEffect.transform.SetParent(ParticleContainer.transform);
            var particleEffect = newParticleEffect.GetComponent<PowerGemParticleEffect>();

            particleEffect.Reset(delay, position, ParticleTarget);
            Debug.Log("** CREATED " + newParticleEffect.name);
        }

    }
}