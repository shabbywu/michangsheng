using System;
using Fungus;
using UnityEngine;

// Token: 0x02000251 RID: 593
[CommandInfo("YSPlayer", "添加玩家打听过的NPC", "添加玩家打听过的NPC", 0)]
[AddComponentMenu("")]
public class CmdSetPlayerDaTing : Command
{
	// Token: 0x0600164F RID: 5711 RVA: 0x00096D3A File Offset: 0x00094F3A
	public override void OnEnter()
	{
		PlayerEx.AddDaTingNPC(NPCEx.NPCIDToNew(this.NPCID.Value));
		this.Continue();
	}

	// Token: 0x0400109B RID: 4251
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
