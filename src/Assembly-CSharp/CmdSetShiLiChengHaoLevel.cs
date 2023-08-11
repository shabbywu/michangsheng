using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "根据变量设置势力称号等级", "根据变量设置势力称号等级", 0)]
[AddComponentMenu("")]
public class CmdSetShiLiChengHaoLevel : Command
{
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected Variable shiLiID;

	[SerializeField]
	[Tooltip("称号等级")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected Variable chengHaoLevel;

	public override void OnEnter()
	{
		PlayerEx.SetShiLiChengHaoLevel((shiLiID as IntegerVariable).Value, (chengHaoLevel as IntegerVariable).Value);
		Continue();
	}
}
