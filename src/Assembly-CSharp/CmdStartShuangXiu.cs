using System;
using Fungus;
using UnityEngine;

// Token: 0x0200033E RID: 830
[CommandInfo("YSDongFu", "开始双修", "开始双修", 0)]
[AddComponentMenu("")]
public class CmdStartShuangXiu : Command
{
	// Token: 0x0600186F RID: 6255 RVA: 0x00015370 File Offset: 0x00013570
	public override void OnEnter()
	{
		UINPCJiaoHu.Inst.ShowNPCShuangXiuSelect();
		this.Continue();
	}
}
