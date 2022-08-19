using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000FA2 RID: 4002
	[Serializable]
	public class StarttFightAddBuff
	{
		// Token: 0x06006FB1 RID: 28593 RVA: 0x002A74BC File Offset: 0x002A56BC
		public StarttFightAddBuff()
		{
			this.buffID = 0;
			this.BuffNum = 1;
		}

		// Token: 0x04005C23 RID: 23587
		[Tooltip("buff的ID")]
		public int buffID;

		// Token: 0x04005C24 RID: 23588
		[Tooltip("buff的层数")]
		public int BuffNum = 1;
	}
}
