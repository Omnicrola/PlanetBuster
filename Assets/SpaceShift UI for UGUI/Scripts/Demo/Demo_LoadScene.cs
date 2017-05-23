using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.SpaceShift_UI_for_UGUI.Scripts.Demo
{
    public class Demo_LoadScene : MonoBehaviour {

        public string scene;
	
        public void LoadScene()
        {
            if (!string.IsNullOrEmpty(this.scene))
                SceneManager.LoadScene(this.scene);
        }
    }
}
