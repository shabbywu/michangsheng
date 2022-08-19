using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000B25 RID: 2853
	public class ETFXPitchRandomizer : MonoBehaviour
	{
		// Token: 0x06004F94 RID: 20372 RVA: 0x00219E60 File Offset: 0x00218060
		private void Start()
		{
			base.transform.GetComponent<AudioSource>().pitch *= 1f + Random.Range(-this.randomPercent / 100f, this.randomPercent / 100f);
		}

		// Token: 0x04004E92 RID: 20114
		public float randomPercent = 10f;
	}
}
