using System;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.Statistics;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class GameOverWindowController : UnityBehavior
    {
        public GameObject StatsText;
        public GameObject TitleText;
        public GameObject StatisticsManager;


        public void UpdateStats()
        {
            var statsManager = StatisticsManager.GetComponent<IStatsManager>();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Time : ");
            stringBuilder.Append(statsManager.ElapsedTime);

            stringBuilder.Append("\nScore : ");
            stringBuilder.Append(statsManager.TotalScore);

            stringBuilder.Append("\nPlanets Launched : ");
            stringBuilder.Append(statsManager.BallsLaunched);

            stringBuilder.Append("\nMatches Made : ");
            stringBuilder.Append(statsManager.TotalMatches);

            stringBuilder.Append("\nBiggest Match : ");
            stringBuilder.Append(statsManager.LargestMatch);

            StatsText.GetComponent<Text>().text = stringBuilder.ToString();
        }

        public void OnClick_GotoMainMenu()
        {
            SceneManager.LoadScene(GameConstants.SceneNames.MainMenu);
        }

    }
}
