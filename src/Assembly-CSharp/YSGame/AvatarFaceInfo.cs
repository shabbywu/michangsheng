using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A79 RID: 2681
	[Serializable]
	public class AvatarFaceInfo
	{
		// Token: 0x04004A71 RID: 19057
		public string name;

		// Token: 0x04004A72 RID: 19058
		[Tooltip("武将随机的范围")]
		public List<Vector2> AvatarScope;

		// Token: 0x04004A73 RID: 19059
		[Tooltip("随机的部位")]
		public SetAvatarFaceRandomInfo.InfoName SkinTypeName;

		// Token: 0x04004A74 RID: 19060
		[Tooltip("随机部位的范围")]
		public List<Vector2> SkinTypeScope;
	}
}
