using System;
using System.Collections.Generic;
using Bag;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A43 RID: 2627
	public abstract class IExchangeIO
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600481B RID: 18459 RVA: 0x001E7649 File Offset: 0x001E5849
		protected IExchangeSource source
		{
			get
			{
				return PlayerEx.Player.StreamData.ExchangeSource;
			}
		}

		// Token: 0x0600481C RID: 18460
		public abstract void Add(IExchangeData data);

		// Token: 0x0600481D RID: 18461
		public abstract void Remove(IExchangeData data);

		// Token: 0x0600481E RID: 18462
		public abstract bool NeedUpdateNpcExchange();

		// Token: 0x0600481F RID: 18463
		public abstract List<int> GetGuDingList();

		// Token: 0x06004820 RID: 18464
		public abstract void CreateNpcExchange();

		// Token: 0x06004821 RID: 18465
		protected abstract List<int> GetRandomExchangeList(int count);

		// Token: 0x06004822 RID: 18466
		public abstract void CreatePlayerExchange(List<BaseItem> needs, List<BaseItem> gets);

		// Token: 0x06004823 RID: 18467
		public abstract List<IExchangeData> GetPlayerList();

		// Token: 0x06004824 RID: 18468
		public abstract List<IExchangeData> GetSystemList();

		// Token: 0x06004825 RID: 18469
		public abstract void SaveGuDingId(int id);

		// Token: 0x040048C9 RID: 18633
		protected int minRandomNum = 2;

		// Token: 0x040048CA RID: 18634
		protected int minTotalNum = 5;

		// Token: 0x040048CB RID: 18635
		protected readonly Random random = new Random();
	}
}
