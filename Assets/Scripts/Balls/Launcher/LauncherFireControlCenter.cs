using System;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class LauncherFireControlCenter
    {
        private readonly Camera _mainCamera;
        private readonly Transform _localTransform;
        private readonly float _projectileSpeed;


        public LauncherFireControlCenter(Transform localTransform, Camera mainCamera, float projectileSpeed)
        {
            _localTransform = localTransform;
            _mainCamera = mainCamera;
            _projectileSpeed = projectileSpeed;
        }

        public void Fire(int nextProjectileType)
        {
            var trajectory = CalculateTrajectory();
            var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg);

            FireNextBall(rotation, trajectory, nextProjectileType);
        }

        private Vector3 CalculateTrajectory()
        {
            var targetPoint = _mainCamera.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            targetPoint.z = 0;
            var launcherPosition = new Vector3(_localTransform.position.x, _localTransform.position.y, 0);
            var trajectory = targetPoint - launcherPosition;
            return trajectory;
        }

        private void FireNextBall(Quaternion rotation, Vector3 trajectory, int nextProjectileType)
        {

            var nextProjectile = GameManager.Instance.GenerateBall(nextProjectileType);

            var ballController = nextProjectile.GetComponent<IBallController>();
            ballController.Active = true;
            ballController.IsProjectile = true;
            ballController.Launch(_localTransform.position, rotation, trajectory, _projectileSpeed);

            GameManager.Instance.EventBus.BroadcastBallFired(this, EventArgs.Empty);
        }

    }
}