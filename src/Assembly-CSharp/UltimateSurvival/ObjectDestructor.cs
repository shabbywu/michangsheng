using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000596 RID: 1430
	public class ObjectDestructor : MonoBehaviour
	{
		// Token: 0x06002F0D RID: 12045 RVA: 0x00156144 File Offset: 0x00154344
		private void Awake()
		{
			base.Invoke("DestroyNow", this.m_TimeOut);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x00156157 File Offset: 0x00154357
		private void DestroyNow()
		{
			if (this.m_DetachChildren)
			{
				base.transform.DetachChildren();
			}
			Object.DestroyObject(base.gameObject);
		}

		// Token: 0x04002958 RID: 10584
		[SerializeField]
		private float m_TimeOut = 1f;

		// Token: 0x04002959 RID: 10585
		[SerializeField]
		private bool m_DetachChildren;
	}
}
