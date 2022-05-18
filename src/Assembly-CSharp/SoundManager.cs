using System;
using UnityEngine;

// Token: 0x0200077E RID: 1918
public class SoundManager : MonoBehaviour
{
	// Token: 0x060030F7 RID: 12535 RVA: 0x00184FB8 File Offset: 0x001831B8
	private void Awake()
	{
		base.name = "SoundManager";
		Object.DontDestroyOnLoad(base.gameObject);
		if (PlayerPrefs.HasKey("soundOn"))
		{
			SoundManager.soundOn = (PlayerPrefs.GetInt("soundOn") == 1);
			SoundManager.musicOn = (PlayerPrefs.GetInt("musicOn") == 1);
			return;
		}
		SoundManager.soundOn = (SoundManager.musicOn = true);
		PlayerPrefs.SetInt("soundOn", 1);
		PlayerPrefs.SetInt("musicOn", 1);
		PlayerPrefs.Save();
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x00023EF6 File Offset: 0x000220F6
	private void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("soundOn", SoundManager.soundOn ? 1 : 0);
		PlayerPrefs.SetInt("musicOn", SoundManager.musicOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x00023F27 File Offset: 0x00022127
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PlayerPrefs.SetInt("soundOn", SoundManager.soundOn ? 1 : 0);
			PlayerPrefs.SetInt("musicOn", SoundManager.musicOn ? 1 : 0);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x04002CD2 RID: 11474
	public static bool soundOn;

	// Token: 0x04002CD3 RID: 11475
	public static bool musicOn;
}
