using System;
using Fungus;
using UnityEngine;

// Token: 0x020001B3 RID: 435
[CommandInfo("YSTool", "检查游戏是否为测试版", "检查游戏是否为测试版", 0)]
[AddComponentMenu("")]
public class CmdGetGameVersionIsTest : Command
{
	// Token: 0x0600124E RID: 4686 RVA: 0x0006F361 File Offset: 0x0006D561
	public override void OnEnter()
	{
		this.IsTest.Value = clientApp.IsTestVersion;
		this.Continue();
	}

	// Token: 0x04000CF6 RID: 3318
	[SerializeField]
	[Tooltip("是否为测试版")]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable)
	})]
	protected BooleanVariable IsTest;
}
