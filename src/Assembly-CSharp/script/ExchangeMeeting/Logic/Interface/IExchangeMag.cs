using System;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A44 RID: 2628
	public abstract class IExchangeMag
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x001E767B File Offset: 0x001E587B
		public static IExchangeMag Inst
		{
			get
			{
				if (IExchangeMag._inst == null)
				{
					IExchangeMag._inst = new ExchangeMag();
				}
				return IExchangeMag._inst;
			}
		}

		// Token: 0x040048CC RID: 18636
		private static IExchangeMag _inst;

		// Token: 0x040048CD RID: 18637
		public readonly Random random = new Random();

		// Token: 0x040048CE RID: 18638
		public IExchangeIO ExchangeIO;

		// Token: 0x040048CF RID: 18639
		public IUpdateExchange IUpdateExchange;

		// Token: 0x040048D0 RID: 18640
		public IExchangeDataFactory ExchangeDataFactory;
	}
}
