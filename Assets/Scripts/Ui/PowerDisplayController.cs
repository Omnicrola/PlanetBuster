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

        protected override void Start()
        {
            GameManager.Instance.EventBus.Subscribe<PowerChangeEventArgs>(OnPowerChanged);
        }

        private void OnPowerChanged(PowerChangeEventArgs e)
        {
            PowerMeter.GetComponent<ProgressBarController>().Level = e.NewPowerLevel;

        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<PowerChangeEventArgs>(OnPowerChanged);
        }
    }
}
