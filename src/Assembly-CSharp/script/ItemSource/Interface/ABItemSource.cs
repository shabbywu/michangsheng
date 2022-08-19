using System;
using System.Collections.Generic;

namespace script.ItemSource.Interface
{
	// Token: 0x02000A1B RID: 2587
	[Serializable]
	public abstract class ABItemSource
	{
		// Token: 0x0600477C RID: 18300 RVA: 0x001E3E80 File Offset: 0x001E2080
		public static ABItemSource Get()
		{
			return PlayerEx.Player.StreamData.ABItemSource;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x001E3E91 File Offset: 0x001E2091
		public virtual void Init()
		{
			if (this.ItemSourceDataDic == null)
			{
				this.ItemSourceDataDic = new Dictionary<int, ABItemSourceData>();
			}
		}

		// Token: 0x04004884 RID: 18564
		public Dictionary<int, ABItemSourceData> ItemSourceDataDic;
	}
}
