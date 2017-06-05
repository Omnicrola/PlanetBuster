using System;
using Assets.Scripts.Balls;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class GameManager : UnityBehavior, IGameManager
    {
        #region Singleton
        private static IGameManager _instance = null;

        public static IGameManager Instance
        {
            get { return _instance; }
        }


        protected override void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }


        public GameManager()
        {
            EventBus = new UniversalEventBus();
            LevelManager = new LevelManager();
        }
        #endregion

        protected override void Update()
        {
        }

        public ILevelManager LevelManager { get; private set; }
        public LevelSummary CurrentLevel { get; set; }
        public IGameEventBus EventBus { get; private set; }

        public bool Pause { get; set; }

        protected override void Start()
        {
        }

    }
}