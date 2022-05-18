using System;
using Fungus;
using UnityEngine;

// Token: 0x0200036E RID: 878
[CommandInfo("YSPlayer", "传送固定NPC到当前副本指定位置", "传送固定NPC到当前副本指定位置(传送后会自动刷新NPC列表，玩家当前必须在副本)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToFuBen : Command
{
	// Token: 0x0600190B RID: 6411 RVA: 0x0001579B File Offset: 0x0001399B
	public override void OnEnter()
	{
		NPCEx.WarpToPlayerNowFuBen(this.NPCID.Value, this.MapIndex.Value);
		this.Continue();
	}

	// Token: 0x040013F5 RID: 5109
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013F6 RID: 5110
	[SerializeField]
	[Tooltip("地图点索引(填0则为玩家当前位置)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable MapIndex;
}
