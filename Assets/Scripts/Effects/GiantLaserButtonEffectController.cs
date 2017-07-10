using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Effects
{
    public class GiantLaserButtonEffectController : UnityBehavior
    {
        public GameObject ActivationWave;
        public Transform ParticlePivot;
        public Color WaveColor;

        public float WaveStartScale = 1f;
        public float WaveEndScale = 10f;

        public float StartingRotationSpeed = 1f;
        public float MaxRotationSpeed = 2f;

        private bool _isActive;

        public float SpeedScale { get; set; }
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                var oldValue = _isActive;
                _isActive = value;
                if (value && !oldValue)
                {
                    PlayEffects();
                }
                else if (!value && oldValue)
                {
                    HideEffects();
                }
            }
        }


        public void Test()
        {
            IsActive = true;
        }

        private void PlayEffects()
        {
            ActivationWave.SetActive(true);
            ActivationWave.transform.localScale = new Vector3(WaveStartScale, WaveStartScale, WaveStartScale);
            var scale = new Vector3(WaveEndScale, WaveEndScale, WaveEndScale);

            var scaleHt = iTween.Hash(
                "scale", scale,
                "easetype", iTween.EaseType.easeInQuad,
                "time", 1f);
            iTween.ScaleTo(ActivationWave, scaleHt);

            var colorHt = iTween.Hash(
                "from", 1f,
                "to", 0f,
                "time", 1f,
                "easetype", iTween.EaseType.easeInQuad,
                "onupdatetarget", gameObject,
                "onupdate", "UpdateWaveColor");
            iTween.ValueTo(ActivationWave, colorHt);
        }

        private void UpdateWaveColor(float alpha)
        {
            ActivationWave.GetComponent<Image>().color = new Color(WaveColor.r, WaveColor.g, WaveColor.b, alpha);
        }

        private void HideEffects()
        {
            ActivationWave.gameObject.SetActive(false);
        }

        protected override void Start()
        {
            HideEffects();
        }

        protected override void Update()
        {
            float extraSpeed = (MaxRotationSpeed - StartingRotationSpeed) * SpeedScale;
            var rotationRate = (StartingRotationSpeed + extraSpeed) * Time.deltaTime;
            ParticlePivot.Rotate(Vector3.forward, rotationRate);
        }
    }
}