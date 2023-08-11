using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取势力称号等级", "获取势力称号等级，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShiLiChengHaoLevel : Command
{
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable shiLiID;

	public override void OnEnter()
	{
		GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.GetShiLiChengHaoLevel(shiLiID.Value));
		Continue();
	}
}
