using System;
using Fungus;
using UnityEngine;

// Token: 0x02000257 RID: 599
[CommandInfo("YSNPCJiaoHu", "跳转到NPC交互Talk", "跳转到NPC交互Talk", 0)]
[AddComponentMenu("")]
public class CmdWarpToNPCJiaoHuTalk : Command
{
	// Token: 0x0600165B RID: 5723 RVA: 0x00096E5C File Offset: 0x0009505C
	public override void OnEnter()
	{
		UINPCJiaoHu.Inst.IsNeedWarpToNPCTalk = true;
		this.Continue();
	}
}
