using System;
using Fungus;
using UnityEngine;

// Token: 0x0200041A RID: 1050
[CommandInfo("剑灵", "增加剑灵记忆恢复度", "增加剑灵记忆恢复度", 0)]
[AddComponentMenu("")]
public class CmdAddJianLingJiYiHuiFuDu : Command
{
	// Token: 0x06001C36 RID: 7222 RVA: 0x00017992 File Offset: 0x00015B92
	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.AddExJiYiHuiFuDu(this.HuiFuDu);
		this.Continue();
	}

	// Token: 0x0400183D RID: 6205
	[Tooltip("恢复度")]
	protected int HuiFuDu;
}
