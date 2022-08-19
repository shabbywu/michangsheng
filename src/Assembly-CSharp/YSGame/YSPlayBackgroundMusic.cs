using System;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A7D RID: 2685
	public class YSPlayBackgroundMusic : MonoBehaviour
	{
		// Token: 0x06004B64 RID: 19300 RVA: 0x0020034E File Offset: 0x001FE54E
		private void Start()
		{
			MusicMag.instance.playMusic(this.MusicIndex);
		}

		// Token: 0x04004A7D RID: 19069
		public int MusicIndex;
	}
}
