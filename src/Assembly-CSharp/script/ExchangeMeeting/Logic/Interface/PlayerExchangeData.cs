using System;
using System.Collections.Generic;
using Bag;
using JSONClass;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A40 RID: 2624
	[Serializable]
	public class PlayerExchangeData : IExchangeData
	{
		// Token: 0x06004812 RID: 18450 RVA: 0x001E73A8 File Offset: 0x001E55A8
		public PlayerExchangeData(List<BaseItem> needItems, List<BaseItem> giveItems)
		{
			if (needItems == null || giveItems == null)
			{
				throw new Exception("needItems or giveItems is null");
			}
			if (needItems.Count == 0 || giveItems.Count == 0)
			{
				throw new Exception("needItems or giveItems is empty");
			}
			foreach (BaseItem item in needItems)
			{
				this.NeedItems.Add(item);
			}
			foreach (BaseItem item2 in giveItems)
			{
				this.GiveItems.Add(item2);
			}
			this.Calculate();
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x001E7484 File Offset: 0x001E5684
		private void Calculate()
		{
			int num = 0;
			int num2 = 0;
			foreach (BaseItem baseItem in this.NeedItems)
			{
				if (_ItemJsonData.DataDict[baseItem.Id].ItemFlag.Contains(53))
				{
					this.NeedUpdate = false;
				}
				num += baseItem.BasePrice * baseItem.Count;
			}
			foreach (BaseItem baseItem2 in this.GiveItems)
			{
				int num3 = baseItem2.BasePrice * baseItem2.Count;
				if (_ItemJsonData.DataDict[baseItem2.Id].ItemFlag.Contains(52))
				{
					num3 = num3 * 130 / 100;
				}
				if (_ItemJsonData.DataDict[baseItem2.Id].ItemFlag.Contains(53))
				{
					num3 = num3 * 130 / 100;
				}
				num2 += num3;
			}
			int num4 = num * 2 - num2;
			int num5 = IExchangeData.random.Next(900, 1101);
			this.NeedTime = num4 / num5;
			if (num4 % num5 != 0)
			{
				this.NeedTime++;
			}
			if (this.NeedTime <= 0)
			{
				this.NeedTime = 1;
			}
			this.CostMoney = num * 2 / 100;
		}

		// Token: 0x040048C0 RID: 18624
		public int NeedTime = 1;

		// Token: 0x040048C1 RID: 18625
		public int CostMoney;

		// Token: 0x040048C2 RID: 18626
		public int HasCostTime;

		// Token: 0x040048C3 RID: 18627
		public bool NeedUpdate = true;
	}
}
