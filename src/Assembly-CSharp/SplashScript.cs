using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
	private void Start()
	{
		MessageMag.Instance.Register(MessageName.MSG_PreloadFinish, OnJsonDataInited);
	}

	public void OnJsonDataInited(MessageData data)
	{
		ToInitScene();
	}

	public void ToInitScene()
	{
		SceneManager.LoadSceneAsync("InitScene");
	}
}
