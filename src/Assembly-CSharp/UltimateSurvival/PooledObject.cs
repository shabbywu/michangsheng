using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E4 RID: 1508
	public class PooledObject : MonoBehaviour
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x0015B8AC File Offset: 0x00159AAC
		public virtual void OnUse(Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parent = null)
		{
			try
			{
				base.gameObject.SetActive(true);
				base.transform.position = position;
				base.transform.rotation = rotation;
				if (base.transform.parent)
				{
					base.transform.SetParent(parent);
				}
				for (int i = 0; i < this.m_ToResetParticles.Length; i++)
				{
					this.m_ToResetParticles[i].Play(true);
				}
				if (this.m_ReleaseOnTimer)
				{
					base.StopAllCoroutines();
					base.StartCoroutine(this.ReleaseWithDelay());
				}
			}
			catch
			{
			}
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0015B94C File Offset: 0x00159B4C
		public virtual void OnRelease()
		{
			base.gameObject.SetActive(false);
			this.Released.Send(this);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0015B966 File Offset: 0x00159B66
		private void Awake()
		{
			if (this.m_ReleaseOnTimer)
			{
				this.m_WaitInterval = new WaitForSeconds(this.m_ReleaseTimer);
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x0015B981 File Offset: 0x00159B81
		private IEnumerator ReleaseWithDelay()
		{
			yield return this.m_WaitInterval;
			this.OnRelease();
			yield break;
		}

		// Token: 0x04002AAC RID: 10924
		public Message<PooledObject> Released = new Message<PooledObject>();

		// Token: 0x04002AAD RID: 10925
		[SerializeField]
		private bool m_ReleaseOnTimer = true;

		// Token: 0x04002AAE RID: 10926
		[SerializeField]
		private float m_ReleaseTimer = 20f;

		// Token: 0x04002AAF RID: 10927
		[SerializeField]
		private ParticleSystem[] m_ToResetParticles;

		// Token: 0x04002AB0 RID: 10928
		private WaitForSeconds m_WaitInterval;
	}
}
