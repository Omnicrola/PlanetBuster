using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Scorekeeping
{
    public class HighScoreController : UnityBehavior
    {
        public GameObject ScoreText;

        protected override void Start()
        {
            var highScoreService = new HighScoreService();
            var scores = highScoreService.GetScores().OrderBy(s => s.Score);

            var stringBuilder = new StringBuilder();
            int i = 0;
            foreach (var highScore in scores)
            {
                stringBuilder.Append(i);
                stringBuilder.Append(")");
                stringBuilder.Append(highScore.Name);
                stringBuilder.Append(" ");
                stringBuilder.Append(highScore.Score);
            }
            var displayText = stringBuilder.ToString();
            ScoreText.GetComponent<Text>().text = displayText;
        }
    }
}