using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GUIPackage;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class LianQiController : MonoBehaviour
{
	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06001A29 RID: 6697 RVA: 0x000BAC90 File Offset: 0x000B8E90
	public int QingHe
	{
		get
		{
			return this.getQingHe();
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06001A2A RID: 6698 RVA: 0x000BAC98 File Offset: 0x000B8E98
	public int CaoKong
	{
		get
		{
			return this.getCaoKong();
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06001A2B RID: 6699 RVA: 0x000BACA0 File Offset: 0x000B8EA0
	public int LingXing
	{
		get
		{
			return this.getLingXing();
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06001A2C RID: 6700 RVA: 0x000BACA8 File Offset: 0x000B8EA8
	public int JianGu
	{
		get
		{
			return this.getJianGu();
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06001A2D RID: 6701 RVA: 0x000BACB0 File Offset: 0x000B8EB0
	public int RenXing
	{
		get
		{
			return this.getRenXing();
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06001A2E RID: 6702 RVA: 0x000BACB8 File Offset: 0x000B8EB8
	public int NengLiang
	{
		get
		{
			return this.getNengLiang();
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06001A2F RID: 6703 RVA: 0x000BACC0 File Offset: 0x000B8EC0
	public List<int> ShuXing
	{
		get
		{
			return this.getShuXing();
		}
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x000BACC8 File Offset: 0x000B8EC8
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

	// Token: 0x06001A31 RID: 6705 RVA: 0x000BAD40 File Offset: 0x000B8F40
	public void setCurSelectEquipMuBanID(int itemID)
	{
		this.curSelectEquipMuBanID = itemID;
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x000BAD49 File Offset: 0x000B8F49
	public int getCurSelectEquipMuBanID()
	{
		return this.curSelectEquipMuBanID;
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x000BAD51 File Offset: 0x000B8F51
	public void setCurJinDu(int jindu)
	{
		this.curJinDu = jindu;
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x000BAD5A File Offset: 0x000B8F5A
	public int getCurJinDu()
	{
		return this.curJinDu;
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x000BAD64 File Offset: 0x000B8F64
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

	// Token: 0x06001A36 RID: 6710 RVA: 0x000BAE0C File Offset: 0x000B900C
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

	// Token: 0x06001A37 RID: 6711 RVA: 0x000BAEB4 File Offset: 0x000B90B4
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

	// Token: 0x06001A38 RID: 6712 RVA: 0x000BAF5C File Offset: 0x000B915C
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

	// Token: 0x06001A39 RID: 6713 RVA: 0x000BB004 File Offset: 0x000B9204
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

	// Token: 0x06001A3A RID: 6714 RVA: 0x000BB0AC File Offset: 0x000B92AC
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

	// Token: 0x06001A3B RID: 6715 RVA: 0x000BB10C File Offset: 0x000B930C
	private int GetItemNengLiang(int ItemID)
	{
		int i = jsonData.instance.ItemJsonData[ItemID.ToString()]["quality"].I;
		return jsonData.instance.CaiLiaoNengLiangBiao[i.ToString()]["value1"].I;
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x000BB164 File Offset: 0x000B9364
	public bool WhetherSuccess()
	{
		return this.QingHe != 0 && this.CaoKong != 0 && this.LingXing != 0 && this.JianGu != 0 && this.RenXing != 0;
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x000BB194 File Offset: 0x000B9394
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

	// Token: 0x06001A3E RID: 6718 RVA: 0x000BB218 File Offset: 0x000B9418
	public void LiQiSuccess()
	{
		this.CreateEpuieItem();
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x000BB220 File Offset: 0x000B9420
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

	// Token: 0x06001A40 RID: 6720 RVA: 0x000BB3A4 File Offset: 0x000B95A4
	private string GetItemName()
	{
		return this.ItemName;
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x000BB3AC File Offset: 0x000B95AC
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

	// Token: 0x06001A42 RID: 6722 RVA: 0x000BB3FC File Offset: 0x000B95FC
	private int GetItemMoney()
	{
		JToken nowQualityItemJson = this.getNowQualityItemJson();
		if (nowQualityItemJson != null)
		{
			return (int)nowQualityItemJson["price"];
		}
		return -1;
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x00024C5F File Offset: 0x00022E5F
	public int GetItemCD()
	{
		return 1;
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x000BB428 File Offset: 0x000B9628
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

	// Token: 0x06001A45 RID: 6725 RVA: 0x000BBCF4 File Offset: 0x000B9EF4
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

	// Token: 0x06001A46 RID: 6726 RVA: 0x000BBD34 File Offset: 0x000B9F34
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

	// Token: 0x06001A47 RID: 6727 RVA: 0x000BBD7C File Offset: 0x000B9F7C
	private JSONObject GetJsonObjectArray(List<int> value1)
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
		foreach (int val in value1)
		{
			jsonobject.Add(val);
		}
		return jsonobject;
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x000BBDD4 File Offset: 0x000B9FD4
	private JSONObject GetItemAttackType()
	{
		return new JSONObject(JSONObject.Type.ARRAY);
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x000BBDDC File Offset: 0x000B9FDC
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

	// Token: 0x06001A4A RID: 6730 RVA: 0x000BBF14 File Offset: 0x000BA114
	public void setSelectZhuangBeiType(int type)
	{
		this.selectZhuangBeiType = (LianQiController.ZhuangBeiType)type;
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x000BBF1D File Offset: 0x000BA11D
	public int getCurSelectZhuangBeiType()
	{
		return (int)this.selectZhuangBeiType;
	}

	// Token: 0x0400152A RID: 5418
	[SerializeField]
	private List<ItemCellEX> CaiLiao = new List<ItemCellEX>();

	// Token: 0x0400152B RID: 5419
	private LianQiController.ZhuangBeiType selectZhuangBeiType;

	// Token: 0x0400152C RID: 5420
	private int curSelectEquipMuBanID = -1;

	// Token: 0x0400152D RID: 5421
	private int curJinDu;

	// Token: 0x0400152E RID: 5422
	public int ZhuangBeiIndexID = -1;

	// Token: 0x0400152F RID: 5423
	public string ItemName = "";

	// Token: 0x02001331 RID: 4913
	public enum ZhuangBeiType
	{
		// Token: 0x040067C9 RID: 26569
		空,
		// Token: 0x040067CA RID: 26570
		武器,
		// Token: 0x040067CB RID: 26571
		防具,
		// Token: 0x040067CC RID: 26572
		饰品
	}
}
