using System;
using Fungus;
using UnityEngine;

// Token: 0x02000245 RID: 581
[CommandInfo("YSNPCJiaoHu", "设置交互UI的隐藏", "设置交互UI的隐藏，为true时隐藏，为false时显示", 0)]
[AddComponentMenu("")]
public class CmdHideAllJiaoHuUI : Command
{
	// Token: 0x06001636 RID: 5686 RVA: 0x000966BA File Offset: 0x000948BA
	public override void OnEnter()
	{
		UINPCJiaoHu.AllShouldHide = this.isHide;
		this.Continue();
	}

	// Token: 0x04001085 RID: 4229
	[SerializeField]
	[Tooltip("为true时隐藏，为false时显示")]
	protected bool isHide;
}
