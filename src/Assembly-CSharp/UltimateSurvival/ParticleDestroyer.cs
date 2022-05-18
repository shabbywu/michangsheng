using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000841 RID: 2113
	public class ParticleDestroyer : MonoBehaviour
	{
		// Token: 0x06003786 RID: 14214 RVA: 0x00028520 File Offset: 0x00026720
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

		// Token: 0x06003787 RID: 14215 RVA: 0x0002852F File Offset: 0x0002672F
		public void Stop()
		{
			this.m_EarlyStop = true;
		}

		// Token: 0x0400319C RID: 12700
		[SerializeField]
		private float m_MinDuration = 8f;

		// Token: 0x0400319D RID: 12701
		[SerializeField]
		private float m_MaxDuration = 10f;

		// Token: 0x0400319E RID: 12702
		private float m_MaxLifetime;

		// Token: 0x0400319F RID: 12703
		private bool m_EarlyStop;
	}
}
