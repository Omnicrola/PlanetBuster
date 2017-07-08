using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Balls;
using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts.Core.Levels
{
    public class LevelDataController : UnityBehavior, ILevelDataController
    {
        public int LevelNumber;
        public string LevelName;
        public bool ShowPowerBar;
        public string StartMessage;

        public GameObject i_BallSequence;

        public int GetLevelNumber()
        {
            return LevelNumber;
        }

        public string GetLevelName()
        {
            return LevelName;
        }

        public bool ShouldShowPowerbar()
        {
            return ShowPowerBar;
        }

        protected override void Start()
        {
        }

        protected override void Update()
        {
        }

        public Dictionary<GridPosition, IBallController> GetInitialBallData()
        {
            var ballControllers = gameObject.GetChildren()
                .Select(c => c.GetComponent<IBallController>())
                .Where(Linq.IsNotNull)
                .ToList();

            var maxY = ballControllers.Max(c => c.gameObject.transform.localPosition.y);
            if (maxY > 0)
            {
                Logging.Instance.Log(LogLevel.Error,
                    string.Format("Position of balls in level {0} exceed 0 ({1}).", LevelNumber, maxY));
            }

            return ballControllers
                .ToDictionary(controller => GetGridPosition(controller));
        }

        public List<BallType> GetLauncherSequence()
        {
            return i_BallSequence.GetChildren()
                .Select(c => c.GetComponent<IBallController>())
                .Where(Linq.IsNotNull)
                .OrderBy(c => c.gameObject.transform.position.y)
                .Select(c => c.BallType)
                .ToList();
        }

        private GridPosition GetGridPosition(IBallController ballController)
        {
            var position = ballController.gameObject.transform.localPosition;
            int x = (int)Math.Round(position.x);
            int y = (int)Math.Round(Math.Abs(position.y));
            var gridPosition = new GridPosition(x, y);
            Debug.Log(position + " => " + gridPosition);
            return gridPosition;
        }

    }
}