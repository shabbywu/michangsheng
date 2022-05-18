using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000932 RID: 2354
	[Serializable]
	public class DurabilityBar
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x0002B66E File Offset: 0x0002986E
		public bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0002B676 File Offset: 0x00029876
		public void SetActive(bool active)
		{
			this.m_Background.SetActive(active);
			this.m_Bar.enabled = active;
			this.m_Active = active;
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0002B697 File Offset: 0x00029897
		public void SetFillAmount(float fillAmount)
		{
			this.m_Bar.color = this.m_ColorGradient.Evaluate(fillAmount);
			this.m_Bar.fillAmount = fillAmount;
		}

		// Token: 0x04003678 RID: 13944
		[SerializeField]
		private GameObject m_Background;

		// Token: 0x04003679 RID: 13945
		[SerializeField]
		private Image m_Bar;

		// Token: 0x0400367A RID: 13946
		[SerializeField]
		private Gradient m_ColorGradient;

		// Token: 0x0400367B RID: 13947
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Durability;

		// Token: 0x0400367C RID: 13948
		private bool m_Active = true;
	}
}
