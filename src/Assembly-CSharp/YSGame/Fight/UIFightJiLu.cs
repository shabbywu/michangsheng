using System;
using UnityEngine;
using UnityEngine.UI;

namespace YSGame.Fight
{
	// Token: 0x02000DFE RID: 3582
	public class UIFightJiLu : MonoBehaviour
	{
		// Token: 0x06005669 RID: 22121 RVA: 0x0003DC88 File Offset: 0x0003BE88
		public void Clear()
		{
			this.JiLuText.text = "";
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x0003DC9A File Offset: 0x0003BE9A
		public void AddText(string text)
		{
			Text jiLuText = this.JiLuText;
			jiLuText.text = jiLuText.text + text + "\n";
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x00240694 File Offset: 0x0023E894
		public void Show()
		{
			this.isShow = true;
			float num = Mathf.Max(0f, this.ContentRT.sizeDelta.y - (this.ContentRT.parent as RectTransform).sizeDelta.y);
			this.ContentRT.anchoredPosition = new Vector2(this.ContentRT.anchoredPosition.x, num);
			this.ScaleObj.SetActive(true);
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
		public void Hide()
		{
			this.isShow = false;
			this.ScaleObj.SetActive(false);
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x0003DCCD File Offset: 0x0003BECD
		public void ToggleOpen()
		{
			if (this.isShow)
			{
				this.Hide();
				return;
			}
			this.Show();
		}

		// Token: 0x04005611 RID: 22033
		public Text JiLuText;

		// Token: 0x04005612 RID: 22034
		public RectTransform ContentRT;

		// Token: 0x04005613 RID: 22035
		public GameObject ScaleObj;

		// Token: 0x04005614 RID: 22036
		private bool isShow;
	}
}
