using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class EasyAudioUtility_SceneManager : MonoBehaviour
{
	// Token: 0x06000C93 RID: 3219 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x000987A4 File Offset: 0x000969A4
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

	// Token: 0x06000C95 RID: 3221 RVA: 0x00098808 File Offset: 0x00096A08
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

	// Token: 0x06000C96 RID: 3222 RVA: 0x0000E6F3 File Offset: 0x0000C8F3
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

	// Token: 0x040009C7 RID: 2503
	public EasyAudioUtility_SMHelper[] manager;
}
