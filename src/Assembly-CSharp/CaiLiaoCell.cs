using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

public class CaiLiaoCell : ItemCellEX
{
	public int qinHe;

	public int caoKong;

	public int linxing;

	public int jianGuo;

	public int renXing;

	public int lingLi;

	public int shuXingTypeID;

	public int attackType = -1;

	public void setShuXing()
	{
		int i = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["WuWeiType"].I;
		setShuXingType();
		if (i != 0)
		{
			JSONObject jSONObject = jsonData.instance.LianQiWuWeiBiao[i.ToString()];
			setWuWei(jSONObject["value1"].I, jSONObject["value2"].I, jSONObject["value3"].I, jSONObject["value4"].I, jSONObject["value5"].I);
			setLingLi();
		}
	}

	public void updateItem()
	{
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		qinHe = 0;
		caoKong = 0;
		linxing = 0;
		jianGuo = 0;
		renXing = 0;
		lingLi = 0;
		shuXingTypeID = 0;
		attackType = -1;
	}

	private void setWuWei(int qinHe, int caoKong, int linxing, int jianGuo, int renXing)
	{
		this.qinHe = qinHe;
		this.caoKong = caoKong;
		this.linxing = linxing;
		this.jianGuo = jianGuo;
		this.renXing = renXing;
	}

	private void setLingLi()
	{
		int i = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["quality"].I;
		lingLi = jsonData.instance.CaiLiaoNengLiangBiao[i.ToString()]["value1"].I;
	}

	private void setShuXingType()
	{
		shuXingTypeID = 0;
		int i = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["ShuXingType"].I;
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
				shuXingTypeID = list[j]["id"].I;
				attackType = jsonData.instance.LianQiShuXinLeiBie[list[j]["ShuXingType"].ToString()]["AttackType"].I;
				break;
			}
		}
	}

	public override void PCOnPress()
	{
	}

	public override void PCOnHover(bool isOver)
	{
		if (!LianQiTotalManager.inst.lianQiResultManager.lianQiResultPanelIsOpen)
		{
			base.PCOnHover(isOver);
		}
	}

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
		if (!Input.GetMouseButtonDown(1))
		{
			return Input.GetMouseButtonDown(0);
		}
		return true;
	}

	private new void Update()
	{
		base.Update();
		if (Item.itemID != -1)
		{
			string str = jsonData.instance.ItemJsonData[Item.itemID.ToString()]["name"].str;
			KeyObject.SetActive(true);
			KeyName.text = Tools.Code64(str);
		}
		else
		{
			KeyObject.SetActive(false);
		}
	}
}
