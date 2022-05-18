using System;
using Fungus;
using UnityEngine;

// Token: 0x02000369 RID: 873
[CommandInfo("YSNPCJiaoHu", "设置NPC灵石", "设置NPC灵石", 0)]
[AddComponentMenu("")]
public class CmdSetNPCMoney : Command
{
	// Token: 0x06001901 RID: 6401 RVA: 0x00015707 File Offset: 0x00013907
	public override void OnEnter()
	{
		NPCEx.SetMoney(this.NPCID.Value, this.Count.Value);
		this.Continue();
	}

	// Token: 0x040013E9 RID: 5097
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013EA RID: 5098
	[Tooltip("灵石数量")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable Count;
}
