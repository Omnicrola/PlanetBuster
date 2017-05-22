using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class LauncherController : UnityBehavior
    {
        public float ProjectileSpeed = 1;
        public float RotationSpeed = 4;

        public GameObject Camera;
        public GameObject LevelController;
        public GameObject BulletContainer;

        private Camera _mainCamera;


        protected override void Start()
        {
            _mainCamera = Camera.GetComponent<Camera>();
        }

        private int count;
        protected override void Update()
        {
            RotateToFaceCursor();

            if (TouchWasReleased() || Input.GetMouseButtonUp(0))
            {
                var targetPoint = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                targetPoint.z = 0;
                var launcherPosition = new Vector3(transform.position.x, transform.position.y, 0);
                var trajectory = targetPoint - launcherPosition;
                var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg);

                var newProjectile = GameManager.Instance.GenerateBall(1);
                var ballController = newProjectile.GetComponent<BallController>();
                ballController.IsProjectile = true;
                ballController.Fire(transform.position, rotation, trajectory, ProjectileSpeed);

            }
        }

        private bool TouchWasReleased()
        {
            return Input.touches.Any(t => t.phase == TouchPhase.Ended);
        }

        private void RotateToFaceCursor()
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
