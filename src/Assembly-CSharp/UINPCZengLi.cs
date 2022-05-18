using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A7 RID: 935
public class UINPCZengLi : MonoBehaviour, IESCClose
{
	// Token: 0x06001A05 RID: 6661 RVA: 0x000E6084 File Offset: 0x000E4284
	public void RefreshUI()
	{
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		this.NPCXingQu.npc = this.npc;
		this.NPCXingQu.RefreshUI();
		this.PlayerInventory.NPCID = this.npc.ID;
		this.PlayerInventory.RefreshUI();
		this.QingFenText.text = this.npc.QingFen.ToString();
		this.ZengLiSlot.NPCID = this.npc.ID;
		this.ZengLiSlot.SetNull();
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x000E6128 File Offset: 0x000E4328
	public void OnOKBtnClick()
	{
		if (this.ZengLiSlot.NowType != UIIconShow.UIIconType.None)
		{
			NPCEx.ZengLiToNPC(this.npc, this.ZengLiSlot.tmpItem, this.ZengLiSlot.Count);
			this.RefreshUI();
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	// Token: 0x06001A07 RID: 6663 RVA: 0x00016538 File Offset: 0x00014738
	public void OnReturnBtnClick()
	{
		UINPCJiaoHu.Inst.HideNPCZengLi();
		UINPCJiaoHu.Inst.ShowJiaoHuPop();
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x0001654E File Offset: 0x0001474E
	public bool TryEscClose()
	{
		this.OnReturnBtnClick();
		return true;
	}

	// Token: 0x0400155B RID: 5467
	private UINPCData npc;

	// Token: 0x0400155C RID: 5468
	public UIInventory PlayerInventory;

	// Token: 0x0400155D RID: 5469
	public UIIconShow ZengLiSlot;

	// Token: 0x0400155E RID: 5470
	public UINPCXingQu NPCXingQu;

	// Token: 0x0400155F RID: 5471
	public Text QingFenText;
}
