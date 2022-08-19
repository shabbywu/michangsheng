using System;
using Fungus;
using UnityEngine;

// Token: 0x0200024B RID: 587
[CommandInfo("YSNPCJiaoHu", "设置当前交互的NPC", "设置当前交互的NPC", 0)]
[AddComponentMenu("")]
public class CmdSetJiaoHuNPC : Command
{
	// Token: 0x06001645 RID: 5701 RVA: 0x00096A14 File Offset: 0x00094C14
	public override void OnEnter()
	{
		if (this.NPCID.Value == 0)
		{
			Debug.LogError("设置当前交互NPC出错，NPCID不能为0，当前flowchart:" + this.GetFlowchart().GetParentName() + "，当前block:" + this.ParentBlock.BlockName);
		}
		else
		{
			UINPCData uinpcdata = new UINPCData(this.NPCID.Value, false);
			uinpcdata.RefreshData();
			uinpcdata.IsFight = this.IsFight;
			UINPCJiaoHu.Inst.NowJiaoHuNPC = uinpcdata;
		}
		this.Continue();
	}

	// Token: 0x0400108A RID: 4234
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x0400108B RID: 4235
	[Tooltip("战斗模式(影响战前探查)")]
	[SerializeField]
	protected bool IsFight = true;
}
