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
            GameManager.Instance.EventBus.PowerChanged += OnPowerChanged;
        }

        private void OnPowerChanged(object sender, PowerChangeEventArgs e)
        {
            PowerValue.GetComponent<Text>().text = string.Format("{0}%", e.NewPowerLevel.ToString("P"));

        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.PowerChanged -= OnPowerChanged;
        }
    }
}
