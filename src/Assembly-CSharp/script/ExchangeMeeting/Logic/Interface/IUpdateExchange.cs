using System;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A46 RID: 2630
	public abstract class IUpdateExchange
	{
		// Token: 0x0600482B RID: 18475
		public abstract void UpdateProcess(int times);

		// Token: 0x0600482C RID: 18476
		protected abstract void SuccessExchange(IExchangeData data);
	}
}
