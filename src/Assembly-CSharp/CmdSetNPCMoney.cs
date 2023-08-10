using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "设置NPC灵石", "设置NPC灵石", 0)]
[AddComponentMenu("")]
public class CmdSetNPCMoney : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("灵石数量")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable Count;

	public override void OnEnter()
	{
		NPCEx.SetMoney(NPCID.Value, Count.Value);
		Continue();
	}
}
