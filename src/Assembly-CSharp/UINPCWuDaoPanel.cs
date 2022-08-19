using System;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class UINPCWuDaoPanel : TabPanelBase
{
	// Token: 0x0600172C RID: 5932 RVA: 0x0009E368 File Offset: 0x0009C568
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

	// Token: 0x040011D9 RID: 4569
	private UINPCData npc;

	// Token: 0x040011DA RID: 4570
	public RectTransform ContentRT;

	// Token: 0x040011DB RID: 4571
	public GameObject SVItemPrefab;
}
