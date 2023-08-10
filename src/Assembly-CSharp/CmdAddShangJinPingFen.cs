using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "根据变量增加赏金评分", "根据变量增加赏金评分", 0)]
[AddComponentMenu("")]
public class CmdAddShangJinPingFen : Command
{
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected Variable shiLiID;

	[SerializeField]
	[Tooltip("增加量")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected Variable addCount;

	public override void OnEnter()
	{
		PlayerEx.AddShangJinPingFen((shiLiID as IntegerVariable).Value, (addCount as IntegerVariable).Value);
		Continue();
	}
}
