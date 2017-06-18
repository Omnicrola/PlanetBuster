using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scorekeeping
{
    public class ScoreDisplayController : UnityBehavior
    {
        public GameObject ScoreText;

        public int CurrentScore
        {
            get { return _scoreKeeper.CurrentScore; }
        }

        private Scorekeeper _scoreKeeper;
        private float _currentDisplayScore;

        protected override void Start()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallGridMatchArgs>(OnMatchFound);
            gameEventBus.Subscribe<OrphanedBallsEventArgs>(OnOrphansFound);
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            _scoreKeeper = new Scorekeeper();
        }


        protected override void Update()
        {
            var currentScore = _scoreKeeper.CurrentScore;
            _currentDisplayScore = iTween.FloatUpdate(_currentDisplayScore, currentScore, 10f);
            if (currentScore - _currentDisplayScore < 10)
            {
                _currentDisplayScore = currentScore;
            }
            ScoreText.GetComponent<Text>().text = ((int)_currentDisplayScore).ToString();
        }

        private void OnOrphansFound(OrphanedBallsEventArgs e)
        {
            _scoreKeeper.ScoreOrphans(e.OrphanedBalls);
        }


        private void OnMatchFound(BallGridMatchArgs e)
        {
            _scoreKeeper.ScoreMatch(e.BallPath);
        }

        private void OnBallDestroyed(BallDestroyEventArgs obj)
        {
            _scoreKeeper.ScoreDestroyedBall();
        }

        protected override void OnDestroy()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallGridMatchArgs>(OnMatchFound);
            gameEventBus.Unsubscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Unsubscribe<OrphanedBallsEventArgs>(OnOrphansFound);
        }
    }
}