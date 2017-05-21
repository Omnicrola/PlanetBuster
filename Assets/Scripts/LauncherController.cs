using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SimpleObjectPool))]
    public class LauncherController : UnityBehavior
    {
        public float ProjectileSpeed = 1;
        public float RotationSpeed = 4;

        public GameObject Camera;
        public GameObject LevelController;
        public GameObject BulletContainer;

        private Camera _mainCamera;
        private SimpleObjectPool _simpleObjectPool;


        protected override void Start()
        {
            _mainCamera = Camera.GetComponent<Camera>();
            _simpleObjectPool = GetComponent<SimpleObjectPool>();
        }

        private int count;
        protected override void Update()
        {
            RotateToFaceCursor();

            if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
            {
                var targetPoint = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                var launcherPosition = new Vector3(transform.position.x, transform.position.y, 0);
                var trajectory = targetPoint - launcherPosition;
                trajectory.Normalize();
                var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg);

                var newProjectile = GameManager.Instance.GenerateBall(0, 0);
                var ballController = newProjectile.GetComponent<BallController>();
                ballController.IsProjectile = true;
                ballController.Fire(transform.position, rotation, trajectory, ProjectileSpeed);

            }
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
