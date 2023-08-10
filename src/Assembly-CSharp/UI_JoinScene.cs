using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_JoinScene : MonoBehaviour
{
	public string SceneName = "";

	private void Start()
	{
	}

	public void MoveToScence()
	{
		if (SceneName != "")
		{
			SceneManager.LoadScene(SceneName);
		}
	}
}
