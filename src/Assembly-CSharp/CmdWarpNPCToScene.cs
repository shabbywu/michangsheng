using System;
using Fungus;
using UnityEngine;

// Token: 0x02000370 RID: 880
[CommandInfo("YSPlayer", "传送固定NPC到三级场景", "传送固定NPC到三级场景(传送后会自动刷新NPC列表)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToScene : Command
{
	// Token: 0x0600190F RID: 6415 RVA: 0x000157E6 File Offset: 0x000139E6
	public override void OnEnter()
	{
		NPCEx.WarpToScene(NPCEx.NPCIDToNew(this.NPCID.Value), this.SceneName.Value);
		this.Continue();
	}

	// Token: 0x040013F9 RID: 5113
	[SerializeField]
	[Tooltip("NPCID(老ID)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013FA RID: 5114
	[SerializeField]
	[Tooltip("场景名称")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable SceneName;
}
