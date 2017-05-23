using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    public class LauncherController : UnityBehavior
    {
        public float ProjectileSpeed = 1;
        public float RotationSpeed = 4;

        public GameObject Camera;
        public GameObject BallContainer;

        private Camera _mainCamera;

        public GameObject NextProjectile;
        private int _nextProjectileType;

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
                var trajectory = CalculateTrajectory();
                var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg);

                FireNextBall(rotation, trajectory);
                GenerateNextBall();
            }
        }

        private void GenerateNextBall()
        {
            _nextProjectileType = GameManager.Instance.GetNextBallType();
            var ballSprite = GameManager.Instance.GetBallSpriteOfType(_nextProjectileType);
            NextProjectile.GetComponent<SpriteRenderer>().sprite = ballSprite;
        }

        private void FireNextBall(Quaternion rotation, Vector3 trajectory)
        {
            var nextProjectile = GameManager.Instance.GenerateBall(_nextProjectileType);

            var ballController = nextProjectile.GetComponent<BallController>();
            ballController.Active = true;
            ballController.IsProjectile = true;
            ballController.Fire(transform.position, rotation, trajectory, ProjectileSpeed);
        }

        private Vector3 CalculateTrajectory()
        {
            var targetPoint = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            targetPoint.z = 0;
            var launcherPosition = new Vector3(transform.position.x, transform.position.y, 0);
            var trajectory = targetPoint - launcherPosition;
            return trajectory;
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
