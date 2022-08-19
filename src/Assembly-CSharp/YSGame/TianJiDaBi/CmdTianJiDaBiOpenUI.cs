using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A8C RID: 2700
	[CommandInfo("天机大比", "打开对战UI", "打开对战UI", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiOpenUI : Command
	{
		// Token: 0x06004BB2 RID: 19378 RVA: 0x00203950 File Offset: 0x00201B50
		public override void OnEnter()
		{
			UITianJiDaBiSaiChang.ShowNormal();
			this.Continue();
		}
	}
}
