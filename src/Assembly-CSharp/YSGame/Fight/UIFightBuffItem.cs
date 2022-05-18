using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000DF9 RID: 3577
	public class UIFightBuffItem : MonoBehaviour
	{
		// Token: 0x040055F3 RID: 22003
		public Image BuffIconImage;

		// Token: 0x040055F4 RID: 22004
		public Text BuffCountText;

		// Token: 0x040055F5 RID: 22005
		[HideInInspector]
		public int BuffID;

		// Token: 0x040055F6 RID: 22006
		[HideInInspector]
		public int BuffCount;

		// Token: 0x040055F7 RID: 22007
		[HideInInspector]
		public int BuffRound;

		// Token: 0x040055F8 RID: 22008
		[HideInInspector]
		public List<int> AvatarBuff;

		// Token: 0x040055F9 RID: 22009
		[HideInInspector]
		public Avatar Avatar;
	}
}
