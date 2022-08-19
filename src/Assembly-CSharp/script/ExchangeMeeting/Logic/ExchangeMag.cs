using System;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.Logic
{
	// Token: 0x02000A3C RID: 2620
	public class ExchangeMag : IExchangeMag
	{
		// Token: 0x060047FD RID: 18429 RVA: 0x001E68FC File Offset: 0x001E4AFC
		public ExchangeMag()
		{
			this.IUpdateExchange = new UpdateExchange();
			this.ExchangeIO = new ExchangeIO();
			this.ExchangeDataFactory = new ExchangeDataFactory();
		}
	}
}
