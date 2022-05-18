using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000462 RID: 1122
public class ZhongLingLiManager : MonoBehaviour
{
	// Token: 0x06001E09 RID: 7689 RVA: 0x00018F1B File Offset: 0x0001711B
	public void init()
	{
		this.yunSuanFu.text = "";
		this.totalZongLingLi.text = "0";
		this.lingWenXiaoGuo.text = "";
		this.initZhongLingLiRing();
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x00018F53 File Offset: 0x00017153
	public void updateZhongLingLi()
	{
		this.updateZhongLingLiRing();
		this.totalZongLingLi.text = string.Concat((int)this.getTotalZongLingLi());
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x001053FC File Offset: 0x001035FC
	private void updateZhongLingLiRing()
	{
		float num = this.getTotalZongLingLi() / this.LingLiMax * 0.32f + 0.68f;
		this.zhongLingLiRing.transform.localScale = new Vector3(num, num, 1f);
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x00105440 File Offset: 0x00103640
	public int getRealZongLingLi()
	{
		if (LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.checkWuWeiIsDaoBiao())
		{
			return LianQiTotalManager.inst.putMaterialPageManager.chuShiLingLiManager.getAllchuShiLingLi();
		}
		float wuWeiBaiFenBi = LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.getWuWeiBaiFenBi();
		return (int)((float)LianQiTotalManager.inst.putMaterialPageManager.chuShiLingLiManager.getAllchuShiLingLi() * wuWeiBaiFenBi);
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x001054A8 File Offset: 0x001036A8
	public float getTotalZongLingLi()
	{
		Avatar player = Tools.instance.getPlayer();
		float num = (float)this.getRealZongLingLi();
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		if (selectLingWenID == -1)
		{
			this.yunSuanFu.text = "";
			this.lingWenXiaoGuo.text = "";
		}
		else
		{
			JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()];
			float n = jsonobject["value4"].n;
			if (jsonobject["value3"].I == 1)
			{
				num *= jsonobject["value4"].n;
				this.yunSuanFu.text = "x";
			}
			else
			{
				num += n;
				this.yunSuanFu.text = "+";
			}
			this.lingWenXiaoGuo.text = n.ToString();
		}
		if (player.checkHasStudyWuDaoSkillByID(2241))
		{
			num *= 1.5f;
		}
		return num;
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x00018F77 File Offset: 0x00017177
	private void initZhongLingLiRing()
	{
		this.zhongLingLiRing.transform.localScale = new Vector3(0.68f, 0.68f, 1f);
	}

	// Token: 0x040019A2 RID: 6562
	private float LingLiMax = 2160f;

	// Token: 0x040019A3 RID: 6563
	[SerializeField]
	private GameObject zhongLingLiRing;

	// Token: 0x040019A4 RID: 6564
	[SerializeField]
	private Text yunSuanFu;

	// Token: 0x040019A5 RID: 6565
	[SerializeField]
	private Text totalZongLingLi;

	// Token: 0x040019A6 RID: 6566
	[SerializeField]
	private Text lingWenXiaoGuo;
}
