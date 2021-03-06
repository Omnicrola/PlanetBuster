using System;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls.Launcher
{
    public class LauncherFireControlCenter
    {
        private readonly Camera _mainCamera;
        private readonly Transform _localTransform;
        private readonly float _projectileSpeed;
        private readonly Vector3 _firePositionOffset;
        private readonly IBallGridManager _ballGridManager;


        public LauncherFireControlCenter(Transform localTransform, Camera mainCamera, IBallGridManager ballGridManager, float projectileSpeed, Vector2 firePositionOffset)
        {
            _localTransform = localTransform;
            _mainCamera = mainCamera;
            _projectileSpeed = projectileSpeed;
            _firePositionOffset = firePositionOffset;
            _ballGridManager = ballGridManager;
        }

        public void Fire(BallType nextProjectileType, Vector2 inputPosition)
        {
            var trajectory = CalculateTrajectory(inputPosition);
            var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg);

            FireNextBall(rotation, trajectory, nextProjectileType);
        }

        private Vector3 CalculateTrajectory(Vector2 inputPosition)
        {
            var targetPoint = _mainCamera.ScreenToWorldPoint(inputPosition);
            targetPoint.z = 0;
            var launcherPosition = new Vector3(_localTransform.position.x, _localTransform.position.y, 0);
            var trajectory = targetPoint - launcherPosition;
            return trajectory;
        }

        private void FireNextBall(Quaternion rotation, Vector3 trajectory, BallType nextProjectileType)
        {

            var nextProjectile = _ballGridManager.GenerateBall(nextProjectileType);

            var ballController = nextProjectile.GetComponent<IBallController>();
            ballController.Active = true;
            ballController.IsProjectile = true;
            ballController.Launch(_localTransform.position + _firePositionOffset, rotation, trajectory, _projectileSpeed);

            GameManager.Instance.EventBus.Broadcast(new BallFiredEventArgs());
        }

    }
}