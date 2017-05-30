using System;
using Assets.Scripts.Balls;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);

        IGameEventBus EventBus { get; }
        bool Pause { get; set; }
    }
}