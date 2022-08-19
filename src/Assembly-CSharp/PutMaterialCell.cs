using System;
using System.Collections.Generic;
using JSONClass;
using LianQi;

// Token: 0x02000309 RID: 777
public class PutMaterialCell : LianQiSlot
{
	// Token: 0x06001B17 RID: 6935 RVA: 0x000C16BE File Offset: 0x000BF8BE
	public override void SetSlotData(object data)
	{
		this.UpdateItem();
		base.SetSlotData(data);
		this.SetShuXing();
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x000C16D3 File Offset: 0x000BF8D3
	public override void SetNull()
	{
		base.SetNull();
		this.UpdateItem();
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x000C16E4 File Offset: 0x000BF8E4
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

	// Token: 0x06001B1A RID: 6938 RVA: 0x000C1792 File Offset: 0x000BF992
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

	// Token: 0x06001B1B RID: 6939 RVA: 0x000C17CC File Offset: 0x000BF9CC
	private void SetWuWei(int qinHe, int caoKong, int linxing, int jianGuo, int renXing)
	{
		this.qinHe = qinHe;
		this.caoKong = caoKong;
		this.linxing = linxing;
		this.jianGuo = jianGuo;
		this.renXing = renXing;
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x000C17F4 File Offset: 0x000BF9F4
	private void SetLingLi()
	{
		int quality = _ItemJsonData.DataDict[this.Item.Id].quality;
		this.lingLi = jsonData.instance.CaiLiaoNengLiangBiao[quality.ToString()]["value1"].I;
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x000C1848 File Offset: 0x000BFA48
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

	// Token: 0x040015A0 RID: 5536
	public int qinHe;

	// Token: 0x040015A1 RID: 5537
	public int caoKong;

	// Token: 0x040015A2 RID: 5538
	public int linxing;

	// Token: 0x040015A3 RID: 5539
	public int jianGuo;

	// Token: 0x040015A4 RID: 5540
	public int renXing;

	// Token: 0x040015A5 RID: 5541
	public int lingLi;

	// Token: 0x040015A6 RID: 5542
	public int shuXingTypeID;

	// Token: 0x040015A7 RID: 5543
	public int attackType = -1;
}
