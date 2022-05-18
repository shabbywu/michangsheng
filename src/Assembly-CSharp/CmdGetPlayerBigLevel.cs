using System;
using Fungus;
using UnityEngine;

// Token: 0x02000358 RID: 856
[CommandInfo("YSPlayer", "获取玩家的大境界", "获取玩家的大境界，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerBigLevel : Command
{
	// Token: 0x060018D8 RID: 6360 RVA: 0x000155DC File Offset: 0x000137DC
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.getLevelType());
		this.Continue();
	}
}
