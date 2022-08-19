using System;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000305 RID: 773
public class ZhongLingLiManager : MonoBehaviour
{
	// Token: 0x06001AE3 RID: 6883 RVA: 0x000BF832 File Offset: 0x000BDA32
	public void init()
	{
		this.yunSuanFu.text = "";
		this.totalZongLingLi.text = "0";
		this.lingWenXiaoGuo.text = "";
		this.initZhongLingLiRing();
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x000BF86A File Offset: 0x000BDA6A
	public void updateZhongLingLi()
	{
		this.updateZhongLingLiRing();
		this.totalZongLingLi.text = string.Concat((int)this.getTotalZongLingLi());
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x000BF890 File Offset: 0x000BDA90
	private void updateZhongLingLiRing()
	{
		float num = this.getTotalZongLingLi() / this.LingLiMax * 0.32f + 0.68f;
		this.zhongLingLiRing.transform.localScale = new Vector3(num, num, 1f);
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x000BF8D4 File Offset: 0x000BDAD4
	public int getRealZongLingLi()
	{
		if (LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.checkWuWeiIsDaoBiao())
		{
			return LianQiTotalManager.inst.putMaterialPageManager.chuShiLingLiManager.getAllchuShiLingLi();
		}
		float wuWeiBaiFenBi = LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.getWuWeiBaiFenBi();
		return (int)((float)LianQiTotalManager.inst.putMaterialPageManager.chuShiLingLiManager.getAllchuShiLingLi() * wuWeiBaiFenBi);
	}

	// Token: 0x06001AE7 RID: 6887 RVA: 0x000BF93C File Offset: 0x000BDB3C
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

	// Token: 0x06001AE8 RID: 6888 RVA: 0x000BFA34 File Offset: 0x000BDC34
	private void initZhongLingLiRing()
	{
		this.zhongLingLiRing.transform.localScale = new Vector3(0.68f, 0.68f, 1f);
	}

	// Token: 0x04001595 RID: 5525
	private float LingLiMax = 2160f;

	// Token: 0x04001596 RID: 5526
	[SerializeField]
	private GameObject zhongLingLiRing;

	// Token: 0x04001597 RID: 5527
	[SerializeField]
	private Text yunSuanFu;

	// Token: 0x04001598 RID: 5528
	[SerializeField]
	private Text totalZongLingLi;

	// Token: 0x04001599 RID: 5529
	[SerializeField]
	private Text lingWenXiaoGuo;
}
