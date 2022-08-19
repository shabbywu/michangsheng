using System;
using Fungus;
using UnityEngine;

// Token: 0x0200022C RID: 556
[CommandInfo("YSNPCJiaoHu", "增加NPC情分", "增加NPC情分", 0)]
[AddComponentMenu("")]
public class CmdAddNPCQingFen : Command
{
	// Token: 0x060015F6 RID: 5622 RVA: 0x00094E34 File Offset: 0x00093034
	public override void OnEnter()
	{
		NPCEx.AddQingFen(this.NPCID.Value, this.Count, true);
		this.Continue();
	}

	// Token: 0x04001050 RID: 4176
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x04001051 RID: 4177
	[Tooltip("增加的情分")]
	[SerializeField]
	protected int Count;
}
