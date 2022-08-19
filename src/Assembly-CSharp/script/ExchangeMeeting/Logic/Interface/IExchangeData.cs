using System;
using System.Collections.Generic;
using Bag;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A41 RID: 2625
	[Serializable]
	public abstract class IExchangeData
	{
		// Token: 0x040048C4 RID: 18628
		[NonSerialized]
		protected static readonly Random random = new Random();

		// Token: 0x040048C5 RID: 18629
		public List<BaseItem> NeedItems = new List<BaseItem>();

		// Token: 0x040048C6 RID: 18630
		public List<BaseItem> GiveItems = new List<BaseItem>();

		// Token: 0x040048C7 RID: 18631
		public int Id;
	}
}
