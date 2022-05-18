using System;
using System.Collections.Generic;

namespace script.NpcAction
{
	// Token: 0x02000AC0 RID: 2752
	public class NpcDataGroup
	{
		// Token: 0x0600464E RID: 17998 RVA: 0x000042DD File Offset: 0x000024DD
		public void GroupAction(int times)
		{
		}

		// Token: 0x04003E73 RID: 15987
		public Dictionary<int, NpcData> NpcDict = new Dictionary<int, NpcData>();

		// Token: 0x04003E74 RID: 15988
		public bool IsFree = true;
	}
}
