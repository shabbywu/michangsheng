using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A64 RID: 2660
	public class Money : MonoBehaviour
	{
		// Token: 0x06004ABE RID: 19134 RVA: 0x001FC710 File Offset: 0x001FA910
		private void Update()
		{
			if (this.MoneyType == Money.MoneyUIType.PlayerMoney)
			{
				this.Label.GetComponent<UILabel>().text = Tools.instance.getPlayer().money.ToString("#,##0");
			}
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x001FC744 File Offset: 0x001FA944
		public void Set_money(int num, bool isShowFuHao = false)
		{
			this.money = num;
			if (isShowFuHao && num >= 0)
			{
				this.Label.GetComponent<UILabel>().text = "+" + this.money.ToString("#,##0");
				return;
			}
			this.Label.GetComponent<UILabel>().text = this.money.ToString("#,##0");
		}

		// Token: 0x040049DE RID: 18910
		public int money;

		// Token: 0x040049DF RID: 18911
		public GameObject Label;

		// Token: 0x040049E0 RID: 18912
		public Money.MoneyUIType MoneyType;

		// Token: 0x020015A1 RID: 5537
		public enum MoneyUIType
		{
			// Token: 0x04006FE6 RID: 28646
			EXPlayerMoney,
			// Token: 0x04006FE7 RID: 28647
			EXMonstarMoney,
			// Token: 0x04006FE8 RID: 28648
			EXPlayerPayMoney,
			// Token: 0x04006FE9 RID: 28649
			EXMonstarPayMoney,
			// Token: 0x04006FEA RID: 28650
			PlayerMoney
		}
	}
}
