using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取赏金", "获取赏金赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShangJin : Command
{
	[SerializeField]
	[Tooltip("势力ID 0宁州 19无尽之海")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected Variable ShiLiID;

	public override void OnEnter()
	{
		Flowchart flowchart = GetFlowchart();
		int shangJin = PlayerEx.GetShangJin((ShiLiID as IntegerVariable).Value);
		flowchart.SetIntegerVariable("TmpValue", shangJin);
		Continue();
	}
}
