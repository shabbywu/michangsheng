using System;
using Fungus;
using UnityEngine;

// Token: 0x020002F6 RID: 758
[CommandInfo("YSPlayer", "开始天劫倒计时", "开始天劫倒计时", 0)]
[AddComponentMenu("")]
public class CmdStartTianJieCD : Command
{
	// Token: 0x060016DD RID: 5853 RVA: 0x000143DC File Offset: 0x000125DC
	public override void OnEnter()
	{
		TianJieManager.StartTianJieCD();
		this.Continue();
	}
}
