using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000303 RID: 771
public class WuWeiManager : MonoBehaviour
{
	// Token: 0x06001AD4 RID: 6868 RVA: 0x000BF3D4 File Offset: 0x000BD5D4
	public void init()
	{
		this.qinHe.text = "0";
		this.caoKong.text = "0";
		this.linxing.text = "0";
		this.jianGuo.text = "0";
		this.renXing.text = "0";
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x000BF434 File Offset: 0x000BD634
	public void updateWuWei()
	{
		this.qinHe.text = this.getAllQingHe().ToString();
		this.caoKong.text = this.getAllCaoKong().ToString();
		this.linxing.text = this.getAllLinxing().ToString();
		this.jianGuo.text = this.getAllJianGuo().ToString();
		this.renXing.text = this.getAllRenXing().ToString();
		this.updateWuWeiRing();
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x000BF4C4 File Offset: 0x000BD6C4
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

	// Token: 0x06001AD7 RID: 6871 RVA: 0x000BF5A6 File Offset: 0x000BD7A6
	public bool checkWuWeiIsDaoBiao()
	{
		return this.getAllWuWei() >= this.Min;
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x000BF5B9 File Offset: 0x000BD7B9
	public bool checkIsHasWuWeiZero()
	{
		return this.getAllQingHe() * this.getAllCaoKong() * this.getAllLinxing() * this.getAllJianGuo() * this.getAllRenXing() == 0f;
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x000BF5E7 File Offset: 0x000BD7E7
	public float getAllWuWei()
	{
		return this.getAllQingHe() + this.getAllCaoKong() + this.getAllLinxing() + this.getAllJianGuo() + this.getAllRenXing();
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x000BF60C File Offset: 0x000BD80C
	public float getWuWeiBaiFenBi()
	{
		float num = this.getAllWuWei() / this.Min;
		if (num > 1f)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x000BF638 File Offset: 0x000BD838
	private void initWuWeiRing()
	{
		for (int i = 0; i < 5; i++)
		{
			this.wuWeiRing.VerticesDistances[i] = 0.325f;
		}
		this.wuWeiRing.updateImage();
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x000BF670 File Offset: 0x000BD870
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

	// Token: 0x06001ADD RID: 6877 RVA: 0x000BF6C4 File Offset: 0x000BD8C4
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

	// Token: 0x06001ADE RID: 6878 RVA: 0x000BF718 File Offset: 0x000BD918
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

	// Token: 0x06001ADF RID: 6879 RVA: 0x000BF76C File Offset: 0x000BD96C
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

	// Token: 0x06001AE0 RID: 6880 RVA: 0x000BF7C0 File Offset: 0x000BD9C0
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

	// Token: 0x0400158D RID: 5517
	[SerializeField]
	private Text qinHe;

	// Token: 0x0400158E RID: 5518
	[SerializeField]
	private Text caoKong;

	// Token: 0x0400158F RID: 5519
	[SerializeField]
	private Text linxing;

	// Token: 0x04001590 RID: 5520
	[SerializeField]
	private Text jianGuo;

	// Token: 0x04001591 RID: 5521
	[SerializeField]
	private Text renXing;

	// Token: 0x04001592 RID: 5522
	[SerializeField]
	private MyUIPolygon wuWeiRing;

	// Token: 0x04001593 RID: 5523
	private float Max = 500f;

	// Token: 0x04001594 RID: 5524
	private float Min = 2200f;
}
