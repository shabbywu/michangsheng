using System;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A7B RID: 2683
	[Serializable]
	public class StaticFaceRandomInfo
	{
		// Token: 0x04004A78 RID: 19064
		[Tooltip("随机的部位")]
		public SetAvatarFaceRandomInfo.InfoName SkinTypeName;

		// Token: 0x04004A79 RID: 19065
		[Tooltip("随机部位的值")]
		public int SkinTypeScope;
	}
}
