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
        public AudioClip[] BallMatch;
        public AudioClip LauncherEffect;
        public AudioClip ScoreEffect;
        public AudioClip BonusScoreEffect;

        private readonly Random _random = new Random();
        private AudioSource _launcherEffectAudioSource;
        private AudioSource _ballMatchAudioSource;

        protected override void Start()
        {
            _launcherEffectAudioSource = gameObject.AddComponent<AudioSource>();
            _ballMatchAudioSource = gameObject.AddComponent<AudioSource>();
            GameManager.Instance.EventBus.BallFired += OnBallFired;
            GameManager.Instance.EventBus.BallMatchFound += OnBallMatch;
        }

        private void OnBallMatch(object sender, BallGridMatchArgs e)
        {
            foreach (var ballController in e.BallPath)
            {
                var audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = BallMatch[_random.Next(BallMatch.Length)];
                audioSource.playOnAwake = false;
                float delay = (float)(_random.NextDouble() * .25f);
                WaitForSeconds(delay, () => audioSource.Play());
                WaitForSeconds(delay + audioSource.clip.length, () => Destroy(audioSource));
            }
        }

        private void OnBallFired(object sender, EventArgs e)
        {
            _launcherEffectAudioSource.clip = LauncherEffect;
            _launcherEffectAudioSource.Play();
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.BallFired -= OnBallFired;
            GameManager.Instance.EventBus.BallMatchFound -= OnBallMatch;
        }
    }
}