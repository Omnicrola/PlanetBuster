using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.LevelEditor
{
    public class EditorPanController : UnityBehavior
    {
        public float Sensitivity = 0.01f;

        private Camera _camera;
        private Vector3 _mouseStartPosition;

        protected override void Start()
        {
            _camera = GetComponent<Camera>();
        }

        protected override void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _mouseStartPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                var delta = _mouseStartPosition - Input.mousePosition;
                transform.Translate(0, delta.y * Sensitivity, 0);
                _mouseStartPosition = Input.mousePosition;
            }
        }
    }

}
