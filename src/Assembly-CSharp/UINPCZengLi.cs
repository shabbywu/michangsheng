using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000280 RID: 640
public class UINPCZengLi : MonoBehaviour, IESCClose
{
	// Token: 0x06001735 RID: 5941 RVA: 0x0009E848 File Offset: 0x0009CA48
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

	// Token: 0x06001736 RID: 5942 RVA: 0x0009E8EC File Offset: 0x0009CAEC
	public void OnOKBtnClick()
	{
		if (this.ZengLiSlot.NowType != UIIconShow.UIIconType.None)
		{
			NPCEx.ZengLiToNPC(this.npc, this.ZengLiSlot.tmpItem, this.ZengLiSlot.Count);
			this.RefreshUI();
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x0009E938 File Offset: 0x0009CB38
	public void OnReturnBtnClick()
	{
		UINPCJiaoHu.Inst.HideNPCZengLi();
		UINPCJiaoHu.Inst.ShowJiaoHuPop();
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x0009E94E File Offset: 0x0009CB4E
	public bool TryEscClose()
	{
		this.OnReturnBtnClick();
		return true;
	}

	// Token: 0x040011E8 RID: 4584
	private UINPCData npc;

	// Token: 0x040011E9 RID: 4585
	public UIInventory PlayerInventory;

	// Token: 0x040011EA RID: 4586
	public UIIconShow ZengLiSlot;

	// Token: 0x040011EB RID: 4587
	public UINPCXingQu NPCXingQu;

	// Token: 0x040011EC RID: 4588
	public Text QingFenText;
}
