using System;
using KBEngine;
using UnityEngine;

// Token: 0x020005F6 RID: 1526
public class YSAutoSaveGame : MonoBehaviour
{
	// Token: 0x0600264B RID: 9803 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600264C RID: 9804 RVA: 0x0001E890 File Offset: 0x0001CA90
	private void OnDestroy()
	{
		YSAutoSaveGame.RestTime = 0;
		YSAutoSaveGame.IsSave = false;
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x0001E89E File Offset: 0x0001CA9E
	public void AutoSave()
	{
		base.Invoke("WaitTime", 0.5f);
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x0012E3F0 File Offset: 0x0012C5F0
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

	// Token: 0x0600264F RID: 9807 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x040020B9 RID: 8377
	public static bool IsSave;

	// Token: 0x040020BA RID: 8378
	public static int RestTime;
}
