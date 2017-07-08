using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class PowerDisplayController : UnityBehavior
    {
        public GameObject PowerMeter;
        private ProgressBarController _progressBarController;

        protected override void Start()
        {
            _progressBarController = PowerMeter.GetComponent<ProgressBarController>();
            var gameEventBus = GameManager.Instance.EventBus;
            gameEventBus.Subscribe<PowerChangeEventArgs>(OnPowerChanged);
            gameEventBus.Subscribe<GameStartEventArgs>(OnGameStart);
        }

        private void OnGameStart(GameStartEventArgs obj)
        {
            var levelDataController = GameManager.Instance.CurrentLevel;
            var shouldShowPowerBar = levelDataController.ShouldShowPowerbar();
            PowerMeter.SetActive(shouldShowPowerBar);
        }

        private void OnPowerChanged(PowerChangeEventArgs e)
        {
            var currentLevel = _progressBarController.Level;
            Hashtable ht = iTween.Hash(
                   "from", currentLevel,
                   "to", e.NewPowerLevel,
                   "time", 1f,
                   "onupdate", "IncrementChargeLevel");
            iTween.ValueTo(gameObject, ht);


        }

        void IncrementChargeLevel(float newValue)
        {
            _progressBarController.Level = newValue;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<PowerChangeEventArgs>(OnPowerChanged);
        }
    }
}
