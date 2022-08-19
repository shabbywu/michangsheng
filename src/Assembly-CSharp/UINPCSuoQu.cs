using System;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class UINPCSuoQu : MonoBehaviour, IESCClose
{
	// Token: 0x06001716 RID: 5910 RVA: 0x0009DBA8 File Offset: 0x0009BDA8
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

	// Token: 0x06001717 RID: 5911 RVA: 0x0009DC24 File Offset: 0x0009BE24
	public void OnOKBtnClick()
	{
		if (this.SuoQuSlot.NowType != UIIconShow.UIIconType.None)
		{
			NPCEx.SuoQuFromNPC(this.npc, this.SuoQuSlot.tmpItem, this.SuoQuSlot.Count);
			this.RefreshUI();
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x0009DC70 File Offset: 0x0009BE70
	public void OnReturnBtnClick()
	{
		UINPCJiaoHu.Inst.HideNPCSuoQu();
		UINPCJiaoHu.Inst.ShowJiaoHuPop();
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x0009DC86 File Offset: 0x0009BE86
	public bool TryEscClose()
	{
		this.OnReturnBtnClick();
		return true;
	}

	// Token: 0x040011C0 RID: 4544
	private UINPCData npc;

	// Token: 0x040011C1 RID: 4545
	public UIInventory NPCInventory;

	// Token: 0x040011C2 RID: 4546
	public UIIconShow SuoQuSlot;
}
