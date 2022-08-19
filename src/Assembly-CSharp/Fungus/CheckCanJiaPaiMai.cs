using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F17 RID: 3863
	[CommandInfo("YS", "CheckCanJiaPaiMai", "检测是否参加过拍卖", 0)]
	[AddComponentMenu("")]
	public class CheckCanJiaPaiMai : Command
	{
		// Token: 0x06006DA1 RID: 28065 RVA: 0x002A3AEB File Offset: 0x002A1CEB
		public override void OnEnter()
		{
			this.TempValue.Value = Tools.instance.getPlayer().StreamData.PaiMaiDataMag.IsJoin(this.PaiMaiID);
			this.Continue();
		}

		// Token: 0x06006DA2 RID: 28066 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006DA3 RID: 28067 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B43 RID: 23363
		[Tooltip("拍卖行的ID")]
		[SerializeField]
		protected int PaiMaiID;

		// Token: 0x04005B44 RID: 23364
		[Tooltip("获取到的值存放位置")]
		[VariableProperty(new Type[]
		{
			typeof(BooleanVariable)
		})]
		[SerializeField]
		protected BooleanVariable TempValue;
	}
}
