using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "增加NPC情分", "增加NPC情分", 0)]
[AddComponentMenu("")]
public class CmdAddNPCQingFen : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("增加的情分")]
	[SerializeField]
	protected int Count;

	public override void OnEnter()
	{
		NPCEx.AddQingFen(NPCID.Value, Count, showTip: true);
		Continue();
	}
}
