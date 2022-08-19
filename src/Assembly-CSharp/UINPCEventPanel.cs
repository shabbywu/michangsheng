using System;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class UINPCEventPanel : TabPanelBase
{
	// Token: 0x06001694 RID: 5780 RVA: 0x0009A12C File Offset: 0x0009832C
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

	// Token: 0x0400110C RID: 4364
	private UINPCData npc;

	// Token: 0x0400110D RID: 4365
	public RectTransform ContentRT;

	// Token: 0x0400110E RID: 4366
	public GameObject SVItemPrefab;
}
