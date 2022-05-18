using System;
using Fungus;
using UnityEngine;

// Token: 0x020002F7 RID: 759
[CommandInfo("YSPlayer", "天劫加速", "天劫加速", 0)]
[AddComponentMenu("")]
public class CmdTianJieJiaSu : Command
{
	// Token: 0x060016DF RID: 5855 RVA: 0x000143E9 File Offset: 0x000125E9
	public override void OnEnter()
	{
		TianJieManager.TianJieJiaSu(this.year);
		this.Continue();
	}

	// Token: 0x0400123D RID: 4669
	[SerializeField]
	[Tooltip("年数")]
	protected int year;
}
