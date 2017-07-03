using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.LevelBrowser
{
    public class LevelBrowserIconController : DirtyBehavior<LevelSummary>
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
            if (Model.IsLocked)
            {
                LockIcon.SetActive(true);
                LevelNumber.SetActive(false);
            }
            else
            {
                LockIcon.SetActive(false);
                LevelNumber.SetActive(true);
                LevelNumber.GetComponent<Text>().text = Model.LevelName;
            }
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
