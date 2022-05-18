using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class CaiLiaoCell : ItemCellEX
{
	// Token: 0x06001DA4 RID: 7588 RVA: 0x00102CFC File Offset: 0x00100EFC
	public void setShuXing()
	{
		int i = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["WuWeiType"].I;
		this.setShuXingType();
		if (i == 0)
		{
			return;
		}
		JSONObject jsonobject = jsonData.instance.LianQiWuWeiBiao[i.ToString()];
		this.setWuWei(jsonobject["value1"].I, jsonobject["value2"].I, jsonobject["value3"].I, jsonobject["value4"].I, jsonobject["value5"].I);
		this.setLingLi();
	}

	// Token: 0x06001DA5 RID: 7589 RVA: 0x00102DB8 File Offset: 0x00100FB8
	public void updateItem()
	{
		this.Item = this.inventory.inventory[int.Parse(base.name)];
		this.qinHe = 0;
		this.caoKong = 0;
		this.linxing = 0;
		this.jianGuo = 0;
		this.renXing = 0;
		this.lingLi = 0;
		this.shuXingTypeID = 0;
		this.attackType = -1;
	}

	// Token: 0x06001DA6 RID: 7590 RVA: 0x00018ACF File Offset: 0x00016CCF
	private void setWuWei(int qinHe, int caoKong, int linxing, int jianGuo, int renXing)
	{
		this.qinHe = qinHe;
		this.caoKong = caoKong;
		this.linxing = linxing;
		this.jianGuo = jianGuo;
		this.renXing = renXing;
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x00102E20 File Offset: 0x00101020
	private void setLingLi()
	{
		int i = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["quality"].I;
		this.lingLi = jsonData.instance.CaiLiaoNengLiangBiao[i.ToString()]["value1"].I;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00102E88 File Offset: 0x00101088
	private void setShuXingType()
	{
		this.shuXingTypeID = 0;
		int i = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["ShuXingType"].I;
		if (i == 0)
		{
			return;
		}
		int selectZhongLei = LianQiTotalManager.inst.selectTypePageManager.getSelectZhongLei();
		if (selectZhongLei <= 0)
		{
			return;
		}
		List<JSONObject> list = jsonData.instance.LianQiHeCheng.list;
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j]["ShuXingType"].I == i && list[j]["zhonglei"].I == selectZhongLei)
			{
				this.shuXingTypeID = list[j]["id"].I;
				this.attackType = jsonData.instance.LianQiShuXinLeiBie[list[j]["ShuXingType"].ToString()]["AttackType"].I;
				return;
			}
		}
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x000042DD File Offset: 0x000024DD
	public override void PCOnPress()
	{
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x00018AF6 File Offset: 0x00016CF6
	public override void PCOnHover(bool isOver)
	{
		if (LianQiTotalManager.inst.lianQiResultManager.lianQiResultPanelIsOpen)
		{
			return;
		}
		base.PCOnHover(isOver);
	}

	// Token: 0x06001DAB RID: 7595 RVA: 0x00018B11 File Offset: 0x00016D11
	private bool isCanClick(ref int type)
	{
		if (LianQiTotalManager.inst.lianQiResultManager.lianQiResultPanelIsOpen)
		{
			return false;
		}
		if (Input.GetMouseButtonDown(1))
		{
			type = 1;
		}
		else if (Input.GetMouseButtonDown(0))
		{
			type = 0;
		}
		return Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0);
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x0010081C File Offset: 0x000FEA1C
	private new void Update()
	{
		base.Update();
		if (this.Item.itemID != -1)
		{
			string str = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["name"].str;
			this.KeyObject.SetActive(true);
			this.KeyName.text = Tools.Code64(str);
			return;
		}
		this.KeyObject.SetActive(false);
	}

	// Token: 0x04001958 RID: 6488
	public int qinHe;

	// Token: 0x04001959 RID: 6489
	public int caoKong;

	// Token: 0x0400195A RID: 6490
	public int linxing;

	// Token: 0x0400195B RID: 6491
	public int jianGuo;

	// Token: 0x0400195C RID: 6492
	public int renXing;

	// Token: 0x0400195D RID: 6493
	public int lingLi;

	// Token: 0x0400195E RID: 6494
	public int shuXingTypeID;

	// Token: 0x0400195F RID: 6495
	public int attackType = -1;
}
