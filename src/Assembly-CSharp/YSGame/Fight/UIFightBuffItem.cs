using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000ABC RID: 2748
	public class UIFightBuffItem : MonoBehaviour
	{
		// Token: 0x04004C1B RID: 19483
		public Image BuffIconImage;

		// Token: 0x04004C1C RID: 19484
		public Text BuffCountText;

		// Token: 0x04004C1D RID: 19485
		[HideInInspector]
		public int BuffID;

		// Token: 0x04004C1E RID: 19486
		[HideInInspector]
		public int BuffCount;

		// Token: 0x04004C1F RID: 19487
		[HideInInspector]
		public int BuffRound;

		// Token: 0x04004C20 RID: 19488
		[HideInInspector]
		public List<int> AvatarBuff;

		// Token: 0x04004C21 RID: 19489
		[HideInInspector]
		public Avatar Avatar;
	}
}
