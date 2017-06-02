using System.Collections;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Effects;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class GiantLaserController : UnityBehavior
    {
        public GameObject GiantLaserBeam;
        public GameObject BeamChargeupEffect;
        public AudioClip BeamAudio;

        private ParticleChargeupEffect _particleChargeupEffect;
        private AudioSource _audioSource;
        private bool _isPrimedToFire;

        public bool IsCurrentlyActive { get; set; }
        public float ChargeLevel { get; private set; }

        public bool IsAbleToFire
        {
            get { return ChargeLevel >= GameConstants.LaserMinimumPercentCharge; }
        }

        protected override void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.clip = BeamAudio;
            _particleChargeupEffect = BeamChargeupEffect.GetComponent<ParticleChargeupEffect>();

            GameManager.Instance.EventBus.BallMatchFound += OnBallMatch_ChargeLaser;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.BallMatchFound -= OnBallMatch_ChargeLaser;
        }

        private void OnBallMatch_ChargeLaser(object sender, BallGridMatchArgs e)
        {
            bool changedPower = false;
            foreach (var ballController in e.BallPath)
            {
                if (ballController.Model.HasPowerGem)
                {
                    ChargeLevel += GameConstants.LaserChargePercentPerGem;
                    changedPower = true;
                }
            }
            if (changedPower)
            {
                GameManager.Instance.EventBus.BroadcastPowerChanged(this, new PowerChangeEventArgs(ChargeLevel));
            }
        }

        public void FireGiantLaser()
        {
            if (IsAbleToFire)
            {
                _isPrimedToFire = true;
            }
        }

        protected override void Update()
        {
            if (TouchWasStarted() || Input.GetMouseButtonDown(0))
            {
                if (_isPrimedToFire && !IsCurrentlyActive && IsAbleToFire)
                {
                    IsCurrentlyActive = true;
                    _isPrimedToFire = false;
                    _particleChargeupEffect.Chargeup();
                    WaitForSeconds(_particleChargeupEffect.Duration, ActuallyFireLaser);
                }
            }
        }

        private void ActuallyFireLaser()
        {
            float totalDuration = ChargeLevel / GameConstants.LaserDrainPercentPerSecond;
            GiantLaserBeam.SetActive(true);
            _audioSource.Play();
            WaitForSeconds(totalDuration, StopFiring);

            Hashtable ht = iTween.Hash("from", ChargeLevel, "to", 0, "time", totalDuration, "onupdate",
                "DecrementChargeLevel");
            iTween.ValueTo(gameObject, ht);
        }

        void DecrementChargeLevel(float newValue)
        {
            ChargeLevel = newValue;
            GameManager.Instance.EventBus.BroadcastPowerChanged(this, new PowerChangeEventArgs(ChargeLevel));
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