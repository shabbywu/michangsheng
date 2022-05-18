using System;
using Fungus;
using UnityEngine;

// Token: 0x0200041E RID: 1054
[CommandInfo("剑灵", "剑灵主界面交谈结束", "剑灵主界面交谈结束", 0)]
[AddComponentMenu("")]
public class CmdJianLingPanelJiaoTanEnd : Command
{
	// Token: 0x06001C3E RID: 7230 RVA: 0x000179AF File Offset: 0x00015BAF
	public override void OnEnter()
	{
		if (UIJianLingPanel.Inst != null)
		{
			UIJianLingPanel.Inst.OnJiaoTanEnd();
			this.Continue();
		}
	}
}
