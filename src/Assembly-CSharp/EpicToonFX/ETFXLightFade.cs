using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000B24 RID: 2852
	public class ETFXLightFade : MonoBehaviour
	{
		// Token: 0x06004F91 RID: 20369 RVA: 0x00219D7C File Offset: 0x00217F7C
		private void Start()
		{
			if (base.gameObject.GetComponent<Light>())
			{
				this.li = base.gameObject.GetComponent<Light>();
				this.initIntensity = this.li.intensity;
				return;
			}
			MonoBehaviour.print("No light object found on " + base.gameObject.name);
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x00219DD8 File Offset: 0x00217FD8
		private void Update()
		{
			if (base.gameObject.GetComponent<Light>())
			{
				this.li.intensity -= this.initIntensity * (Time.deltaTime / this.life);
				if (this.killAfterLife && this.li.intensity <= 0f)
				{
					Object.Destroy(base.gameObject.GetComponent<Light>());
				}
			}
		}

		// Token: 0x04004E8E RID: 20110
		[Header("Seconds to dim the light")]
		public float life = 0.2f;

		// Token: 0x04004E8F RID: 20111
		public bool killAfterLife = true;

		// Token: 0x04004E90 RID: 20112
		private Light li;

		// Token: 0x04004E91 RID: 20113
		private float initIntensity;
	}
}
