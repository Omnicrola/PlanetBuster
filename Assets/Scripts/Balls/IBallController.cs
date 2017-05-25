using System;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallController
    {
        bool Active { get; set; }
        bool IsProjectile { get; set; }
        BallModel Model { get; set; }
        bool IsFalling { get; set; }
        Transform transform { get; }
        GameObject gameObject { get; }
        void Fire(Vector3 position, Quaternion rotation, Vector3 trajectory, float projectileSpeed);
        bool IsAtGrid(int gridX, int gridY);
        event EventHandler<BallCollisionEventArgs> OnHit;
    }
}