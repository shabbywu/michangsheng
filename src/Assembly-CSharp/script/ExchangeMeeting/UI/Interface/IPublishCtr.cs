using System;
using Bag;
using script.ExchangeMeeting.UI.Factory;

namespace script.ExchangeMeeting.UI.Interface
{
	// Token: 0x02000A28 RID: 2600
	public abstract class IPublishCtr
	{
		// Token: 0x060047AE RID: 18350
		public abstract void Publish();

		// Token: 0x060047AF RID: 18351
		public abstract void CreatePlayerList();

		// Token: 0x060047B0 RID: 18352
		public abstract void UpdatePlayerList();

		// Token: 0x060047B1 RID: 18353
		public abstract void PutNeedItem(BaseItem baseItem);

		// Token: 0x060047B2 RID: 18354
		public abstract void BackNeedItem();

		// Token: 0x060047B3 RID: 18355
		public abstract void PutGiveItem(BaseSlot slot);

		// Token: 0x060047B4 RID: 18356
		public abstract void BackGiveItem(BaseSlot slot);

		// Token: 0x060047B5 RID: 18357
		public abstract bool CheckCanClickPublish();

		// Token: 0x0400489C RID: 18588
		public IPublishUI UI;

		// Token: 0x0400489D RID: 18589
		public IExchangeFactory Factory;
	}
}
