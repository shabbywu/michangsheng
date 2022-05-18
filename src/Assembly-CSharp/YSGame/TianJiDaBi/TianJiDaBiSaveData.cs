using System;
using System.Collections.Generic;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DC5 RID: 3525
	[Serializable]
	public class TianJiDaBiSaveData
	{
		// Token: 0x0400549E RID: 21662
		public int LastMatchIndex;

		// Token: 0x0400549F RID: 21663
		public int LastMatchYear;

		// Token: 0x040054A0 RID: 21664
		public Match LastMatch;

		// Token: 0x040054A1 RID: 21665
		public Match NowMatch;

		// Token: 0x040054A2 RID: 21666
		public List<Match> HistotyMatchList = new List<Match>();
	}
}
