using Assets.Scripts.Core;
using Assets.Scripts.Models;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.LevelBrowser
{
    public class LevelDescriptionWindowController : DirtyBehavior<LevelSummary>
    {
        public GameObject Description;
        public GameObject LevelBrowser;


        protected override void DirtyUpdate()
        {
            Description.GetComponent<Text>().text = "Level " + Model.LevelNumber;
        }

        public void OnClick_Cancel()
        {
            LevelBrowser.GetComponent<LevelBrowserController>().ShowLevelBrowser();
        }

        public void OnClick_Confirm()
        {
            GameManager.Instance.CurrentLevel = Model;
            SceneManager.LoadScene(GameConstants.SceneNames.MainPlay);
        }
    }
}