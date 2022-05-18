using System;
using Fungus;
using UnityEngine;

// Token: 0x020002F5 RID: 757
[CommandInfo("YSPlayer", "开始突破化神", "开始突破化神", 0)]
[AddComponentMenu("")]
public class CmdStartHuaShen : Command
{
	// Token: 0x060016DB RID: 5851 RVA: 0x000143CA File Offset: 0x000125CA
	public override void OnEnter()
	{
		UIHuaShenRuDaoSelect.Inst.Show();
		this.Continue();
	}
}
