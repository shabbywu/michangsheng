using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000625 RID: 1573
	public class PivotDisplayer : MonoBehaviour
	{
		// Token: 0x060031FF RID: 12799 RVA: 0x0016209E File Offset: 0x0016029E
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = this.m_Color;
			Gizmos.DrawSphere(base.transform.position, this.m_Radius);
		}

		// Token: 0x04002C51 RID: 11345
		[SerializeField]
		private Color m_Color = Color.red;

		// Token: 0x04002C52 RID: 11346
		[SerializeField]
		private float m_Radius = 0.06f;
	}
}
