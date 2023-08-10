using System;
using UnityEngine;
using UnityEngine.Audio;

public class EasyAudioUtility : MonoBehaviour
{
	public static EasyAudioUtility instance;

	public AudioMixerGroup mixerGroup;

	public EasyAudioUtility_Helper[] helper;

	private void Awake()
	{
		if ((Object)(object)instance != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}
		else
		{
			instance = this;
			Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		}
		EasyAudioUtility_Helper[] array = helper;
		foreach (EasyAudioUtility_Helper easyAudioUtility_Helper in array)
		{
			easyAudioUtility_Helper.source = ((Component)this).gameObject.AddComponent<AudioSource>();
			easyAudioUtility_Helper.source.clip = easyAudioUtility_Helper.clip;
			easyAudioUtility_Helper.source.loop = easyAudioUtility_Helper.loop;
			easyAudioUtility_Helper.source.outputAudioMixerGroup = mixerGroup;
		}
		if (!Object.op_Implicit((Object)(object)Object.FindObjectOfType<MainMenuController>()))
		{
			return;
		}
		Play("BG");
		if (!Object.op_Implicit((Object)(object)Object.FindObjectOfType<OptionsController_Game>()))
		{
			return;
		}
		array = helper;
		foreach (EasyAudioUtility_Helper easyAudioUtility_Helper2 in array)
		{
			if (easyAudioUtility_Helper2.name == "BG")
			{
				Object.FindObjectOfType<OptionsController_Game>().musicSource = easyAudioUtility_Helper2.source;
			}
		}
	}

	public void Play(string sound)
	{
	}

	public void Stop(string sound)
	{
		Array.Find(helper, (EasyAudioUtility_Helper item) => item.name == sound).source.Stop();
	}
}
