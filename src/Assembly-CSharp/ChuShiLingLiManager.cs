using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F5 RID: 757
public class ChuShiLingLiManager : MonoBehaviour
{
	// Token: 0x06001A75 RID: 6773 RVA: 0x000BC9C8 File Offset: 0x000BABC8
	public void init()
	{
		this.chuShiLingLi.text = "0";
		this.ChushiLingRing.transform.localScale = new Vector3(0.475f, 0.475f, 1f);
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x000BCA00 File Offset: 0x000BAC00
	public void updateChushiLingLi()
	{
		this.chuShiLingLi.text = this.getAllchuShiLingLi().ToString();
		float num = 0.475f + 0.525f * (float)this.getAllchuShiLingLi() / this.Max;
		this.ChushiLingRing.transform.localScale = new Vector3(num, num, 1f);
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000BCA60 File Offset: 0x000BAC60
	public int getAllchuShiLingLi()
	{
		int num = 0;
		for (int i = 25; i <= 34; i++)
		{
			num += LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).lingLi;
		}
		return num;
	}

	// Token: 0x04001546 RID: 5446
	[SerializeField]
	private Image ChushiLingRing;

	// Token: 0x04001547 RID: 5447
	[SerializeField]
	private Text chuShiLingLi;

	// Token: 0x04001548 RID: 5448
	private float Max = 480f;
}
