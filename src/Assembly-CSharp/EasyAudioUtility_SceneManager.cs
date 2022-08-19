using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class EasyAudioUtility_SceneManager : MonoBehaviour
{
	// Token: 0x06000B9A RID: 2970 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00046D54 File Offset: 0x00044F54
	public void FadeVolume(float to)
	{
		for (int i = 0; i < base.GetComponent<EasyAudioUtility>().helper.Length; i++)
		{
			if (base.GetComponent<EasyAudioUtility>().helper[i].source.isPlaying)
			{
				base.StartCoroutine(this.LerpAudioVolume(to, 0.5f, base.GetComponent<EasyAudioUtility>().helper[i].source));
			}
		}
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00046DB8 File Offset: 0x00044FB8
	public void onSceneChange(string sName)
	{
		Debug.Log(sName + " is opend");
		for (int i = 0; i < this.manager.Length; i++)
		{
			if (sName == this.manager[i].SceneName)
			{
				Debug.Log(sName + " is found");
				for (int j = 0; j < base.GetComponent<EasyAudioUtility>().helper.Length; j++)
				{
					if (base.GetComponent<EasyAudioUtility>().helper[j].name == this.manager[i].name)
					{
						Debug.Log(sName + " audio replaced");
						base.GetComponent<EasyAudioUtility>().helper[j].source.clip = this.manager[i].clip;
						base.GetComponent<EasyAudioUtility>().Play(this.manager[i].name);
						this.FadeVolume(PlayerPrefs.GetFloat("musicValue", 1f));
					}
				}
				return;
			}
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00046EBC File Offset: 0x000450BC
	private IEnumerator LerpAudioVolume(float to, float time, AudioSource source)
	{
		float elapsedTime = 0f;
		while (elapsedTime < time)
		{
			source.volume = Mathf.Lerp(source.volume, to, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	// Token: 0x040007E4 RID: 2020
	public EasyAudioUtility_SMHelper[] manager;
}
