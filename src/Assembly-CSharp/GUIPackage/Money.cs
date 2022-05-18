using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D8F RID: 3471
	public class Money : MonoBehaviour
	{
		// Token: 0x060053C8 RID: 21448 RVA: 0x0003BE7B File Offset: 0x0003A07B
		private void Update()
		{
			if (this.MoneyType == Money.MoneyUIType.PlayerMoney)
			{
				this.Label.GetComponent<UILabel>().text = Tools.instance.getPlayer().money.ToString("#,##0");
			}
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0022E900 File Offset: 0x0022CB00
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

		// Token: 0x04005375 RID: 21365
		public int money;

		// Token: 0x04005376 RID: 21366
		public GameObject Label;

		// Token: 0x04005377 RID: 21367
		public Money.MoneyUIType MoneyType;

		// Token: 0x02000D90 RID: 3472
		public enum MoneyUIType
		{
			// Token: 0x04005379 RID: 21369
			EXPlayerMoney,
			// Token: 0x0400537A RID: 21370
			EXMonstarMoney,
			// Token: 0x0400537B RID: 21371
			EXPlayerPayMoney,
			// Token: 0x0400537C RID: 21372
			EXMonstarPayMoney,
			// Token: 0x0400537D RID: 21373
			PlayerMoney
		}
	}
}
