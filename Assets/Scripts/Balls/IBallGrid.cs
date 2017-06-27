using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallGrid
    {
        int[] TypesLeftActive { get; }
        int ActiveBalls { get; }
        float LowestBallPosition { get; }
        void Append(IBallController newBall, GridPosition gridPosition);
        void Initialize(List<BallLevelData> newBalls);
        void Clear();
        void Remove(GridPosition gameObject);
        void HandleOrphanedBalls();
    }
}