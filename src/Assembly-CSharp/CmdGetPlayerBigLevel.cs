using System;
using Fungus;
using UnityEngine;

// Token: 0x0200023C RID: 572
[CommandInfo("YSPlayer", "获取玩家的大境界", "获取玩家的大境界，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerBigLevel : Command
{
	// Token: 0x06001620 RID: 5664 RVA: 0x00095EB0 File Offset: 0x000940B0
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.getLevelType());
		this.Continue();
	}
}
