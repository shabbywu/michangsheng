using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSSea", "增加海域探索度", "增加海域探索度", 0)]
[AddComponentMenu("")]
public class CmdAddSeaTanSuoDu : Command
{
	[Tooltip("海域ID(大海域)")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable SeaID;

	[Tooltip("增加的探索度")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable TanSuoDu;

	public override void OnEnter()
	{
		PlayerEx.AddSeaTanSuoDu(SeaID.Value, TanSuoDu.Value);
		Continue();
	}
}
