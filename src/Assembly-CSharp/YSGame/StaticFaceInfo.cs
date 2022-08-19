using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A7A RID: 2682
	[Serializable]
	public class StaticFaceInfo
	{
		// Token: 0x04004A75 RID: 19061
		public string name;

		// Token: 0x04004A76 RID: 19062
		[Tooltip("武将ID")]
		public int AvatarScope;

		// Token: 0x04004A77 RID: 19063
		public List<StaticFaceRandomInfo> faceinfoList;
	}
}
