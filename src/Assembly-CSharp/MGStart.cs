using UnityEngine;
using UnityEngine.SceneManagement;

public class MGStart : MonoBehaviour
{
	private void Start()
	{
		SceneManager.LoadScene("Mainmenu");
		SceneManager.LoadSceneAsync("login", (LoadSceneMode)1);
	}

	private void Update()
	{
	}
}
