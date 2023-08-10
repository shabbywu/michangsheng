using System.Collections.Generic;
using JSONClass;
using LianQi;

public class PutMaterialCell : LianQiSlot
{
	public int qinHe;

	public int caoKong;

	public int linxing;

	public int jianGuo;

	public int renXing;

	public int lingLi;

	public int shuXingTypeID;

	public int attackType = -1;

	public override void SetSlotData(object data)
	{
		UpdateItem();
		base.SetSlotData(data);
		SetShuXing();
	}

	public override void SetNull()
	{
		base.SetNull();
		UpdateItem();
	}

	public void SetShuXing()
	{
		if (!IsNull())
		{
			int wuWeiType = _ItemJsonData.DataDict[Item.Id].WuWeiType;
			SetShuXingType();
			if (wuWeiType != 0)
			{
				JSONObject jSONObject = jsonData.instance.LianQiWuWeiBiao[wuWeiType.ToString()];
				SetWuWei(jSONObject["value1"].I, jSONObject["value2"].I, jSONObject["value3"].I, jSONObject["value4"].I, jSONObject["value5"].I);
				SetLingLi();
			}
		}
	}

	public void UpdateItem()
	{
		qinHe = 0;
		caoKong = 0;
		linxing = 0;
		jianGuo = 0;
		renXing = 0;
		lingLi = 0;
		shuXingTypeID = 0;
		attackType = -1;
	}

	private void SetWuWei(int qinHe, int caoKong, int linxing, int jianGuo, int renXing)
	{
		this.qinHe = qinHe;
		this.caoKong = caoKong;
		this.linxing = linxing;
		this.jianGuo = jianGuo;
		this.renXing = renXing;
	}

	private void SetLingLi()
	{
		int quality = _ItemJsonData.DataDict[Item.Id].quality;
		lingLi = jsonData.instance.CaiLiaoNengLiangBiao[quality.ToString()]["value1"].I;
	}

	private void SetShuXingType()
	{
		shuXingTypeID = 0;
		int shuXingType = _ItemJsonData.DataDict[Item.Id].ShuXingType;
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
				shuXingTypeID = list[i]["id"].I;
				attackType = jsonData.instance.LianQiShuXinLeiBie[list[i]["ShuXingType"].ToString()]["AttackType"].I;
				break;
			}
		}
	}
}
