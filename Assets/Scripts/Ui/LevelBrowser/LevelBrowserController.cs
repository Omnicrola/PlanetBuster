using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui.LevelBrowser
{
    public class LevelBrowserController : UnityBehavior
    {
        public GameObject IconContainer;
        public GameObject LevelDescriptionWindow;
        public GameObject LevelBrowseWindow;

        private SimpleObjectPool _iconPool;

        protected override void Start()
        {
            _iconPool = GetComponent<SimpleObjectPool>();
            var allLevels = GameManager.Instance.LevelManager.GetAll();
            DisplayLevels(allLevels);
            GameManager.Instance.EventBus.Subscribe<SelectLevelEventArgs>(OnLevelSelected);
            ShowLevelBrowser();
        }

        public void ShowLevelBrowser()
        {
            LevelDescriptionWindow.SetActive(false);
            LevelBrowseWindow.SetActive(true);
        }

        private void ShowLevelDescriptionWindow()
        {
            LevelDescriptionWindow.SetActive(true);
            LevelBrowseWindow.SetActive(false);
        }

        private void OnLevelSelected(SelectLevelEventArgs eventArgs)
        {
            LevelDescriptionWindow.GetComponent<LevelDescriptionWindowController>().Model = eventArgs.LevelSummary;
            ShowLevelDescriptionWindow();
        }

        private void DisplayLevels(List<LevelSummary> allLevels)
        {
            foreach (var levelSummary in allLevels)
            {
                var levelIcon = _iconPool.GetObjectFromPool();
                levelIcon.GetComponent<LevelBrowserIconController>().Model = levelSummary;
                levelIcon.transform.SetParent(IconContainer.transform);
            }
        }


        protected override void OnDestroy()
        {
            GameManager.Instance.EventBus.Unsubscribe<SelectLevelEventArgs>(OnLevelSelected);
        }
    }
}
