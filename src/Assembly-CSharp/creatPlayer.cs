using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class creatPlayer : MonoBehaviour
{
	public InputField _userName;

	public Text text_status;

	private void Start()
	{
		Event.registerOut("goToHome", this, "goToHome");
		Event.registerOut("HomeErrorMessage", this, "HomeErrorMessage");
	}

	public void HomeErrorMessage(string msg)
	{
		text_status.text = msg;
	}

	public void goToHome()
	{
		SceneManager.UnloadScene("creatPlayer");
		SceneManager.LoadSceneAsync("homeScene", (LoadSceneMode)1);
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void onHellohaha()
	{
		((Account)KBEngineApp.app.player())?.reqCreatePlayer(_userName.text);
	}
}
