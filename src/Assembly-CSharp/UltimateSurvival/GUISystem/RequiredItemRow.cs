using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200064B RID: 1611
	public class RequiredItemRow : MonoBehaviour
	{
		// Token: 0x06003358 RID: 13144 RVA: 0x00168CFC File Offset: 0x00166EFC
		public void Set(int amount, string type, int total, int have)
		{
			bool flag = !string.IsNullOrEmpty(type);
			this.m_Amount.text = (flag ? amount.ToString() : "");
			this.m_Type.text = (flag ? type : "");
			this.m_Total.text = (flag ? total.ToString() : "");
			this.m_Have.text = (flag ? have.ToString() : "");
			if (flag)
			{
				this.m_Have.color = ((have >= total) ? this.m_HaveEnoughColor : this.m_DontHaveEnoughColor);
			}
		}

		// Token: 0x04002D8B RID: 11659
		[SerializeField]
		private Color m_HaveEnoughColor = Color.white;

		// Token: 0x04002D8C RID: 11660
		[SerializeField]
		private Color m_DontHaveEnoughColor = Color.red;

		// Token: 0x04002D8D RID: 11661
		[SerializeField]
		private Text m_Amount;

		// Token: 0x04002D8E RID: 11662
		[SerializeField]
		private Text m_Type;

		// Token: 0x04002D8F RID: 11663
		[SerializeField]
		private Text m_Total;

		// Token: 0x04002D90 RID: 11664
		[SerializeField]
		private Text m_Have;
	}
}
