using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "传送固定NPC到当前副本指定位置", "传送固定NPC到当前副本指定位置(传送后会自动刷新NPC列表，玩家当前必须在副本)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToFuBen : Command
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
		NPCEx.WarpToPlayerNowFuBen(NPCID.Value, MapIndex.Value);
		Continue();
	}
}
