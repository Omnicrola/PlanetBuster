using System;
using Assets.Scripts.Balls;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);
        IBallController GenerateBall();
        GameObject GenerateBall(int type);
        int GetNextBallType();
        Sprite GetBallSpriteOfType(int type);

        IGameEventBus EventBus { get; }
        bool Pause { get; set; }
        void StartNewLevel();
    }
}