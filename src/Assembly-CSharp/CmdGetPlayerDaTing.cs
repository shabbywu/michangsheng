using System;
using Fungus;
using UnityEngine;

// Token: 0x0200023D RID: 573
[CommandInfo("YSPlayer", "获取玩家是否打听过某NPC", "获取玩家是否打听过某NPC，赋值到TmpBool", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerDaTing : Command
{
	// Token: 0x06001622 RID: 5666 RVA: 0x00095ED4 File Offset: 0x000940D4
	public override void OnEnter()
	{
		bool value = PlayerEx.IsDaTing(NPCEx.NPCIDToNew(this.NPCID.Value));
		this.GetFlowchart().SetBooleanVariable("TmpBool", value);
		this.Continue();
	}

	// Token: 0x04001075 RID: 4213
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
