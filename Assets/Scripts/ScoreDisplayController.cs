using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreDisplayController : UnityBehavior
    {
        public GameObject ScoreText;
        private Scorekeeper _scoreKeeper;

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
    }
}
