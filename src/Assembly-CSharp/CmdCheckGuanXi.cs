using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "检测是否有特殊关系", "检测是否有特殊关系，赋值到TmpValue，0无关系 1师傅 2兄弟 3道侣 4徒弟", 0)]
[AddComponentMenu("")]
public class CmdCheckGuanXi : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		int value = 0;
		int item = NPCEx.NPCIDToNew(NPCID.Value);
		if (PlayerEx.Player.TeatherId.ToList().Contains(item))
		{
			value = 1;
		}
		if (PlayerEx.Player.Brother.ToList().Contains(item))
		{
			value = 2;
		}
		if (PlayerEx.Player.DaoLvId.ToList().Contains(item))
		{
			value = 3;
		}
		if (PlayerEx.Player.TuDiId.ToList().Contains(item))
		{
			value = 4;
		}
		GetFlowchart().SetIntegerVariable("TmpValue", value);
		Continue();
	}
}
