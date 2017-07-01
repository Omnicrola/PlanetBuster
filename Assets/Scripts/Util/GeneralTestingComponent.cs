using Assets.Scripts.Balls.Launcher;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class GeneralTestingComponent : UnityBehavior
    {
        public GameObject GiantLaserController;
        private GiantLaserController _giantLaserController;

        protected override void Start()
        {
            _giantLaserController = GiantLaserController.GetComponent<GiantLaserController>();
        }

        protected override void Update()
        {
        }

        public void OnClick_PrimeLaser()
        {
            _giantLaserController.ChargeLevel = 1f;
            _giantLaserController.PrimeZeeLazer();
        }
    }
}