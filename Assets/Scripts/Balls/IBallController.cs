using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallController
    {
        float Hitpoints { get; set; }
        float MaxHitpoints { get; set; }
        GridPosition GridPosition { get; }
        BallType BallType { get; set; }
        BallMagnitude Magnitude { get; set; }
        bool HasPowerGem { get; set; }

        bool Active { get; set; }
        bool IsProjectile { get; set; }

        GameObject gameObject { get; }
        Vector3 Position { get; set; }
        Sprite CurrentBallSprite { get; set; }

        void Launch(Vector3 position, Quaternion rotation, Vector3 trajectory, float projectileSpeed);
        void ResetBall();
        void SetActiveInGrid(GridPosition gridPosition, Vector3 worldPosition, Transform parentTransform);
        void SetInactiveInGrid();
        void MarkDirty();
    }
}