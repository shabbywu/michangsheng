using System;
using System.Collections.Generic;
using Bag;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A42 RID: 2626
	public abstract class IExchangeDataFactory
	{
		// Token: 0x06004816 RID: 18454
		public abstract IExchangeData Create(int id, int type);

		// Token: 0x06004817 RID: 18455
		public abstract IExchangeData Create(List<BaseItem> needItems, List<BaseItem> giveItems);

		// Token: 0x06004818 RID: 18456
		protected abstract int GetConditionCount();

		// Token: 0x06004819 RID: 18457
		protected abstract Dictionary<int, Dictionary<int, int>> GetConditionDict(List<int> tags1, List<int> nums1, List<int> items1, List<int> nums2);

		// Token: 0x040048C8 RID: 18632
		protected readonly Random random = new Random();
	}
}
