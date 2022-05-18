using System;
using Fungus;
using UnityEngine;

// Token: 0x02000502 RID: 1282
[CommandInfo("YSPlayer", "根据变量设置势力称号等级", "根据变量设置势力称号等级", 0)]
[AddComponentMenu("")]
public class CmdSetShiLiChengHaoLevel : Command
{
	// Token: 0x06002127 RID: 8487 RVA: 0x0001B4C3 File Offset: 0x000196C3
	public override void OnEnter()
	{
		PlayerEx.SetShiLiChengHaoLevel((this.shiLiID as IntegerVariable).Value, (this.chengHaoLevel as IntegerVariable).Value);
		this.Continue();
	}

	// Token: 0x04001C97 RID: 7319
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable shiLiID;

	// Token: 0x04001C98 RID: 7320
	[SerializeField]
	[Tooltip("称号等级")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable chengHaoLevel;
}
