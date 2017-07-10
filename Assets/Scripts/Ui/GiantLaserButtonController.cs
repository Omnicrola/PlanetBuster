using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Effects;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class GiantLaserButtonController : UnityBehavior
    {
        private Button _button;
        private GiantLaserButtonEffectController _giantLaserButtonEffectController;

        protected override void Start()
        {
            _button = GetComponent<Button>();
            _giantLaserButtonEffectController = GetComponent<GiantLaserButtonEffectController>();

            _button.enabled = false;
            _giantLaserButtonEffectController.IsActive = false;
            GameManager.Instance.EventBus.Subscribe<PowerChangeEventArgs>(PowerChanged);
        }

        private void PowerChanged(PowerChangeEventArgs e)
        {
            bool laserIsEnabled = e.NewPowerLevel >= GameConstants.LaserMinimumPercentCharge;
            _button.enabled = laserIsEnabled;

            _giantLaserButtonEffectController.IsActive = laserIsEnabled;
            _giantLaserButtonEffectController.SpeedScale = e.NewPowerLevel;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<PowerChangeEventArgs>(PowerChanged);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("PARTICLE IMPACT");
        }
    }
}
