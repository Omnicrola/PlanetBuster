using System;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Effects
{
    public class BallAudioEffects : UnityBehavior
    {
        public AudioClip LauncherEffect;
        public AudioClip ScoreEffect;
        public AudioClip BonusScoreEffect;

        private readonly Random _random = new Random();
        private AudioSource _launcherEffectAudioSource;

        protected override void Start()
        {
            _launcherEffectAudioSource = gameObject.AddComponent<AudioSource>();
            GameManager.Instance.EventBus.BallFired += OnBallFired;
        }


        private void OnBallFired(object sender, EventArgs e)
        {
            _launcherEffectAudioSource.clip = LauncherEffect;
            _launcherEffectAudioSource.Play();
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.BallFired -= OnBallFired;
        }
    }
}