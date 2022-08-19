using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class ShowXiaoGuoManager : MonoBehaviour
{
	// Token: 0x06001ABD RID: 6845 RVA: 0x000BE807 File Offset: 0x000BCA07
	public void init()
	{
		Tools.ClearObj(this.xiaoGuoCell.transform);
		this.xiaoGuoScrollRect.setContentChild(0);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x000BE825 File Offset: 0x000BCA25
	public bool checkHasOneChiTiao()
	{
		this.updateEquipCiTiao();
		return this.entryDictionary.Keys.Count > 0;
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x000BE840 File Offset: 0x000BCA40
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

	// Token: 0x06001AC0 RID: 6848 RVA: 0x000BE984 File Offset: 0x000BCB84
	public void getTotalCiTiao()
	{
		List<PutMaterialCell> caiLiaoCells = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells;
		this.entryDictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < caiLiaoCells.Count; i++)
		{
			if (caiLiaoCells[i].shuXingTypeID != 0)
			{
				if (caiLiaoCells[i].shuXingTypeID >= 49 && caiLiaoCells[i].shuXingTypeID <= 56)
				{
					if (this.entryDictionary.ContainsKey(49))
					{
						Dictionary<int, int> dictionary2 = this.entryDictionary;
						dictionary2[49] = dictionary2[49] + caiLiaoCells[i].lingLi;
						dictionary2 = dictionary;
						dictionary2[49] = dictionary2[49] + 1;
					}
					else
					{
						this.entryDictionary.Add(49, caiLiaoCells[i].lingLi);
						dictionary.Add(49, 1);
					}
				}
				else if (caiLiaoCells[i].shuXingTypeID >= 1 && caiLiaoCells[i].shuXingTypeID <= 8)
				{
					if (this.entryDictionary.ContainsKey(1))
					{
						Dictionary<int, int> dictionary2 = this.entryDictionary;
						dictionary2[1] = dictionary2[1] + caiLiaoCells[i].lingLi;
						dictionary2 = dictionary;
						dictionary2[1] = dictionary2[1] + 1;
					}
					else
					{
						this.entryDictionary.Add(1, caiLiaoCells[i].lingLi);
						dictionary.Add(1, 1);
					}
				}
				else if (this.entryDictionary.ContainsKey(caiLiaoCells[i].shuXingTypeID))
				{
					Dictionary<int, int> dictionary2 = this.entryDictionary;
					int shuXingTypeID = caiLiaoCells[i].shuXingTypeID;
					dictionary2[shuXingTypeID] += caiLiaoCells[i].lingLi;
					dictionary2 = dictionary;
					shuXingTypeID = caiLiaoCells[i].shuXingTypeID;
					dictionary2[shuXingTypeID]++;
				}
				else
				{
					this.entryDictionary.Add(caiLiaoCells[i].shuXingTypeID, caiLiaoCells[i].lingLi);
					dictionary.Add(caiLiaoCells[i].shuXingTypeID, 1);
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
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		foreach (int num3 in this.entryDictionary.Keys)
		{
			int num4 = this.entryDictionary[num3];
			num4 = (int)((float)num4 * wuWeiBaiFenBi);
			if (num3 == 49)
			{
				if (num4 >= jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I)
				{
					dictionary3.Add(num3, num4);
				}
			}
			else
			{
				int i2 = lianQiHeCheng[num3.ToString()]["cast"].I;
				if (num4 >= i2)
				{
					dictionary3.Add(num3, num4);
				}
			}
		}
		if (dictionary3.Keys.Count == 0)
		{
			this.entryDictionary = dictionary3;
			return;
		}
		Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
		int num5 = 0;
		foreach (int key in dictionary3.Keys)
		{
			num5 += dictionary[key];
		}
		if (selectLingWenID > 0 && num == 2)
		{
			num2 /= (float)num5;
		}
		foreach (int key2 in dictionary3.Keys)
		{
			int num6 = dictionary3[key2];
			if (selectLingWenID > 0)
			{
				if (num == 1)
				{
					num6 = (int)((float)num6 * num2);
				}
				else
				{
					int num7 = (int)num2 * dictionary[key2];
					num6 += num7;
				}
			}
			if (flag)
			{
				num6 = (int)((float)num6 * 1.5f);
			}
			int i3 = lianQiHeCheng[key2.ToString()]["cast"].I;
			int value = num6 / i3;
			dictionary4.Add(key2, value);
		}
		this.entryDictionary = dictionary4;
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x000BEEA4 File Offset: 0x000BD0A4
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

	// Token: 0x06001AC2 RID: 6850 RVA: 0x000BF014 File Offset: 0x000BD214
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

	// Token: 0x06001AC3 RID: 6851 RVA: 0x000BF0D4 File Offset: 0x000BD2D4
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

	// Token: 0x0400157E RID: 5502
	[SerializeField]
	private GameObject xiaoGuoCell;

	// Token: 0x0400157F RID: 5503
	[SerializeField]
	private MyScrollRect xiaoGuoScrollRect;

	// Token: 0x04001580 RID: 5504
	public Dictionary<int, int> entryDictionary;
}
