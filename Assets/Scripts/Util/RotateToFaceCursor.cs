using Assets.Scripts.Core;
using Assets.Scripts.Ui;
using UnityEngine;

namespace Assets.Scripts.Util
{
    public class RotateToFaceCursor : UnityBehavior
    {
        public GameObject Camera;
        public float Minimum = -90f;
        public float Maximum = 90f;

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
            angle = ClampAngle(angle);
            //            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            iTween.RotateUpdate(gameObject, new Vector3(0, 0, angle), .5f);
        }

        private float ClampAngle(float angle)
        {
            if (angle < Minimum)
            {
                return Minimum;
            }
            else if (angle > Maximum)
            {
                return Maximum; ;
            }
            return angle;
        }
    }
}