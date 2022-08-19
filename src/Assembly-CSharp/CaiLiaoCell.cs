using System;
using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class CaiLiaoCell : ItemCellEX
{
	// Token: 0x06001A7E RID: 6782 RVA: 0x000BCB90 File Offset: 0x000BAD90
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

	// Token: 0x06001A7F RID: 6783 RVA: 0x000BCC4C File Offset: 0x000BAE4C
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

	// Token: 0x06001A80 RID: 6784 RVA: 0x000BCCB2 File Offset: 0x000BAEB2
	private void setWuWei(int qinHe, int caoKong, int linxing, int jianGuo, int renXing)
	{
		this.qinHe = qinHe;
		this.caoKong = caoKong;
		this.linxing = linxing;
		this.jianGuo = jianGuo;
		this.renXing = renXing;
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x000BCCDC File Offset: 0x000BAEDC
	private void setLingLi()
	{
		int i = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["quality"].I;
		this.lingLi = jsonData.instance.CaiLiaoNengLiangBiao[i.ToString()]["value1"].I;
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x000BCD44 File Offset: 0x000BAF44
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

	// Token: 0x06001A83 RID: 6787 RVA: 0x00004095 File Offset: 0x00002295
	public override void PCOnPress()
	{
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x000BCE50 File Offset: 0x000BB050
	public override void PCOnHover(bool isOver)
	{
		if (LianQiTotalManager.inst.lianQiResultManager.lianQiResultPanelIsOpen)
		{
			return;
		}
		base.PCOnHover(isOver);
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000BCE6B File Offset: 0x000BB06B
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

	// Token: 0x06001A86 RID: 6790 RVA: 0x000BCEA8 File Offset: 0x000BB0A8
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

	// Token: 0x0400154B RID: 5451
	public int qinHe;

	// Token: 0x0400154C RID: 5452
	public int caoKong;

	// Token: 0x0400154D RID: 5453
	public int linxing;

	// Token: 0x0400154E RID: 5454
	public int jianGuo;

	// Token: 0x0400154F RID: 5455
	public int renXing;

	// Token: 0x04001550 RID: 5456
	public int lingLi;

	// Token: 0x04001551 RID: 5457
	public int shuXingTypeID;

	// Token: 0x04001552 RID: 5458
	public int attackType = -1;
}
