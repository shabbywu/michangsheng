using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000109 RID: 265
public class OptionsController_Game : MonoBehaviour
{
	// Token: 0x06000C1C RID: 3100 RVA: 0x0004929B File Offset: 0x0004749B
	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		this.game_setDefaults();
		yield break;
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x000492AC File Offset: 0x000474AC
	public void game_toggleHUD()
	{
		if (this.toggleHud == 0)
		{
			this.toggleHud = 1;
			this.HUD_text.text = "On";
			PlayerPrefs.SetString("HUD_name", "");
		}
		else
		{
			this.toggleHud = 0;
			this.HUD_text.text = "Off";
			PlayerPrefs.SetString("HUD_name", this.HUD_name);
		}
		PlayerPrefs.SetInt("toggleHud", this.toggleHud);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00049330 File Offset: 0x00047530
	private void game_setHUD()
	{
		if (this.toggleHud == 0)
		{
			this.HUD_text.text = "Off";
			PlayerPrefs.SetString("HUD_name", "");
			return;
		}
		this.HUD_text.text = "On";
		PlayerPrefs.SetString("HUD_name", this.HUD_name);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00049385 File Offset: 0x00047585
	public void game_Contrast()
	{
		this.contrastValue = this.contrast_slider.value;
		PlayerPrefs.SetFloat("contrastValue", this.contrastValue);
	}

	// Token: 0x06000C20 RID: 3104 RVA: 0x000493A8 File Offset: 0x000475A8
	private void game_setContrast()
	{
		if (this.contrastValue == 0f)
		{
			this.contrastValue = 1f;
		}
		this.contrast_slider.value = this.contrastValue;
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x000493D3 File Offset: 0x000475D3
	public void game_Brightness()
	{
		this.brightnessValue = this.brightness_slider.value;
		PlayerPrefs.SetFloat("brightnessValue", this.brightnessValue);
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x000493F6 File Offset: 0x000475F6
	private void game_setBrightness()
	{
		if (this.brightnessValue == 0f)
		{
			this.brightnessValue = 1f;
		}
		this.brightness_slider.value = this.brightnessValue;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00049421 File Offset: 0x00047621
	public void game_Music()
	{
		this.musicSource.volume = this.music_slider.value;
		this.musicValue = this.music_slider.value;
		PlayerPrefs.SetFloat("musicValue", this.musicValue);
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0004945C File Offset: 0x0004765C
	private void game_setMusic()
	{
		EasyAudioUtility easyAudioUtility = Object.FindObjectOfType<EasyAudioUtility>();
		if (easyAudioUtility != null)
		{
			for (int i = 0; i < easyAudioUtility.helper.Length; i++)
			{
				if (easyAudioUtility.helper[i].name == "BG")
				{
					this.musicSource = easyAudioUtility.helper[i].source;
					if (!this.musicSource.isPlaying)
					{
						this.musicSource.Play();
					}
				}
			}
		}
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x000494D0 File Offset: 0x000476D0
	public void game_Sound()
	{
		EasyAudioUtility_Helper[] array = this.soundSource;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].volume = this.sound_slider.value;
			this.soundValue = this.sound_slider.value;
		}
		PlayerPrefs.SetFloat("soundValue", this.soundValue);
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x00049528 File Offset: 0x00047728
	private void game_setSound()
	{
		EasyAudioUtility easyAudioUtility = Object.FindObjectOfType<EasyAudioUtility>();
		if (easyAudioUtility != null)
		{
			for (int i = 0; i < easyAudioUtility.helper.Length; i++)
			{
				if (easyAudioUtility.helper[i].name == "Hover")
				{
					this.soundSource[i] = easyAudioUtility.helper[i];
				}
				if (easyAudioUtility.helper[i].name == "Click")
				{
					this.soundSource[i] = easyAudioUtility.helper[i];
				}
			}
			EasyAudioUtility_Helper[] array = this.soundSource;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].volume = this.soundValue;
				this.sound_slider.value = this.soundValue;
			}
		}
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x000495E0 File Offset: 0x000477E0
	private void game_setDefaults()
	{
		this.toggleHud = PlayerPrefs.GetInt("toggleHud");
		this.contrastValue = PlayerPrefs.GetFloat("contrastValue", 1f);
		this.brightnessValue = PlayerPrefs.GetFloat("brightnessValue", 1f);
		this.musicValue = PlayerPrefs.GetFloat("musicValue", 1f);
		this.soundValue = PlayerPrefs.GetFloat("soundValue", 1f);
		this.game_setHUD();
		this.game_setMusic();
		this.game_setSound();
	}

	// Token: 0x0400086C RID: 2156
	[Header("_Game Options_")]
	[Space(5f)]
	[Tooltip("HUD GameObject name in scene")]
	public string HUD_name;

	// Token: 0x0400086D RID: 2157
	public Text HUD_text;

	// Token: 0x0400086E RID: 2158
	public int toggleHud = 1;

	// Token: 0x0400086F RID: 2159
	[Space(10f)]
	public Slider contrast_slider;

	// Token: 0x04000870 RID: 2160
	[HideInInspector]
	public float contrastValue;

	// Token: 0x04000871 RID: 2161
	[Space(10f)]
	[HideInInspector]
	public float brightnessValue;

	// Token: 0x04000872 RID: 2162
	public Slider brightness_slider;

	// Token: 0x04000873 RID: 2163
	[Space(10f)]
	[HideInInspector]
	public float musicValue;

	// Token: 0x04000874 RID: 2164
	[Tooltip("Music Source to control")]
	public AudioSource musicSource;

	// Token: 0x04000875 RID: 2165
	public Slider music_slider;

	// Token: 0x04000876 RID: 2166
	[Space(10f)]
	[HideInInspector]
	public float soundValue;

	// Token: 0x04000877 RID: 2167
	[Tooltip("Audio Sources to control")]
	public EasyAudioUtility_Helper[] soundSource;

	// Token: 0x04000878 RID: 2168
	public Slider sound_slider;
}
