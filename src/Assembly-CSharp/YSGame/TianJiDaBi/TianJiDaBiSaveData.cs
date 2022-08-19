using System;
using System.Collections.Generic;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A93 RID: 2707
	[Serializable]
	public class TianJiDaBiSaveData
	{
		// Token: 0x04004AE1 RID: 19169
		public int LastMatchIndex;

		// Token: 0x04004AE2 RID: 19170
		public int LastMatchYear;

		// Token: 0x04004AE3 RID: 19171
		public Match LastMatch;

		// Token: 0x04004AE4 RID: 19172
		public Match NowMatch;

		// Token: 0x04004AE5 RID: 19173
		public List<Match> HistotyMatchList = new List<Match>();
	}
}
