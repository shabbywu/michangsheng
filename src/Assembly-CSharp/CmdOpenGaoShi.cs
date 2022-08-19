using System;
using Fungus;
using UnityEngine;

// Token: 0x020002BC RID: 700
[CommandInfo("YSPlayer", "打开告示", "打开告示", 0)]
[AddComponentMenu("")]
public class CmdOpenGaoShi : Command
{
	// Token: 0x0600189D RID: 6301 RVA: 0x000B08F6 File Offset: 0x000AEAF6
	public override void OnEnter()
	{
		UIGaoShi.Inst.Show();
		this.Continue();
	}
}
