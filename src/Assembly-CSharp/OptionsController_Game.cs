using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController_Game : MonoBehaviour
{
	[Header("_Game Options_")]
	[Space(5f)]
	[Tooltip("HUD GameObject name in scene")]
	public string HUD_name;

	public Text HUD_text;

	public int toggleHud = 1;

	[Space(10f)]
	public Slider contrast_slider;

	[HideInInspector]
	public float contrastValue;

	[Space(10f)]
	[HideInInspector]
	public float brightnessValue;

	public Slider brightness_slider;

	[Space(10f)]
	[HideInInspector]
	public float musicValue;

	[Tooltip("Music Source to control")]
	public AudioSource musicSource;

	public Slider music_slider;

	[Space(10f)]
	[HideInInspector]
	public float soundValue;

	[Tooltip("Audio Sources to control")]
	public EasyAudioUtility_Helper[] soundSource;

	public Slider sound_slider;

	private IEnumerator Start()
	{
		yield return (object)new WaitForEndOfFrame();
		game_setDefaults();
	}

	public void game_toggleHUD()
	{
		if (toggleHud == 0)
		{
			toggleHud = 1;
			HUD_text.text = "On";
			PlayerPrefs.SetString("HUD_name", "");
		}
		else
		{
			toggleHud = 0;
			HUD_text.text = "Off";
			PlayerPrefs.SetString("HUD_name", HUD_name);
		}
		PlayerPrefs.SetInt("toggleHud", toggleHud);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void game_setHUD()
	{
		if (toggleHud == 0)
		{
			HUD_text.text = "Off";
			PlayerPrefs.SetString("HUD_name", "");
		}
		else
		{
			HUD_text.text = "On";
			PlayerPrefs.SetString("HUD_name", HUD_name);
		}
	}

	public void game_Contrast()
	{
		contrastValue = contrast_slider.value;
		PlayerPrefs.SetFloat("contrastValue", contrastValue);
	}

	private void game_setContrast()
	{
		if (contrastValue == 0f)
		{
			contrastValue = 1f;
		}
		contrast_slider.value = contrastValue;
	}

	public void game_Brightness()
	{
		brightnessValue = brightness_slider.value;
		PlayerPrefs.SetFloat("brightnessValue", brightnessValue);
	}

	private void game_setBrightness()
	{
		if (brightnessValue == 0f)
		{
			brightnessValue = 1f;
		}
		brightness_slider.value = brightnessValue;
	}

	public void game_Music()
	{
		musicSource.volume = music_slider.value;
		musicValue = music_slider.value;
		PlayerPrefs.SetFloat("musicValue", musicValue);
	}

	private void game_setMusic()
	{
		EasyAudioUtility easyAudioUtility = Object.FindObjectOfType<EasyAudioUtility>();
		if (!((Object)(object)easyAudioUtility != (Object)null))
		{
			return;
		}
		for (int i = 0; i < easyAudioUtility.helper.Length; i++)
		{
			if (easyAudioUtility.helper[i].name == "BG")
			{
				musicSource = easyAudioUtility.helper[i].source;
				if (!musicSource.isPlaying)
				{
					musicSource.Play();
				}
			}
		}
	}

	public void game_Sound()
	{
		EasyAudioUtility_Helper[] array = soundSource;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].volume = sound_slider.value;
			soundValue = sound_slider.value;
		}
		PlayerPrefs.SetFloat("soundValue", soundValue);
	}

	private void game_setSound()
	{
		EasyAudioUtility easyAudioUtility = Object.FindObjectOfType<EasyAudioUtility>();
		if (!((Object)(object)easyAudioUtility != (Object)null))
		{
			return;
		}
		for (int i = 0; i < easyAudioUtility.helper.Length; i++)
		{
			if (easyAudioUtility.helper[i].name == "Hover")
			{
				soundSource[i] = easyAudioUtility.helper[i];
			}
			if (easyAudioUtility.helper[i].name == "Click")
			{
				soundSource[i] = easyAudioUtility.helper[i];
			}
		}
		EasyAudioUtility_Helper[] array = soundSource;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].volume = soundValue;
			sound_slider.value = soundValue;
		}
	}

	private void game_setDefaults()
	{
		toggleHud = PlayerPrefs.GetInt("toggleHud");
		contrastValue = PlayerPrefs.GetFloat("contrastValue", 1f);
		brightnessValue = PlayerPrefs.GetFloat("brightnessValue", 1f);
		musicValue = PlayerPrefs.GetFloat("musicValue", 1f);
		soundValue = PlayerPrefs.GetFloat("soundValue", 1f);
		game_setHUD();
		game_setMusic();
		game_setSound();
	}
}
