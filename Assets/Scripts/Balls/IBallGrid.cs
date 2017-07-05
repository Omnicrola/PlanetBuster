using System.Collections.Generic;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallGrid
    {
        BallType[] TypesLeftActive { get; }
        int ActiveBalls { get; }
        float LowestBallPosition { get; }
        void Append(IBallController newBall, GridPosition gridPosition);
        void Initialize(List<IBallController> ballsToPutInGrid);
        void Clear();
        void Remove(GridPosition gameObject);
        void HandleOrphanedBalls();
    }
}