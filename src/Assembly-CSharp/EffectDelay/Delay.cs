using System;
using UnityEngine;

namespace EffectDelay
{
	// Token: 0x020011C2 RID: 4546
	public class Delay : MonoBehaviour
	{
		// Token: 0x060077B2 RID: 30642 RVA: 0x002B9645 File Offset: 0x002B7845
		private void Start()
		{
			base.gameObject.SetActiveRecursively(false);
			base.Invoke("DelayFunc", this.delayTime);
		}

		// Token: 0x060077B3 RID: 30643 RVA: 0x00003B8C File Offset: 0x00001D8C
		private void DelayFunc()
		{
			base.gameObject.SetActiveRecursively(true);
		}

		// Token: 0x0400632B RID: 25387
		public float delayTime = 1f;
	}
}
