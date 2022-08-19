using System;
using Fungus;
using UnityEngine;

// Token: 0x020001E3 RID: 483
[CommandInfo("YSPlayer", "开始天劫倒计时", "开始天劫倒计时", 0)]
[AddComponentMenu("")]
public class CmdStartTianJieCD : Command
{
	// Token: 0x06001439 RID: 5177 RVA: 0x000829D4 File Offset: 0x00080BD4
	public override void OnEnter()
	{
		TianJieManager.StartTianJieCD();
		this.Continue();
	}
}
