using System;
using System.Collections.Generic;

namespace PaiMai
{
	// Token: 0x02000A5E RID: 2654
	[Serializable]
	public class PaiMaiData
	{
		// Token: 0x04003C78 RID: 15480
		public int Id;

		// Token: 0x04003C79 RID: 15481
		public List<int> ShopList = new List<int>();

		// Token: 0x04003C7A RID: 15482
		public bool IsJoined;

		// Token: 0x04003C7B RID: 15483
		public DateTime NextUpdateTime;

		// Token: 0x04003C7C RID: 15484
		public int No;
	}
}
