using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x0200094A RID: 2378
	public class RequiredItemRow : MonoBehaviour
	{
		// Token: 0x06003CCE RID: 15566 RVA: 0x001B1D30 File Offset: 0x001AFF30
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

		// Token: 0x04003702 RID: 14082
		[SerializeField]
		private Color m_HaveEnoughColor = Color.white;

		// Token: 0x04003703 RID: 14083
		[SerializeField]
		private Color m_DontHaveEnoughColor = Color.red;

		// Token: 0x04003704 RID: 14084
		[SerializeField]
		private Text m_Amount;

		// Token: 0x04003705 RID: 14085
		[SerializeField]
		private Text m_Type;

		// Token: 0x04003706 RID: 14086
		[SerializeField]
		private Text m_Total;

		// Token: 0x04003707 RID: 14087
		[SerializeField]
		private Text m_Have;
	}
}
