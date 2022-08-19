using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x020000F6 RID: 246
public class EasyAudioUtility : MonoBehaviour
{
	// Token: 0x06000B95 RID: 2965 RVA: 0x00046C08 File Offset: 0x00044E08
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

	// Token: 0x06000B96 RID: 2966 RVA: 0x00046CFC File Offset: 0x00044EFC
	public void Play(string sound)
	{
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00046D18 File Offset: 0x00044F18
	public void Stop(string sound)
	{
		Array.Find<EasyAudioUtility_Helper>(this.helper, (EasyAudioUtility_Helper item) => item.name == sound).source.Stop();
	}

	// Token: 0x040007D9 RID: 2009
	public static EasyAudioUtility instance;

	// Token: 0x040007DA RID: 2010
	public AudioMixerGroup mixerGroup;

	// Token: 0x040007DB RID: 2011
	public EasyAudioUtility_Helper[] helper;
}
