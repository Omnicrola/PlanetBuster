using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenuController : UnityBehavior
    {


        public void OnClick_Start()
        {
            SceneManager.LoadScene("PlayScene");
        }

        public void OnClick_HighScores()
        {
            SceneManager.LoadScene("HighScores");
        }

    }
}
