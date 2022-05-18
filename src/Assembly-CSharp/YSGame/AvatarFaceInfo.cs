using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DA8 RID: 3496
	[Serializable]
	public class AvatarFaceInfo
	{
		// Token: 0x04005416 RID: 21526
		public string name;

		// Token: 0x04005417 RID: 21527
		[Tooltip("武将随机的范围")]
		public List<Vector2> AvatarScope;

		// Token: 0x04005418 RID: 21528
		[Tooltip("随机的部位")]
		public SetAvatarFaceRandomInfo.InfoName SkinTypeName;

		// Token: 0x04005419 RID: 21529
		[Tooltip("随机部位的范围")]
		public List<Vector2> SkinTypeScope;
	}
}
