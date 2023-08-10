using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSTool", "检查游戏是否为测试版", "检查游戏是否为测试版", 0)]
[AddComponentMenu("")]
public class CmdGetGameVersionIsTest : Command
{
	[SerializeField]
	[Tooltip("是否为测试版")]
	[VariableProperty(new Type[] { typeof(BooleanVariable) })]
	protected BooleanVariable IsTest;

	public override void OnEnter()
	{
		IsTest.Value = clientApp.IsTestVersion;
		Continue();
	}
}
