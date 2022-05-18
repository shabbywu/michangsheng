using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.TianJiDaBi
{
	// Token: 0x02000DC9 RID: 3529
	public class UITianJiDaBiFire : MonoBehaviour
	{
		// Token: 0x06005504 RID: 21764 RVA: 0x0023688C File Offset: 0x00234A8C
		public void SetNormal()
		{
			this.Normal.SetActive(true);
			this.Win.SetActive(false);
			this.Fail.SetActive(false);
			this.NumberText.color = this.normalColor;
			this.FireAnim.Play("UITianJiDaBiFireAnim");
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0003CC0D File Offset: 0x0003AE0D
		public void SetWin()
		{
			this.Normal.SetActive(false);
			this.Win.SetActive(true);
			this.Fail.SetActive(false);
			this.NumberText.color = this.winColor;
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0003CC44 File Offset: 0x0003AE44
		public void SetFail()
		{
			this.Normal.SetActive(false);
			this.Win.SetActive(false);
			this.Fail.SetActive(true);
			this.NumberText.color = this.failColor;
		}

		// Token: 0x040054B9 RID: 21689
		public Text NumberText;

		// Token: 0x040054BA RID: 21690
		public GameObject Normal;

		// Token: 0x040054BB RID: 21691
		public GameObject Win;

		// Token: 0x040054BC RID: 21692
		public GameObject Fail;

		// Token: 0x040054BD RID: 21693
		public Animator FireAnim;

		// Token: 0x040054BE RID: 21694
		private Color normalColor = new Color(0.91764706f, 0.87058824f, 0.7411765f);

		// Token: 0x040054BF RID: 21695
		private Color winColor = new Color(0.91764706f, 0.87058824f, 0.7411765f);

		// Token: 0x040054C0 RID: 21696
		private Color failColor = new Color(0.7411765f, 0.91764706f, 0.90588236f);
	}
}
