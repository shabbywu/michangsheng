using System;
using Fungus;
using UnityEngine;

// Token: 0x02000501 RID: 1281
[CommandInfo("YSPlayer", "获取势力称号等级", "获取势力称号等级，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShiLiChengHaoLevel : Command
{
	// Token: 0x06002125 RID: 8485 RVA: 0x0001B49B File Offset: 0x0001969B
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.GetShiLiChengHaoLevel(this.shiLiID.Value));
		this.Continue();
	}

	// Token: 0x04001C96 RID: 7318
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable shiLiID;
}
