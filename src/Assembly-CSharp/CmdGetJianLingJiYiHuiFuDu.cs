using System;
using Fungus;
using UnityEngine;

// Token: 0x020002CF RID: 719
[CommandInfo("剑灵", "获取剑灵记忆恢复度", "获取剑灵记忆恢复度，保存到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetJianLingJiYiHuiFuDu : Command
{
	// Token: 0x06001930 RID: 6448 RVA: 0x000B4E60 File Offset: 0x000B3060
	public override void OnEnter()
	{
		int jiYiHuiFuDu = PlayerEx.Player.jianLingManager.GetJiYiHuiFuDu();
		this.GetFlowchart().SetIntegerVariable("TmpValue", jiYiHuiFuDu);
		this.Continue();
	}
}
