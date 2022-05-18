using System;
using System.Collections.Generic;
using JSONClass;
using LianQi;

// Token: 0x02000466 RID: 1126
public class PutMaterialCell : LianQiSlot
{
	// Token: 0x06001E3D RID: 7741 RVA: 0x0001917C File Offset: 0x0001737C
	public override void SetSlotData(object data)
	{
		this.UpdateItem();
		base.SetSlotData(data);
		this.SetShuXing();
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x00019191 File Offset: 0x00017391
	public override void SetNull()
	{
		base.SetNull();
		this.UpdateItem();
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x00106F18 File Offset: 0x00105118
	public void SetShuXing()
	{
		if (base.IsNull())
		{
			return;
		}
		int wuWeiType = _ItemJsonData.DataDict[this.Item.Id].WuWeiType;
		this.SetShuXingType();
		if (wuWeiType == 0)
		{
			return;
		}
		JSONObject jsonobject = jsonData.instance.LianQiWuWeiBiao[wuWeiType.ToString()];
		this.SetWuWei(jsonobject["value1"].I, jsonobject["value2"].I, jsonobject["value3"].I, jsonobject["value4"].I, jsonobject["value5"].I);
		this.SetLingLi();
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x0001919F File Offset: 0x0001739F
	public void UpdateItem()
	{
		this.qinHe = 0;
		this.caoKong = 0;
		this.linxing = 0;
		this.jianGuo = 0;
		this.renXing = 0;
		this.lingLi = 0;
		this.shuXingTypeID = 0;
		this.attackType = -1;
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x000191D9 File Offset: 0x000173D9
	private void SetWuWei(int qinHe, int caoKong, int linxing, int jianGuo, int renXing)
	{
		this.qinHe = qinHe;
		this.caoKong = caoKong;
		this.linxing = linxing;
		this.jianGuo = jianGuo;
		this.renXing = renXing;
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x00106FC8 File Offset: 0x001051C8
	private void SetLingLi()
	{
		int quality = _ItemJsonData.DataDict[this.Item.Id].quality;
		this.lingLi = jsonData.instance.CaiLiaoNengLiangBiao[quality.ToString()]["value1"].I;
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x0010701C File Offset: 0x0010521C
	private void SetShuXingType()
	{
		this.shuXingTypeID = 0;
		int shuXingType = _ItemJsonData.DataDict[this.Item.Id].ShuXingType;
		if (shuXingType == 0)
		{
			return;
		}
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		if (selectZhongLei <= 0)
		{
			return;
		}
		List<JSONObject> list = jsonData.instance.LianQiHeCheng.list;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["ShuXingType"].I == shuXingType && list[i]["zhonglei"].I == selectZhongLei)
			{
				this.shuXingTypeID = list[i]["id"].I;
				this.attackType = jsonData.instance.LianQiShuXinLeiBie[list[i]["ShuXingType"].ToString()]["AttackType"].I;
				return;
			}
		}
	}

	// Token: 0x040019AD RID: 6573
	public int qinHe;

	// Token: 0x040019AE RID: 6574
	public int caoKong;

	// Token: 0x040019AF RID: 6575
	public int linxing;

	// Token: 0x040019B0 RID: 6576
	public int jianGuo;

	// Token: 0x040019B1 RID: 6577
	public int renXing;

	// Token: 0x040019B2 RID: 6578
	public int lingLi;

	// Token: 0x040019B3 RID: 6579
	public int shuXingTypeID;

	// Token: 0x040019B4 RID: 6580
	public int attackType = -1;
}
