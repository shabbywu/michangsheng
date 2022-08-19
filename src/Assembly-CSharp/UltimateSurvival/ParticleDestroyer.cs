using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000597 RID: 1431
	public class ParticleDestroyer : MonoBehaviour
	{
		// Token: 0x06002F10 RID: 12048 RVA: 0x0015618A File Offset: 0x0015438A
		private IEnumerator Start()
		{
			ParticleSystem[] systems = base.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem particleSystem in systems)
			{
				this.m_MaxLifetime = Mathf.Max(particleSystem.main.startLifetimeMultiplier, this.m_MaxLifetime);
			}
			float stopTime = Time.time + Random.Range(this.m_MinDuration, this.m_MaxDuration);
			while (Time.time < stopTime || this.m_EarlyStop)
			{
				yield return null;
			}
			ParticleSystem[] array = systems;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].emission.enabled = false;
			}
			yield return new WaitForSeconds(this.m_MaxLifetime);
			Object.Destroy(base.gameObject);
			yield break;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x00156199 File Offset: 0x00154399
		public void Stop()
		{
			this.m_EarlyStop = true;
		}

		// Token: 0x0400295A RID: 10586
		[SerializeField]
		private float m_MinDuration = 8f;

		// Token: 0x0400295B RID: 10587
		[SerializeField]
		private float m_MaxDuration = 10f;

		// Token: 0x0400295C RID: 10588
		private float m_MaxLifetime;

		// Token: 0x0400295D RID: 10589
		private bool m_EarlyStop;
	}
}
