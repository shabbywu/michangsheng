using Fungus;
using UnityEngine;

[CommandInfo("剑灵", "增加剑灵记忆恢复度", "增加剑灵记忆恢复度", 0)]
[AddComponentMenu("")]
public class CmdAddJianLingJiYiHuiFuDu : Command
{
	[Tooltip("恢复度")]
	protected int HuiFuDu;

	public override void OnEnter()
	{
		PlayerEx.Player.jianLingManager.AddExJiYiHuiFuDu(HuiFuDu);
		Continue();
	}
}
