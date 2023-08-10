using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "传送固定NPC到三级场景", "传送固定NPC到三级场景(传送后会自动刷新NPC列表)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToScene : Command
{
	[SerializeField]
	[Tooltip("NPCID(老ID)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[SerializeField]
	[Tooltip("场景名称")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	protected StringVariable SceneName;

	public override void OnEnter()
	{
		NPCEx.WarpToScene(NPCEx.NPCIDToNew(NPCID.Value), SceneName.Value);
		Continue();
	}
}
