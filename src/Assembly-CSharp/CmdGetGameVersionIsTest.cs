using System;
using Fungus;
using UnityEngine;

// Token: 0x020002B1 RID: 689
[CommandInfo("YSTool", "检查游戏是否为测试版", "检查游戏是否为测试版", 0)]
[AddComponentMenu("")]
public class CmdGetGameVersionIsTest : Command
{
	// Token: 0x060014F9 RID: 5369 RVA: 0x000133A3 File Offset: 0x000115A3
	public override void OnEnter()
	{
		this.IsTest.Value = clientApp.IsTestVersion;
		this.Continue();
	}

	// Token: 0x0400101E RID: 4126
	[SerializeField]
	[Tooltip("是否为测试版")]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable)
	})]
	protected BooleanVariable IsTest;
}
