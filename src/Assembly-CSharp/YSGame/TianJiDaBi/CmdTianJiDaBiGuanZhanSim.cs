using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A89 RID: 2697
	[CommandInfo("天机大比", "观赛模拟", "观赛模拟", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiGuanZhanSim : Command
	{
		// Token: 0x06004BAC RID: 19372 RVA: 0x00203879 File Offset: 0x00201A79
		public override void OnEnter()
		{
			UITianJiDaBiSaiChang.ShowAllRoundSim(this);
		}
	}
}
