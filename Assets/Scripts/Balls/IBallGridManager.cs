using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Balls
{
    public interface IBallGridManager
    {
        IBallController GenerateBall();
        GameObject GenerateBall(BallType type);
        BallType GetNextBallType();
        void StartNewLevel();
        float LowestBallPosition { get; }
        void StickBallToCeiling(IBallController gameObject);
    }
}