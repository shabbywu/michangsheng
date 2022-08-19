using System;
using System.Collections;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000B22 RID: 2850
	public class ETFXLoopScript : MonoBehaviour
	{
		// Token: 0x06004F89 RID: 20361 RVA: 0x00219AC2 File Offset: 0x00217CC2
		private void Start()
		{
			this.PlayEffect();
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x00219ACA File Offset: 0x00217CCA
		public void PlayEffect()
		{
			base.StartCoroutine("EffectLoop");
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x00219AD8 File Offset: 0x00217CD8
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

		// Token: 0x04004E7D RID: 20093
		public GameObject chosenEffect;

		// Token: 0x04004E7E RID: 20094
		public float loopTimeLimit = 2f;

		// Token: 0x04004E7F RID: 20095
		[Header("Spawn without")]
		public bool spawnWithoutLight = true;

		// Token: 0x04004E80 RID: 20096
		public bool spawnWithoutSound = true;
	}
}
