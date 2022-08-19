using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA0 RID: 4000
	[CommandInfo("YSTools", "设置无尽之海事件可点击", "设置无尽之海事件可点击", 0)]
	[AddComponentMenu("")]
	public class SetEndlessEvent : Command
	{
		// Token: 0x06006FAC RID: 28588 RVA: 0x002A7490 File Offset: 0x002A5690
		public override void OnEnter()
		{
			EndlessSeaMag.Inst.NeedRefresh = true;
			this.Continue();
		}
	}
}
