using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallGridManager
    {
        GameObject GenerateBall(BallType type);
        BallType GetNextBallType();
        void StartNewLevel(ILevelDataController newLevel);
        float LowestBallPosition { get; }
        void StickBallToCeiling(IBallController gameObject);
    }
}