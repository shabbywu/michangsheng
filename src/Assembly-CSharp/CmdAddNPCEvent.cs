using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "添加NPC重要事件", "添加NPC重要事件", 0)]
[AddComponentMenu("")]
public class CmdAddNPCEvent : Command
{
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[SerializeField]
	[Tooltip("事件描述")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	protected StringVariable EventDesc;

	public override void OnEnter()
	{
		NPCEx.AddEvent(NPCEx.NPCIDToNew(NPCID.Value), PlayerEx.Player.worldTimeMag.nowTime, EventDesc.Value);
		Continue();
	}
}
