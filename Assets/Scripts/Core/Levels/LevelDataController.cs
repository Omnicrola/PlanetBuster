using System.Collections.Generic;
using Assets.Scripts.Balls;
using Assets.Scripts.Models;
using Assets.Scripts.Util;

namespace Assets.Scripts.Core.Levels
{
    public class LevelDataController : UnityBehavior, ILevelDataController
    {
        public int LevelNumber;
        public string LevelName;

        public int GetLevelNumber()
        {
            return LevelNumber;
        }

        public string GetLevelName()
        {
            return LevelName;
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
        }

        public List<IBallController> GetInitialBallData()
        {
            // be sure to derive grid positions from existing coordinates
            return new List<IBallController>();
        }

        public int MaxVerticalGridPosition { get; private set; }
    }
}