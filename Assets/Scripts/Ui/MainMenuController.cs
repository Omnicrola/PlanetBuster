using Assets.Scripts.Core;
using Assets.Scripts.Util;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenuController : UnityBehavior
    {


        public void OnClick_Start()
        {
            SceneManager.LoadScene(GameConstants.SceneNames.LevelBrowser);
        }

        public void OnClick_HighScores()
        {
            SceneManager.LoadScene(GameConstants.SceneNames.HighScores);
        }

    }


}
