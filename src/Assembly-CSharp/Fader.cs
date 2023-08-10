using UnityEngine;

public class Fader : MonoBehaviour
{
	private string sceneToLoad;

	private void Start()
	{
	}

	public void FadeIntoLevel(string sceneName)
	{
		sceneToLoad = sceneName;
		((Component)this).GetComponent<Animator>().Play("Fader In");
		load();
	}

	public void setCanClick()
	{
		Tools.canClickFlag = true;
	}

	public void setCanNotClick()
	{
		Tools.canClickFlag = false;
	}

	private void load()
	{
		Tools.instance.loadOtherScenes(sceneToLoad);
	}
}
