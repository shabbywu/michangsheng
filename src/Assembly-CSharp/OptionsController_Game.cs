using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018C RID: 396
public class OptionsController_Game : MonoBehaviour
{
	// Token: 0x06000D37 RID: 3383 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		this.game_setDefaults();
		yield break;
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0009AC70 File Offset: 0x00098E70
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

	// Token: 0x06000D39 RID: 3385 RVA: 0x0009ACF4 File Offset: 0x00098EF4
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

	// Token: 0x06000D3A RID: 3386 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
	public void game_Contrast()
	{
		this.contrastValue = this.contrast_slider.value;
		PlayerPrefs.SetFloat("contrastValue", this.contrastValue);
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0000EFE7 File Offset: 0x0000D1E7
	private void game_setContrast()
	{
		if (this.contrastValue == 0f)
		{
			this.contrastValue = 1f;
		}
		this.contrast_slider.value = this.contrastValue;
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0000F012 File Offset: 0x0000D212
	public void game_Brightness()
	{
		this.brightnessValue = this.brightness_slider.value;
		PlayerPrefs.SetFloat("brightnessValue", this.brightnessValue);
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0000F035 File Offset: 0x0000D235
	private void game_setBrightness()
	{
		if (this.brightnessValue == 0f)
		{
			this.brightnessValue = 1f;
		}
		this.brightness_slider.value = this.brightnessValue;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0000F060 File Offset: 0x0000D260
	public void game_Music()
	{
		this.musicSource.volume = this.music_slider.value;
		this.musicValue = this.music_slider.value;
		PlayerPrefs.SetFloat("musicValue", this.musicValue);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0009AD4C File Offset: 0x00098F4C
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

	// Token: 0x06000D40 RID: 3392 RVA: 0x0009ADC0 File Offset: 0x00098FC0
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

	// Token: 0x06000D41 RID: 3393 RVA: 0x0009AE18 File Offset: 0x00099018
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

	// Token: 0x06000D42 RID: 3394 RVA: 0x0009AED0 File Offset: 0x000990D0
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

	// Token: 0x04000A65 RID: 2661
	[Header("_Game Options_")]
	[Space(5f)]
	[Tooltip("HUD GameObject name in scene")]
	public string HUD_name;

	// Token: 0x04000A66 RID: 2662
	public Text HUD_text;

	// Token: 0x04000A67 RID: 2663
	public int toggleHud = 1;

	// Token: 0x04000A68 RID: 2664
	[Space(10f)]
	public Slider contrast_slider;

	// Token: 0x04000A69 RID: 2665
	[HideInInspector]
	public float contrastValue;

	// Token: 0x04000A6A RID: 2666
	[Space(10f)]
	[HideInInspector]
	public float brightnessValue;

	// Token: 0x04000A6B RID: 2667
	public Slider brightness_slider;

	// Token: 0x04000A6C RID: 2668
	[Space(10f)]
	[HideInInspector]
	public float musicValue;

	// Token: 0x04000A6D RID: 2669
	[Tooltip("Music Source to control")]
	public AudioSource musicSource;

	// Token: 0x04000A6E RID: 2670
	public Slider music_slider;

	// Token: 0x04000A6F RID: 2671
	[Space(10f)]
	[HideInInspector]
	public float soundValue;

	// Token: 0x04000A70 RID: 2672
	[Tooltip("Audio Sources to control")]
	public EasyAudioUtility_Helper[] soundSource;

	// Token: 0x04000A71 RID: 2673
	public Slider sound_slider;
}
