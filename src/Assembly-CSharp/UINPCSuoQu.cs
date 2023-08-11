using UnityEngine;

public class UINPCSuoQu : MonoBehaviour, IESCClose
{
	private UINPCData npc;

	public UIInventory NPCInventory;

	public UIIconShow SuoQuSlot;

	public void RefreshUI()
	{
		npc = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		npc.RefreshData();
		NPCInventory.ID = npc.ID;
		NPCInventory.NPC = npc;
		NPCInventory.RefreshUI();
		SuoQuSlot.NPCID = npc.ID;
		SuoQuSlot.SetNull();
	}

	public void OnOKBtnClick()
	{
		if (SuoQuSlot.NowType != 0)
		{
			NPCEx.SuoQuFromNPC(npc, SuoQuSlot.tmpItem, SuoQuSlot.Count);
			RefreshUI();
			NpcJieSuanManager.inst.isUpDateNpcList = true;
		}
	}

	public void OnReturnBtnClick()
	{
		UINPCJiaoHu.Inst.HideNPCSuoQu();
		UINPCJiaoHu.Inst.ShowJiaoHuPop();
	}

	public bool TryEscClose()
	{
		OnReturnBtnClick();
		return true;
	}
}
