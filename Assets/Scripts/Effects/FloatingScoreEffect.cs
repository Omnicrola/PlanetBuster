using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Effects
{
    public class FloatingScoreEffect : UnityBehavior
    {
        public float FloatSpeed = 10f;
        public float Timeout = 1f;

        private int _score;
        private bool _isDirty;
        private Text _uiText;
        private float _displayTime;
        private float _endTime;

        public void Reset(float delay, int score, Vector2 position)
        {
            transform.position = position;
            _score = score;
            _isDirty = true;

            var currentTime = Time.time;
            _displayTime = currentTime + delay;
            _endTime = currentTime + delay + Timeout;

        }


        protected override void Start()
        {
            _uiText = GetComponent<Text>();
        }

        protected override void Update()
        {
            UpdateText();
            FloatAlong();
            CheckForRecycling();
        }

        private void FloatAlong()
        {
            transform.position = transform.position.Translate(0, FloatSpeed, 0);
        }

        private void CheckForRecycling()
        {
            if (!isActiveAndEnabled && Time.time >= _displayTime)
            {
                gameObject.SetActive(true);
            }
            else if (isActiveAndEnabled && Time.time > _endTime)
            {
                GetComponent<PooledObject>().ObjectPool.ReturnObjectToPool(gameObject);
            }
        }

        private void UpdateText()
        {
            var isTimeForDisplay = Time.time >= _displayTime;
            if (!isTimeForDisplay)
            {
                _uiText.text = string.Empty;
            }
            if (_isDirty && isTimeForDisplay)
            {
                _isDirty = false;
                _uiText.text = _score.ToString();
            }
        }
    }
}