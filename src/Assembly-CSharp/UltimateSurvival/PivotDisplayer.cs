using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200090D RID: 2317
	public class PivotDisplayer : MonoBehaviour
	{
		// Token: 0x06003B39 RID: 15161 RVA: 0x0002AE3A File Offset: 0x0002903A
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = this.m_Color;
			Gizmos.DrawSphere(base.transform.position, this.m_Radius);
		}

		// Token: 0x0400357F RID: 13695
		[SerializeField]
		private Color m_Color = Color.red;

		// Token: 0x04003580 RID: 13696
		[SerializeField]
		private float m_Radius = 0.06f;
	}
}
