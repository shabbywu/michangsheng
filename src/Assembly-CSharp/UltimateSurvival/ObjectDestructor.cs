using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000840 RID: 2112
	public class ObjectDestructor : MonoBehaviour
	{
		// Token: 0x06003783 RID: 14211 RVA: 0x000284DA File Offset: 0x000266DA
		private void Awake()
		{
			base.Invoke("DestroyNow", this.m_TimeOut);
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000284ED File Offset: 0x000266ED
		private void DestroyNow()
		{
			if (this.m_DetachChildren)
			{
				base.transform.DetachChildren();
			}
			Object.DestroyObject(base.gameObject);
		}

		// Token: 0x0400319A RID: 12698
		[SerializeField]
		private float m_TimeOut = 1f;

		// Token: 0x0400319B RID: 12699
		[SerializeField]
		private bool m_DetachChildren;
	}
}
