using System;
using script.ExchangeMeeting.UI.Factory;
using script.ExchangeMeeting.UI.UI;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A27 RID: 2599
	public abstract class IExchangeCtr
	{
		// Token: 0x060047AC RID: 18348
		public abstract void UpdateEventList();

		// Token: 0x04004899 RID: 18585
		public IExchangeUI UI;

		// Token: 0x0400489A RID: 18586
		public IExchangeFactory Factory;

		// Token: 0x0400489B RID: 18587
		public SysExchangeDataUI ClickDataUI;
	}
}
