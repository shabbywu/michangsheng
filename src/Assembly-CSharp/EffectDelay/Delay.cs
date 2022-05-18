using System;
using UnityEngine;

namespace EffectDelay
{
	// Token: 0x02001684 RID: 5764
	public class Delay : MonoBehaviour
	{
		// Token: 0x060085BD RID: 34237 RVA: 0x0005CBC0 File Offset: 0x0005ADC0
		private void Start()
		{
			base.gameObject.SetActiveRecursively(false);
			base.Invoke("DelayFunc", this.delayTime);
		}

		// Token: 0x060085BE RID: 34238 RVA: 0x00004236 File Offset: 0x00002436
		private void DelayFunc()
		{
			base.gameObject.SetActiveRecursively(true);
		}

		// Token: 0x0400725A RID: 29274
		public float delayTime = 1f;
	}
}
