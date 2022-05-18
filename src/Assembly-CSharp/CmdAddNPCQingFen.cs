using System;
using Fungus;
using UnityEngine;

// Token: 0x02000348 RID: 840
[CommandInfo("YSNPCJiaoHu", "增加NPC情分", "增加NPC情分", 0)]
[AddComponentMenu("")]
public class CmdAddNPCQingFen : Command
{
	// Token: 0x060018AE RID: 6318 RVA: 0x0001552D File Offset: 0x0001372D
	public override void OnEnter()
	{
		NPCEx.AddQingFen(this.NPCID.Value, this.Count, true);
		this.Continue();
	}

	// Token: 0x040013A8 RID: 5032
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013A9 RID: 5033
	[Tooltip("增加的情分")]
	[SerializeField]
	protected int Count;
}
