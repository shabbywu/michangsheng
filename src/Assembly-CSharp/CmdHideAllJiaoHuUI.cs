using System;
using Fungus;
using UnityEngine;

// Token: 0x02000361 RID: 865
[CommandInfo("YSNPCJiaoHu", "设置交互UI的隐藏", "设置交互UI的隐藏，为true时隐藏，为false时显示", 0)]
[AddComponentMenu("")]
public class CmdHideAllJiaoHuUI : Command
{
	// Token: 0x060018EE RID: 6382 RVA: 0x0001568D File Offset: 0x0001388D
	public override void OnEnter()
	{
		UINPCJiaoHu.AllShouldHide = this.isHide;
		this.Continue();
	}

	// Token: 0x040013DD RID: 5085
	[SerializeField]
	[Tooltip("为true时隐藏，为false时显示")]
	protected bool isHide;
}
