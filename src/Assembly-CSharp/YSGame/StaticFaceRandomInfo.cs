using System;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DAA RID: 3498
	[Serializable]
	public class StaticFaceRandomInfo
	{
		// Token: 0x0400541D RID: 21533
		[Tooltip("随机的部位")]
		public SetAvatarFaceRandomInfo.InfoName SkinTypeName;

		// Token: 0x0400541E RID: 21534
		[Tooltip("随机部位的值")]
		public int SkinTypeScope;
	}
}
