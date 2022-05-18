using System;
using Fungus;
using UnityEngine;

// Token: 0x02000367 RID: 871
[CommandInfo("YSNPCJiaoHu", "设置当前交互的NPC", "设置当前交互的NPC", 0)]
[AddComponentMenu("")]
public class CmdSetJiaoHuNPC : Command
{
	// Token: 0x060018FD RID: 6397 RVA: 0x000DEEE8 File Offset: 0x000DD0E8
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

	// Token: 0x040013E2 RID: 5090
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013E3 RID: 5091
	[Tooltip("战斗模式(影响战前探查)")]
	[SerializeField]
	protected bool IsFight = true;
}
