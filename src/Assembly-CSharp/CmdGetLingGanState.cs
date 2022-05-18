using System;
using Fungus;
using UnityEngine;

// Token: 0x02000351 RID: 849
[CommandInfo("YSPlayer", "获取玩家的灵感状态", "获取玩家的灵感状态，赋值到TmpValue，1天人感应 2灵光闪现 3无心波澜 4灵思枯竭", 0)]
[AddComponentMenu("")]
public class CmdGetLingGanState : Command
{
	// Token: 0x060018C6 RID: 6342 RVA: 0x0001558E File Offset: 0x0001378E
	public override void OnEnter()
	{
		this.GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.GetLunDaoState());
		this.Continue();
	}
}
