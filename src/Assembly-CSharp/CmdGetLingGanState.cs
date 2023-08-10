using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取玩家的灵感状态", "获取玩家的灵感状态，赋值到TmpValue，1天人感应 2灵光闪现 3无心波澜 4灵思枯竭", 0)]
[AddComponentMenu("")]
public class CmdGetLingGanState : Command
{
	public override void OnEnter()
	{
		GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.GetLunDaoState());
		Continue();
	}
}
