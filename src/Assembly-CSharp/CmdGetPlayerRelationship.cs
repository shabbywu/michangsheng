using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取玩家人际关系(未完成)", "获取玩家人际关系", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerRelationship : Command
{
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		Continue();
	}
}
