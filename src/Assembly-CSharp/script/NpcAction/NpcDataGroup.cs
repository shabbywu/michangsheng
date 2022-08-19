using System;
using System.Collections.Generic;

namespace script.NpcAction
{
	// Token: 0x020009F3 RID: 2547
	public class NpcDataGroup
	{
		// Token: 0x060046A0 RID: 18080 RVA: 0x001DDEB6 File Offset: 0x001DC0B6
		public void Start(bool isNeedSavePlace = false)
		{
			this.IsNeedSavePlace = isNeedSavePlace;
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x001DDEBF File Offset: 0x001DC0BF
		public void Clear()
		{
			this.NpcDict = new Dictionary<int, NpcData>();
			this.IsFree = true;
			this.IsNeedSavePlace = false;
		}

		// Token: 0x04004801 RID: 18433
		public Dictionary<int, NpcData> NpcDict = new Dictionary<int, NpcData>();

		// Token: 0x04004802 RID: 18434
		public bool IsFree = true;

		// Token: 0x04004803 RID: 18435
		public string Error;

		// Token: 0x04004804 RID: 18436
		public bool IsNeedSavePlace;
	}
}
