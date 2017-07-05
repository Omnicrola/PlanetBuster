using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public interface IGameManager
    {
        void WhenTargetIsActive(GameObject gameObject, Action action);

        IGameEventBus EventBus { get; }
        bool Pause { get; set; }
        ILevelManager LevelManager { get; }
        ILevelDataController CurrentLevel { get; set; }
        string TransitionToScene { get; set; }
    }
}