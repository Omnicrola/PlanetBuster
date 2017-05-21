using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class LauncherController : UnityBehavior
    {
        public float RotationSpeed = 4;
        public GameObject Camera;
        private Camera _mainCamera;


        protected override void Start()
        {
            _mainCamera = Camera.GetComponent<Camera>();
        }

        protected override void Update()
        {
            var mousePosition = Input.mousePosition;
            var ourLocation = _mainCamera.WorldToScreenPoint(transform.position);
            mousePosition.x -= ourLocation.x;
            mousePosition.y -= ourLocation.y;
            float angle = Mathf.Atan2(mousePosition.x, mousePosition.y) * Mathf.Rad2Deg * -1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
