using System;
using System.Collections.Generic;

namespace script.ExchangeMeeting.Logic.Interface
{
	// Token: 0x02000A45 RID: 2629
	[Serializable]
	public abstract class IExchangeSource
	{
		// Token: 0x06004829 RID: 18473 RVA: 0x001E76A6 File Offset: 0x001E58A6
		public void Init()
		{
			if (this.playerList == null)
			{
				this.playerList = new List<IExchangeData>();
			}
			if (this.sysList == null)
			{
				this.sysList = new List<IExchangeData>();
			}
			if (this.guDingList == null)
			{
				this.guDingList = new List<int>();
			}
		}

		// Token: 0x040048D1 RID: 18641
		public List<IExchangeData> playerList;

		// Token: 0x040048D2 RID: 18642
		public List<IExchangeData> sysList;

		// Token: 0x040048D3 RID: 18643
		public List<int> guDingList;

		// Token: 0x040048D4 RID: 18644
		public DateTime NextUpdate = DateTime.Parse("0001-1-1");
	}
}
