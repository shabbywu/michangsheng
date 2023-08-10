using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi;

[CommandInfo("天机大比", "结算本轮", "结算本(如果玩家参赛，则必须在玩家打完记录结果之后调用)", 0)]
[AddComponentMenu("")]
public class CmdTianJiDaBiCalcNewRound : Command
{
	[Tooltip("是否显示过程")]
	[SerializeField]
	protected bool ShowProcess;

	public override void OnEnter()
	{
		Match nowMatch = TianJiDaBiManager.GetNowMatch();
		if (ShowProcess)
		{
			UITianJiDaBiSaiChang.ShowOneRoundSim(this);
			return;
		}
		nowMatch.NewRound();
		nowMatch.AfterRound();
		Continue();
	}
}
