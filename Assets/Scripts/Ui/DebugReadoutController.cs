using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls.Launcher;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class DebugReadoutController : UnityBehavior
    {
        public GameObject TextReadout;
        public GameObject GiantLaser;

        private readonly Dictionary<string, string> debugValues = new Dictionary<String, String>();
        private Text _debugText;
        private bool _isDirty;

        protected override void Start()
        {
            _debugText = TextReadout.GetComponent<Text>();
            TextReadout.SetActive(false);
            GameManager.Instance.EventBus.Subscribe<DebugEventArgs>(OnDebugEvent);
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<DebugEventArgs>(OnDebugEvent);
        }

        private void OnDebugEvent(DebugEventArgs debugEventArgs)
        {
            _isDirty = true;
            debugValues[debugEventArgs.EventName] = debugEventArgs.EventValue;
        }

        protected override void Update()
        {
            if (_isDirty && TextReadout.activeInHierarchy)
            {
                _isDirty = false;
                _debugText.text = "Debug\n" + GenerateText();
            }
            if (Input.GetKeyUp(KeyCode.F3))
            {
                TextReadout.SetActive(!TextReadout.activeInHierarchy);
            }
        }

        private string GenerateText()
        {
            return debugValues.Select((k, v) => k + ": " + v)
                .Aggregate((aggregate, element) => aggregate + "\n" + element);
        }

        public void OnClick_FullyChargeLaser()
        {
            GiantLaser.GetComponent<GiantLaserController>().ChargeLevel = 1f;
        }
    }

    public class DebugEventArgs : IGameEvent
    {
        public DebugEventArgs(string eventName, string eventValue)
        {
            EventValue = eventValue;
            EventName = eventName;
        }

        public string EventName { get; private set; }
        public string EventValue { get; private set; }
    }
}