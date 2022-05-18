using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DBD RID: 3517
	[CommandInfo("天机大比", "打开天机榜", "打开天机榜(关闭UI时才会Continue)", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiOpenRank : Command
	{
		// Token: 0x060054CC RID: 21708 RVA: 0x0003CA66 File Offset: 0x0003AC66
		public override void OnEnter()
		{
			UITianJiDaBiRankPanel.Show(this);
		}
	}
}
