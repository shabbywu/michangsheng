using System;
using Fungus;
using UnityEngine;

// Token: 0x02000244 RID: 580
[CommandInfo("YSPlayer", "获取悬赏等级", "获取悬赏等级赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetXuanShangLevel : Command
{
	// Token: 0x06001634 RID: 5684 RVA: 0x00096680 File Offset: 0x00094880
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int xuanShangLevel = PlayerEx.GetXuanShangLevel((this.ShiLiID as IntegerVariable).Value);
		flowchart.SetIntegerVariable("TmpValue", xuanShangLevel);
		this.Continue();
	}

	// Token: 0x04001084 RID: 4228
	[SerializeField]
	[Tooltip("势力ID 0宁州 19无尽之海")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable ShiLiID;
}
