using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200063C RID: 1596
	[Serializable]
	public class DurabilityBar
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x00166C1D File Offset: 0x00164E1D
		public bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x00166C25 File Offset: 0x00164E25
		public void SetActive(bool active)
		{
			this.m_Background.SetActive(active);
			this.m_Bar.enabled = active;
			this.m_Active = active;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x00166C46 File Offset: 0x00164E46
		public void SetFillAmount(float fillAmount)
		{
			this.m_Bar.color = this.m_ColorGradient.Evaluate(fillAmount);
			this.m_Bar.fillAmount = fillAmount;
		}

		// Token: 0x04002D1B RID: 11547
		[SerializeField]
		private GameObject m_Background;

		// Token: 0x04002D1C RID: 11548
		[SerializeField]
		private Image m_Bar;

		// Token: 0x04002D1D RID: 11549
		[SerializeField]
		private Gradient m_ColorGradient;

		// Token: 0x04002D1E RID: 11550
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Durability;

		// Token: 0x04002D1F RID: 11551
		private bool m_Active = true;
	}
}
