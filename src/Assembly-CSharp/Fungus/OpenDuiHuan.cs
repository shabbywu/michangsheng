using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F90 RID: 3984
	[CommandInfo("YSTools", "OpenDuiHuan", "打开兑换界面", 0)]
	[AddComponentMenu("")]
	public class OpenDuiHuan : Command
	{
		// Token: 0x06006F75 RID: 28533 RVA: 0x002A6FBD File Offset: 0x002A51BD
		public override void OnEnter()
		{
			UIDuiHuanShop.Inst.Show(this.DuiHuanType);
			UIDuiHuanShop.Inst.RefreshUI();
			this.Continue();
		}

		// Token: 0x06006F76 RID: 28534 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C0F RID: 23567
		[Tooltip("兑换界面ID")]
		[SerializeField]
		protected int DuiHuanType = 1;
	}
}
