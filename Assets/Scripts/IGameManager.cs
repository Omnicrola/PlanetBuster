using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);
        GameObject GenerateBall();
        GameObject GenerateBall(int type);
        int GetNextBallType();
        Sprite GetBallSpriteOfType(int type);

        event EventHandler<BallGridMatchArgs> MatchFound;
        event EventHandler<OrphanedBallsEventArgs> OrphansFound;
    }
}