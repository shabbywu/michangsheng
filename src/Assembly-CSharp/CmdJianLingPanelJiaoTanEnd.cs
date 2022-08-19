using System;
using Fungus;
using UnityEngine;

// Token: 0x020002D2 RID: 722
[CommandInfo("剑灵", "剑灵主界面交谈结束", "剑灵主界面交谈结束", 0)]
[AddComponentMenu("")]
public class CmdJianLingPanelJiaoTanEnd : Command
{
	// Token: 0x06001936 RID: 6454 RVA: 0x000B4F0A File Offset: 0x000B310A
	public override void OnEnter()
	{
		if (UIJianLingPanel.Inst != null)
		{
			UIJianLingPanel.Inst.OnJiaoTanEnd();
			this.Continue();
		}
	}
}
