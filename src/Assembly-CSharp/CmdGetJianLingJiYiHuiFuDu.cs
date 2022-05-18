using System;
using Fungus;
using UnityEngine;

// Token: 0x0200041B RID: 1051
[CommandInfo("剑灵", "获取剑灵记忆恢复度", "获取剑灵记忆恢复度，保存到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetJianLingJiYiHuiFuDu : Command
{
	// Token: 0x06001C38 RID: 7224 RVA: 0x000FAE2C File Offset: 0x000F902C
	public override void OnEnter()
	{
		int jiYiHuiFuDu = PlayerEx.Player.jianLingManager.GetJiYiHuiFuDu();
		this.GetFlowchart().SetIntegerVariable("TmpValue", jiYiHuiFuDu);
		this.Continue();
	}
}
