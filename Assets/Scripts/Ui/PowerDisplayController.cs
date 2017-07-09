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
        public float GrowthAmount = 1.0f;
        private ProgressBarController _progressBarController;
        private float _targetLevel;

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
            _targetLevel = e.NewPowerLevel;
        }

        protected override void Update()
        {
            if (_targetLevel > _progressBarController.Level)
            {
                _progressBarController.Level += GrowthAmount * Time.deltaTime;
            }
            else if (_targetLevel < _progressBarController.Level - GrowthAmount)
            {
                _progressBarController.Level -= GrowthAmount * Time.deltaTime * 2;
            }
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<PowerChangeEventArgs>(OnPowerChanged);
        }
    }
}
