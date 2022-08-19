using System;
using Fungus;
using UnityEngine;

// Token: 0x0200022B RID: 555
[CommandInfo("YSNPCJiaoHu", "添加NPC重要事件", "添加NPC重要事件", 0)]
[AddComponentMenu("")]
public class CmdAddNPCEvent : Command
{
	// Token: 0x060015F4 RID: 5620 RVA: 0x00094DFD File Offset: 0x00092FFD
	public override void OnEnter()
	{
		NPCEx.AddEvent(NPCEx.NPCIDToNew(this.NPCID.Value), PlayerEx.Player.worldTimeMag.nowTime, this.EventDesc.Value);
		this.Continue();
	}

	// Token: 0x0400104E RID: 4174
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x0400104F RID: 4175
	[SerializeField]
	[Tooltip("事件描述")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable EventDesc;
}
