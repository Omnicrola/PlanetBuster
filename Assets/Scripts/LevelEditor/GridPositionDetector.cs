using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI.Tweens;

namespace Assets.Scripts.LevelEditor
{
    public class GridPositionDetector : UnityBehavior
    {
        public GameObject Camera;
        public GameObject GridGhost;
        public float GridSize = 0.55f;
        public float PalleteLocation = -5f;

        private Camera _camera;

        protected override void Start()
        {
            _camera = Camera.GetComponent<Camera>();

        }

        protected override void Update()
        {
            var clampedPosition = GetGridPosition();
            GridGhost.transform.position = clampedPosition;

            if (Input.GetMouseButtonUp(0))
            {
                if (Input.mousePosition.y > PalleteLocation)
                {
                    GetComponent<LevelEditorManager>().PlaceBall();
                }
            }
        }

        public Vector2 GetGridPosition()
        {
            var screenToWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            var x = ClampToGrid(screenToWorldPoint.x);
            var y = ClampToGrid(screenToWorldPoint.y);
            return new Vector2(x, y);
        }

        private float ClampToGrid(float value)
        {
            var count = Mathf.Floor(value / GridSize);
            return count * GridSize;
        }
    }
}
