using Assets.Scripts.Balls;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui
{
    public class InGameMenuController : MonoBehaviour
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
            GameManager.Instance.Pause = false;
        }

        public void OnClick_Restart()
        {
            BallGridManager.GetComponent<IBallGridManager>().StartNewLevel();
        }

        public void OnClick_ExitToMainMenu()
        {
            GameOverWindow.SetActive(true);
        }
    }
}
