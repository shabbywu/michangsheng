using System;
using Fungus;
using UnityEngine;

// Token: 0x0200036F RID: 879
[CommandInfo("YSPlayer", "传送固定NPC到大地图指定位置", "传送固定NPC到大地图指定位置(传送后会自动刷新NPC列表，玩家当前必须在大地图)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToMap : Command
{
	// Token: 0x0600190D RID: 6413 RVA: 0x000157BE File Offset: 0x000139BE
	public override void OnEnter()
	{
		NPCEx.WarpToMap(NPCEx.NPCIDToNew(this.NPCID.Value), this.MapIndex.Value);
		this.Continue();
	}

	// Token: 0x040013F7 RID: 5111
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013F8 RID: 5112
	[SerializeField]
	[Tooltip("地图点索引(填0则为玩家当前位置)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable MapIndex;
}
