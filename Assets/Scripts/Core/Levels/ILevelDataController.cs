using System.Collections.Generic;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using JetBrains.Annotations;

namespace Assets.Scripts.Core.Levels
{
    public interface ILevelDataController
    {
        int GetLevelNumber();
        string GetLevelName();
        List<IBallController> GetInitialBallData();
        int MaxVerticalGridPosition { get; }
    }
}