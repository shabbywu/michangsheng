using System;
using Fungus;
using UnityEngine;

// Token: 0x02000254 RID: 596
[CommandInfo("YSPlayer", "传送固定NPC到当前副本指定位置", "传送固定NPC到当前副本指定位置(传送后会自动刷新NPC列表，玩家当前必须在副本)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToFuBen : Command
{
	// Token: 0x06001655 RID: 5717 RVA: 0x00096DE9 File Offset: 0x00094FE9
	public override void OnEnter()
	{
		NPCEx.WarpToPlayerNowFuBen(this.NPCID.Value, this.MapIndex.Value);
		this.Continue();
	}

	// Token: 0x040010A3 RID: 4259
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040010A4 RID: 4260
	[SerializeField]
	[Tooltip("地图点索引(填0则为玩家当前位置)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable MapIndex;
}
