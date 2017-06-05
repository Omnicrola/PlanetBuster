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
        private Scorekeeper _scoreKeeper;
        public int CurrentScore { get { return _scoreKeeper.CurrentScore; } }

        protected override void Start()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<BallGridMatchArgs>(OnMatchFound);
            gameEventBus.Subscribe<OrphanedBallsEventArgs>(OnOrphansFound);
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            _scoreKeeper = new Scorekeeper();
        }


        private void UpdateScore()
        {
            ScoreText.GetComponent<Text>().text = _scoreKeeper.CurrentScore.ToString();
        }

        private void OnOrphansFound(OrphanedBallsEventArgs e)
        {
            _scoreKeeper.ScoreOrphans(e.OrphanedBalls);
            UpdateScore();
        }


        private void OnMatchFound(BallGridMatchArgs e)
        {
            _scoreKeeper.ScoreMatch(e.BallPath);
            UpdateScore();
        }
        private void OnBallDestroyed(BallDestroyEventArgs obj)
        {
            _scoreKeeper.ScoreDestroyedBall();
            UpdateScore();
        }

        protected override void OnDestroy()
        {
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Unsubscribe<BallGridMatchArgs>(OnMatchFound);
            gameEventBus.Subscribe<BallDestroyEventArgs>(OnBallDestroyed);
            gameEventBus.Unsubscribe<OrphanedBallsEventArgs>(OnOrphansFound);
        }
    }
}
