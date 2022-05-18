using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003FF RID: 1023
public class UIGaoShiRenWuItem : MonoBehaviour
{
	// Token: 0x06001BAA RID: 7082 RVA: 0x000F7CF8 File Offset: 0x000F5EF8
	public void SetYiLingQu(bool yiLingQu, JSONObject pos, bool anim = false)
	{
		if (yiLingQu)
		{
			this.TiJiaoBtn.gameObject.SetActive(false);
			GaoShiManager.SetYinZhangShow(this.YinZhangMask, this.YinZhang, pos, anim);
			return;
		}
		this.TiJiaoBtn.gameObject.SetActive(true);
		this.YinZhang.gameObject.SetActive(false);
	}

	// Token: 0x04001765 RID: 5989
	public Text Desc;

	// Token: 0x04001766 RID: 5990
	public Text LingShiTitle;

	// Token: 0x04001767 RID: 5991
	public Text LingShi;

	// Token: 0x04001768 RID: 5992
	public Image LingShiIcon;

	// Token: 0x04001769 RID: 5993
	public Text ShengWang;

	// Token: 0x0400176A RID: 5994
	public FpBtn TiJiaoBtn;

	// Token: 0x0400176B RID: 5995
	public RectMask2D YinZhangMask;

	// Token: 0x0400176C RID: 5996
	public RectTransform YinZhang;
}
