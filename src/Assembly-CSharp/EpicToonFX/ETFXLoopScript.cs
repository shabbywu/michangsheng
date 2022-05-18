using System;
using System.Collections;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000E94 RID: 3732
	public class ETFXLoopScript : MonoBehaviour
	{
		// Token: 0x060059A2 RID: 22946 RVA: 0x0003FA0C File Offset: 0x0003DC0C
		private void Start()
		{
			this.PlayEffect();
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x0003FA14 File Offset: 0x0003DC14
		public void PlayEffect()
		{
			base.StartCoroutine("EffectLoop");
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x0003FA22 File Offset: 0x0003DC22
		private IEnumerator EffectLoop()
		{
			GameObject effectPlayer = Object.Instantiate<GameObject>(this.chosenEffect, base.transform.position, base.transform.rotation);
			if (this.spawnWithoutLight = effectPlayer.GetComponent<Light>())
			{
				effectPlayer.GetComponent<Light>().enabled = false;
			}
			if (this.spawnWithoutSound = effectPlayer.GetComponent<AudioSource>())
			{
				effectPlayer.GetComponent<AudioSource>().enabled = false;
			}
			yield return new WaitForSeconds(this.loopTimeLimit);
			Object.Destroy(effectPlayer);
			this.PlayEffect();
			yield break;
		}

		// Token: 0x040058ED RID: 22765
		public GameObject chosenEffect;

		// Token: 0x040058EE RID: 22766
		public float loopTimeLimit = 2f;

		// Token: 0x040058EF RID: 22767
		[Header("Spawn without")]
		public bool spawnWithoutLight = true;

		// Token: 0x040058F0 RID: 22768
		public bool spawnWithoutSound = true;
	}
}
