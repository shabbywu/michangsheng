using System;
using Fungus;
using UnityEngine;

// Token: 0x02000345 RID: 837
[CommandInfo("YSNPCJiaoHu", "根据变量增加好感度", "根据变量增加好感度", 0)]
[AddComponentMenu("")]
public class CmdAddFavor : Command
{
	// Token: 0x060018A8 RID: 6312 RVA: 0x000154C1 File Offset: 0x000136C1
	public override void OnEnter()
	{
		NPCEx.AddFavor(UINPCSVItem.RefreshNPCFavorID = NPCEx.NPCIDToNew(this.NPCID.Value), this.Favor.Value, true, this.ShowTips);
		this.Continue();
	}

	// Token: 0x040013A1 RID: 5025
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013A2 RID: 5026
	[SerializeField]
	[Tooltip("增加的好感度")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable Favor;

	// Token: 0x040013A3 RID: 5027
	[SerializeField]
	[Tooltip("是否显示提示")]
	protected bool ShowTips;
}
