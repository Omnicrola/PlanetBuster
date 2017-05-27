using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BallDestroyEffect : UnityBehavior
    {
        public float ShockwaveTime = 1f;
        public float ShockwaveSize = 1f;
        public GameObject Shockwave;


        private bool _hasStartedEmitting;

        protected override void Start()
        {
        }

        public void PlayEffect()
        {
            ResetEffects();
            PlayEffects();
        }

        private void PlayEffects()
        {
            iTween.ScaleTo(Shockwave, new Vector3(ShockwaveSize, ShockwaveSize, ShockwaveSize), ShockwaveTime);
            iTween.FadeTo(Shockwave, 0f, ShockwaveTime * 0.9f);
            GetComponent<ParticleSystem>().Play();
        }

        private void ResetEffects()
        {
            _hasStartedEmitting = false;
            var shockwaveMaterial = Shockwave.GetComponent<Renderer>().material;
            var existingColor = shockwaveMaterial.color;
            shockwaveMaterial.color = new Color(existingColor.r, existingColor.g, existingColor.b, 1f);

            Shockwave.transform.localScale = Vector3.zero;
            GetComponent<ParticleSystem>().time = 0;
        }

        protected override void Update()
        {
            var particleCount = GetComponent<ParticleSystem>().particleCount;
            if (!_hasStartedEmitting && particleCount > 0)
            {
                _hasStartedEmitting = true;
            }
            if (_hasStartedEmitting && particleCount == 0)
            {
                GetComponent<PooledObject>().ObjectPool.ReturnObjectToPool(gameObject);
            }
        }
    }
}