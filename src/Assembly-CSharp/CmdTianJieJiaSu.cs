using System;
using Fungus;
using UnityEngine;

// Token: 0x020001E4 RID: 484
[CommandInfo("YSPlayer", "天劫加速", "天劫加速", 0)]
[AddComponentMenu("")]
public class CmdTianJieJiaSu : Command
{
	// Token: 0x0600143B RID: 5179 RVA: 0x000829E1 File Offset: 0x00080BE1
	public override void OnEnter()
	{
		TianJieManager.TianJieJiaSu(this.year);
		this.Continue();
	}

	// Token: 0x04000EFF RID: 3839
	[SerializeField]
	[Tooltip("年数")]
	protected int year;
}
