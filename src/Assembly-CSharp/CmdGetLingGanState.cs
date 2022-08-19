using System;
using Fungus;
using UnityEngine;

// Token: 0x02000235 RID: 565
[CommandInfo("YSPlayer", "获取玩家的灵感状态", "获取玩家的灵感状态，赋值到TmpValue，1天人感应 2灵光闪现 3无心波澜 4灵思枯竭", 0)]
[AddComponentMenu("")]
public class CmdGetLingGanState : Command
{
	// Token: 0x0600160E RID: 5646 RVA: 0x00095656 File Offset: 0x00093856
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.GetLunDaoState());
		this.Continue();
	}
}
