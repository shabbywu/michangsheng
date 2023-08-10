using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "设置npc的状态", "设置npc的状态", 0)]
[AddComponentMenu("")]
public class CmdSetNpcStatus : Command
{
	[SerializeField]
	[Tooltip("npcId")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable npc;

	[SerializeField]
	[Tooltip("状态Id")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable state;

	public override void OnEnter()
	{
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npc.Value, state.Value);
		Continue();
	}
}
