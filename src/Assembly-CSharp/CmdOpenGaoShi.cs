using System;
using Fungus;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
[CommandInfo("YSPlayer", "打开告示", "打开告示", 0)]
[AddComponentMenu("")]
public class CmdOpenGaoShi : Command
{
	// Token: 0x06001B91 RID: 7057 RVA: 0x0001728C File Offset: 0x0001548C
	public override void OnEnter()
	{
		UIGaoShi.Inst.Show();
		this.Continue();
	}
}
