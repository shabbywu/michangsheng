using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "传送固定NPC到大地图指定位置", "传送固定NPC到大地图指定位置(传送后会自动刷新NPC列表，玩家当前必须在大地图)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToMap : Command
{
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[SerializeField]
	[Tooltip("地图点索引(填0则为玩家当前位置)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable MapIndex;

	public override void OnEnter()
	{
		NPCEx.WarpToMap(NPCEx.NPCIDToNew(NPCID.Value), MapIndex.Value);
		Continue();
	}
}
