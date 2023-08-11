using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "检测是否有某个特殊关系", "检测是否有某个特殊关系", 0)]
[AddComponentMenu("")]
public class CmdCheckGuanXi2 : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("关系")]
	[SerializeField]
	protected GuanXiType GuanXi;

	[Tooltip("是否有关系")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	protected BooleanVariable HasGuanXi;

	public override void OnEnter()
	{
		int item = NPCEx.NPCIDToNew(NPCID.Value);
		if (GuanXi == GuanXiType.None)
		{
			HasGuanXi.Value = false;
		}
		else if (GuanXi == GuanXiType.道侣)
		{
			if (PlayerEx.Player.DaoLvId.ToList().Contains(item))
			{
				HasGuanXi.Value = true;
			}
		}
		else if (GuanXi == GuanXiType.师傅)
		{
			if (PlayerEx.Player.TeatherId.ToList().Contains(item))
			{
				HasGuanXi.Value = true;
			}
		}
		else if (GuanXi == GuanXiType.兄弟 && PlayerEx.Player.Brother.ToList().Contains(item))
		{
			HasGuanXi.Value = true;
		}
		Continue();
	}
}
