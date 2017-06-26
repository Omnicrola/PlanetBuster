using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class InGameMenuController : UnityBehavior
    {
        public GameObject BallGridManager;
        public GameObject PopupMenu;
        public GameObject GameOverWindow;

        public void OnClick_ShowMenu()
        {
            PopupMenu.SetActive(true);
            GameManager.Instance.Pause = true;
        }

        public void OnClick_Resume()
        {
            PopupMenu.SetActive(false);
            WaitForSeconds(0.5f, () => { GameManager.Instance.Pause = false; });
        }

        public void OnClick_Restart()
        {
            GameManager.Instance.TransitionToScene = GameConstants.SceneNames.MainPlay;
            SceneManager.LoadScene(GameConstants.SceneNames.LoadingScene);
        }

        public void OnClick_ExitToMainMenu()
        {
            GameOverWindow.SetActive(true);
        }
    }
}