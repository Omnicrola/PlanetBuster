using System.Collections.Generic;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;

namespace Assets.Scripts.Core.Levels
{
    public interface ILevelDataController
    {
        int GetLevelNumber();
        string GetLevelName();
        bool ShouldShowPowerbar();

        Dictionary<GridPosition, IBallController> GetInitialBallData();
        List<BallType> GetLauncherSequence();
    }
}