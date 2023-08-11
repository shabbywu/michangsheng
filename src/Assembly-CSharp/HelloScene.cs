using UnityEngine;
using UnityEngine.SceneManagement;

public class HelloScene : MonoBehaviour
{
	private AsyncOperation scene;

	private void Start()
	{
		scene = SceneManager.LoadSceneAsync("MainMenu");
		scene.allowSceneActivation = false;
		((MonoBehaviour)this).Invoke("GameStart", 6f);
	}

	public void GameStart()
	{
		scene.allowSceneActivation = true;
	}

	private void Update()
	{
	}
}
