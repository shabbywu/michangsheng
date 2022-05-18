using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;

// Token: 0x0200045C RID: 1116
public class ShowXiaoGuoManager : MonoBehaviour
{
	// Token: 0x06001DE3 RID: 7651 RVA: 0x00018D9C File Offset: 0x00016F9C
	public void init()
	{
		Tools.ClearObj(this.xiaoGuoCell.transform);
		this.xiaoGuoScrollRect.setContentChild(0);
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x00018DBA File Offset: 0x00016FBA
	public bool checkHasOneChiTiao()
	{
		this.updateEquipCiTiao();
		return this.entryDictionary.Keys.Count > 0;
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x00104628 File Offset: 0x00102828
	public void updateEquipCiTiao()
	{
		Tools.ClearObj(this.xiaoGuoCell.transform);
		this.getTotalCiTiao();
		if (this.entryDictionary.Keys.Count != 0)
		{
			foreach (int id in this.entryDictionary.Keys)
			{
				string descBy = this.getDescBy(id);
				if (!(descBy == ""))
				{
					XiaoGuoCell component = Tools.InstantiateGameObject(this.xiaoGuoCell, this.xiaoGuoCell.transform.parent).GetComponent<XiaoGuoCell>();
					component.setDesc(descBy);
					component.ID = id;
					component.status = 1;
				}
			}
		}
		int num = this.entryDictionary.Keys.Count;
		if (this.getLingWenDesc() != "")
		{
			XiaoGuoCell component2 = Tools.InstantiateGameObject(this.xiaoGuoCell, this.xiaoGuoCell.transform.parent).GetComponent<XiaoGuoCell>();
			component2.setDesc(this.getLingWenDesc());
			component2.status = 2;
			component2.linWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
			num++;
		}
		this.xiaoGuoScrollRect.setContentChild(num);
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x0010476C File Offset: 0x0010296C
	public void getTotalCiTiao()
	{
		List<PutMaterialCell> caiLiaoCells = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells;
		this.entryDictionary = new Dictionary<int, int>();
		for (int i = 0; i < caiLiaoCells.Count; i++)
		{
			if (caiLiaoCells[i].shuXingTypeID != 0)
			{
				if (caiLiaoCells[i].shuXingTypeID >= 49 && caiLiaoCells[i].shuXingTypeID <= 56)
				{
					if (this.entryDictionary.ContainsKey(49))
					{
						Dictionary<int, int> dictionary = this.entryDictionary;
						dictionary[49] = dictionary[49] + caiLiaoCells[i].lingLi;
					}
					else
					{
						this.entryDictionary.Add(49, caiLiaoCells[i].lingLi);
					}
				}
				else if (caiLiaoCells[i].shuXingTypeID >= 1 && caiLiaoCells[i].shuXingTypeID <= 8)
				{
					if (this.entryDictionary.ContainsKey(1))
					{
						Dictionary<int, int> dictionary = this.entryDictionary;
						dictionary[1] = dictionary[1] + caiLiaoCells[i].lingLi;
					}
					else
					{
						this.entryDictionary.Add(1, caiLiaoCells[i].lingLi);
					}
				}
				else if (this.entryDictionary.ContainsKey(caiLiaoCells[i].shuXingTypeID))
				{
					Dictionary<int, int> dictionary = this.entryDictionary;
					int shuXingTypeID = caiLiaoCells[i].shuXingTypeID;
					dictionary[shuXingTypeID] += caiLiaoCells[i].lingLi;
				}
				else
				{
					this.entryDictionary.Add(caiLiaoCells[i].shuXingTypeID, caiLiaoCells[i].lingLi);
				}
			}
		}
		if (this.entryDictionary.Keys.Count == 0)
		{
			return;
		}
		float wuWeiBaiFenBi = LianQiTotalManager.inst.putMaterialPageManager.wuWeiManager.getWuWeiBaiFenBi();
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		int num = -1;
		float num2 = -1f;
		Avatar player = Tools.instance.getPlayer();
		bool flag = false;
		if (player.checkHasStudyWuDaoSkillByID(2241))
		{
			flag = true;
		}
		if (selectLingWenID > 0)
		{
			num = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()]["value3"].I;
			num2 = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()]["value4"].n;
		}
		JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		foreach (int num3 in this.entryDictionary.Keys)
		{
			int num4 = this.entryDictionary[num3];
			num4 = (int)((float)num4 * wuWeiBaiFenBi);
			if (num3 == 49)
			{
				if (num4 >= jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I)
				{
					dictionary2.Add(num3, num4);
				}
			}
			else
			{
				int i2 = lianQiHeCheng[num3.ToString()]["cast"].I;
				if (num4 >= i2)
				{
					dictionary2.Add(num3, num4);
				}
			}
		}
		if (dictionary2.Keys.Count == 0)
		{
			this.entryDictionary = dictionary2;
			return;
		}
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		if (selectLingWenID > 0 && num == 2)
		{
			num2 /= (float)dictionary2.Keys.Count;
		}
		foreach (int key in dictionary2.Keys)
		{
			int num5 = dictionary2[key];
			if (selectLingWenID > 0)
			{
				if (num == 1)
				{
					num5 = (int)((float)num5 * num2);
				}
				else
				{
					num5 = (int)((float)num5 + num2);
				}
			}
			if (flag)
			{
				num5 = (int)((float)num5 * 1.5f);
			}
			int i3 = lianQiHeCheng[key.ToString()]["cast"].I;
			int value = num5 / i3;
			dictionary3.Add(key, value);
		}
		this.entryDictionary = dictionary3;
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x00104BB0 File Offset: 0x00102DB0
	public string getDescBy(int id)
	{
		string text = "";
		if (id == 49)
		{
			int duoDuanIDByLingLi = this.getDuoDuanIDByLingLi(this.entryDictionary[id]);
			if (duoDuanIDByLingLi == 0)
			{
				return text;
			}
			return jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()]["desc"].str;
		}
		else
		{
			JSONObject jsonobject = jsonData.instance.LianQiHeCheng[id.ToString()];
			int num = this.entryDictionary[id];
			text += jsonobject["descfirst"].str;
			string text2 = jsonobject["desc"].str;
			if (text2.Contains("(HP)"))
			{
				text2 = text2.Replace("(HP)", (num * jsonobject["HP"].I).ToString());
				return text + text2;
			}
			if (text2.Contains("(listvalue2)"))
			{
				text2 = text2.Replace("(listvalue2)", (jsonobject["listvalue2"][0].I * num).ToString());
				return text + text2;
			}
			if (text2.Contains("(Itemintvalue2)"))
			{
				text2 = text2.Replace("(Itemintvalue2)", (jsonobject["Itemintvalue2"][0].I * num).ToString());
				return text + text2;
			}
			return text;
		}
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00104D20 File Offset: 0x00102F20
	public string getLingWenDesc()
	{
		string result = "";
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		if (selectLingWenID != -1)
		{
			JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()];
			if (jsonobject["type"].I == 3)
			{
				result = Tools.Code64(jsonobject["desc"].str).Replace("获得", "获得<color=#ff624d>") + string.Format("</color>x{0}", jsonobject["value2"].I);
			}
			else
			{
				result = Regex.Split(LianQiTotalManager.inst.buildNomalLingWenDesc(jsonobject), ",", RegexOptions.None)[0];
			}
		}
		return result;
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x00104DE0 File Offset: 0x00102FE0
	public int getDuoDuanIDByLingLi(int sum)
	{
		int result = 0;
		List<JSONObject> list = jsonData.instance.LianQiDuoDuanShangHaiBiao.list;
		int num = 0;
		while (num < list.Count && list[num]["cast"].I <= sum)
		{
			result = list[num]["id"].I;
			num++;
		}
		return result;
	}

	// Token: 0x0400198B RID: 6539
	[SerializeField]
	private GameObject xiaoGuoCell;

	// Token: 0x0400198C RID: 6540
	[SerializeField]
	private MyScrollRect xiaoGuoScrollRect;

	// Token: 0x0400198D RID: 6541
	public Dictionary<int, int> entryDictionary;
}
