using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BA RID: 954
public class UISiXuItem : MonoBehaviour
{
	// Token: 0x06001A73 RID: 6771 RVA: 0x000E9A30 File Offset: 0x000E7C30
	public void SetData(SiXuData sixudata)
	{
		this.data = sixudata;
		this.PinJieText.text = this.data.PinJieStr;
		this.TypeText.text = this.data.WuDaoTypeStr;
		this.TimeText.text = this.data.ShengYuTime;
		this.SiXuImage.sprite = UIBiGuanGanWuPanel.Inst.WuDaoTypeSpriteList[this.data.wuDaoFilter - 1];
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x00016889 File Offset: 0x00014A89
	public void OnToggleChanged(bool value)
	{
		if (value)
		{
			UIBiGuanGanWuPanel.Inst.NowSiXu = this.data;
		}
		else
		{
			UIBiGuanGanWuPanel.Inst.NowSiXu = null;
		}
		UIBiGuanGanWuPanel.Inst.SetGanWu(UIBiGuanGanWuPanel.Inst.NowSiXu);
	}

	// Token: 0x040015D8 RID: 5592
	public Image SiXuImage;

	// Token: 0x040015D9 RID: 5593
	public Text PinJieText;

	// Token: 0x040015DA RID: 5594
	public Text TypeText;

	// Token: 0x040015DB RID: 5595
	public Text TimeText;

	// Token: 0x040015DC RID: 5596
	private SiXuData data;
}
