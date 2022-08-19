using System;
using Fungus;
using UnityEngine;

// Token: 0x0200022D RID: 557
[CommandInfo("YSPlayer", "改变NPC行为", "改变NPC行为", 0)]
[AddComponentMenu("")]
public class CmdChangeNPCAction : Command
{
	// Token: 0x060015F8 RID: 5624 RVA: 0x00094E54 File Offset: 0x00093054
	public override void OnEnter()
	{
		int num = NPCEx.NPCIDToNew(this.NPCID.Value);
		if (NpcJieSuanManager.inst.IsDeath(num))
		{
			Debug.LogError(string.Format("CmdChangeNPCAction命令出错,id为{0}的NPC已死", num));
			Debug.LogError("talk名称为：" + this.GetFlowchart().GetParentName());
			Debug.LogError("block名称为：" + this.ParentBlock.BlockName);
			this.Continue();
			return;
		}
		jsonData.instance.AvatarJsonData[num.ToString()].SetField("ActionId", this.ActionID.Value);
		if (UINPCJiaoHu.Inst.NowJiaoHuNPC != null)
		{
			UINPCJiaoHu.Inst.NowJiaoHuNPC.RefreshData();
			UINPCSVItem.RefreshNPCTaskID = UINPCJiaoHu.Inst.NowJiaoHuNPC.ID;
		}
		this.Continue();
	}

	// Token: 0x04001052 RID: 4178
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x04001053 RID: 4179
	[SerializeField]
	[Tooltip("ActionID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable ActionID;
}
