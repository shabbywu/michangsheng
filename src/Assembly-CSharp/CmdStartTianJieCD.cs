using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "开始天劫倒计时", "开始天劫倒计时", 0)]
[AddComponentMenu("")]
public class CmdStartTianJieCD : Command
{
	public override void OnEnter()
	{
		TianJieManager.StartTianJieCD();
		Continue();
	}
}
