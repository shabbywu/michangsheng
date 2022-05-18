using System;
using Fungus;
using UnityEngine;

// Token: 0x02000500 RID: 1280
[CommandInfo("YSPlayer", "获取赏金", "获取赏金赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShangJin : Command
{
	// Token: 0x06002123 RID: 8483 RVA: 0x001157C0 File Offset: 0x001139C0
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int shangJin = PlayerEx.GetShangJin((this.ShiLiID as IntegerVariable).Value);
		flowchart.SetIntegerVariable("TmpValue", shangJin);
		this.Continue();
	}

	// Token: 0x04001C95 RID: 7317
	[SerializeField]
	[Tooltip("势力ID 0宁州 19无尽之海")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable ShiLiID;
}
