using System;
using UnityEngine;

// Token: 0x020004F5 RID: 1269
public class SoundManager : MonoBehaviour
{
	// Token: 0x0600291C RID: 10524 RVA: 0x0013823C File Offset: 0x0013643C
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

	// Token: 0x0600291D RID: 10525 RVA: 0x001382BF File Offset: 0x001364BF
	private void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("soundOn", SoundManager.soundOn ? 1 : 0);
		PlayerPrefs.SetInt("musicOn", SoundManager.musicOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	// Token: 0x0600291E RID: 10526 RVA: 0x001382F0 File Offset: 0x001364F0
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			PlayerPrefs.SetInt("soundOn", SoundManager.soundOn ? 1 : 0);
			PlayerPrefs.SetInt("musicOn", SoundManager.musicOn ? 1 : 0);
			PlayerPrefs.Save();
		}
	}

	// Token: 0x0400250C RID: 9484
	public static bool soundOn;

	// Token: 0x0400250D RID: 9485
	public static bool musicOn;
}
