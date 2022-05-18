using System;
using Fungus;
using UnityEngine;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DBE RID: 3518
	[CommandInfo("天机大比", "打开对战UI", "打开对战UI", 0)]
	[AddComponentMenu("")]
	public class CmdTianJiDaBiOpenUI : Command
	{
		// Token: 0x060054CE RID: 21710 RVA: 0x0003CA6E File Offset: 0x0003AC6E
		public override void OnEnter()
		{
			UITianJiDaBiSaiChang.ShowNormal();
			this.Continue();
		}
	}
}
