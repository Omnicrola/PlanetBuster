using System.Collections;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BallDestroyedByLaserEffect : UnityBehavior
    {
        public float PlanetFadeTime = 1f;
        public GameObject Planet;
        public GameObject ParticleExplosion;
        public AudioClip BallLaserDeathSound;
        private bool _hasStartedEmitting;

        protected override void Start()
        {
        }

        protected override void Update()
        {
            var particleCount = ParticleExplosion.GetComponent<ParticleSystem>().particleCount;
            if (!_hasStartedEmitting && particleCount > 0)
            {
                _hasStartedEmitting = true;
            }
            if (_hasStartedEmitting && particleCount == 0)
            {
                var pooledObject = GetComponent<PooledObject>();
                if (pooledObject != null)
                {
                    pooledObject.ObjectPool.ReturnObjectToPool(gameObject);
                }
            }
        }

        public void RePlayEffect(Sprite planetSprite, float magnitudeScale)
        {
            gameObject.transform.localScale = new Vector3(magnitudeScale, magnitudeScale, magnitudeScale);
            ResetEffects(planetSprite);
            PlayEffects();
        }

        private void ResetEffects(Sprite planetSprite)
        {
            _hasStartedEmitting = false;
            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = BallLaserDeathSound;
            audioSource.time = 0;

            ParticleExplosion.GetComponent<ParticleSystem>().time = 0;

            Planet.GetComponent<SpriteRenderer>().sprite = planetSprite;
            Planet.SetActive(true);
        }

        private void PlayEffects()
        {
            ParticleExplosion.GetComponent<ParticleSystem>().Play();

            Hashtable ht = iTween.Hash(
           "from", 1,
           "to", 0,
           "time", PlanetFadeTime,
           "onupdate", "SetPlanetTransparency");
            iTween.ValueTo(gameObject, ht);
        }

        private void SetPlanetTransparency(float percent)
        {
            var material = Planet.GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, percent);
        }

        public void ResetTest()
        {
            RePlayEffect(Planet.GetComponent<SpriteRenderer>().sprite, 1);
        }
    }
}