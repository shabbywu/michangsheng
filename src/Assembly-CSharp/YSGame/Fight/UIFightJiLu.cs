using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000AC0 RID: 2752
	public class UIFightJiLu : MonoBehaviour
	{
		// Token: 0x06004D1A RID: 19738 RVA: 0x0020FF76 File Offset: 0x0020E176
		public void Clear()
		{
			this.JiLuText.text = "";
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x0020FF88 File Offset: 0x0020E188
		public void AddText(string text)
		{
			Text jiLuText = this.JiLuText;
			jiLuText.text = jiLuText.text + text + "\n";
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0020FFA8 File Offset: 0x0020E1A8
		public void Show()
		{
			this.isShow = true;
			float num = Mathf.Max(0f, this.ContentRT.sizeDelta.y - (this.ContentRT.parent as RectTransform).sizeDelta.y);
			this.ContentRT.anchoredPosition = new Vector2(this.ContentRT.anchoredPosition.x, num);
			this.ScaleObj.SetActive(true);
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x0021001F File Offset: 0x0020E21F
		public void Hide()
		{
			this.isShow = false;
			this.ScaleObj.SetActive(false);
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00210034 File Offset: 0x0020E234
		public void ToggleOpen()
		{
			if (this.isShow)
			{
				this.Hide();
				return;
			}
			this.Show();
		}

		// Token: 0x04004C37 RID: 19511
		public Text JiLuText;

		// Token: 0x04004C38 RID: 19512
		public RectTransform ContentRT;

		// Token: 0x04004C39 RID: 19513
		public GameObject ScaleObj;

		// Token: 0x04004C3A RID: 19514
		private bool isShow;
	}
}
