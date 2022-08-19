using System;
using Fungus;
using UnityEngine;

// Token: 0x0200024F RID: 591
[CommandInfo("YSNPCJiaoHu", "设置NPC灵石", "设置NPC灵石", 0)]
[AddComponentMenu("")]
public class CmdSetNPCMoney : Command
{
	// Token: 0x0600164B RID: 5707 RVA: 0x00096CEA File Offset: 0x00094EEA
	public override void OnEnter()
	{
		NPCEx.SetMoney(this.NPCID.Value, this.Count.Value);
		this.Continue();
	}

	// Token: 0x04001097 RID: 4247
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x04001098 RID: 4248
	[Tooltip("灵石数量")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable Count;
}
