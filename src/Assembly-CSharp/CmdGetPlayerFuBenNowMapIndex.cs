using System;
using Fungus;
using UnityEngine;

// Token: 0x0200035A RID: 858
[CommandInfo("YSPlayer", "获取玩家在副本的位置", "获取玩家在副本的位置(必须在副本内才能使用)，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerFuBenNowMapIndex : Command
{
	// Token: 0x060018DC RID: 6364 RVA: 0x000155FE File Offset: 0x000137FE
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex);
		this.Continue();
	}
}
