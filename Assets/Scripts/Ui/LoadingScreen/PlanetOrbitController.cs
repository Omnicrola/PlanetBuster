using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Ui.LoadingScreen
{
    public class PlanetOrbitController : UnityBehavior
    {
        public GameObject Star;
        public float RotationSpeed = 1f;

        protected override void Update()
        {
            float rotationInterval = RotationSpeed * Time.deltaTime;
            transform.RotateAround(Star.transform.position, Vector3.forward, rotationInterval);
        }
    }
}
