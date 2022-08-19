using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A87 RID: 2695
	[CommandInfo("天机大比", "结算本轮", "结算本(如果玩家参赛，则必须在玩家打完记录结果之后调用)", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiCalcNewRound : Command
	{
		// Token: 0x06004BA8 RID: 19368 RVA: 0x002037F8 File Offset: 0x002019F8
		public override void OnEnter()
		{
			Match nowMatch = TianJiDaBiManager.GetNowMatch();
			if (this.ShowProcess)
			{
				UITianJiDaBiSaiChang.ShowOneRoundSim(this);
				return;
			}
			nowMatch.NewRound();
			nowMatch.AfterRound();
			this.Continue();
		}

		// Token: 0x04004ABC RID: 19132
		[Tooltip("是否显示过程")]
		[SerializeField]
		protected bool ShowProcess;
	}
}
