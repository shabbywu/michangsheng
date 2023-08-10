using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_statr : MonoBehaviour
{
	public static int startStatus;

	private void Start()
	{
		Event.registerOut("MatchSuccess", this, "MatchSuccess");
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void MatchSuccess()
	{
		SceneManager.LoadScene("world");
		SceneManager.LoadSceneAsync("selectAvatar", (LoadSceneMode)1);
	}

	public void cancelMatch()
	{
		((Account)KBEngineApp.app.player())?.cancelMatch();
	}
}
