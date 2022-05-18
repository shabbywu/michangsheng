using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200016F RID: 367
public class EasyAudioUtility : MonoBehaviour
{
	// Token: 0x06000C8A RID: 3210 RVA: 0x00098658 File Offset: 0x00096858
	private void Awake()
	{
		if (EasyAudioUtility.instance != null)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			EasyAudioUtility.instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		foreach (EasyAudioUtility_Helper easyAudioUtility_Helper in this.helper)
		{
			easyAudioUtility_Helper.source = base.gameObject.AddComponent<AudioSource>();
			easyAudioUtility_Helper.source.clip = easyAudioUtility_Helper.clip;
			easyAudioUtility_Helper.source.loop = easyAudioUtility_Helper.loop;
			easyAudioUtility_Helper.source.outputAudioMixerGroup = this.mixerGroup;
		}
		if (Object.FindObjectOfType<MainMenuController>())
		{
			this.Play("BG");
			if (Object.FindObjectOfType<OptionsController_Game>())
			{
				foreach (EasyAudioUtility_Helper easyAudioUtility_Helper2 in this.helper)
				{
					if (easyAudioUtility_Helper2.name == "BG")
					{
						Object.FindObjectOfType<OptionsController_Game>().musicSource = easyAudioUtility_Helper2.source;
					}
				}
			}
		}
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0009874C File Offset: 0x0009694C
	public void Play(string sound)
	{
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00098768 File Offset: 0x00096968
	public void Stop(string sound)
	{
		Array.Find<EasyAudioUtility_Helper>(this.helper, (EasyAudioUtility_Helper item) => item.name == sound).source.Stop();
	}

	// Token: 0x040009BA RID: 2490
	public static EasyAudioUtility instance;

	// Token: 0x040009BB RID: 2491
	public AudioMixerGroup mixerGroup;

	// Token: 0x040009BC RID: 2492
	public EasyAudioUtility_Helper[] helper;
}
