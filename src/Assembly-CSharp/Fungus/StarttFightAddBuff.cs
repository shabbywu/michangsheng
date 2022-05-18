using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001451 RID: 5201
	[Serializable]
	public class StarttFightAddBuff
	{
		// Token: 0x06007D8F RID: 32143 RVA: 0x00054EA2 File Offset: 0x000530A2
		public StarttFightAddBuff()
		{
			this.buffID = 0;
			this.BuffNum = 1;
		}

		// Token: 0x04006AF2 RID: 27378
		[Tooltip("buff的ID")]
		public int buffID;

		// Token: 0x04006AF3 RID: 27379
		[Tooltip("buff的层数")]
		public int BuffNum = 1;
	}
}
