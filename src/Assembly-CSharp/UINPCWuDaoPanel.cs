using System;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class UINPCWuDaoPanel : TabPanelBase
{
	// Token: 0x060019FC RID: 6652 RVA: 0x000E5BC8 File Offset: 0x000E3DC8
	public override void OnPanelShow()
	{
		base.OnPanelShow();
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.ContentRT.DestoryAllChild();
		foreach (UINPCWuDaoData wuDao in this.npc.WuDao)
		{
			Object.Instantiate<GameObject>(this.SVItemPrefab, this.ContentRT).GetComponent<UINPCWuDaoSVItem>().SetWuDao(wuDao);
		}
	}

	// Token: 0x0400154C RID: 5452
	private UINPCData npc;

	// Token: 0x0400154D RID: 5453
	public RectTransform ContentRT;

	// Token: 0x0400154E RID: 5454
	public GameObject SVItemPrefab;
}
