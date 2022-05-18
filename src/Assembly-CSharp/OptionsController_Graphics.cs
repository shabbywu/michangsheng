using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018E RID: 398
public class OptionsController_Graphics : MonoBehaviour
{
	// Token: 0x06000D4A RID: 3402 RVA: 0x0000F0BF File Offset: 0x0000D2BF
	private void Start()
	{
		this.gfx_setDefaults();
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0009AFA4 File Offset: 0x000991A4
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

	// Token: 0x06000D4C RID: 3404 RVA: 0x0000F0C7 File Offset: 0x0000D2C7
	private void gfx_setFullScreen()
	{
		this.toggleFullscreen_text.text = ((this.toggleFullscreen == 1) ? "Yes" : "No");
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0009B008 File Offset: 0x00099208
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

	// Token: 0x06000D4E RID: 3406 RVA: 0x0000F0E9 File Offset: 0x0000D2E9
	private void gfx_setAnisoFiltering()
	{
		QualitySettings.anisotropicFiltering = ((this.toggleAnisoFilt == 1) ? 1 : 0);
		this.AnisoFiltering_text.text = ((this.toggleAnisoFilt == 1) ? "On" : "Off");
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0009B080 File Offset: 0x00099280
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

	// Token: 0x06000D50 RID: 3408 RVA: 0x0009B14C File Offset: 0x0009934C
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

	// Token: 0x06000D51 RID: 3409 RVA: 0x0009B1D8 File Offset: 0x000993D8
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

	// Token: 0x06000D52 RID: 3410 RVA: 0x0000F11D File Offset: 0x0000D31D
	private void gfx_setVsync()
	{
		QualitySettings.vSyncCount = ((this.toggleVsync == 1) ? 1 : 0);
		this.toggleVsync_text.text = ((this.toggleVsync == 1) ? "On" : "Off");
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0009B248 File Offset: 0x00099448
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

	// Token: 0x06000D54 RID: 3412 RVA: 0x0009B2EC File Offset: 0x000994EC
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

	// Token: 0x06000D55 RID: 3413 RVA: 0x0009B358 File Offset: 0x00099558
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

	// Token: 0x06000D56 RID: 3414 RVA: 0x0009B3FC File Offset: 0x000995FC
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

	// Token: 0x06000D57 RID: 3415 RVA: 0x0009B468 File Offset: 0x00099668
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

	// Token: 0x06000D58 RID: 3416 RVA: 0x0009B4E8 File Offset: 0x000996E8
	private void gfx_setScreenResolution()
	{
		this.allScreenResolutions = Screen.resolutions;
		if (PlayerPrefs.HasKey("currentScreenResolutionCount"))
		{
			this.currentScreenResolution_text.text = this.allScreenResolutions[this.currentScreenResolutionCount].width + " x " + this.allScreenResolutions[this.currentScreenResolutionCount].height;
		}
		this.currentScreenResolution_text.text = Screen.currentResolution.width + " x " + Screen.currentResolution.height;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0009B590 File Offset: 0x00099790
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

	// Token: 0x04000A75 RID: 2677
	[Header("_Graphics Options_")]
	[Space(5f)]
	public Text toggleFullscreen_text;

	// Token: 0x04000A76 RID: 2678
	[HideInInspector]
	public int toggleFullscreen;

	// Token: 0x04000A77 RID: 2679
	[Space(10f)]
	[HideInInspector]
	public int toggleAnisoFilt;

	// Token: 0x04000A78 RID: 2680
	public Text AnisoFiltering_text;

	// Token: 0x04000A79 RID: 2681
	[Space(10f)]
	[HideInInspector]
	public int toggleAntiAlias;

	// Token: 0x04000A7A RID: 2682
	public Text AntiAlias_text;

	// Token: 0x04000A7B RID: 2683
	[Space(10f)]
	[HideInInspector]
	public int toggleVsync;

	// Token: 0x04000A7C RID: 2684
	public Text toggleVsync_text;

	// Token: 0x04000A7D RID: 2685
	[Space(10f)]
	[HideInInspector]
	public int toggleShadows;

	// Token: 0x04000A7E RID: 2686
	public Text toggleShadows_text;

	// Token: 0x04000A7F RID: 2687
	[Space(10f)]
	[HideInInspector]
	public int toggleTextureQuality;

	// Token: 0x04000A80 RID: 2688
	public Text toggleTextureQuality_text;

	// Token: 0x04000A81 RID: 2689
	[Space(10f)]
	[HideInInspector]
	public int currentScreenResolutionCount;

	// Token: 0x04000A82 RID: 2690
	public Text currentScreenResolution_text;

	// Token: 0x04000A83 RID: 2691
	private Resolution[] allScreenResolutions;
}
