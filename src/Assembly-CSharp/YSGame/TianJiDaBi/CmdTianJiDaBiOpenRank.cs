using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A8B RID: 2699
	[CommandInfo("天机大比", "打开天机榜", "打开天机榜(关闭UI时才会Continue)", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiOpenRank : Command
	{
		// Token: 0x06004BB0 RID: 19376 RVA: 0x00203948 File Offset: 0x00201B48
		public override void OnEnter()
		{
			UITianJiDaBiRankPanel.Show(this);
		}
	}
}
