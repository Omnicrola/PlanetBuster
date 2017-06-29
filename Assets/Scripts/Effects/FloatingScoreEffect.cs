using System.Collections;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Extensions;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Effects
{
    public class FloatingScoreEffect : UnityBehavior
    {
        public float TravelDistance = -3f;
        public float Duration = 1f;

        private int _score;
        private bool _isDirty;
        private Text _uiText;

        public void Reset(float delay, int score, Vector2 targetPosition)
        {
            transform.position = targetPosition;
            SetTransparency(0);

            Hashtable positionHash = iTween.Hash(
                "from", targetPosition.y,
                "to", targetPosition.y + TravelDistance,
                "time", Duration,
                "delay", delay,
                "easetype", iTween.EaseType.easeInQuart,
                "onupdate", "SetScorePosition",
                "oncomplete", "Recycle");
            iTween.ValueTo(gameObject, positionHash);

            Hashtable transparencyHash = iTween.Hash(
                "from", 1,
                "to", 0,
                "time", Duration,
                "delay", delay,
                "onupdate", "SetTransparency");
            iTween.ValueTo(gameObject, transparencyHash);

            _score = score;
            _isDirty = true;
        }

        private void SetScorePosition(float value)
        {
            transform.position = new Vector2(transform.position.x, value);
        }

        private void SetTransparency(float percent)
        {
            var canvasRenderer = GetComponent<CanvasRenderer>();
            var color = canvasRenderer.GetColor();
            canvasRenderer.SetColor(new Color(color.r, color.g, color.b, percent));
        }

        protected override void Start()
        {
            _uiText = GetComponent<Text>();
        }

        protected override void Update()
        {
            if (_isDirty)
            {
                _isDirty = false;
                _uiText.text = _score.ToString();
            }
        }

        private void Recycle()
        {
            GetComponent<PooledObject>().ObjectPool.ReturnObjectToPool(gameObject);
        }
    }
}