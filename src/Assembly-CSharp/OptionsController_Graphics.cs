using UnityEngine;
using UnityEngine.UI;

public class OptionsController_Graphics : MonoBehaviour
{
	[Header("_Graphics Options_")]
	[Space(5f)]
	public Text toggleFullscreen_text;

	[HideInInspector]
	public int toggleFullscreen;

	[Space(10f)]
	[HideInInspector]
	public int toggleAnisoFilt;

	public Text AnisoFiltering_text;

	[Space(10f)]
	[HideInInspector]
	public int toggleAntiAlias;

	public Text AntiAlias_text;

	[Space(10f)]
	[HideInInspector]
	public int toggleVsync;

	public Text toggleVsync_text;

	[Space(10f)]
	[HideInInspector]
	public int toggleShadows;

	public Text toggleShadows_text;

	[Space(10f)]
	[HideInInspector]
	public int toggleTextureQuality;

	public Text toggleTextureQuality_text;

	[Space(10f)]
	[HideInInspector]
	public int currentScreenResolutionCount;

	public Text currentScreenResolution_text;

	private Resolution[] allScreenResolutions;

	private void Start()
	{
		gfx_setDefaults();
	}

	public void gfx_fullScreen()
	{
		if (toggleFullscreen == 0)
		{
			toggleFullscreen = 1;
			toggleFullscreen_text.text = "Yes";
		}
		else
		{
			toggleFullscreen = 0;
			toggleFullscreen_text.text = "No";
		}
		PlayerPrefs.SetInt("toggleFullscreen", toggleFullscreen);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void gfx_setFullScreen()
	{
		toggleFullscreen_text.text = ((toggleFullscreen == 1) ? "Yes" : "No");
	}

	public void gfx_AnisoFiltering()
	{
		if (toggleAnisoFilt == 0)
		{
			toggleAnisoFilt = 1;
			AnisoFiltering_text.text = "On";
		}
		else
		{
			toggleAnisoFilt = 0;
			AnisoFiltering_text.text = "Off";
		}
		QualitySettings.anisotropicFiltering = (AnisotropicFiltering)(toggleAnisoFilt == 1);
		PlayerPrefs.SetInt("toggleAnisoFilt", toggleAnisoFilt);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void gfx_setAnisoFiltering()
	{
		QualitySettings.anisotropicFiltering = (AnisotropicFiltering)(toggleAnisoFilt == 1);
		AnisoFiltering_text.text = ((toggleAnisoFilt == 1) ? "On" : "Off");
	}

	public void gfx_AntiAlias()
	{
		if (toggleAntiAlias == 0)
		{
			QualitySettings.antiAliasing = 2;
			toggleAntiAlias = 1;
			AntiAlias_text.text = "2x";
		}
		else if (toggleAntiAlias == 1)
		{
			QualitySettings.antiAliasing = 4;
			toggleAntiAlias = 2;
			AntiAlias_text.text = "4x";
		}
		else if (toggleAntiAlias == 2)
		{
			QualitySettings.antiAliasing = 8;
			toggleAntiAlias = 3;
			AntiAlias_text.text = "8x";
		}
		else if (toggleAntiAlias == 3)
		{
			QualitySettings.antiAliasing = 0;
			toggleAntiAlias = 0;
			AntiAlias_text.text = "Off";
		}
		PlayerPrefs.SetInt("toggleAntiAlias", toggleAntiAlias);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void gfx_setAntiAlias()
	{
		if (toggleAntiAlias == 0)
		{
			QualitySettings.antiAliasing = 0;
			AntiAlias_text.text = "Off";
		}
		else if (toggleAntiAlias == 1)
		{
			QualitySettings.antiAliasing = 2;
			AntiAlias_text.text = "2x";
		}
		else if (toggleAntiAlias == 2)
		{
			QualitySettings.antiAliasing = 4;
			AntiAlias_text.text = "4x";
		}
		else if (toggleAntiAlias == 3)
		{
			QualitySettings.antiAliasing = 8;
			AntiAlias_text.text = "8x";
		}
	}

	public void gfx_Vsync()
	{
		if (toggleVsync == 0)
		{
			QualitySettings.vSyncCount = 1;
			toggleVsync = 1;
			toggleVsync_text.text = "On";
		}
		else
		{
			QualitySettings.vSyncCount = 0;
			toggleVsync = 0;
			toggleVsync_text.text = "Off";
		}
		PlayerPrefs.SetInt("toggleVsync", toggleVsync);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void gfx_setVsync()
	{
		QualitySettings.vSyncCount = ((toggleVsync == 1) ? 1 : 0);
		toggleVsync_text.text = ((toggleVsync == 1) ? "On" : "Off");
	}

	public void gfx_shadows()
	{
		if (toggleShadows == 0)
		{
			QualitySettings.shadows = (ShadowQuality)1;
			toggleShadows = 1;
			toggleShadows_text.text = "Hard";
		}
		else if (toggleShadows == 1)
		{
			QualitySettings.shadows = (ShadowQuality)2;
			toggleShadows = 2;
			toggleShadows_text.text = "Soft";
		}
		else if (toggleShadows == 2)
		{
			QualitySettings.shadows = (ShadowQuality)0;
			toggleShadows = 0;
			toggleShadows_text.text = "Off";
		}
		PlayerPrefs.SetInt("toggleShadows", toggleShadows);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void gfx_setShadows()
	{
		if (toggleShadows == 0)
		{
			QualitySettings.shadows = (ShadowQuality)0;
			toggleShadows_text.text = "Off";
		}
		else if (toggleShadows == 1)
		{
			QualitySettings.shadows = (ShadowQuality)1;
			toggleShadows_text.text = "Hard";
		}
		else if (toggleShadows == 2)
		{
			QualitySettings.shadows = (ShadowQuality)2;
			toggleShadows_text.text = "Soft";
		}
	}

	public void gfx_textureQuality()
	{
		if (toggleTextureQuality == 0)
		{
			QualitySettings.masterTextureLimit = 1;
			toggleTextureQuality_text.text = "Half";
			toggleTextureQuality = 1;
		}
		else if (toggleTextureQuality == 1)
		{
			QualitySettings.masterTextureLimit = 2;
			toggleTextureQuality_text.text = "Quarter";
			toggleTextureQuality = 2;
		}
		else if (toggleTextureQuality == 2)
		{
			QualitySettings.masterTextureLimit = 0;
			toggleTextureQuality_text.text = "Full";
			toggleTextureQuality = 0;
		}
		PlayerPrefs.SetInt("toggleTextureQuality", toggleTextureQuality);
		EasyAudioUtility.instance.Play("Hover");
	}

	private void gfx_setTextureQuality()
	{
		if (toggleTextureQuality == 0)
		{
			QualitySettings.masterTextureLimit = 0;
			toggleTextureQuality_text.text = "Full";
		}
		else if (toggleTextureQuality == 1)
		{
			QualitySettings.masterTextureLimit = 1;
			toggleTextureQuality_text.text = "Half";
		}
		else if (toggleTextureQuality == 2)
		{
			QualitySettings.masterTextureLimit = 2;
			toggleTextureQuality_text.text = "Quarter";
		}
	}

	public void gfx_ScreenResolution()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (currentScreenResolutionCount < allScreenResolutions.Length)
		{
			currentScreenResolutionCount++;
		}
		else
		{
			currentScreenResolutionCount = 0;
		}
		Text obj = currentScreenResolution_text;
		Resolution currentResolution = Screen.currentResolution;
		object obj2 = ((Resolution)(ref currentResolution)).width;
		currentResolution = Screen.currentResolution;
		obj.text = string.Concat(obj2, " x ", ((Resolution)(ref currentResolution)).height);
		PlayerPrefs.SetInt("currentScreenResolutionCount", currentScreenResolutionCount);
	}

	private void gfx_setScreenResolution()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		allScreenResolutions = Screen.resolutions;
		if (PlayerPrefs.HasKey("currentScreenResolutionCount"))
		{
			currentScreenResolution_text.text = ((Resolution)(ref allScreenResolutions[currentScreenResolutionCount])).width + " x " + ((Resolution)(ref allScreenResolutions[currentScreenResolutionCount])).height;
		}
		Text obj = currentScreenResolution_text;
		Resolution currentResolution = Screen.currentResolution;
		object obj2 = ((Resolution)(ref currentResolution)).width;
		currentResolution = Screen.currentResolution;
		obj.text = string.Concat(obj2, " x ", ((Resolution)(ref currentResolution)).height);
	}

	private void gfx_setDefaults()
	{
		toggleFullscreen = PlayerPrefs.GetInt("toggleFullscreen", 1);
		toggleAnisoFilt = PlayerPrefs.GetInt("toggleAnisoFilt", 1);
		toggleVsync = PlayerPrefs.GetInt("toggleVsync", 1);
		toggleShadows = PlayerPrefs.GetInt("toggleShadows", 1);
		toggleTextureQuality = PlayerPrefs.GetInt("toggleTextureQuality", 1);
		toggleAntiAlias = PlayerPrefs.GetInt("toggleAntiAlias", 1);
		currentScreenResolutionCount = PlayerPrefs.GetInt("currentScreenResolutionCount", currentScreenResolutionCount);
		gfx_setFullScreen();
		gfx_setAnisoFiltering();
		gfx_setAntiAlias();
		gfx_setVsync();
		gfx_setShadows();
		gfx_setTextureQuality();
		gfx_setScreenResolution();
	}
}
