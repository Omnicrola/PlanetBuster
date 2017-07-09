using Assets.Scripts.Effects;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class ProgressBarController : UnityBehavior
    {
        public GameObject Camera;
        public GameObject FillBar;

        private Camera _camera;

        public float Level { get; set; }
        protected override void Start()
        {
            _camera = Camera.GetComponent<Camera>();
        }

        protected override void Update()
        {
            var fillImage = FillBar.GetComponent<Image>();
            fillImage.fillAmount = Level;
        }

    }
}