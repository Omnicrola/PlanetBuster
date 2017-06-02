using System.Linq;
using Assets.Scripts.Effects;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class GiantLaserController : UnityBehavior
    {
        private static readonly float MINIMUM_CHARGE = 2.0f;
        private static readonly float FIRE_RATE_PER_SECOND = 2.0f;

        public GameObject GiantLaserBeam;
        public GameObject BeamChargeupEffect;
        public AudioClip BeamAudio;

        private ParticleChargeupEffect _particleChargeupEffect;
        private AudioSource _audioSource;

        public bool IsCurrentlyActive { get; set; }
        public float ChargeLevel { get; set; }

        public bool IsAbleToFire
        {
            get { return ChargeLevel >= MINIMUM_CHARGE; }
        }

        protected override void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = BeamAudio;
            _particleChargeupEffect = BeamChargeupEffect.GetComponent<ParticleChargeupEffect>();
        }

        public void FreeChargeUp()
        {
            ChargeLevel = 2;
        }

        protected override void Update()
        {
            if (TouchWasStarted() || Input.GetMouseButtonDown(0))
            {
                if (!IsCurrentlyActive && IsAbleToFire)
                {
                    IsCurrentlyActive = true;
                    _particleChargeupEffect.Chargeup();
                    WaitForSeconds(_particleChargeupEffect.Duration, ActuallyFireLaser);
                }
            }
        }

        private void ActuallyFireLaser()
        {
            float totalDuration = ChargeLevel / FIRE_RATE_PER_SECOND;
            GiantLaserBeam.SetActive(true);
            _audioSource.Play();
            WaitForSeconds(totalDuration, StopFiring);
        }

        private void StopFiring()
        {
            GiantLaserBeam.SetActive(false);
            IsCurrentlyActive = false;
            _audioSource.Stop();
            ChargeLevel = 0;
        }


        private bool TouchWasStarted()
        {
            return Input.touches.Any(t => t.phase == TouchPhase.Began);
        }
    }
}