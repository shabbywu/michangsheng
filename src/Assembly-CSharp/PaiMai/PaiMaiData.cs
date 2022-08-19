using System;
using System.Collections.Generic;

namespace PaiMai
{
	// Token: 0x0200070E RID: 1806
	[Serializable]
	public class PaiMaiData
	{
		// Token: 0x040031F1 RID: 12785
		public int Id;

		// Token: 0x040031F2 RID: 12786
		public List<int> ShopList = new List<int>();

		// Token: 0x040031F3 RID: 12787
		public bool IsJoined;

		// Token: 0x040031F4 RID: 12788
		public DateTime NextUpdateTime;

		// Token: 0x040031F5 RID: 12789
		public int No;
	}
}
