using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "设置当前交互的NPC", "设置当前交互的NPC", 0)]
[AddComponentMenu("")]
public class CmdSetJiaoHuNPC : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("战斗模式(影响战前探查)")]
	[SerializeField]
	protected bool IsFight = true;

	public override void OnEnter()
	{
		if (NPCID.Value == 0)
		{
			Debug.LogError((object)("设置当前交互NPC出错，NPCID不能为0，当前flowchart:" + GetFlowchart().GetParentName() + "，当前block:" + ParentBlock.BlockName));
		}
		else
		{
			UINPCData uINPCData = new UINPCData(NPCID.Value);
			uINPCData.RefreshData();
			uINPCData.IsFight = IsFight;
			UINPCJiaoHu.Inst.NowJiaoHuNPC = uINPCData;
		}
		Continue();
	}
}
