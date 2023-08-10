using System.Collections.Generic;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;

public class ShowXiaoGuoManager : MonoBehaviour
{
	[SerializeField]
	private GameObject xiaoGuoCell;

	[SerializeField]
	private MyScrollRect xiaoGuoScrollRect;

	public Dictionary<int, int> entryDictionary;

	public void init()
	{
		Tools.ClearObj(xiaoGuoCell.transform);
		xiaoGuoScrollRect.setContentChild(0);
	}

	public bool checkHasOneChiTiao()
	{
		updateEquipCiTiao();
		return entryDictionary.Keys.Count > 0;
	}

	public void updateEquipCiTiao()
	{
		Tools.ClearObj(xiaoGuoCell.transform);
		getTotalCiTiao();
		if (entryDictionary.Keys.Count != 0)
		{
			foreach (int key in entryDictionary.Keys)
			{
				string descBy = getDescBy(key);
				if (!(descBy == ""))
				{
					XiaoGuoCell component = Tools.InstantiateGameObject(xiaoGuoCell, xiaoGuoCell.transform.parent).GetComponent<XiaoGuoCell>();
					component.setDesc(descBy);
					component.ID = key;
					component.status = 1;
				}
			}
		}
		int num = entryDictionary.Keys.Count;
		if (getLingWenDesc() != "")
		{
			XiaoGuoCell component2 = Tools.InstantiateGameObject(xiaoGuoCell, xiaoGuoCell.transform.parent).GetComponent<XiaoGuoCell>();
			component2.setDesc(getLingWenDesc());
			component2.status = 2;
			component2.linWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
			num++;
		}
		xiaoGuoScrollRect.setContentChild(num);
	}

	public void getTotalCiTiao()
	{
		List<PutMaterialCell> caiLiaoCells = LianQiTotalManager.inst.putMaterialPageManager.lianQiPageManager.putCaiLiaoCell.caiLiaoCells;
		entryDictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < caiLiaoCells.Count; i++)
		{
			if (caiLiaoCells[i].shuXingTypeID == 0)
			{
				continue;
			}
			if (caiLiaoCells[i].shuXingTypeID >= 49 && caiLiaoCells[i].shuXingTypeID <= 56)
			{
				if (entryDictionary.ContainsKey(49))
				{
					entryDictionary[49] += caiLiaoCells[i].lingLi;
					dictionary[49]++;
				}
				else
				{
					entryDictionary.Add(49, caiLiaoCells[i].lingLi);
					dictionary.Add(49, 1);
				}
			}
			else if (caiLiaoCells[i].shuXingTypeID >= 1 && caiLiaoCells[i].shuXingTypeID <= 8)
			{
				if (entryDictionary.ContainsKey(1))
				{
					entryDictionary[1] += caiLiaoCells[i].lingLi;
					dictionary[1]++;
				}
				else
				{
					entryDictionary.Add(1, caiLiaoCells[i].lingLi);
					dictionary.Add(1, 1);
				}
			}
			else if (entryDictionary.ContainsKey(caiLiaoCells[i].shuXingTypeID))
			{
				entryDictionary[caiLiaoCells[i].shuXingTypeID] += caiLiaoCells[i].lingLi;
				dictionary[caiLiaoCells[i].shuXingTypeID]++;
			}
			else
			{
				entryDictionary.Add(caiLiaoCells[i].shuXingTypeID, caiLiaoCells[i].lingLi);
				dictionary.Add(caiLiaoCells[i].shuXingTypeID, 1);
			}
		}
		if (entryDictionary.Keys.Count == 0)
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
		foreach (int key in entryDictionary.Keys)
		{
			int num3 = entryDictionary[key];
			num3 = (int)((float)num3 * wuWeiBaiFenBi);
			if (key == 49)
			{
				if (num3 >= jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I)
				{
					dictionary2.Add(key, num3);
				}
				continue;
			}
			int i2 = lianQiHeCheng[key.ToString()]["cast"].I;
			if (num3 >= i2)
			{
				dictionary2.Add(key, num3);
			}
		}
		if (dictionary2.Keys.Count == 0)
		{
			entryDictionary = dictionary2;
			return;
		}
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		int num4 = 0;
		foreach (int key2 in dictionary2.Keys)
		{
			num4 += dictionary[key2];
		}
		if (selectLingWenID > 0 && num == 2)
		{
			num2 /= (float)num4;
		}
		foreach (int key3 in dictionary2.Keys)
		{
			int num5 = dictionary2[key3];
			if (selectLingWenID > 0)
			{
				if (num == 1)
				{
					num5 = (int)((float)num5 * num2);
				}
				else
				{
					int num6 = (int)num2 * dictionary[key3];
					num5 += num6;
				}
			}
			if (flag)
			{
				num5 = (int)((float)num5 * 1.5f);
			}
			int i3 = lianQiHeCheng[key3.ToString()]["cast"].I;
			int value = num5 / i3;
			dictionary3.Add(key3, value);
		}
		entryDictionary = dictionary3;
	}

	public string getDescBy(int id)
	{
		string text = "";
		if (id == 49)
		{
			int duoDuanIDByLingLi = getDuoDuanIDByLingLi(entryDictionary[id]);
			if (duoDuanIDByLingLi == 0)
			{
				return text;
			}
			return jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()]["desc"].str;
		}
		JSONObject jSONObject = jsonData.instance.LianQiHeCheng[id.ToString()];
		int num = entryDictionary[id];
		text += jSONObject["descfirst"].str;
		string str = jSONObject["desc"].str;
		int num2 = 0;
		if (str.Contains("(HP)"))
		{
			str = str.Replace("(HP)", (num * jSONObject["HP"].I).ToString());
			return text + str;
		}
		if (str.Contains("(listvalue2)"))
		{
			str = str.Replace("(listvalue2)", (jSONObject["listvalue2"][0].I * num).ToString());
			return text + str;
		}
		if (str.Contains("(Itemintvalue2)"))
		{
			str = str.Replace("(Itemintvalue2)", (jSONObject["Itemintvalue2"][0].I * num).ToString());
			return text + str;
		}
		return text;
	}

	public string getLingWenDesc()
	{
		string result = "";
		int selectLingWenID = LianQiTotalManager.inst.putMaterialPageManager.lingWenManager.getSelectLingWenID();
		if (selectLingWenID != -1)
		{
			JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[selectLingWenID.ToString()];
			result = ((jSONObject["type"].I != 3) ? Regex.Split(LianQiTotalManager.inst.buildNomalLingWenDesc(jSONObject), ",", RegexOptions.None)[0] : (Tools.Code64(jSONObject["desc"].str).Replace("获得", "获得<color=#ff624d>") + string.Format("</color>x{0}", jSONObject["value2"].I)));
		}
		return result;
	}

	public int getDuoDuanIDByLingLi(int sum)
	{
		int result = 0;
		List<JSONObject> list = jsonData.instance.LianQiDuoDuanShangHaiBiao.list;
		for (int i = 0; i < list.Count && list[i]["cast"].I <= sum; i++)
		{
			result = list[i]["id"].I;
		}
		return result;
	}
}
