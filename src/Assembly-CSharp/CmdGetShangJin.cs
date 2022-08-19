using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037E RID: 894
[CommandInfo("YSPlayer", "获取赏金", "获取赏金赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShangJin : Command
{
	// Token: 0x06001DAC RID: 7596 RVA: 0x000D17F0 File Offset: 0x000CF9F0
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int shangJin = PlayerEx.GetShangJin((this.ShiLiID as IntegerVariable).Value);
		flowchart.SetIntegerVariable("TmpValue", shangJin);
		this.Continue();
	}

	// Token: 0x0400183C RID: 6204
	[SerializeField]
	[Tooltip("势力ID 0宁州 19无尽之海")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable ShiLiID;
}
