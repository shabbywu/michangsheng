using System;
using Fungus;
using UnityEngine;

// Token: 0x0200036B RID: 875
[CommandInfo("YSPlayer", "添加玩家打听过的NPC", "添加玩家打听过的NPC", 0)]
[AddComponentMenu("")]
public class CmdSetPlayerDaTing : Command
{
	// Token: 0x06001905 RID: 6405 RVA: 0x00015757 File Offset: 0x00013957
	public override void OnEnter()
	{
		PlayerEx.AddDaTingNPC(NPCEx.NPCIDToNew(this.NPCID.Value));
		this.Continue();
	}

	// Token: 0x040013ED RID: 5101
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
