using System;
using Fungus;
using UnityEngine;

// Token: 0x02000255 RID: 597
[CommandInfo("YSPlayer", "传送固定NPC到大地图指定位置", "传送固定NPC到大地图指定位置(传送后会自动刷新NPC列表，玩家当前必须在大地图)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToMap : Command
{
	// Token: 0x06001657 RID: 5719 RVA: 0x00096E0C File Offset: 0x0009500C
	public override void OnEnter()
	{
		NPCEx.WarpToMap(NPCEx.NPCIDToNew(this.NPCID.Value), this.MapIndex.Value);
		this.Continue();
	}

	// Token: 0x040010A5 RID: 4261
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040010A6 RID: 4262
	[SerializeField]
	[Tooltip("地图点索引(填0则为玩家当前位置)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable MapIndex;
}
