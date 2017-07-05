using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Levels;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.LevelBrowser
{
    public class LevelBrowserIconController : DirtyBehavior<ILevelDataController>
    {
        public GameObject LockIcon;
        public GameObject LevelNumber;
        public GameObject LevelPip1;
        public GameObject LevelPip2;
        public GameObject LevelPip3;

        protected override void Start()
        {
        }

        protected override void DirtyUpdate()
        {
            SetLockIcon();
            SetPips();
        }

        private void SetLockIcon()
        {
            LevelNumber.SetActive(true);
            LevelNumber.GetComponent<Text>().text = Model.GetLevelNumber().ToString();
            //            if (Model.IsLocked)
            //            {
            //                LockIcon.SetActive(true);
            //                LevelNumber.SetActive(false);
            //            }
            //            else
            //            {
            //                LockIcon.SetActive(false);
            //                LevelNumber.SetActive(true);
            //                LevelNumber.GetComponent<Text>().text = Model.LevelNumber;
            //            }
        }

        private void SetPips()
        {
        }

        public void OnClick_SelectLevel()
        {
            GameManager.Instance.EventBus.Broadcast(new SelectLevelEventArgs(Model));
        }

    }
}
