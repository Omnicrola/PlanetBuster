using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class PowerDisplayController : UnityBehavior
    {
        public GameObject PowerValue;

        protected override void Start()
        {
            GameManager.Instance.EventBus.Subscribe<PowerChangeEventArgs>(OnPowerChanged);
        }

        private void OnPowerChanged(PowerChangeEventArgs e)
        {
            PowerValue.GetComponent<Text>().text = string.Format("{0}%", e.NewPowerLevel.ToString("P"));

        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<PowerChangeEventArgs>(OnPowerChanged);
        }
    }
}
