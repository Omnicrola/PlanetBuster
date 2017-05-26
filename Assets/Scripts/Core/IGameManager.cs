using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);
        GameObject GenerateBall();
        GameObject GenerateBall(int type);
        int GetNextBallType();
        Sprite GetBallSpriteOfType(int type);

        EventBus EventBus { get; }
        void StartNewLevel();
    }
}