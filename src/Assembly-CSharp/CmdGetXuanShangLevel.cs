using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取悬赏等级", "获取悬赏等级赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetXuanShangLevel : Command
{
	[SerializeField]
	[Tooltip("势力ID 0宁州 19无尽之海")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected Variable ShiLiID;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		int xuanShangLevel = PlayerEx.GetXuanShangLevel((ShiLiID as IntegerVariable).Value);
		flowchart.SetIntegerVariable("TmpValue", xuanShangLevel);
		Continue();
	}
}
