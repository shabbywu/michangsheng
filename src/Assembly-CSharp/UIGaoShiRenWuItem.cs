using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BF RID: 703
public class UIGaoShiRenWuItem : MonoBehaviour
{
	// Token: 0x060018AF RID: 6319 RVA: 0x000B1690 File Offset: 0x000AF890
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

	// Token: 0x040013B5 RID: 5045
	public Text Desc;

	// Token: 0x040013B6 RID: 5046
	public Text LingShiTitle;

	// Token: 0x040013B7 RID: 5047
	public Text LingShi;

	// Token: 0x040013B8 RID: 5048
	public Image LingShiIcon;

	// Token: 0x040013B9 RID: 5049
	public Text ShengWang;

	// Token: 0x040013BA RID: 5050
	public FpBtn TiJiaoBtn;

	// Token: 0x040013BB RID: 5051
	public RectMask2D YinZhangMask;

	// Token: 0x040013BC RID: 5052
	public RectTransform YinZhang;
}
