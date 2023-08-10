using System.Collections;
using UnityEngine;

public class EasyAudioUtility_SceneManager : MonoBehaviour
{
	public EasyAudioUtility_SMHelper[] manager;

	private void Start()
	{
	}

	public void FadeVolume(float to)
	{
		for (int i = 0; i < ((Component)this).GetComponent<EasyAudioUtility>().helper.Length; i++)
		{
			if (((Component)this).GetComponent<EasyAudioUtility>().helper[i].source.isPlaying)
			{
				((MonoBehaviour)this).StartCoroutine(LerpAudioVolume(to, 0.5f, ((Component)this).GetComponent<EasyAudioUtility>().helper[i].source));
			}
		}
	}

	public void onSceneChange(string sName)
	{
		Debug.Log((object)(sName + " is opend"));
		for (int i = 0; i < manager.Length; i++)
		{
			if (!(sName == manager[i].SceneName))
			{
				continue;
			}
			Debug.Log((object)(sName + " is found"));
			for (int j = 0; j < ((Component)this).GetComponent<EasyAudioUtility>().helper.Length; j++)
			{
				if (((Component)this).GetComponent<EasyAudioUtility>().helper[j].name == manager[i].name)
				{
					Debug.Log((object)(sName + " audio replaced"));
					((Component)this).GetComponent<EasyAudioUtility>().helper[j].source.clip = manager[i].clip;
					((Component)this).GetComponent<EasyAudioUtility>().Play(manager[i].name);
					FadeVolume(PlayerPrefs.GetFloat("musicValue", 1f));
				}
			}
			break;
		}
	}

	private IEnumerator LerpAudioVolume(float to, float time, AudioSource source)
	{
		float elapsedTime = 0f;
		while (elapsedTime < time)
		{
			source.volume = Mathf.Lerp(source.volume, to, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
