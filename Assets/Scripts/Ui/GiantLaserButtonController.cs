using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class GiantLaserButtonController : UnityBehavior
    {
        private Button _button;

        protected override void Start()
        {
            _button = GetComponent<Button>();
            _button.enabled = false;
            GameManager.Instance.EventBus.Subscribe<PowerChangeEventArgs>(PowerChanged);
        }

        private void PowerChanged(PowerChangeEventArgs e)
        {
            bool laserIsEnabled = e.NewPowerLevel >= GameConstants.LaserMinimumPercentCharge;
            _button.enabled = laserIsEnabled;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<PowerChangeEventArgs>(PowerChanged);
        }
    }
}
