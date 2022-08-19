using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037F RID: 895
[CommandInfo("YSPlayer", "获取势力称号等级", "获取势力称号等级，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShiLiChengHaoLevel : Command
{
	// Token: 0x06001DAE RID: 7598 RVA: 0x000D182A File Offset: 0x000CFA2A
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.GetShiLiChengHaoLevel(this.shiLiID.Value));
		this.Continue();
	}

	// Token: 0x0400183D RID: 6205
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable shiLiID;
}
