using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000452 RID: 1106
public class ChuShiLingLiManager : MonoBehaviour
{
	// Token: 0x06001D9B RID: 7579 RVA: 0x00018A44 File Offset: 0x00016C44
	public void init()
	{
		this.chuShiLingLi.text = "0";
		this.ChushiLingRing.transform.localScale = new Vector3(0.475f, 0.475f, 1f);
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x00102BBC File Offset: 0x00100DBC
	public void updateChushiLingLi()
	{
		this.chuShiLingLi.text = this.getAllchuShiLingLi().ToString();
		float num = 0.475f + 0.525f * (float)this.getAllchuShiLingLi() / this.Max;
		this.ChushiLingRing.transform.localScale = new Vector3(num, num, 1f);
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x00102C1C File Offset: 0x00100E1C
	public int getAllchuShiLingLi()
	{
		int num = 0;
		for (int i = 25; i <= 34; i++)
		{
			num += LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.GetCaiLiaoCellByName(i.ToString()).lingLi;
		}
		return num;
	}

	// Token: 0x04001953 RID: 6483
	[SerializeField]
	private Image ChushiLingRing;

	// Token: 0x04001954 RID: 6484
	[SerializeField]
	private Text chuShiLingLi;

	// Token: 0x04001955 RID: 6485
	private float Max = 480f;
}
