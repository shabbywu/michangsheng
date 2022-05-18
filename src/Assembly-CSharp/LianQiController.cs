using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200044C RID: 1100
public class LianQiController : MonoBehaviour
{
	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06001D4D RID: 7501 RVA: 0x000186A4 File Offset: 0x000168A4
	public int QingHe
	{
		get
		{
			return this.getQingHe();
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06001D4E RID: 7502 RVA: 0x000186AC File Offset: 0x000168AC
	public int CaoKong
	{
		get
		{
			return this.getCaoKong();
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06001D4F RID: 7503 RVA: 0x000186B4 File Offset: 0x000168B4
	public int LingXing
	{
		get
		{
			return this.getLingXing();
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06001D50 RID: 7504 RVA: 0x000186BC File Offset: 0x000168BC
	public int JianGu
	{
		get
		{
			return this.getJianGu();
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06001D51 RID: 7505 RVA: 0x000186C4 File Offset: 0x000168C4
	public int RenXing
	{
		get
		{
			return this.getRenXing();
		}
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06001D52 RID: 7506 RVA: 0x000186CC File Offset: 0x000168CC
	public int NengLiang
	{
		get
		{
			return this.getNengLiang();
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06001D53 RID: 7507 RVA: 0x000186D4 File Offset: 0x000168D4
	public List<int> ShuXing
	{
		get
		{
			return this.getShuXing();
		}
	}

	// Token: 0x06001D54 RID: 7508 RVA: 0x00101214 File Offset: 0x000FF414
	public JToken getNowQualityItemJson()
	{
		int nengLiang = this.NengLiang;
		JObject lianQiWuQiQuality = jsonData.instance.LianQiWuQiQuality;
		JToken result = null;
		foreach (KeyValuePair<string, JToken> keyValuePair in lianQiWuQiQuality)
		{
			if (nengLiang >= (int)keyValuePair.Value["power"])
			{
				result = keyValuePair.Value;
			}
		}
		return result;
	}

	// Token: 0x06001D55 RID: 7509 RVA: 0x000186DC File Offset: 0x000168DC
	public void setCurSelectEquipMuBanID(int itemID)
	{
		this.curSelectEquipMuBanID = itemID;
	}

	// Token: 0x06001D56 RID: 7510 RVA: 0x000186E5 File Offset: 0x000168E5
	public int getCurSelectEquipMuBanID()
	{
		return this.curSelectEquipMuBanID;
	}

	// Token: 0x06001D57 RID: 7511 RVA: 0x000186ED File Offset: 0x000168ED
	public void setCurJinDu(int jindu)
	{
		this.curJinDu = jindu;
	}

	// Token: 0x06001D58 RID: 7512 RVA: 0x000186F6 File Offset: 0x000168F6
	public int getCurJinDu()
	{
		return this.curJinDu;
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x0010128C File Offset: 0x000FF48C
	private int getQingHe()
	{
		int num = 0;
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[i2.ToString()]["value1"].I;
			}
		}
		return num;
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x00101334 File Offset: 0x000FF534
	private int getCaoKong()
	{
		int num = 0;
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[i2.ToString()]["value2"].I;
			}
		}
		return num;
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x001013DC File Offset: 0x000FF5DC
	private int getLingXing()
	{
		int num = 0;
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[i2.ToString()]["value3"].I;
			}
		}
		return num;
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x00101484 File Offset: 0x000FF684
	private int getJianGu()
	{
		int num = 0;
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[i2.ToString()]["value4"].I;
			}
		}
		return num;
	}

	// Token: 0x06001D5D RID: 7517 RVA: 0x0010152C File Offset: 0x000FF72C
	private int getRenXing()
	{
		int num = 0;
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["WuWeiType"].I;
				num += jsonData.instance.LianQiWuWeiBiao[i2.ToString()]["value5"].I;
			}
		}
		return num;
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x001015D4 File Offset: 0x000FF7D4
	private int getNengLiang()
	{
		int num = 0;
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				num += this.GetItemNengLiang(this.CaiLiao[i].GetItem.itemID);
			}
		}
		return num;
	}

	// Token: 0x06001D5F RID: 7519 RVA: 0x00101634 File Offset: 0x000FF834
	private int GetItemNengLiang(int ItemID)
	{
		int i = jsonData.instance.ItemJsonData[ItemID.ToString()]["quality"].I;
		return jsonData.instance.CaiLiaoNengLiangBiao[i.ToString()]["value1"].I;
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x000186FE File Offset: 0x000168FE
	public bool WhetherSuccess()
	{
		return this.QingHe != 0 && this.CaoKong != 0 && this.LingXing != 0 && this.JianGu != 0 && this.RenXing != 0;
	}

	// Token: 0x06001D61 RID: 7521 RVA: 0x0010168C File Offset: 0x000FF88C
	private List<int> getShuXing()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["ShuXingType"].I;
				list.Add(i2);
			}
		}
		return list;
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x0001872B File Offset: 0x0001692B
	public void LiQiSuccess()
	{
		this.CreateEpuieItem();
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x00101710 File Offset: 0x000FF910
	private string GetItemDesc(int quality, int _typepingji)
	{
		string text = "";
		Avatar player = Tools.instance.getPlayer();
		text = text + player.firstName + player.lastName;
		text = string.Concat(new object[]
		{
			text,
			"于",
			player.worldTimeMag.getNowTime().Year,
			"年"
		});
		string text2 = "";
		for (int i = 0; i < 4; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				text2 = text2 + jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["name"].str + ((i != 3) ? "、" : "");
			}
		}
		text = string.Concat(new string[]
		{
			text,
			"以五行相合的手法将",
			text2,
			"等材料炼制的",
			Tools.getStr("shangzhongxia" + _typepingji),
			Tools.getStr("EquipPingji" + quality)
		});
		text = text + "此" + jsonData.instance.LianQiEquipType[this.ZhuangBeiIndexID.ToString()]["desc"].ToString();
		return text + "铭刻XXX符文";
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x00018733 File Offset: 0x00016933
	private string GetItemName()
	{
		return this.ItemName;
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x00101894 File Offset: 0x000FFA94
	private int GetItemQuality(ref int quality, ref int shangzhongxia)
	{
		JToken nowQualityItemJson = this.getNowQualityItemJson();
		if (nowQualityItemJson != null)
		{
			quality = (int)nowQualityItemJson["quality"];
			shangzhongxia = (int)nowQualityItemJson["shangxia"];
			return (int)nowQualityItemJson["quality"];
		}
		return -1;
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x001018E4 File Offset: 0x000FFAE4
	private int GetItemMoney()
	{
		JToken nowQualityItemJson = this.getNowQualityItemJson();
		if (nowQualityItemJson != null)
		{
			return (int)nowQualityItemJson["price"];
		}
		return -1;
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x0000A093 File Offset: 0x00008293
	public int GetItemCD()
	{
		return 1;
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x00101910 File Offset: 0x000FFB10
	private JSONObject GetItemSkillSeid(JSONObject temp, JSONObject ItemSeidTemp, ref int Damage, ref string seidDesc)
	{
		Dictionary<string, Dictionary<string, int>> dictionary = new Dictionary<string, Dictionary<string, int>>();
		temp.Add(this.AddItemSeid(29, this.GetItemCD(), -9999));
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		for (int i = 0; i < this.CaiLiao.Count; i++)
		{
			if (this.CaiLiao[i].GetItem.itemID != -1)
			{
				int itemNengLiang = this.GetItemNengLiang(this.CaiLiao[i].GetItem.itemID);
				int i2 = jsonData.instance.ItemJsonData[this.CaiLiao[i].GetItem.itemID.ToString()]["ShuXingType"].I;
				if (dictionary2.ContainsKey(i2))
				{
					Dictionary<int, int> dictionary3 = dictionary2;
					int key = i2;
					dictionary3[key] += itemNengLiang;
				}
				else
				{
					dictionary2[i2] = itemNengLiang;
				}
			}
		}
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in dictionary2)
		{
			num += keyValuePair.Value;
		}
		if (num == 0)
		{
			return temp;
		}
		using (List<JSONObject>.Enumerator enumerator2 = jsonData.instance.LianQiHeCheng.list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				JSONObject _ajson = enumerator2.Current;
				Predicate<JSONObject> <>9__0;
				foreach (KeyValuePair<int, int> keyValuePair2 in dictionary2)
				{
					if (_ajson["ShuXingType"].I == keyValuePair2.Key && _ajson["zhonglei"].I == this.ZhuangBeiIndexID && keyValuePair2.Value / _ajson["cast"].I > 0)
					{
						int num2 = keyValuePair2.Value / _ajson["cast"].I;
						if (_ajson["seid"].I != 0)
						{
							JSONObject jsonobject = new JSONObject();
							jsonobject.SetField("id", _ajson["seid"].I);
							for (int j = 1; j < 3; j++)
							{
								int num3 = _ajson["fanbei"].HasItem(j) ? num2 : 1;
								if (_ajson.HasField("intvalue" + j) && _ajson["intvalue" + j].I != 0)
								{
									jsonobject.SetField("value" + j, _ajson["intvalue" + j].I * num3);
								}
							}
							for (int k = 1; k < 3; k++)
							{
								if (_ajson.HasField("listvalue" + k) && _ajson["listvalue" + k].list.Count != 0)
								{
									int num4 = _ajson["fanbei"].HasItem(k) ? num2 : 1;
									JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
									foreach (JSONObject jsonobject3 in _ajson["listvalue" + k].list)
									{
										jsonobject2.Add(jsonobject3.I * num4);
									}
									jsonobject.SetField("value" + k, jsonobject2);
								}
							}
							temp.Add(jsonobject);
						}
						if (_ajson["Itemseid"].I != 0)
						{
							List<JSONObject> list = ItemSeidTemp.list;
							Predicate<JSONObject> match;
							if ((match = <>9__0) == null)
							{
								match = (<>9__0 = ((JSONObject aa) => aa["id"].I == _ajson["Itemseid"].I && aa["value1"].I == _ajson["Itemintvalue1"].I));
							}
							if (list.Find(match) == null)
							{
								JSONObject jsonobject4 = new JSONObject();
								int num5 = _ajson["itemfanbei"].HasItem(1) ? num2 : 1;
								jsonobject4.SetField("id", _ajson["Itemseid"].I);
								jsonobject4.SetField("value1", _ajson["Itemintvalue1"].I * num5);
								ItemSeidTemp.Add(jsonobject4);
							}
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
							Dictionary<string, int> dictionary4 = dictionary[_ajson["descfirst"].str];
							string str = _ajson["desc"].str;
							dictionary4[str] += num6;
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
			}
		}
		foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair3 in dictionary)
		{
			seidDesc += keyValuePair3.Key;
			foreach (KeyValuePair<string, int> keyValuePair4 in keyValuePair3.Value)
			{
				string text = keyValuePair4.Key;
				foreach (object obj in Regex.Matches(text, "\\(\\w*\\)"))
				{
					Match match2 = (Match)obj;
					text = text.Replace(match2.Value, keyValuePair4.Value.ToString());
				}
				seidDesc += text;
			}
		}
		return temp;
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x001021DC File Offset: 0x001003DC
	private JSONObject AddItemSeid(int seid, JSONObject value1 = null, JSONObject value2 = null)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("id", seid);
		if (value1 != null)
		{
			jsonobject.SetField("value1", value1);
		}
		if (value2 != null)
		{
			jsonobject.SetField("value2", value2);
		}
		return jsonobject;
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x000AD1D8 File Offset: 0x000AB3D8
	private JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("id", seid);
		if (value1 != -9999)
		{
			jsonobject.SetField("value1", value1);
		}
		if (value2 != -9999)
		{
			jsonobject.SetField("value2", value2);
		}
		return jsonobject;
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x0010221C File Offset: 0x0010041C
	private JSONObject GetJsonObjectArray(List<int> value1)
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
		foreach (int val in value1)
		{
			jsonobject.Add(val);
		}
		return jsonobject;
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x0001873B File Offset: 0x0001693B
	private JSONObject GetItemAttackType()
	{
		return new JSONObject(JSONObject.Type.ARRAY);
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x00102274 File Offset: 0x00100474
	public void CreateEpuieItem()
	{
		Avatar player = Tools.instance.getPlayer();
		int itemID = (int)jsonData.instance.LianQiEquipType[this.ZhuangBeiIndexID.ToString()]["ItemID"];
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		string val = "";
		JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jsonobject3 = Tools.CreateItemSeid(itemID);
		this.GetItemSkillSeid(jsonobject, jsonobject2, ref num, ref val);
		this.GetItemQuality(ref num2, ref num3);
		jsonobject3.AddField("SkillSeids", jsonobject);
		if (jsonobject2.list.Count > 0)
		{
			jsonobject3.AddField("ItemSeids", jsonobject2);
		}
		jsonobject3.AddField("Name", this.GetItemName());
		jsonobject3.AddField("SeidDesc", val);
		jsonobject3.AddField("Desc", this.GetItemDesc(num2, num3));
		if (num > 0)
		{
			jsonobject3.AddField("Damage", num);
		}
		jsonobject3.AddField("quality", num2);
		jsonobject3.AddField("QPingZhi", num3);
		jsonobject3.AddField("AttackType", this.GetItemAttackType());
		jsonobject3.AddField("Money", this.GetItemMoney());
		player.addItem(itemID, 1, jsonobject3, false);
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x00018743 File Offset: 0x00016943
	public void setSelectZhuangBeiType(int type)
	{
		this.selectZhuangBeiType = (LianQiController.ZhuangBeiType)type;
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x0001874C File Offset: 0x0001694C
	public int getCurSelectZhuangBeiType()
	{
		return (int)this.selectZhuangBeiType;
	}

	// Token: 0x04001930 RID: 6448
	[SerializeField]
	private List<ItemCellEX> CaiLiao = new List<ItemCellEX>();

	// Token: 0x04001931 RID: 6449
	private LianQiController.ZhuangBeiType selectZhuangBeiType;

	// Token: 0x04001932 RID: 6450
	private int curSelectEquipMuBanID = -1;

	// Token: 0x04001933 RID: 6451
	private int curJinDu;

	// Token: 0x04001934 RID: 6452
	public int ZhuangBeiIndexID = -1;

	// Token: 0x04001935 RID: 6453
	public string ItemName = "";

	// Token: 0x0200044D RID: 1101
	public enum ZhuangBeiType
	{
		// Token: 0x04001937 RID: 6455
		空,
		// Token: 0x04001938 RID: 6456
		武器,
		// Token: 0x04001939 RID: 6457
		防具,
		// Token: 0x0400193A RID: 6458
		饰品
	}
}
