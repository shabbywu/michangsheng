using KBEngine;
using UnityEngine;

public class UI_SelectPatterns : MonoBehaviour
{
	private void Start()
	{
		World.instance.init();
	}

	public void onZombieEnterGame()
	{
		if ((Account)KBEngineApp.app.player() != null)
		{
			Application.LoadLevel("world");
		}
	}

	public void onHeroEnterGame()
	{
		if ((Account)KBEngineApp.app.player() != null)
		{
			Application.LoadLevel("world");
		}
	}
}
