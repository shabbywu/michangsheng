using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "获取剑灵记忆恢复度", "获取剑灵记忆恢复度，保存到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetJianLingJiYiHuiFuDu : Command
{
	public override void OnEnter()
	{
		int jiYiHuiFuDu = PlayerEx.Player.jianLingManager.GetJiYiHuiFuDu();
		GetFlowchart().SetIntegerVariable("TmpValue", jiYiHuiFuDu);
		Continue();
	}
}
