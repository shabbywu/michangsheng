using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F8E RID: 3982
	[CommandInfo("YSTools", "OpenBiGuan", "打开闭关界面", 0)]
	[AddComponentMenu("")]
	public class OpenBiGuan : Command
	{
		// Token: 0x06006F6E RID: 28526 RVA: 0x002A6F51 File Offset: 0x002A5151
		public override void OnEnter()
		{
			this.NewUI();
			this.Continue();
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x002A6F5F File Offset: 0x002A515F
		public void NewUI()
		{
			UIBiGuanPanel.Inst.OpenBiGuan(this.BiGuanType);
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04005C0D RID: 23565
		[Tooltip("闭关界面的类型")]
		[SerializeField]
		protected int BiGuanType = 1;
	}
}
