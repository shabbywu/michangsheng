using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000460 RID: 1120
public class WuWeiManager : MonoBehaviour
{
	// Token: 0x06001DFA RID: 7674 RVA: 0x00105020 File Offset: 0x00103220
	public void init()
	{
		this.qinHe.text = "0";
		this.caoKong.text = "0";
		this.linxing.text = "0";
		this.jianGuo.text = "0";
		this.renXing.text = "0";
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x00105080 File Offset: 0x00103280
	public void updateWuWei()
	{
		this.qinHe.text = this.getAllQingHe().ToString();
		this.caoKong.text = this.getAllCaoKong().ToString();
		this.linxing.text = this.getAllLinxing().ToString();
		this.jianGuo.text = this.getAllJianGuo().ToString();
		this.renXing.text = this.getAllRenXing().ToString();
		this.updateWuWeiRing();
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x00105110 File Offset: 0x00103310
	private void updateWuWeiRing()
	{
		float num = 0.325f + this.getAllQingHe() / this.Max * 0.675f;
		float num2 = 0.325f + this.getAllCaoKong() / this.Max * 0.675f;
		float num3 = 0.325f + this.getAllLinxing() / this.Max * 0.675f;
		float num4 = 0.325f + this.getAllJianGuo() / this.Max * 0.675f;
		float num5 = 0.325f + this.getAllRenXing() / this.Max * 0.675f;
		this.wuWeiRing.VerticesDistances[0] = num;
		this.wuWeiRing.VerticesDistances[1] = num2;
		this.wuWeiRing.VerticesDistances[2] = num3;
		this.wuWeiRing.VerticesDistances[3] = num4;
		this.wuWeiRing.VerticesDistances[4] = num5;
		this.wuWeiRing.updateImage();
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x00018E98 File Offset: 0x00017098
	public bool checkWuWeiIsDaoBiao()
	{
		return this.getAllWuWei() >= this.Min;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x00018EAB File Offset: 0x000170AB
	public bool checkIsHasWuWeiZero()
	{
		return this.getAllQingHe() * this.getAllCaoKong() * this.getAllLinxing() * this.getAllJianGuo() * this.getAllRenXing() == 0f;
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x00018ED9 File Offset: 0x000170D9
	public float getAllWuWei()
	{
		return this.getAllQingHe() + this.getAllCaoKong() + this.getAllLinxing() + this.getAllJianGuo() + this.getAllRenXing();
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x001051F4 File Offset: 0x001033F4
	public float getWuWeiBaiFenBi()
	{
		float num = this.getAllWuWei() / this.Min;
		if (num > 1f)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00105220 File Offset: 0x00103420
	private void initWuWeiRing()
	{
		for (int i = 0; i < 5; i++)
		{
			this.wuWeiRing.VerticesDistances[i] = 0.325f;
		}
		this.wuWeiRing.updateImage();
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x00105258 File Offset: 0x00103458
	public float getAllQingHe()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).qinHe;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x001052AC File Offset: 0x001034AC
	public float getAllCaoKong()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).caoKong;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x00105300 File Offset: 0x00103500
	public float getAllLinxing()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).linxing;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x00105354 File Offset: 0x00103554
	public float getAllJianGuo()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).jianGuo;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x001053A8 File Offset: 0x001035A8
	public float getAllRenXing()
	{
		float num = 0f;
		for (int i = 25; i <= 34; i++)
		{
			num += (float)LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).renXing;
		}
		if (num > 500f)
		{
			num = 500f;
		}
		return num;
	}

	// Token: 0x0400199A RID: 6554
	[SerializeField]
	private Text qinHe;

	// Token: 0x0400199B RID: 6555
	[SerializeField]
	private Text caoKong;

	// Token: 0x0400199C RID: 6556
	[SerializeField]
	private Text linxing;

	// Token: 0x0400199D RID: 6557
	[SerializeField]
	private Text jianGuo;

	// Token: 0x0400199E RID: 6558
	[SerializeField]
	private Text renXing;

	// Token: 0x0400199F RID: 6559
	[SerializeField]
	private MyUIPolygon wuWeiRing;

	// Token: 0x040019A0 RID: 6560
	private float Max = 500f;

	// Token: 0x040019A1 RID: 6561
	private float Min = 2200f;
}
