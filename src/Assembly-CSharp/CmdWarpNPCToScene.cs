using System;
using Fungus;
using UnityEngine;

// Token: 0x02000256 RID: 598
[CommandInfo("YSPlayer", "传送固定NPC到三级场景", "传送固定NPC到三级场景(传送后会自动刷新NPC列表)", 0)]
[AddComponentMenu("")]
public class CmdWarpNPCToScene : Command
{
	// Token: 0x06001659 RID: 5721 RVA: 0x00096E34 File Offset: 0x00095034
	public override void OnEnter()
	{
		NPCEx.WarpToScene(NPCEx.NPCIDToNew(this.NPCID.Value), this.SceneName.Value);
		this.Continue();
	}

	// Token: 0x040010A7 RID: 4263
	[SerializeField]
	[Tooltip("NPCID(老ID)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040010A8 RID: 4264
	[SerializeField]
	[Tooltip("场景名称")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable SceneName;
}
