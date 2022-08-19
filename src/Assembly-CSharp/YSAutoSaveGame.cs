using System;
using KBEngine;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public class YSAutoSaveGame : MonoBehaviour
{
	// Token: 0x0600228C RID: 8844 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x000ED56D File Offset: 0x000EB76D
	private void OnDestroy()
	{
		YSAutoSaveGame.RestTime = 0;
		YSAutoSaveGame.IsSave = false;
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x000ED57B File Offset: 0x000EB77B
	public void AutoSave()
	{
		base.Invoke("WaitTime", 0.5f);
	}

	// Token: 0x0600228F RID: 8847 RVA: 0x000ED590 File Offset: 0x000EB790
	public void WaitTime()
	{
		Avatar player = Tools.instance.getPlayer();
		if (player.age > player.shouYuan)
		{
			return;
		}
		if (YSAutoSaveGame.IsSave)
		{
			return;
		}
		YSAutoSaveGame.IsSave = true;
	}

	// Token: 0x06002290 RID: 8848 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001BED RID: 7149
	public static bool IsSave;

	// Token: 0x04001BEE RID: 7150
	public static int RestTime;
}
