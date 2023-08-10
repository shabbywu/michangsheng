using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取玩家在副本的位置", "获取玩家在副本的位置(必须在副本内才能使用)，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerFuBenNowMapIndex : Command
{
	public override void OnEnter()
	{
		GetFlowchart().SetIntegerVariable("TmpValue", PlayerEx.Player.fubenContorl[Tools.getScreenName()].NowIndex);
		Continue();
	}
}
