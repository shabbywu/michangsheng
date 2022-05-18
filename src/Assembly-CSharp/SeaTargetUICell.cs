using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000567 RID: 1383
public class SeaTargetUICell : MonoBehaviour
{
	// Token: 0x0600233B RID: 9019 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x001228F0 File Offset: 0x00120AF0
	public void click()
	{
		bool flag = true;
		foreach (SeaAvatarObjBase seaAvatarObjBase in EndlessSeaMag.Inst.MonstarList)
		{
			if (seaAvatarObjBase._EventId == this.eventId)
			{
				seaAvatarObjBase.EventShiJian();
				flag = false;
				break;
			}
		}
		if (flag)
		{
			int num = (int)jsonData.instance.EndlessSeaNPCData[string.Concat(this.eventId)]["EventList"];
			Object.Instantiate<GameObject>(Resources.Load<GameObject>("talkPrefab/TalkPrefab/talk" + num));
		}
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04001E5E RID: 7774
	public int eventId;

	// Token: 0x04001E5F RID: 7775
	public Button jiaohu;

	// Token: 0x04001E60 RID: 7776
	public Text Title;
}
