using System;
using Fungus;
using UnityEngine;

// Token: 0x0200035D RID: 861
[CommandInfo("YSPlayer", "获取玩家人际关系(未完成)", "获取玩家人际关系", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerRelationship : Command
{
	// Token: 0x060018E2 RID: 6370 RVA: 0x00011424 File Offset: 0x0000F624
	public override void OnEnter()
	{
		this.Continue();
	}

	// Token: 0x040013D0 RID: 5072
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
