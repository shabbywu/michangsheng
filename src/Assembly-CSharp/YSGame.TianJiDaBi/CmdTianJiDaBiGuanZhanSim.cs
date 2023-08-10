using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "观赛模拟", "观赛模拟", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiGuanZhanSim : Command
{
	public override void OnEnter()
	{
		UITianJiDaBiSaiChang.ShowAllRoundSim(this);
	}
}
