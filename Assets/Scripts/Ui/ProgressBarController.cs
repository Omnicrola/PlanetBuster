using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui
{
    public class ProgressBarController : UnityBehavior
    {
        public GameObject FillBar;

        public float Level { get; set; }


        protected override void Update()
        {
            var fillImage = FillBar.GetComponent<Image>();
            fillImage.fillAmount = Level;
        }
    }
}