using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class SystemConfig
{
	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06001252 RID: 4690 RVA: 0x0006F39E File Offset: 0x0006D59E
	public static SystemConfig Inst
	{
		get
		{
			if (SystemConfig._inst == null)
			{
				SystemConfig._inst = new SystemConfig();
			}
			return SystemConfig._inst;
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x0006F3B6 File Offset: 0x0006D5B6
	public float GetBackGroundVolume()
	{
		return PlayerPrefs.GetFloat("MusicBg", 0.5f);
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x0006F3C7 File Offset: 0x0006D5C7
	public float GetEffectVolume()
	{
		return PlayerPrefs.GetFloat("MusicEffect", 0.5f);
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x0006F3D8 File Offset: 0x0006D5D8
	public int GetSaveTimes()
	{
		return PlayerPrefs.GetInt("SaveTimes", 10);
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0006F3E6 File Offset: 0x0006D5E6
	public void SetSaveTimes(int value)
	{
		PlayerPrefs.SetInt("SaveTimes", value);
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x0006F3F3 File Offset: 0x0006D5F3
	public int GetNpcActionTimes()
	{
		return PlayerPrefs.GetInt("NpcActionTimes", 0);
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x0006F400 File Offset: 0x0006D600
	public void SetActionTimes(int value)
	{
		PlayerPrefs.SetInt("NpcActionTimes", value);
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x0006F40D File Offset: 0x0006D60D
	public void Reset()
	{
		Screen.SetResolution(1280, 720, false);
	}

	// Token: 0x04000CF8 RID: 3320
	private static SystemConfig _inst;
}
