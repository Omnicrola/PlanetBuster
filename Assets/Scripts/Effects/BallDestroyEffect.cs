using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Effects
{
    public class BallDestroyEffect : UnityBehavior
    {
        private static readonly Random _random = new Random();

        public float ShockwaveTime = 0.5f;
        public float ShockwaveSize = 0.5f;
        public GameObject Shockwave;
        public GameObject Planet;
        public GameObject PowerGem;
        public GameObject ParticleExplosion;
        public AudioClip[] BallMatchSound;


        private bool _hasStartedEmitting;
        private float _delay;

        protected override void Start()
        {
        }

        public void RePlayEffect(float delay, Sprite planet, bool hasPowerGem)
        {
            _delay = delay;
            ResetEffects(planet, hasPowerGem);

            WaitForSeconds(delay, PlayEffects);
        }

        private void PlayEffects()
        {
            iTween.ScaleTo(Shockwave, new Vector3(ShockwaveSize, ShockwaveSize, ShockwaveSize), ShockwaveTime);
            iTween.FadeTo(Shockwave, 0f, ShockwaveTime * 0.9f);

            WaitForSeconds(0.2f, () =>
            {
                Planet.SetActive(false);
                PowerGem.SetActive(false);
            });
            ParticleExplosion.GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().Play();
        }

        private void ResetEffects(Sprite planet, bool hasPowerGem)
        {
            _hasStartedEmitting = false;
            var shockwaveMaterial = Shockwave.GetComponent<Renderer>().material;
            var existingColor = shockwaveMaterial.color;
            shockwaveMaterial.color = new Color(existingColor.r, existingColor.g, existingColor.b, 1f);

            Shockwave.transform.localScale = Vector3.zero;
            ParticleExplosion.GetComponent<ParticleSystem>().time = 0;

            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = BallMatchSound[_random.Next(BallMatchSound.Length)];
            audioSource.time = 0;

            Planet.GetComponent<SpriteRenderer>().sprite = planet;
            Planet.SetActive(true);

            PowerGem.SetActive(hasPowerGem);
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
                GetComponent<PooledObject>().ObjectPool.ReturnObjectToPool(gameObject);
            }
        }
    }
}