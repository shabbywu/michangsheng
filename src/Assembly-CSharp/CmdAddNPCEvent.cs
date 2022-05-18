using System;
using Fungus;
using UnityEngine;

// Token: 0x02000347 RID: 839
[CommandInfo("YSNPCJiaoHu", "添加NPC重要事件", "添加NPC重要事件", 0)]
[AddComponentMenu("")]
public class CmdAddNPCEvent : Command
{
	// Token: 0x060018AC RID: 6316 RVA: 0x000154F6 File Offset: 0x000136F6
	public override void OnEnter()
	{
		NPCEx.AddEvent(NPCEx.NPCIDToNew(this.NPCID.Value), PlayerEx.Player.worldTimeMag.nowTime, this.EventDesc.Value);
		this.Continue();
	}

	// Token: 0x040013A6 RID: 5030
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013A7 RID: 5031
	[SerializeField]
	[Tooltip("事件描述")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	protected StringVariable EventDesc;
}
