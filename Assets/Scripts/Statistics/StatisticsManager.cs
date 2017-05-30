using System;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Scorekeeping;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Statistics
{
    public class StatisticsManager : UnityBehavior, IStatsManager
    {
        public TimeSpan ElapsedTime
        {
            get
            {
                return new TimeSpan(0, 0, 0, (int)_timeElapsed);
            }
        }

        public int TotalScore
        {
            get { return _scorekeeper.CurrentScore; }
        }

        public GameObject ScoreManager;

        public int BallsLaunched { get; private set; }
        public int TotalMatches { get; private set; }
        public int LargestMatch { get; private set; }
        public int BallsOrphaned { get; private set; }

        private float _timeElapsed = 0;
        private ScoreDisplayController _scorekeeper;

        protected override void Start()
        {
            _scorekeeper = ScoreManager.GetComponent<ScoreDisplayController>();
            GameManager.Instance.EventBus.BallMatchFound += OnMatchFound;
            GameManager.Instance.EventBus.BallOrphansFound += OnOrphansFound;
            GameManager.Instance.EventBus.BallFired += OnBallFired;

        }

        protected override void Update()
        {
            _timeElapsed += Time.deltaTime;
        }

        private void OnBallFired(object sender, EventArgs e)
        {
            BallsLaunched++;
        }

        private void OnOrphansFound(object sender, OrphanedBallsEventArgs e)
        {
            BallsOrphaned += e.OrphanedBalls.Count;
        }

        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            TotalMatches++;
            if (e.BallPath.Count > LargestMatch)
            {
                LargestMatch = e.BallPath.Count;
            }
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.BallMatchFound -= OnMatchFound;
            GameManager.Instance.EventBus.BallOrphansFound -= OnOrphansFound;
            GameManager.Instance.EventBus.BallFired -= OnBallFired;
        }
    }
}