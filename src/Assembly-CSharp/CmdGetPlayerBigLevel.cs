using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取玩家的大境界", "获取玩家的大境界，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerBigLevel : Command
{
	public override void OnEnter()
	{
		GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.getLevelType());
		Continue();
	}
}
