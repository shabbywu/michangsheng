using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008B2 RID: 2226
	public class PooledObject : MonoBehaviour
	{
		// Token: 0x06003954 RID: 14676 RVA: 0x001A5274 File Offset: 0x001A3474
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

		// Token: 0x06003955 RID: 14677 RVA: 0x00029949 File Offset: 0x00027B49
		public virtual void OnRelease()
		{
			base.gameObject.SetActive(false);
			this.Released.Send(this);
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x00029963 File Offset: 0x00027B63
		private void Awake()
		{
			if (this.m_ReleaseOnTimer)
			{
				this.m_WaitInterval = new WaitForSeconds(this.m_ReleaseTimer);
			}
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x0002997E File Offset: 0x00027B7E
		private IEnumerator ReleaseWithDelay()
		{
			yield return this.m_WaitInterval;
			this.OnRelease();
			yield break;
		}

		// Token: 0x0400336A RID: 13162
		public Message<PooledObject> Released = new Message<PooledObject>();

		// Token: 0x0400336B RID: 13163
		[SerializeField]
		private bool m_ReleaseOnTimer = true;

		// Token: 0x0400336C RID: 13164
		[SerializeField]
		private float m_ReleaseTimer = 20f;

		// Token: 0x0400336D RID: 13165
		[SerializeField]
		private ParticleSystem[] m_ToResetParticles;

		// Token: 0x0400336E RID: 13166
		private WaitForSeconds m_WaitInterval;
	}
}
