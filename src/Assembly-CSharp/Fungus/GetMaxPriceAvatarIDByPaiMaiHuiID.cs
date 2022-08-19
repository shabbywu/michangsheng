using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F46 RID: 3910
	[CommandInfo("YSNew/Get", "GetMaxPriceAvatarIDByPaiMaiHuiID", "获取拍卖行出价最高的武将ID", 0)]
	[AddComponentMenu("")]
	public class GetMaxPriceAvatarIDByPaiMaiHuiID : Command
	{
		// Token: 0x06006E59 RID: 28249 RVA: 0x002A4ADC File Offset: 0x002A2CDC
		public override void OnEnter()
		{
			float n = jsonData.instance.AvatarRandomJsonData[this.AvatarID.Value.ToString()]["HaoGanDu"].n;
			int num = Tools.instance.getPlayer().PaiMaiMaxMoneyAvatarDate[this.PaiMaiHuiID.ToString()].I;
			if (jsonData.instance.MonstarIsDeath(num))
			{
				num = -1;
			}
			this.AvatarID.Value = num;
			this.Continue();
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x0005E228 File Offset: 0x0005C428
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06006E5B RID: 28251 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04005B98 RID: 23448
		[Tooltip("拍卖行ID")]
		[SerializeField]
		protected int PaiMaiHuiID;

		// Token: 0x04005B99 RID: 23449
		[Tooltip("存放值的变量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;
	}
}
