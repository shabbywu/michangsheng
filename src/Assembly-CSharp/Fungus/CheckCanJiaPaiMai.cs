using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013CB RID: 5067
	[CommandInfo("YS", "CheckCanJiaPaiMai", "检测是否参加过拍卖", 0)]
	[AddComponentMenu("")]
	public class CheckCanJiaPaiMai : Command
	{
		// Token: 0x06007B8C RID: 31628 RVA: 0x000542F3 File Offset: 0x000524F3
		public override void OnEnter()
		{
			this.TempValue.Value = Tools.instance.getPlayer().StreamData.PaiMaiDataMag.IsJoin(this.PaiMaiID);
			this.Continue();
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A0C RID: 27148
		[Tooltip("拍卖行的ID")]
		[SerializeField]
		protected int PaiMaiID;

		// Token: 0x04006A0D RID: 27149
		[Tooltip("拍卖行的届数")]
		[SerializeField]
		protected int PaiMaiJieShu;

		// Token: 0x04006A0E RID: 27150
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
