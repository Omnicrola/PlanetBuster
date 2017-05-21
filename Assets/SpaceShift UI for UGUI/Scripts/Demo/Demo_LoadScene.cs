using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo_LoadScene : MonoBehaviour {

	public string scene;
	
	public void LoadScene()
	{
		if (!string.IsNullOrEmpty(this.scene))
			SceneManager.LoadScene(this.scene);
	}
}
