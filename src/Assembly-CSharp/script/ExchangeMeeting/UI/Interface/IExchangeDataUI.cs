using System;
using script.ExchangeMeeting.Logic.Interface;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A2A RID: 2602
	public abstract class IExchangeDataUI : UIBase
	{
		// Token: 0x060047BF RID: 18367
		protected abstract void Init();

		// Token: 0x040048A5 RID: 18597
		protected IExchangeData _data;
	}
}
