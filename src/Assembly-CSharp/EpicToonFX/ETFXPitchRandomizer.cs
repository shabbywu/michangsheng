using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000E98 RID: 3736
	public class ETFXPitchRandomizer : MonoBehaviour
	{
		// Token: 0x060059B3 RID: 22963 RVA: 0x0003FA83 File Offset: 0x0003DC83
		private void Start()
		{
			base.transform.GetComponent<AudioSource>().pitch *= 1f + Random.Range(-this.randomPercent / 100f, this.randomPercent / 100f);
		}

		// Token: 0x04005906 RID: 22790
		public float randomPercent = 10f;
	}
}
