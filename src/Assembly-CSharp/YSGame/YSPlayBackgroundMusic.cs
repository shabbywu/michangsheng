using System;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DAD RID: 3501
	public class YSPlayBackgroundMusic : MonoBehaviour
	{
		// Token: 0x06005478 RID: 21624 RVA: 0x0003C775 File Offset: 0x0003A975
		private void Start()
		{
			MusicMag.instance.playMusic(this.MusicIndex);
		}

		// Token: 0x04005433 RID: 21555
		public int MusicIndex;
	}
}
