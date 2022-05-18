using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001444 RID: 5188
	[CommandInfo("YSTools", "OpenDuiHuan", "打开兑换界面", 0)]
	[AddComponentMenu("")]
	public class OpenDuiHuan : Command
	{
		// Token: 0x06007D5F RID: 32095 RVA: 0x00054CD4 File Offset: 0x00052ED4
		public override void OnEnter()
		{
			UIDuiHuanShop.Inst.Show(this.DuiHuanType);
			UIDuiHuanShop.Inst.RefreshUI();
			this.Continue();
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006ADE RID: 27358
		[Tooltip("兑换界面ID")]
		[SerializeField]
		protected int DuiHuanType = 1;
	}
}
