using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
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

            var newLevel = GameManager.Instance.CurrentLevel.Instantiate();
            BallGridManager.GetComponent<IBallGridManager>().StartNewLevel(newLevel);

            GameManager.Instance.EventBus.Subscribe<GameOverEventArgs>(OnGameOver);
        }

        private void OnGameOver(IGameEvent e)
        {
            GameOverWindow.SetActive(true);
            GameOverWindow.GetComponent<GameOverWindowController>().UpdateStats();
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<GameOverEventArgs>(OnGameOver);
        }
    }
}