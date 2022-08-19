using System;
using Fungus;
using UnityEngine;

// Token: 0x02000222 RID: 546
[CommandInfo("YSDongFu", "开始双修", "开始双修", 0)]
[AddComponentMenu("")]
public class CmdStartShuangXiu : Command
{
	// Token: 0x060015B7 RID: 5559 RVA: 0x0009144D File Offset: 0x0008F64D
	public override void OnEnter()
	{
		UINPCJiaoHu.Inst.ShowNPCShuangXiuSelect();
		this.Continue();
	}
}
