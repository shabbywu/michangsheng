using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013FC RID: 5116
	[CommandInfo("YSNew/Get", "GetMaxPriceAvatarIDByPaiMaiHuiID", "获取拍卖行出价最高的武将ID", 0)]
	[AddComponentMenu("")]
	public class GetMaxPriceAvatarIDByPaiMaiHuiID : Command
	{
		// Token: 0x06007C44 RID: 31812 RVA: 0x002C4A54 File Offset: 0x002C2C54
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

		// Token: 0x06007C45 RID: 31813 RVA: 0x000113CF File Offset: 0x0000F5CF
		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, byte.MaxValue);
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04006A6A RID: 27242
		[Tooltip("拍卖行ID")]
		[SerializeField]
		protected int PaiMaiHuiID;

		// Token: 0x04006A6B RID: 27243
		[Tooltip("存放值的变量")]
		[VariableProperty(new Type[]
		{
			typeof(IntegerVariable)
		})]
		[SerializeField]
		protected IntegerVariable AvatarID;
	}
}
