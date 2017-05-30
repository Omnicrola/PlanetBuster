using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelStarter : UnityBehavior
    {
        public GameObject BallGridManager;
        public GameObject PopupMenu;

        protected override void Start()
        {
            PopupMenu.SetActive(false);
            BallGridManager.GetComponent<IBallGridManager>().StartNewLevel();
        }
    }
}
