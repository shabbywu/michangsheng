using System;
using Fungus;
using UnityEngine;

// Token: 0x02000380 RID: 896
[CommandInfo("YSPlayer", "根据变量设置势力称号等级", "根据变量设置势力称号等级", 0)]
[AddComponentMenu("")]
public class CmdSetShiLiChengHaoLevel : Command
{
	// Token: 0x06001DB0 RID: 7600 RVA: 0x000D1852 File Offset: 0x000CFA52
	public override void OnEnter()
	{
		PlayerEx.SetShiLiChengHaoLevel((this.shiLiID as IntegerVariable).Value, (this.chengHaoLevel as IntegerVariable).Value);
		this.Continue();
	}

	// Token: 0x0400183E RID: 6206
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable shiLiID;

	// Token: 0x0400183F RID: 6207
	[SerializeField]
	[Tooltip("称号等级")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable chengHaoLevel;
}
