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
        public GameObject BallPrefab;

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

            return ballControllers
                .ToDictionary(controller => GetGridPosition(controller, maxY));
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

        public ILevelDataController Instantiate()
        {
            var instantiate = GameObject.Instantiate(gameObject);
            return instantiate.GetComponent<ILevelDataController>();
        }

        private GridPosition GetGridPosition(IBallController ballController, float yOffset)
        {
            var position = ballController.gameObject.transform.localPosition;
            int x = (int)Math.Round(position.x);
            int y = (int)Math.Round(Math.Abs(position.y - yOffset));
            var gridPosition = new GridPosition(x, y);
            Debug.Log(position + " => " + gridPosition);
            return gridPosition;
        }

    }
}