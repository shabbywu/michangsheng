using System;
using Fungus;
using UnityEngine;

// Token: 0x0200023E RID: 574
[CommandInfo("YSPlayer", "获取玩家在副本的位置", "获取玩家在副本的位置(必须在副本内才能使用)，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerFuBenNowMapIndex : Command
{
	// Token: 0x06001624 RID: 5668 RVA: 0x00095F0E File Offset: 0x0009410E
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex);
		this.Continue();
	}
}
