using System;
using Fungus;
using UnityEngine;

// Token: 0x02000229 RID: 553
[CommandInfo("YSNPCJiaoHu", "根据变量增加好感度", "根据变量增加好感度", 0)]
[AddComponentMenu("")]
public class CmdAddFavor : Command
{
	// Token: 0x060015F0 RID: 5616 RVA: 0x00094D84 File Offset: 0x00092F84
	public override void OnEnter()
	{
		NPCEx.AddFavor(UINPCSVItem.RefreshNPCFavorID = NPCEx.NPCIDToNew(this.NPCID.Value), this.Favor.Value, true, this.ShowTips);
		this.Continue();
	}

	// Token: 0x04001049 RID: 4169
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x0400104A RID: 4170
	[SerializeField]
	[Tooltip("增加的好感度")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable Favor;

	// Token: 0x0400104B RID: 4171
	[SerializeField]
	[Tooltip("是否显示提示")]
	protected bool ShowTips;
}
