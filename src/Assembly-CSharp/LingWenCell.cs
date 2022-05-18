using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000458 RID: 1112
public class LingWenCell : MonoBehaviour
{
	// Token: 0x06001DC6 RID: 7622 RVA: 0x00018C42 File Offset: 0x00016E42
	public void showDaoSanJiao()
	{
		this.daoSanJiao.SetActive(true);
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x00018C50 File Offset: 0x00016E50
	public void hideFenGeXian()
	{
		this.fenGeXian.SetActive(false);
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x00018C5E File Offset: 0x00016E5E
	public void setDesc(string str)
	{
		this.desc.text = Tools.Code64(str);
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x00018C71 File Offset: 0x00016E71
	public void lingWenCiTiaoOnclick()
	{
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(this.lingWenID);
		this.clickCallBack(this.desc.text);
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x00018CA3 File Offset: 0x00016EA3
	public void buffIDOnclick()
	{
		this.xuanWuBuffIDCallBack(this.buffID, this.desc.text);
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x00018CC1 File Offset: 0x00016EC1
	public void buffChengShu()
	{
		this.xuanWuBuffSumCallBack(this.buffID, this.buffSum, this.desc.text);
	}

	// Token: 0x0400196E RID: 6510
	[SerializeField]
	private GameObject daoSanJiao;

	// Token: 0x0400196F RID: 6511
	[SerializeField]
	private GameObject fenGeXian;

	// Token: 0x04001970 RID: 6512
	[SerializeField]
	private Text desc;

	// Token: 0x04001971 RID: 6513
	public int lingWenID;

	// Token: 0x04001972 RID: 6514
	public Action<string> clickCallBack;

	// Token: 0x04001973 RID: 6515
	public Action<int, string> xuanWuBuffIDCallBack;

	// Token: 0x04001974 RID: 6516
	public Action<int, int, string> xuanWuBuffSumCallBack;

	// Token: 0x04001975 RID: 6517
	public int buffID;

	// Token: 0x04001976 RID: 6518
	public int buffSum;
}
