using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001442 RID: 5186
	[CommandInfo("YSTools", "OpenBiGuan", "打开闭关界面", 0)]
	[AddComponentMenu("")]
	public class OpenBiGuan : Command
	{
		// Token: 0x06007D58 RID: 32088 RVA: 0x00054C68 File Offset: 0x00052E68
		public override void OnEnter()
		{
			this.NewUI();
			this.Continue();
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x00054C76 File Offset: 0x00052E76
		public void NewUI()
		{
			UIBiGuanPanel.Inst.OpenBiGuan(this.BiGuanType);
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x04006ADC RID: 27356
		[Tooltip("闭关界面的类型")]
		[SerializeField]
		protected int BiGuanType = 1;
	}
}
