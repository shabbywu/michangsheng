using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DA9 RID: 3497
	[Serializable]
	public class StaticFaceInfo
	{
		// Token: 0x0400541A RID: 21530
		public string name;

		// Token: 0x0400541B RID: 21531
		[Tooltip("武将ID")]
		public int AvatarScope;

		// Token: 0x0400541C RID: 21532
		public List<StaticFaceRandomInfo> faceinfoList;
	}
}
