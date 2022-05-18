using System;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class UINPCEventPanel : TabPanelBase
{
	// Token: 0x06001946 RID: 6470 RVA: 0x000E204C File Offset: 0x000E024C
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.ContentRT.DestoryAllChild();
		foreach (UINPCEventData uinpceventData in this.npc.Events)
		{
			Object.Instantiate<GameObject>(this.SVItemPrefab, this.ContentRT).GetComponent<UINPCEventSVItem>().SetEvent(string.Format("第{0}年{1}月", uinpceventData.EventTime.Year, uinpceventData.EventTime.Month), uinpceventData.EventDesc);
		}
	}

	// Token: 0x0400145C RID: 5212
	private UINPCData npc;

	// Token: 0x0400145D RID: 5213
	public RectTransform ContentRT;

	// Token: 0x0400145E RID: 5214
	public GameObject SVItemPrefab;
}
