using System;
using Fungus;
using UnityEngine;

// Token: 0x020001E2 RID: 482
[CommandInfo("YSPlayer", "开始突破化神", "开始突破化神", 0)]
[AddComponentMenu("")]
public class CmdStartHuaShen : Command
{
	// Token: 0x06001437 RID: 5175 RVA: 0x000829C2 File Offset: 0x00080BC2
	public override void OnEnter()
	{
		UIHuaShenRuDaoSelect.Inst.Show();
		this.Continue();
	}
}
