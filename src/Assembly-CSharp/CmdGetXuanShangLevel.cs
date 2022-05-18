using System;
using Fungus;
using UnityEngine;

// Token: 0x02000360 RID: 864
[CommandInfo("YSPlayer", "获取悬赏等级", "获取悬赏等级赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetXuanShangLevel : Command
{
	// Token: 0x060018EC RID: 6380 RVA: 0x000DEBC4 File Offset: 0x000DCDC4
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int xuanShangLevel = PlayerEx.GetXuanShangLevel((this.ShiLiID as IntegerVariable).Value);
		flowchart.SetIntegerVariable("TmpValue", xuanShangLevel);
		this.Continue();
	}

	// Token: 0x040013DC RID: 5084
	[SerializeField]
	[Tooltip("势力ID 0宁州 19无尽之海")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable ShiLiID;
}
