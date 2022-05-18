using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DBB RID: 3515
	[CommandInfo("天机大比", "观赛模拟", "观赛模拟", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiGuanZhanSim : Command
	{
		// Token: 0x060054C8 RID: 21704 RVA: 0x0003CA5E File Offset: 0x0003AC5E
		public override void OnEnter()
		{
			UITianJiDaBiSaiChang.ShowAllRoundSim(this);
		}
	}
}
