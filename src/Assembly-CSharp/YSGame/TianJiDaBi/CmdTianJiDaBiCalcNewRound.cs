using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DB9 RID: 3513
	[CommandInfo("天机大比", "结算本轮", "结算本(如果玩家参赛，则必须在玩家打完记录结果之后调用)", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiCalcNewRound : Command
	{
		// Token: 0x060054C4 RID: 21700 RVA: 0x00234D38 File Offset: 0x00232F38
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

		// Token: 0x0400547A RID: 21626
		[Tooltip("是否显示过程")]
		[SerializeField]
		protected bool ShowProcess;
	}
}
