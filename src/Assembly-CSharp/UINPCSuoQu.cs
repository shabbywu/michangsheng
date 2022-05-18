using System;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class UINPCSuoQu : MonoBehaviour, IESCClose
{
	// Token: 0x060019E6 RID: 6630 RVA: 0x000E54C0 File Offset: 0x000E36C0
	public void RefreshUI()
	{
		this.npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		this.npc.RefreshData();
		this.NPCInventory.ID = this.npc.ID;
		this.NPCInventory.NPC = this.npc;
		this.NPCInventory.RefreshUI();
		this.SuoQuSlot.NPCID = this.npc.ID;
		this.SuoQuSlot.SetNull();
	}

	// Token: 0x060019E7 RID: 6631 RVA: 0x000E553C File Offset: 0x000E373C
	public void OnOKBtnClick()
	{
		if (this.SuoQuSlot.NowType != UIIconShow.UIIconType.None)
		{
			NPCEx.SuoQuFromNPC(this.npc, this.SuoQuSlot.tmpItem, this.SuoQuSlot.Count);
			this.RefreshUI();
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	// Token: 0x060019E8 RID: 6632 RVA: 0x0001645B File Offset: 0x0001465B
	public void OnReturnBtnClick()
	{
		UINPCJiaoHu.Inst.HideNPCSuoQu();
		UINPCJiaoHu.Inst.ShowJiaoHuPop();
	}

	// Token: 0x060019E9 RID: 6633 RVA: 0x00016471 File Offset: 0x00014671
	public bool TryEscClose()
	{
		this.OnReturnBtnClick();
		return true;
	}

	// Token: 0x04001533 RID: 5427
	private UINPCData npc;

	// Token: 0x04001534 RID: 5428
	public UIInventory NPCInventory;

	// Token: 0x04001535 RID: 5429
	public UIIconShow SuoQuSlot;
}
