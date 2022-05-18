using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class SystemConfig
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x060014FD RID: 5373 RVA: 0x000133E0 File Offset: 0x000115E0
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

	// Token: 0x060014FE RID: 5374 RVA: 0x000133F8 File Offset: 0x000115F8
	public float GetBackGroundVolume()
	{
		return PlayerPrefs.GetFloat("MusicBg", 0.5f);
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x00013409 File Offset: 0x00011609
	public float GetEffectVolume()
	{
		return PlayerPrefs.GetFloat("MusicEffect", 0.5f);
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x0001341A File Offset: 0x0001161A
	public int GetSaveTimes()
	{
		return PlayerPrefs.GetInt("SaveTimes", 10);
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x00013428 File Offset: 0x00011628
	public void SetSaveTimes(int value)
	{
		PlayerPrefs.SetInt("SaveTimes", value);
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x00013435 File Offset: 0x00011635
	public int GetNpcActionTimes()
	{
		return PlayerPrefs.GetInt("NpcActionTimes", 0);
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x00013442 File Offset: 0x00011642
	public void SetActionTimes(int value)
	{
		PlayerPrefs.SetInt("NpcActionTimes", value);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0001344F File Offset: 0x0001164F
	public void Reset()
	{
		Screen.SetResolution(1280, 720, false);
	}

	// Token: 0x04001020 RID: 4128
	private static SystemConfig _inst;
}
