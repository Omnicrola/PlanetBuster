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
            GameManager.Instance.EventBus.BallMatchFound += OnMatchFound;
            GameManager.Instance.EventBus.BallOrphansFound += OnOrphansFound;
            _scoreKeeper = new Scorekeeper();
        }
        private void UpdateScore()
        {
            ScoreText.GetComponent<Text>().text = _scoreKeeper.CurrentScore.ToString();
        }

        private void OnOrphansFound(object sender, OrphanedBallsEventArgs e)
        {
            _scoreKeeper.ScoreOrphans(e.OrphanedBalls);
            UpdateScore();
        }


        private void OnMatchFound(object sender, BallGridMatchArgs e)
        {
            _scoreKeeper.ScoreMatch(e.BallPath);
            UpdateScore();
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.BallMatchFound -= OnMatchFound;
            GameManager.Instance.EventBus.BallOrphansFound -= OnOrphansFound;
        }
    }
}
