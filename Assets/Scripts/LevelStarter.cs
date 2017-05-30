using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Ui;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelStarter : UnityBehavior
    {
        public GameObject BallGridManager;
        public GameObject PopupMenu;
        public GameObject GameOverWindow;

        protected override void Start()
        {
            PopupMenu.SetActive(false);
            GameOverWindow.SetActive(false);
            BallGridManager.GetComponent<IBallGridManager>().StartNewLevel();

            GameManager.Instance.EventBus.GameOver += OnGameOver;
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            GameOverWindow.SetActive(true);
            GameOverWindow.GetComponent<GameOverWindowController>().UpdateStats();
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.GameOver -= OnGameOver;
        }
    }
}
