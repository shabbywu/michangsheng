using System;
using Fungus;
using UnityEngine;

// Token: 0x02000349 RID: 841
[CommandInfo("YSPlayer", "改变NPC行为", "改变NPC行为", 0)]
[AddComponentMenu("")]
public class CmdChangeNPCAction : Command
{
	// Token: 0x060018B0 RID: 6320 RVA: 0x000DD4DC File Offset: 0x000DB6DC
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

	// Token: 0x040013AA RID: 5034
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013AB RID: 5035
	[SerializeField]
	[Tooltip("ActionID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable ActionID;
}
