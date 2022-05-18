using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000E97 RID: 3735
	public class ETFXLightFade : MonoBehaviour
	{
		// Token: 0x060059B0 RID: 22960 RVA: 0x00249BAC File Offset: 0x00247DAC
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

		// Token: 0x060059B1 RID: 22961 RVA: 0x00249C08 File Offset: 0x00247E08
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

		// Token: 0x04005902 RID: 22786
		[Header("Seconds to dim the light")]
		public float life = 0.2f;

		// Token: 0x04005903 RID: 22787
		public bool killAfterLife = true;

		// Token: 0x04005904 RID: 22788
		private Light li;

		// Token: 0x04005905 RID: 22789
		private float initIntensity;
	}
}
