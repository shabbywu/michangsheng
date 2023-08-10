using KBEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToSpace : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnDestroy()
	{
		Event.deregisterOut(this);
	}

	public void rejoinSpace()
	{
		((Account)KBEngineApp.app.player()).rejoinSpace();
		SceneManager.UnloadScene("goToSpace");
		Event.fireOut("ShowGameUI");
	}
}
