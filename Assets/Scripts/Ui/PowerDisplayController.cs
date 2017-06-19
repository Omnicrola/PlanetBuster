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
            GameManager.Instance.EventBus.Subscribe<PowerChangeEventArgs>(OnPowerChanged);
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
