using System;
using Fungus;
using UnityEngine;

// Token: 0x020002CE RID: 718
[CommandInfo("剑灵", "增加剑灵记忆恢复度", "增加剑灵记忆恢复度", 0)]
[AddComponentMenu("")]
public class CmdAddJianLingJiYiHuiFuDu : Command
{
	// Token: 0x0600192E RID: 6446 RVA: 0x000B4E41 File Offset: 0x000B3041
	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.AddExJiYiHuiFuDu(this.HuiFuDu);
		this.Continue();
	}

	// Token: 0x0400146F RID: 5231
	[Tooltip("恢复度")]
	protected int HuiFuDu;
}
