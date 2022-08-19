using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FB RID: 763
public class LingWenCell : MonoBehaviour
{
	// Token: 0x06001AA0 RID: 6816 RVA: 0x000BDB6D File Offset: 0x000BBD6D
	public void showDaoSanJiao()
	{
		this.daoSanJiao.SetActive(true);
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000BDB7B File Offset: 0x000BBD7B
	public void hideFenGeXian()
	{
		this.fenGeXian.SetActive(false);
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000BDB89 File Offset: 0x000BBD89
	public void setDesc(string str)
	{
		this.desc.text = Tools.Code64(str);
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x000BDB9C File Offset: 0x000BBD9C
	public void lingWenCiTiaoOnclick()
	{
		LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.setSelectLinWenID(this.lingWenID);
		this.clickCallBack(this.desc.text);
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000BDBCE File Offset: 0x000BBDCE
	public void buffIDOnclick()
	{
		this.xuanWuBuffIDCallBack(this.buffID, this.desc.text);
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x000BDBEC File Offset: 0x000BBDEC
	public void buffChengShu()
	{
		this.xuanWuBuffSumCallBack(this.buffID, this.buffSum, this.desc.text);
	}

	// Token: 0x04001561 RID: 5473
	[SerializeField]
	private GameObject daoSanJiao;

	// Token: 0x04001562 RID: 5474
	[SerializeField]
	private GameObject fenGeXian;

	// Token: 0x04001563 RID: 5475
	[SerializeField]
	private Text desc;

	// Token: 0x04001564 RID: 5476
	public int lingWenID;

	// Token: 0x04001565 RID: 5477
	public Action<string> clickCallBack;

	// Token: 0x04001566 RID: 5478
	public Action<int, string> xuanWuBuffIDCallBack;

	// Token: 0x04001567 RID: 5479
	public Action<int, int, string> xuanWuBuffSumCallBack;

	// Token: 0x04001568 RID: 5480
	public int buffID;

	// Token: 0x04001569 RID: 5481
	public int buffSum;
}
