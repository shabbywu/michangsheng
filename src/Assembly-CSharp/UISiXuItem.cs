using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028C RID: 652
public class UISiXuItem : MonoBehaviour
{
	// Token: 0x06001796 RID: 6038 RVA: 0x000A2604 File Offset: 0x000A0804
	public void SetData(SiXuData sixudata)
	{
		this.data = sixudata;
		this.PinJieText.text = this.data.PinJieStr;
		this.TypeText.text = this.data.WuDaoTypeStr;
		this.TimeText.text = this.data.ShengYuTime;
		this.SiXuImage.sprite = UIBiGuanGanWuPanel.Inst.WuDaoTypeSpriteList[this.data.wuDaoFilter - 1];
	}

	// Token: 0x06001797 RID: 6039 RVA: 0x000A2681 File Offset: 0x000A0881
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

	// Token: 0x04001255 RID: 4693
	public Image SiXuImage;

	// Token: 0x04001256 RID: 4694
	public Text PinJieText;

	// Token: 0x04001257 RID: 4695
	public Text TypeText;

	// Token: 0x04001258 RID: 4696
	public Text TimeText;

	// Token: 0x04001259 RID: 4697
	private SiXuData data;
}
