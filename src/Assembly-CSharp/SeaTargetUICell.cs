using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D0 RID: 976
public class SeaTargetUICell : MonoBehaviour
{
	// Token: 0x06001FC2 RID: 8130 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x000DFE0C File Offset: 0x000DE00C
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

	// Token: 0x06001FC4 RID: 8132 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x040019D4 RID: 6612
	public int eventId;

	// Token: 0x040019D5 RID: 6613
	public Button jiaohu;

	// Token: 0x040019D6 RID: 6614
	public Text Title;
}
