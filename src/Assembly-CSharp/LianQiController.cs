using System.Collections.Generic;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class LianQiController : MonoBehaviour
{
	public enum ZhuangBeiType
	{
		空,
		武器,
		防具,
		饰品
	}

	[SerializeField]
	private List<ItemCellEX> CaiLiao = new List<ItemCellEX>();

	private ZhuangBeiType selectZhuangBeiType;

	private int curSelectEquipMuBanID = -1;

	private int curJinDu;

	public int ZhuangBeiIndexID = -1;

	public string ItemName = "";

	public int QingHe => getQingHe();

	public int CaoKong => getCaoKong();

	public int LingXing => getLingXing();

	public int JianGu => getJianGu();

	public int RenXing => getRenXing();

	public int NengLiang => getNengLiang();

	public List<int> ShuXing => getShuXing();

	public JToken getNowQualityItemJson()
	{
		int nengLiang = NengLiang;
		JObject lianQiWuQiQuality = jsonData.instance.LianQiWuQiQuality;
		JToken result = null;
		foreach (KeyValuePair<string, JToken> item in lianQiWuQiQuality)
		{
			if (nengLiang >= (int)item.Value[(object)"power"])
			{
				result = item.Value;
			}
		}
		return result;
	}

	public void setCurSelectEquipMuBanID(int itemID)
	{
		curSelectEquipMuBanID = itemID;
	}

	public int getCurSelectEquipMuBanID()
	{
		return curSelectEquipMuBanID;
	}

	public void setCurJinDu(int jindu)
	{
		curJinDu = jindu;
	}

	public int getCurJinDu()
	{
		return curJinDu;
	}

	private int getQingHe()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				num2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[num2.ToString()]["value1"].I;
			}
		}
		return num;
	}

	private int getCaoKong()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				num2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[num2.ToString()]["value2"].I;
			}
		}
		return num;
	}

	private int getLingXing()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				num2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[num2.ToString()]["value3"].I;
			}
		}
		return num;
	}

	private int getJianGu()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				num2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[num2.ToString()]["value4"].I;
			}
		}
		return num;
	}

	private int getRenXing()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				num2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[num2.ToString()]["value5"].I;
			}
		}
		return num;
	}

	private int getNengLiang()
	{
		int num = 0;
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				num += GetItemNengLiang(CaiLiao[i].GetItem.itemID);
			}
		}
		return num;
	}

	private int GetItemNengLiang(int ItemID)
	{
		int i = jsonData.instance.ItemJsonData[ItemID.ToString()]["quality"].I;
		return jsonData.instance.CaiLiaoNengLiangBiao[i.ToString()]["value1"].I;
	}

	public bool WhetherSuccess()
	{
		if (QingHe == 0 || CaoKong == 0 || LingXing == 0 || JianGu == 0 || RenXing == 0)
		{
			return false;
		}
		return true;
	}

	private List<int> getShuXing()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["ShuXingType"].I;
				list.Add(i2);
			}
		}
		return list;
	}

	public void LiQiSuccess()
	{
		CreateEpuieItem();
	}

	private string GetItemDesc(int quality, int _typepingji)
	{
		string text = "";
		Avatar player = Tools.instance.getPlayer();
		text = text + player.firstName + player.lastName;
		text = text + "于" + player.worldTimeMag.getNowTime().Year + "年";
		string text2 = "";
		for (int i = 0; i < 4; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				text2 = text2 + jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["name"].str + ((i != 3) ? "、" : "");
			}
		}
		text = text + "以五行相合的手法将" + text2 + "等材料炼制的" + Tools.getStr("shangzhongxia" + _typepingji) + Tools.getStr("EquipPingji" + quality);
		text = text + "此" + ((object)jsonData.instance.LianQiEquipType[ZhuangBeiIndexID.ToString()][(object)"desc"]).ToString();
		return text + "铭刻XXX符文";
	}

	private string GetItemName()
	{
		return ItemName;
	}

	private int GetItemQuality(ref int quality, ref int shangzhongxia)
	{
		JToken nowQualityItemJson = getNowQualityItemJson();
		if (nowQualityItemJson != null)
		{
			quality = (int)nowQualityItemJson[(object)"quality"];
			shangzhongxia = (int)nowQualityItemJson[(object)"shangxia"];
			return (int)nowQualityItemJson[(object)"quality"];
		}
		return -1;
	}

	private int GetItemMoney()
	{
		JToken nowQualityItemJson = getNowQualityItemJson();
		if (nowQualityItemJson != null)
		{
			return (int)nowQualityItemJson[(object)"price"];
		}
		return -1;
	}

	public int GetItemCD()
	{
		return 1;
	}

	private JSONObject GetItemSkillSeid(JSONObject temp, JSONObject ItemSeidTemp, ref int Damage, ref string seidDesc)
	{
		Dictionary<string, Dictionary<string, int>> dictionary = new Dictionary<string, Dictionary<string, int>>();
		temp.Add(AddItemSeid(29, GetItemCD()));
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		for (int i = 0; i < CaiLiao.Count; i++)
		{
			if (CaiLiao[i].GetItem.itemID != -1)
			{
				int itemNengLiang = GetItemNengLiang(CaiLiao[i].GetItem.itemID);
				int i2 = jsonData.instance.ItemJsonData[CaiLiao[i].GetItem.itemID.ToString()]["ShuXingType"].I;
				if (dictionary2.ContainsKey(i2))
				{
					dictionary2[i2] += itemNengLiang;
				}
				else
				{
					dictionary2[i2] = itemNengLiang;
				}
			}
		}
		int num = 0;
		foreach (KeyValuePair<int, int> item in dictionary2)
		{
			num += item.Value;
		}
		if (num == 0)
		{
			return temp;
		}
		foreach (JSONObject _ajson in jsonData.instance.LianQiHeCheng.list)
		{
			foreach (KeyValuePair<int, int> item2 in dictionary2)
			{
				if (_ajson["ShuXingType"].I != item2.Key || _ajson["zhonglei"].I != ZhuangBeiIndexID || item2.Value / _ajson["cast"].I <= 0)
				{
					continue;
				}
				int num2 = item2.Value / _ajson["cast"].I;
				if (_ajson["seid"].I != 0)
				{
					JSONObject jSONObject = new JSONObject();
					jSONObject.SetField("id", _ajson["seid"].I);
					for (int j = 1; j < 3; j++)
					{
						int num3 = ((!_ajson["fanbei"].HasItem(j)) ? 1 : num2);
						if (_ajson.HasField("intvalue" + j) && _ajson["intvalue" + j].I != 0)
						{
							jSONObject.SetField("value" + j, _ajson["intvalue" + j].I * num3);
						}
					}
					for (int k = 1; k < 3; k++)
					{
						if (!_ajson.HasField("listvalue" + k) || _ajson["listvalue" + k].list.Count == 0)
						{
							continue;
						}
						int num4 = ((!_ajson["fanbei"].HasItem(k)) ? 1 : num2);
						JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
						foreach (JSONObject item3 in _ajson["listvalue" + k].list)
						{
							jSONObject2.Add(item3.I * num4);
						}
						jSONObject.SetField("value" + k, jSONObject2);
					}
					temp.Add(jSONObject);
				}
				if (_ajson["Itemseid"].I != 0 && ItemSeidTemp.list.Find((JSONObject aa) => aa["id"].I == _ajson["Itemseid"].I && aa["value1"].I == _ajson["Itemintvalue1"].I) == null)
				{
					JSONObject jSONObject3 = new JSONObject();
					int num5 = ((!_ajson["itemfanbei"].HasItem(1)) ? 1 : num2);
					jSONObject3.SetField("id", _ajson["Itemseid"].I);
					jSONObject3.SetField("value1", _ajson["Itemintvalue1"].I * num5);
					ItemSeidTemp.Add(jSONObject3);
				}
				if (!dictionary.ContainsKey(_ajson["descfirst"].str))
				{
					dictionary[_ajson["descfirst"].str] = new Dictionary<string, int>();
				}
				_ajson["desc"].str.Replace("(attack)", num2.ToString());
				int num6 = 0;
				if (_ajson["desc"].str.Contains("(HP)"))
				{
					num6 = _ajson["HP"].I * num2;
				}
				else if (_ajson["desc"].str.Contains("(listvalue2)"))
				{
					num6 = _ajson["listvalue2"][0].I * num2;
				}
				if (dictionary[_ajson["descfirst"].str].ContainsKey(_ajson["desc"].str))
				{
					dictionary[_ajson["descfirst"].str][_ajson["desc"].str] += num6;
				}
				else
				{
					if (!dictionary[_ajson["descfirst"].str].ContainsKey(_ajson["desc"].str))
					{
						dictionary[_ajson["descfirst"].str] = new Dictionary<string, int>();
					}
					dictionary[_ajson["descfirst"].str][_ajson["desc"].str] = num6;
				}
				Damage += _ajson["HP"].I * num2;
			}
		}
		foreach (KeyValuePair<string, Dictionary<string, int>> item4 in dictionary)
		{
			seidDesc += item4.Key;
			foreach (KeyValuePair<string, int> item5 in item4.Value)
			{
				string text = item5.Key;
				foreach (Match item6 in Regex.Matches(text, "\\(\\w*\\)"))
				{
					text = text.Replace(item6.Value, item5.Value.ToString());
				}
				seidDesc += text;
			}
		}
		return temp;
	}

	private JSONObject AddItemSeid(int seid, JSONObject value1 = null, JSONObject value2 = null)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("id", seid);
		if (value1 != null)
		{
			jSONObject.SetField("value1", value1);
		}
		if (value2 != null)
		{
			jSONObject.SetField("value2", value2);
		}
		return jSONObject;
	}

	private JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("id", seid);
		if (value1 != -9999)
		{
			jSONObject.SetField("value1", value1);
		}
		if (value2 != -9999)
		{
			jSONObject.SetField("value2", value2);
		}
		return jSONObject;
	}

	private JSONObject GetJsonObjectArray(List<int> value1)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		foreach (int item in value1)
		{
			jSONObject.Add(item);
		}
		return jSONObject;
	}

	private JSONObject GetItemAttackType()
	{
		return new JSONObject(JSONObject.Type.ARRAY);
	}

	public void CreateEpuieItem()
	{
		Avatar player = Tools.instance.getPlayer();
		int itemID = (int)jsonData.instance.LianQiEquipType[ZhuangBeiIndexID.ToString()][(object)"ItemID"];
		int Damage = 0;
		int quality = 0;
		int shangzhongxia = 0;
		string seidDesc = "";
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jSONObject3 = Tools.CreateItemSeid(itemID);
		GetItemSkillSeid(jSONObject, jSONObject2, ref Damage, ref seidDesc);
		GetItemQuality(ref quality, ref shangzhongxia);
		jSONObject3.AddField("SkillSeids", jSONObject);
		if (jSONObject2.list.Count > 0)
		{
			jSONObject3.AddField("ItemSeids", jSONObject2);
		}
		jSONObject3.AddField("Name", GetItemName());
		jSONObject3.AddField("SeidDesc", seidDesc);
		jSONObject3.AddField("Desc", GetItemDesc(quality, shangzhongxia));
		if (Damage > 0)
		{
			jSONObject3.AddField("Damage", Damage);
		}
		jSONObject3.AddField("quality", quality);
		jSONObject3.AddField("QPingZhi", shangzhongxia);
		jSONObject3.AddField("AttackType", GetItemAttackType());
		jSONObject3.AddField("Money", GetItemMoney());
		player.addItem(itemID, 1, jSONObject3);
	}

	public void setSelectZhuangBeiType(int type)
	{
		selectZhuangBeiType = (ZhuangBeiType)type;
	}

	public int getCurSelectZhuangBeiType()
	{
		return (int)selectZhuangBeiType;
	}
}
