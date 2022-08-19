using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010A RID: 266
public class OptionsController_Graphics : MonoBehaviour
{
	// Token: 0x06000C29 RID: 3113 RVA: 0x00049672 File Offset: 0x00047872
	private void Start()
	{
		this.gfx_setDefaults();
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0004967C File Offset: 0x0004787C
	public void gfx_fullScreen()
	{
		if (this.toggleFullscreen == 0)
		{
			this.toggleFullscreen = 1;
			this.toggleFullscreen_text.text = "Yes";
		}
		else
		{
			this.toggleFullscreen = 0;
			this.toggleFullscreen_text.text = "No";
		}
		PlayerPrefs.SetInt("toggleFullscreen", this.toggleFullscreen);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x000496E0 File Offset: 0x000478E0
	private void gfx_setFullScreen()
	{
		this.toggleFullscreen_text.text = ((this.toggleFullscreen == 1) ? "Yes" : "No");
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00049704 File Offset: 0x00047904
	public void gfx_AnisoFiltering()
	{
		if (this.toggleAnisoFilt == 0)
		{
			this.toggleAnisoFilt = 1;
			this.AnisoFiltering_text.text = "On";
		}
		else
		{
			this.toggleAnisoFilt = 0;
			this.AnisoFiltering_text.text = "Off";
		}
		QualitySettings.anisotropicFiltering = ((this.toggleAnisoFilt == 1) ? 1 : 0);
		PlayerPrefs.SetInt("toggleAnisoFilt", this.toggleAnisoFilt);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0004977A File Offset: 0x0004797A
	private void gfx_setAnisoFiltering()
	{
		QualitySettings.anisotropicFiltering = ((this.toggleAnisoFilt == 1) ? 1 : 0);
		this.AnisoFiltering_text.text = ((this.toggleAnisoFilt == 1) ? "On" : "Off");
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x000497B0 File Offset: 0x000479B0
	public void gfx_AntiAlias()
	{
		if (this.toggleAntiAlias == 0)
		{
			QualitySettings.antiAliasing = 2;
			this.toggleAntiAlias = 1;
			this.AntiAlias_text.text = "2x";
		}
		else if (this.toggleAntiAlias == 1)
		{
			QualitySettings.antiAliasing = 4;
			this.toggleAntiAlias = 2;
			this.AntiAlias_text.text = "4x";
		}
		else if (this.toggleAntiAlias == 2)
		{
			QualitySettings.antiAliasing = 8;
			this.toggleAntiAlias = 3;
			this.AntiAlias_text.text = "8x";
		}
		else if (this.toggleAntiAlias == 3)
		{
			QualitySettings.antiAliasing = 0;
			this.toggleAntiAlias = 0;
			this.AntiAlias_text.text = "Off";
		}
		PlayerPrefs.SetInt("toggleAntiAlias", this.toggleAntiAlias);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0004987C File Offset: 0x00047A7C
	private void gfx_setAntiAlias()
	{
		if (this.toggleAntiAlias == 0)
		{
			QualitySettings.antiAliasing = 0;
			this.AntiAlias_text.text = "Off";
			return;
		}
		if (this.toggleAntiAlias == 1)
		{
			QualitySettings.antiAliasing = 2;
			this.AntiAlias_text.text = "2x";
			return;
		}
		if (this.toggleAntiAlias == 2)
		{
			QualitySettings.antiAliasing = 4;
			this.AntiAlias_text.text = "4x";
			return;
		}
		if (this.toggleAntiAlias == 3)
		{
			QualitySettings.antiAliasing = 8;
			this.AntiAlias_text.text = "8x";
		}
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00049908 File Offset: 0x00047B08
	public void gfx_Vsync()
	{
		if (this.toggleVsync == 0)
		{
			QualitySettings.vSyncCount = 1;
			this.toggleVsync = 1;
			this.toggleVsync_text.text = "On";
		}
		else
		{
			QualitySettings.vSyncCount = 0;
			this.toggleVsync = 0;
			this.toggleVsync_text.text = "Off";
		}
		PlayerPrefs.SetInt("toggleVsync", this.toggleVsync);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00049978 File Offset: 0x00047B78
	private void gfx_setVsync()
	{
		QualitySettings.vSyncCount = ((this.toggleVsync == 1) ? 1 : 0);
		this.toggleVsync_text.text = ((this.toggleVsync == 1) ? "On" : "Off");
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x000499AC File Offset: 0x00047BAC
	public void gfx_shadows()
	{
		if (this.toggleShadows == 0)
		{
			QualitySettings.shadows = 1;
			this.toggleShadows = 1;
			this.toggleShadows_text.text = "Hard";
		}
		else if (this.toggleShadows == 1)
		{
			QualitySettings.shadows = 2;
			this.toggleShadows = 2;
			this.toggleShadows_text.text = "Soft";
		}
		else if (this.toggleShadows == 2)
		{
			QualitySettings.shadows = 0;
			this.toggleShadows = 0;
			this.toggleShadows_text.text = "Off";
		}
		PlayerPrefs.SetInt("toggleShadows", this.toggleShadows);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x00049A50 File Offset: 0x00047C50
	private void gfx_setShadows()
	{
		if (this.toggleShadows == 0)
		{
			QualitySettings.shadows = 0;
			this.toggleShadows_text.text = "Off";
			return;
		}
		if (this.toggleShadows == 1)
		{
			QualitySettings.shadows = 1;
			this.toggleShadows_text.text = "Hard";
			return;
		}
		if (this.toggleShadows == 2)
		{
			QualitySettings.shadows = 2;
			this.toggleShadows_text.text = "Soft";
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x00049ABC File Offset: 0x00047CBC
	public void gfx_textureQuality()
	{
		if (this.toggleTextureQuality == 0)
		{
			QualitySettings.masterTextureLimit = 1;
			this.toggleTextureQuality_text.text = "Half";
			this.toggleTextureQuality = 1;
		}
		else if (this.toggleTextureQuality == 1)
		{
			QualitySettings.masterTextureLimit = 2;
			this.toggleTextureQuality_text.text = "Quarter";
			this.toggleTextureQuality = 2;
		}
		else if (this.toggleTextureQuality == 2)
		{
			QualitySettings.masterTextureLimit = 0;
			this.toggleTextureQuality_text.text = "Full";
			this.toggleTextureQuality = 0;
		}
		PlayerPrefs.SetInt("toggleTextureQuality", this.toggleTextureQuality);
		EasyAudioUtility.instance.Play("Hover");
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x00049B60 File Offset: 0x00047D60
	private void gfx_setTextureQuality()
	{
		if (this.toggleTextureQuality == 0)
		{
			QualitySettings.masterTextureLimit = 0;
			this.toggleTextureQuality_text.text = "Full";
			return;
		}
		if (this.toggleTextureQuality == 1)
		{
			QualitySettings.masterTextureLimit = 1;
			this.toggleTextureQuality_text.text = "Half";
			return;
		}
		if (this.toggleTextureQuality == 2)
		{
			QualitySettings.masterTextureLimit = 2;
			this.toggleTextureQuality_text.text = "Quarter";
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x00049BCC File Offset: 0x00047DCC
	public void gfx_ScreenResolution()
	{
		if (this.currentScreenResolutionCount < this.allScreenResolutions.Length)
		{
			this.currentScreenResolutionCount++;
		}
		else
		{
			this.currentScreenResolutionCount = 0;
		}
		this.currentScreenResolution_text.text = Screen.currentResolution.width + " x " + Screen.currentResolution.height;
		PlayerPrefs.SetInt("currentScreenResolutionCount", this.currentScreenResolutionCount);
	}

	// Token: 0x06000C37 RID: 3127 RVA: 0x00049C4C File Offset: 0x00047E4C
	private void gfx_setScreenResolution()
	{
		this.allScreenResolutions = Screen.resolutions;
		if (PlayerPrefs.HasKey("currentScreenResolutionCount"))
		{
			this.currentScreenResolution_text.text = this.allScreenResolutions[this.currentScreenResolutionCount].width + " x " + this.allScreenResolutions[this.currentScreenResolutionCount].height;
		}
		this.currentScreenResolution_text.text = Screen.currentResolution.width + " x " + Screen.currentResolution.height;
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x00049CF4 File Offset: 0x00047EF4
	private void gfx_setDefaults()
	{
		this.toggleFullscreen = PlayerPrefs.GetInt("toggleFullscreen", 1);
		this.toggleAnisoFilt = PlayerPrefs.GetInt("toggleAnisoFilt", 1);
		this.toggleVsync = PlayerPrefs.GetInt("toggleVsync", 1);
		this.toggleShadows = PlayerPrefs.GetInt("toggleShadows", 1);
		this.toggleTextureQuality = PlayerPrefs.GetInt("toggleTextureQuality", 1);
		this.toggleAntiAlias = PlayerPrefs.GetInt("toggleAntiAlias", 1);
		this.currentScreenResolutionCount = PlayerPrefs.GetInt("currentScreenResolutionCount", this.currentScreenResolutionCount);
		this.gfx_setFullScreen();
		this.gfx_setAnisoFiltering();
		this.gfx_setAntiAlias();
		this.gfx_setVsync();
		this.gfx_setShadows();
		this.gfx_setTextureQuality();
		this.gfx_setScreenResolution();
	}

	// Token: 0x04000879 RID: 2169
	[Header("_Graphics Options_")]
	[Space(5f)]
	public Text toggleFullscreen_text;

	// Token: 0x0400087A RID: 2170
	[HideInInspector]
	public int toggleFullscreen;

	// Token: 0x0400087B RID: 2171
	[Space(10f)]
	[HideInInspector]
	public int toggleAnisoFilt;

	// Token: 0x0400087C RID: 2172
	public Text AnisoFiltering_text;

	// Token: 0x0400087D RID: 2173
	[Space(10f)]
	[HideInInspector]
	public int toggleAntiAlias;

	// Token: 0x0400087E RID: 2174
	public Text AntiAlias_text;

	// Token: 0x0400087F RID: 2175
	[Space(10f)]
	[HideInInspector]
	public int toggleVsync;

	// Token: 0x04000880 RID: 2176
	public Text toggleVsync_text;

	// Token: 0x04000881 RID: 2177
	[Space(10f)]
	[HideInInspector]
	public int toggleShadows;

	// Token: 0x04000882 RID: 2178
	public Text toggleShadows_text;

	// Token: 0x04000883 RID: 2179
	[Space(10f)]
	[HideInInspector]
	public int toggleTextureQuality;

	// Token: 0x04000884 RID: 2180
	public Text toggleTextureQuality_text;

	// Token: 0x04000885 RID: 2181
	[Space(10f)]
	[HideInInspector]
	public int currentScreenResolutionCount;

	// Token: 0x04000886 RID: 2182
	public Text currentScreenResolution_text;

	// Token: 0x04000887 RID: 2183
	private Resolution[] allScreenResolutions;
}
