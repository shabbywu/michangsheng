using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000A95 RID: 2709
	public class UITianJiDaBiFire : MonoBehaviour
	{
		// Token: 0x06004BDB RID: 19419 RVA: 0x00205464 File Offset: 0x00203664
		public void SetNormal()
		{
			this.Normal.SetActive(true);
			this.Win.SetActive(false);
			this.Fail.SetActive(false);
			this.NumberText.color = this.normalColor;
			this.FireAnim.Play("UITianJiDaBiFireAnim");
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x002054B6 File Offset: 0x002036B6
		public void SetWin()
		{
			this.Normal.SetActive(false);
			this.Win.SetActive(true);
			this.Fail.SetActive(false);
			this.NumberText.color = this.winColor;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x002054ED File Offset: 0x002036ED
		public void SetFail()
		{
			this.Normal.SetActive(false);
			this.Win.SetActive(false);
			this.Fail.SetActive(true);
			this.NumberText.color = this.failColor;
		}

		// Token: 0x04004AF4 RID: 19188
		public Text NumberText;

		// Token: 0x04004AF5 RID: 19189
		public GameObject Normal;

		// Token: 0x04004AF6 RID: 19190
		public GameObject Win;

		// Token: 0x04004AF7 RID: 19191
		public GameObject Fail;

		// Token: 0x04004AF8 RID: 19192
		public Animator FireAnim;

		// Token: 0x04004AF9 RID: 19193
		private Color normalColor = new Color(0.91764706f, 0.87058824f, 0.7411765f);

		// Token: 0x04004AFA RID: 19194
		private Color winColor = new Color(0.91764706f, 0.87058824f, 0.7411765f);

		// Token: 0x04004AFB RID: 19195
		private Color failColor = new Color(0.7411765f, 0.91764706f, 0.90588236f);
	}
}
