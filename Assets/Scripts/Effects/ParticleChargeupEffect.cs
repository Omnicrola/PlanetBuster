using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class ParticleChargeupEffect : UnityBehavior
    {
        public float Duration = 1.5f;
        public AudioClip ChargeupSound;

        private ParticleSystem _particleSystem;
        private AudioSource _audioSource1;
        private AudioSource _audioSource2;
        private AudioSource _audioSource3;
        private AudioSource _audioSource4;
        private AudioSource _audioSource5;

        protected override void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _audioSource1 = gameObject.AddComponent<AudioSource>();
            _audioSource2 = gameObject.AddComponent<AudioSource>();
            _audioSource3 = gameObject.AddComponent<AudioSource>();
            _audioSource4 = gameObject.AddComponent<AudioSource>();
            _audioSource5 = gameObject.AddComponent<AudioSource>();

            _audioSource1.clip = ChargeupSound;
            _audioSource2.clip = ChargeupSound;
            _audioSource3.clip = ChargeupSound;
            _audioSource4.clip = ChargeupSound;
            _audioSource5.clip = ChargeupSound;

        }

        public void Chargeup()
        {
            _particleSystem.time = 0;
            _particleSystem.Play();

            _audioSource1.PlayDelayed(0f);
            _audioSource2.PlayDelayed(0.5f);
            _audioSource3.PlayDelayed(0.75f);
            _audioSource4.PlayDelayed(1f);
            _audioSource5.PlayDelayed(1.25f);
        }
    }
}
