using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JSONClass;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace YSGame.EquipRandom;

public static class RandomEquip
{
	private static int failCount;

	private static bool LogFailInfo;

	private static StringBuilder failInfoSB;

	private static bool isInited = false;

	private static Dictionary<int, Dictionary<int, int>> QualityLingLiDict = new Dictionary<int, Dictionary<int, int>>();

	private static Dictionary<int, Dictionary<int, int>> QualityPriceDict = new Dictionary<int, Dictionary<int, int>>();

	private static Dictionary<int, Dictionary<int, int>> HeChengBiaoDict = new Dictionary<int, Dictionary<int, int>>();

	private static Dictionary<int, List<int>> EquipTypeShuXingIDList = new Dictionary<int, List<int>>();

	private static Dictionary<int, int> ShuXingCastDict = new Dictionary<int, int>();

	private static Dictionary<int, int> ShuXingTypeDict = new Dictionary<int, int>();

	private static Dictionary<int, Dictionary<int, List<_FaBaoFirstNameJsonData>>> FirstNameDict = new Dictionary<int, Dictionary<int, List<_FaBaoFirstNameJsonData>>>();

	private static Dictionary<int, Dictionary<int, List<_FaBaoLastNameJsonData>>> LastNameDict = new Dictionary<int, Dictionary<int, List<_FaBaoLastNameJsonData>>>();

	private static List<string> EquipTypeNameList = new List<string> { "剑", "钟", "环", "针", "匣", "袍", "甲", "珠", "令", "印" };

	private static List<string> EquipTypeFullNameList = new List<string> { "剑", "钟", "环", "飞针", "匣", "法袍", "甲胄", "珠", "令", "印" };

	private static List<string> EquipTypePinYinList = new List<string> { "jian", "zhong", "huan", "zhen", "xia", "pao", "jia", "zhu", "ling", "yin" };

	private static Dictionary<int, Dictionary<int, List<JSONObject>>> QualityShuXingCaiLiaoDict = new Dictionary<int, Dictionary<int, List<JSONObject>>>();

	private static Dictionary<int, Dictionary<int, List<CaiLiao>>> CaiLiaoDict = new Dictionary<int, Dictionary<int, List<CaiLiao>>>();

	private static Random random = new Random();

	private static void Init()
	{
		if (!isInited)
		{
			InitCaiLiaoData();
			InitNameData();
			InitHeChengBiaoData();
			InitQualityLingLiData();
			isInited = true;
		}
	}

	private static void InitQualityLingLiData()
	{
		for (int i = 1; i <= 5; i++)
		{
			QualityLingLiDict.Add(i, new Dictionary<int, int>());
			QualityPriceDict.Add(i, new Dictionary<int, int>());
		}
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.LianQiWuQiQuality)
		{
			QualityLingLiDict[(int)item.Value[(object)"quality"]].Add((int)item.Value[(object)"shangxia"], (int)item.Value[(object)"power"]);
			QualityPriceDict[(int)item.Value[(object)"quality"]].Add((int)item.Value[(object)"shangxia"], (int)item.Value[(object)"price"]);
		}
	}

	private static void InitHeChengBiaoData()
	{
		for (int i = 1; i <= 10; i++)
		{
			HeChengBiaoDict.Add(i, new Dictionary<int, int>());
			EquipTypeShuXingIDList.Add(i, new List<int>());
		}
		foreach (JSONObject item in jsonData.instance.LianQiHeCheng.list)
		{
			int i2 = item["zhonglei"].I;
			int i3 = item["ShuXingType"].I;
			HeChengBiaoDict[i2].Add(i3, item["id"].I);
			EquipTypeShuXingIDList[i2].Add(item["id"].I);
			ShuXingCastDict.Add(item["id"].I, item["cast"].I);
			ShuXingTypeDict.Add(item["id"].I, i3);
		}
	}

	private static void InitNameData()
	{
		List<_FaBaoFirstNameJsonData> dataList = _FaBaoFirstNameJsonData.DataList;
		List<_FaBaoLastNameJsonData> dataList2 = _FaBaoLastNameJsonData.DataList;
		for (int i = 1; i <= 5; i++)
		{
			FirstNameDict.Add(i, new Dictionary<int, List<_FaBaoFirstNameJsonData>>());
			LastNameDict.Add(i, new Dictionary<int, List<_FaBaoLastNameJsonData>>());
		}
		foreach (_FaBaoFirstNameJsonData item in dataList)
		{
			for (int j = 0; j < item.quality.Count; j++)
			{
				for (int k = 0; k < item.Type.Count; k++)
				{
					int key = item.quality[j];
					int key2 = item.Type[k];
					_ = item.FirstName;
					if (!FirstNameDict[key].ContainsKey(key2))
					{
						FirstNameDict[key].Add(key2, new List<_FaBaoFirstNameJsonData>());
					}
					FirstNameDict[key][key2].Add(item);
				}
			}
		}
		foreach (_FaBaoLastNameJsonData item2 in dataList2)
		{
			for (int l = 0; l < item2.quality.Count; l++)
			{
				for (int m = 0; m < item2.Type.Count; m++)
				{
					int key3 = item2.quality[l];
					int key4 = item2.Type[m];
					_ = item2.LastName;
					if (!LastNameDict[key3].ContainsKey(key4))
					{
						LastNameDict[key3].Add(key4, new List<_FaBaoLastNameJsonData>());
					}
					LastNameDict[key3][key4].Add(item2);
				}
			}
		}
	}

	private static void InitCaiLiaoData()
	{
		JSONObject itemJsonData = jsonData.instance._ItemJsonData;
		for (int i = 1; i <= 6; i++)
		{
			CaiLiaoDict.Add(i, new Dictionary<int, List<CaiLiao>>());
			QualityShuXingCaiLiaoDict.Add(i, new Dictionary<int, List<JSONObject>>());
		}
		foreach (JSONObject item in itemJsonData.list)
		{
			if (item["type"].I == 8)
			{
				int i2 = item["ShuXingType"].I;
				int i3 = item["quality"].I;
				if (!QualityShuXingCaiLiaoDict[i3].ContainsKey(i2))
				{
					QualityShuXingCaiLiaoDict[i3].Add(i2, new List<JSONObject>());
				}
				if (!CaiLiaoDict[i3].ContainsKey(i2))
				{
					CaiLiaoDict[i3].Add(i2, new List<CaiLiao>());
				}
				QualityShuXingCaiLiaoDict[i3][i2].Add(item);
				CaiLiaoDict[i3][i2].Add(new CaiLiao(item));
			}
		}
	}

	private static int Range(int min, int max)
	{
		return random.Next(min, max + 1);
	}

	private static int GetItemCD(int lingWenID)
	{
		if (lingWenID > 3)
		{
			return 1;
		}
		if (lingWenID > 0)
		{
			return jsonData.instance.LianQiLingWenBiao[lingWenID.ToString()]["value1"].I;
		}
		return 1;
	}

	private static JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
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

	private static void SetLingWenSeid(JSONObject skillSeids, JSONObject itemSeid, int LingWenID, int equipType)
	{
		if (LingWenID == -1)
		{
			return;
		}
		JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()];
		if (jSONObject["type"].I == 1)
		{
			if (LogFailInfo)
			{
				failInfoSB.AppendLine("青龙直接返回");
			}
			return;
		}
		JSONObject jSONObject2 = new JSONObject();
		switch ((equipType < 6) ? 1 : ((equipType >= 8) ? 3 : 2))
		{
		case 1:
			if (LogFailInfo)
			{
				failInfoSB.AppendLine("开始添加武器技能特性");
			}
			jSONObject2.SetField("id", jSONObject["seid"].I);
			if (jSONObject["seid"].I == 77)
			{
				jSONObject2.SetField("value1", jSONObject["listvalue1"]);
			}
			else if (jSONObject["seid"].I == 80)
			{
				jSONObject2.SetField("value1", jSONObject["listvalue1"]);
				jSONObject2.SetField("value2", jSONObject["listvalue2"]);
			}
			else if (jSONObject["seid"].I == 145)
			{
				jSONObject2.SetField("value1", jSONObject["listvalue1"][0]);
			}
			skillSeids.Add(jSONObject2);
			break;
		case 2:
		case 3:
			if (LogFailInfo)
			{
				failInfoSB.AppendLine("开始添加衣服和饰品(item)BUFF特性");
			}
			jSONObject2.SetField("id", jSONObject["Itemseid"].I);
			if (jSONObject["seid"].I == 62)
			{
				jSONObject2.SetField("value1", jSONObject["Itemintvalue1"]);
			}
			else
			{
				jSONObject2.SetField("value1", jSONObject["Itemintvalue1"]);
				jSONObject2.SetField("value2", jSONObject["Itemintvalue2"]);
			}
			itemSeid.Add(jSONObject2);
			break;
		}
	}

	private static string BuildNomalLingWenDesc(JSONObject obj)
	{
		string text = ((obj["value3"].I == 1) ? "x" : "+");
		string result = "";
		switch (obj["type"].I)
		{
		case 1:
			result = string.Format("{0}<color=#fff227>x{1}</color>,灵力<color=#fff227>{2}{3}</color>", obj["desc"].str, obj["value1"].I, text, obj["value4"].I);
			break;
		case 2:
			result = string.Format("对自己造成<color=#fff227>x{0}</color>点真实伤害,灵力<color=#fff227>{1}{2}</color>", obj["value1"].I, text, obj["value4"].I);
			break;
		case 4:
			result = string.Format("{0}才能使用,灵力<color=#fff227>{1}{2}</color>", obj["desc"].str, text, obj["value4"].n);
			break;
		}
		return result;
	}

	private static string GetLingWenDesc(int LingWenID)
	{
		string result = "";
		if (LingWenID != -1)
		{
			JSONObject jSONObject = jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()];
			result = ((jSONObject["type"].I != 3) ? Regex.Split(BuildNomalLingWenDesc(jSONObject), ",", RegexOptions.None)[0] : (Tools.Code64(jSONObject["desc"].str).Replace("获得", "获得<color=#ff624d>") + string.Format("</color>x{0}", jSONObject["value2"].I)));
		}
		return result;
	}

	private static string GetEquipIconPath(int EquipType, int Quality, int Shangxia)
	{
		List<JSONObject> list = jsonData.instance.LianQiEquipIconBiao.list;
		if (EquipType == -1)
		{
			return "";
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i]["zhonglei"].I == EquipType && list[i]["quality"].I == Quality && list[i]["pingjie"].I == Shangxia)
			{
				return string.Format("NewUI/LianQi/EquipIcon/{0}", list[i]["id"].I);
			}
		}
		return "";
	}

	private static string NewGetEquipIconPath(int EquipType, int Quality, int AttackType)
	{
		if (EquipType == -1)
		{
			return "";
		}
		return $"LianQiIcon/MCS_fabao_{EquipTypePinYinList[EquipType - 1]}_{AttackType}_{Quality}";
	}

	private static JSONObject GetEquipItemFlag(int EquipType, int EquipQuality, JSONObject AttackType)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		int num = 0;
		num = ((EquipType >= 1 && EquipType <= 5) ? 1 : ((EquipType < 6 || EquipType >= 8) ? 3 : 2));
		int num2 = num * 100 + EquipQuality;
		jSONObject.Add(num2);
		for (int i = 0; i < AttackType.Count; i++)
		{
			int val = num2 * 10 + AttackType[i].I;
			jSONObject.Add(val);
		}
		jSONObject.Add(num);
		return jSONObject;
	}

	private static string GetEquipQualityDesc(int Quality, int ShangXia)
	{
		string result = "";
		string text = "";
		switch (ShangXia)
		{
		case 1:
			text = "下品";
			break;
		case 2:
			text = "中品";
			break;
		case 3:
			text = "上品";
			break;
		}
		switch (Quality)
		{
		case 1:
			result = text + "符器";
			break;
		case 2:
			result = text + "法器";
			break;
		case 3:
			result = text + "法宝";
			break;
		case 4:
			result = text + "纯阳法宝";
			break;
		case 5:
			result = text + "通天灵宝";
			break;
		}
		return result;
	}

	private static int RandomShuXingID()
	{
		JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
		return Range(1, lianQiHeCheng.list.Count);
	}

	private static int RandomShuXingID(int EuqipType)
	{
		int index = Range(0, EquipTypeShuXingIDList[EuqipType].Count - 1);
		return EquipTypeShuXingIDList[EuqipType][index];
	}

	private static int GetItemIDByEquipType(int EquipType)
	{
		return 18000 + EquipType;
	}

	private static int GetEquipTypeByShuXingID(int id)
	{
		return jsonData.instance.LianQiHeCheng[id.ToString()]["zhonglei"].I;
	}

	private static int RandomLingWenIDByLingWenType(int LingWenType, List<int> banAffix = null)
	{
		JSONObject lianQiLingWenBiao = jsonData.instance.LianQiLingWenBiao;
		List<int> list = new List<int>();
		foreach (JSONObject item in lianQiLingWenBiao.list)
		{
			if (item["type"].I != LingWenType)
			{
				continue;
			}
			if (banAffix != null)
			{
				List<int> list2 = item["Affix"].ToList();
				bool flag = false;
				foreach (int item2 in banAffix)
				{
					if (list2.Contains(item2))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					continue;
				}
			}
			list.Add(item["id"].I);
		}
		return list[Range(0, list.Count - 1)];
	}

	private static JSONObject GetAttackType(List<CaiLiao> CaiLiaoList, int EquipType)
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
		List<int> list = new List<int>();
		int num = 0;
		for (int i = 0; i < CaiLiaoList.Count; i++)
		{
			int num2 = CaiLiaoList[i].AttackType(EquipType);
			if (num2 != -1 && !list.Contains(num2))
			{
				list.Add(num2);
				if (num2 != 5)
				{
					num++;
					jSONObject2.Add(num2);
				}
				jSONObject.Add(num2);
			}
		}
		if (num > 0)
		{
			return jSONObject2;
		}
		return jSONObject;
	}

	private static int GetEquipPrice(int EquipQuality, int ShangXia)
	{
		int result = 0;
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.LianQiWuQiQuality)
		{
			if ((int)item.Value[(object)"quality"] == EquipQuality && (int)item.Value[(object)"shangxia"] == ShangXia)
			{
				result = (int)item.Value[(object)"price"];
			}
		}
		return result;
	}

	private static int GetDuoDuanIDByLingLi(int sum)
	{
		int result = 0;
		List<JSONObject> list = jsonData.instance.LianQiDuoDuanShangHaiBiao.list;
		for (int i = 0; i < list.Count && list[i]["cast"].I <= sum; i++)
		{
			result = list[i]["id"].I;
		}
		return result;
	}

	private static string GetCiTiaoDesc(int id, Dictionary<int, int> entryDictionary)
	{
		string text = "";
		if (id == 49)
		{
			int duoDuanIDByLingLi = GetDuoDuanIDByLingLi(entryDictionary[id]);
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

	private static List<CaiLiao> RandomCaiLiao(int EquipQuality, int TargetShuXing, ref int ShangXia)
	{
		List<CaiLiao> list = new List<CaiLiao>();
		int num = ShuXingCastDict[TargetShuXing];
		int num2 = 0;
		foreach (KeyValuePair<string, JToken> item in jsonData.instance.LianQiWuQiQuality)
		{
			if ((int)item.Value[(object)"quality"] == EquipQuality && (int)item.Value[(object)"shangxia"] == 1)
			{
				num2 = (int)item.Value[(object)"power"];
				break;
			}
		}
		int num3 = 0;
		int num4 = Range(0, 100);
		int num5 = ((num4 < 10) ? 2 : ((num4 < 40) ? 1 : 0));
		int key = ShuXingTypeDict[TargetShuXing];
		if (CaiLiaoDict[EquipQuality][key].Count > 0)
		{
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			while (list.Count < 10 || num6 < num || num6 < num2)
			{
				if (list.Count >= 10)
				{
					num7 = 1;
					if (LogFailInfo)
					{
						failInfoSB.AppendLine("[随机材料]灵力不够，去除一个材料重新随机");
					}
					num3 -= list[0].LingLi;
					list.RemoveAt(0);
					num8++;
					if (num8 > 100)
					{
						if (LogFailInfo)
						{
							failInfoSB.AppendLine("[随机材料]超过100次随机无法完成保底");
						}
						break;
					}
				}
				int num9 = EquipQuality + 1;
				if (EquipQuality > 1)
				{
					num9 = EquipQuality + 1;
				}
				else if (num5 != 2)
				{
					num9 = ((Range(0, 1) != 0) ? EquipQuality : (EquipQuality + 1));
				}
				if (num3 >= num)
				{
					switch (num5)
					{
					case 0:
						num9--;
						break;
					case 1:
						if (Range(0, 1) == 0)
						{
							num9--;
						}
						break;
					}
				}
				num9 += num7;
				num9 = Mathf.Clamp(num9, 1, 6);
				int index = Range(0, CaiLiaoDict[num9][key].Count - 1);
				CaiLiao caiLiao = CaiLiaoDict[num9][key][index];
				list.Add(CaiLiaoDict[num9][key][index]);
				if (LogFailInfo)
				{
					failInfoSB.AppendLine($"[随机材料]添加了材料{caiLiao}");
				}
				num3 += caiLiao.LingLi;
				num6 = (int)(CalcWuWeiBaiFenBi(list) * (float)num3);
				if (LogFailInfo)
				{
					failInfoSB.AppendLine($"[随机材料]当前材料削减后总灵力:{num6}");
				}
			}
		}
		int num10 = (int)(CalcWuWeiBaiFenBi(list) * (float)num3);
		foreach (KeyValuePair<string, JToken> item2 in jsonData.instance.LianQiWuQiQuality)
		{
			if (num10 >= (int)item2.Value[(object)"power"])
			{
				ShangXia = (int)item2.Value[(object)"shangxia"];
				continue;
			}
			break;
		}
		return list;
	}

	public static string RandomEquipName(int EquipQuality, int EquipType, int ShuXingType)
	{
		return BetterRandomEquipName(EquipQuality, EquipType, ShuXingType);
	}

	public static string NormalRandomEquipName(int EquipQuality, int EquipType, int ShuXingType)
	{
		List<_FaBaoFirstNameJsonData> list = FirstNameDict[EquipQuality][ShuXingType];
		List<_FaBaoLastNameJsonData> list2 = LastNameDict[EquipQuality][EquipType];
		int index = Range(0, list.Count - 1);
		string firstName = list[index].FirstName;
		int index2 = Range(0, list2.Count - 1);
		string lastName = list2[index2].LastName;
		return firstName + lastName + EquipTypeNameList[EquipType - 1];
	}

	public static string BetterRandomEquipName(int EquipQuality, int EquipType, int ShuXingType)
	{
		try
		{
			List<_FaBaoFirstNameJsonData> list = FirstNameDict[EquipQuality][ShuXingType];
			List<_FaBaoLastNameJsonData> list2 = LastNameDict[EquipQuality][EquipType];
			int index = Range(0, list.Count - 1);
			_FaBaoFirstNameJsonData faBaoFirstNameJsonData = list[index];
			int index2 = Range(0, list2.Count - 1);
			_FaBaoLastNameJsonData faBaoLastNameJsonData = list2[index2];
			return (faBaoFirstNameJsonData.PosReverse != 1 && faBaoLastNameJsonData.PosReverse != 1) ? (faBaoFirstNameJsonData.FirstName + faBaoLastNameJsonData.LastName + EquipTypeNameList[EquipType - 1]) : (faBaoLastNameJsonData.LastName + faBaoFirstNameJsonData.FirstName + EquipTypeNameList[EquipType - 1]);
		}
		catch
		{
			string result = $"错误{EquipQuality}_{EquipType}_{ShuXingType}";
			Debug.LogError((object)$"炼器随机名字失败，目标装备品质{EquipQuality} 目标装备类型{EquipType} 目标装备属性{ShuXingType}");
			return result;
		}
	}

	public static void TestLogAllBetterRandomEquipName()
	{
		Init();
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		List<string> list = new List<string>();
		for (int i = 1; i <= 5; i++)
		{
			stringBuilder.AppendLine($"[装备品质.{i}↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓]");
			List<int> list2 = LastNameDict[i].Keys.ToList();
			List<int> list3 = FirstNameDict[i].Keys.ToList();
			int num2 = 0;
			for (int j = 0; j < list2.Count; j++)
			{
				int num3 = list2[j];
				string text = EquipTypeNameList[num3 - 1];
				int num4 = 0;
				for (int k = 0; k < list3.Count; k++)
				{
					int num5 = list3[k];
					StringBuilder stringBuilder2 = new StringBuilder();
					int num6 = 0;
					foreach (_FaBaoFirstNameJsonData item in FirstNameDict[i][num5])
					{
						foreach (_FaBaoLastNameJsonData item2 in LastNameDict[i][num3])
						{
							string text2 = ((item.PosReverse != 1 && item2.PosReverse != 1) ? (item.FirstName + item2.LastName + text) : (item2.LastName + item.FirstName + text));
							stringBuilder2.Append(text2 + " ");
							num++;
							num2++;
							num4++;
							num6++;
							if (!list.Contains(text2))
							{
								list.Add(text2);
							}
						}
					}
					stringBuilder.AppendLine(stringBuilder2.ToString());
					stringBuilder.AppendLine($"[装备属性.{num5}.有{num6}个名字组合]");
				}
				stringBuilder.AppendLine($"[装备类型.{text}.有{num4}个名字组合]");
			}
			stringBuilder.AppendLine($"[装备品质.{i}.有{num2}个名字组合]\n");
		}
		stopwatch.Stop();
		stringBuilder.AppendLine($"遍历完毕，全局一共有{num}个名字组合，{list.Count}个不重复组合，遍历耗时{stopwatch.ElapsedMilliseconds}ms");
		File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/装备随机名遍历结果.log", stringBuilder.ToString());
		stringBuilder.Clear();
		foreach (string item3 in list)
		{
			stringBuilder.Append(item3 + " ");
		}
		stringBuilder.AppendLine($"\n共有{list.Count}个不重复组合");
		File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/装备随机名遍历结果_不重复.log", stringBuilder.ToString());
		Debug.Log((object)$"遍历完毕，全局一共有{num}个名字组合，{list.Count}个不重复组合，遍历耗时{stopwatch.ElapsedMilliseconds}ms，日志已输出到桌面");
	}

	private static float CalcWuWeiBaiFenBi(List<CaiLiao> CaiLiaoList)
	{
		float num = 2200f;
		int num2 = 0;
		for (int i = 0; i < CaiLiaoList.Count; i++)
		{
			num2 += CaiLiaoList[i].TotalWuWei;
		}
		float num3 = (float)num2 / num;
		if (num3 > 1f)
		{
			num3 = 1f;
		}
		return num3;
	}

	private static Dictionary<int, int> CalcCiTiao(List<CaiLiao> data, int EquipType, int EquipQuality, int lingWenID, Avatar Maker)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		if (LogFailInfo)
		{
			string text = "材料列表:\n";
			for (int i = 0; i < data.Count; i++)
			{
				text += $"{data[i]}\n";
			}
			failInfoSB.AppendLine(text);
		}
		for (int j = 0; j < data.Count; j++)
		{
			int num = data[j].ShuXingID(EquipType);
			switch (num)
			{
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
				if (dictionary.ContainsKey(49))
				{
					dictionary[49] += data[j].LingLi;
					if (LogFailInfo)
					{
						failInfoSB.AppendLine($"{j}号材料对entryDictionary[49]增加了{data[j].LingLi}点灵力，目前有{dictionary[49]}灵力");
					}
				}
				else
				{
					dictionary.Add(49, data[j].LingLi);
					if (LogFailInfo)
					{
						failInfoSB.AppendLine($"{j}号材料添加entryDictionary，key:49");
					}
				}
				continue;
			case 0:
				if (LogFailInfo)
				{
					failInfoSB.AppendLine($"{j}号材料属性ID为0");
				}
				continue;
			}
			if (num >= 1 && num <= 8)
			{
				if (dictionary.ContainsKey(1))
				{
					dictionary[1] += data[j].LingLi;
					if (LogFailInfo)
					{
						failInfoSB.AppendLine($"{j}号材料对entryDictionary[1]增加了{data[j].LingLi}点灵力，目前有{dictionary[1]}灵力");
					}
				}
				else
				{
					dictionary.Add(1, data[j].LingLi);
					if (LogFailInfo)
					{
						failInfoSB.AppendLine($"{j}号材料添加entryDictionary，key:1");
					}
				}
			}
			else if (dictionary.ContainsKey(num))
			{
				dictionary[num] += data[j].LingLi;
				if (LogFailInfo)
				{
					failInfoSB.AppendLine($"{j}号材料对entryDictionary[{num}]增加了{data[j].LingLi}点灵力，目前有{dictionary[num]}灵力");
				}
			}
			else
			{
				dictionary.Add(num, data[j].LingLi);
				if (LogFailInfo)
				{
					failInfoSB.AppendLine($"{j}号材料添加entryDictionary，key:{num}");
				}
			}
		}
		float num2 = CalcWuWeiBaiFenBi(data);
		if (LogFailInfo)
		{
			failInfoSB.AppendLine($"五维削减百分比{num2}");
		}
		int num3 = -1;
		float num4 = -1f;
		bool flag = false;
		if (EquipQuality > 2)
		{
			flag = Range(0, 1000) % 20 == 0;
		}
		if (lingWenID > 0)
		{
			num3 = jsonData.instance.LianQiLingWenBiao[lingWenID.ToString()]["value3"].I;
			num4 = jsonData.instance.LianQiLingWenBiao[lingWenID.ToString()]["value4"].n;
		}
		JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		foreach (int key in dictionary.Keys)
		{
			int num5 = dictionary[key];
			num5 = (int)((float)num5 * num2);
			if (key == 49)
			{
				if (num5 >= jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I)
				{
					dictionary2.Add(key, num5);
					if (LogFailInfo)
					{
						failInfoSB.AppendLine(string.Format("{0}达标，已有{1}，需求{2}", key, num5, jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I));
					}
				}
				else if (LogFailInfo)
				{
					failInfoSB.AppendLine(string.Format("{0}不达标，已有{1}，需求{2}", key, num5, jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I));
				}
				continue;
			}
			int i2 = lianQiHeCheng[key.ToString()]["cast"].I;
			if (num5 >= i2)
			{
				dictionary2.Add(key, num5);
				if (LogFailInfo)
				{
					failInfoSB.AppendLine($"{key}达标，已有{num5}，需求{i2}");
				}
			}
			else if (LogFailInfo)
			{
				failInfoSB.AppendLine($"{key}不达标，已有{num5}，需求{i2}");
			}
		}
		if (dictionary2.Keys.Count == 0)
		{
			if (LogFailInfo)
			{
				failInfoSB.AppendLine("没有达标材料");
			}
			return dictionary2;
		}
		if (LogFailInfo)
		{
			failInfoSB.AppendLine("开始计算增幅");
		}
		Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
		foreach (int key2 in dictionary2.Keys)
		{
			int num6 = dictionary2[key2];
			if (lingWenID > 0)
			{
				if (num3 == 1)
				{
					num6 = (int)((float)num6 * num4);
				}
				else
				{
					num4 /= (float)dictionary2.Keys.Count;
					num6 = (int)((float)num6 + num4);
				}
			}
			if (flag)
			{
				num6 = (int)((float)num6 * 1.5f);
			}
			int i3 = lianQiHeCheng[key2.ToString()]["cast"].I;
			int value = num6 / i3;
			dictionary3.Add(key2, value);
		}
		return dictionary3;
	}

	private static string GetEquipDesc(int EquipType, int EquipQuality, int ShangXia, int LingWenID, List<CaiLiao> CaiLiaoList, Avatar Maker)
	{
		string text = "";
		if (Maker != null)
		{
			text = Maker.firstName + Maker.lastName;
		}
		string text2 = Tools.instance.getPlayer().worldTimeMag.getNowTime().Year.ToString();
		string text3 = "";
		for (int i = 0; i < 3; i++)
		{
			text3 += CaiLiaoList[i].Name;
			if (i != 2)
			{
				text3 += "、";
			}
		}
		string equipQualityDesc = GetEquipQualityDesc(EquipQuality, ShangXia);
		string text4 = EquipTypeFullNameList[EquipType - 1];
		string text5 = ((LingWenID == -1) ? "聚灵灵纹。" : (jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()]["name"].Str + "灵纹,灵力更加强大，但对使用者也要求更高。"));
		if (Maker == null)
		{
			return "不知何人将" + text3 + "等材料炼制的" + equipQualityDesc + "，此" + text4 + "铭刻" + text5;
		}
		return text + "于" + text2 + "年将" + text3 + "等材料炼制的" + equipQualityDesc + "，此" + text4 + "铭刻" + text5;
	}

	public static int FindShuXingIDByEquipTypeAndShuXingType(int ShuXingType, int EquipType)
	{
		return HeChengBiaoDict[EquipType][ShuXingType];
	}

	public static void CreateRandomEquip(ref int ItemID, ref JSONObject ItemJson, int EquipQuality = -1, int TargetShuXing = -1, int EquipType = -1, int LingWenType = -1, int LingWenID = -1, Avatar Maker = null)
	{
		if (failCount >= 10)
		{
			failInfoSB = new StringBuilder();
			failInfoSB.AppendLine($"随机装备生成连续失败超过10次，请报告程序检查。目标品质:{EquipQuality} 目标属性:{TargetShuXing} 目标装备类型:{EquipType} 目标灵纹类型:{LingWenType} 目标灵纹ID:{LingWenID}");
			LogFailInfo = true;
		}
		Init();
		int equipQuality = EquipQuality;
		int targetShuXing = TargetShuXing;
		int equipType = EquipType;
		int lingWenType = LingWenType;
		int lingWenID = LingWenID;
		if (EquipQuality == -1)
		{
			EquipQuality = Range(1, 5);
		}
		if (TargetShuXing != -1)
		{
			EquipType = GetEquipTypeByShuXingID(TargetShuXing);
		}
		if (EquipType == -1)
		{
			TargetShuXing = RandomShuXingID();
			EquipType = GetEquipTypeByShuXingID(TargetShuXing);
		}
		else if (TargetShuXing == -1)
		{
			TargetShuXing = RandomShuXingID(EquipType);
		}
		int num = jsonData.instance.LianQiHeCheng[TargetShuXing.ToString()]["ShuXingType"].I;
		if (EquipQuality < 2 && num % 2 == 0)
		{
			num--;
			TargetShuXing = FindShuXingIDByEquipTypeAndShuXingType(num, EquipType);
		}
		ItemID = GetItemIDByEquipType(EquipType);
		if (EquipQuality > 2)
		{
			if (LingWenID == -1)
			{
				if (LingWenType == -1)
				{
					LingWenType = ((EquipType >= 6) ? Range(2, 3) : Range(1, 3));
				}
				int num2 = (int)jsonData.instance.LianQiEquipType[EquipType.ToString()][(object)"zhonglei"];
				LingWenID = ((LingWenType != 3 || num2 != 2) ? RandomLingWenIDByLingWenType(LingWenType) : RandomLingWenIDByLingWenType(LingWenType, new List<int> { 8 }));
			}
			else if (LingWenType == -1)
			{
				LingWenType = jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()]["type"].I;
			}
		}
		else
		{
			LingWenID = -1;
		}
		string text = "";
		JSONObject jSONObject = new JSONObject(JSONObject.Type.ARRAY);
		JSONObject jSONObject2 = new JSONObject(JSONObject.Type.ARRAY);
		ItemJson = Tools.CreateItemSeid(ItemID);
		int num3 = 0;
		int ShangXia = 0;
		List<CaiLiao> list = RandomCaiLiao(EquipQuality, TargetShuXing, ref ShangXia);
		JSONObject attackType = GetAttackType(list, EquipType);
		jSONObject2.Add(AddItemSeid(29, GetItemCD(LingWenID)));
		if (EquipQuality > 2)
		{
			if (LogFailInfo)
			{
				failInfoSB.AppendLine($"目标品质{EquipQuality}符合条件，尝试设置灵纹，灵纹ID{LingWenID}");
			}
			SetLingWenSeid(jSONObject2, jSONObject, LingWenID, EquipType);
		}
		if (LogFailInfo)
		{
			failInfoSB.AppendLine("开始计算材料");
		}
		Dictionary<int, int> dictionary = CalcCiTiao(list, EquipType, EquipQuality, LingWenID, Maker);
		if (dictionary.Count == 0)
		{
			failCount++;
			if (LogFailInfo)
			{
				failInfoSB.AppendLine("计算词条失败");
				LogFailInfo = false;
				Debug.LogError((object)failInfoSB.ToString());
			}
			else
			{
				CreateRandomEquip(ref ItemID, ref ItemJson, equipQuality, targetShuXing, equipType, lingWenType, lingWenID);
			}
			return;
		}
		JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
		foreach (int key in dictionary.Keys)
		{
			if (LogFailInfo)
			{
				failInfoSB.AppendLine($"dic遍历中，key:{key}");
			}
			string ciTiaoDesc = GetCiTiaoDesc(key, dictionary);
			if (ciTiaoDesc != "")
			{
				text += ciTiaoDesc;
				text += "\n";
			}
			int num4 = dictionary[key];
			if (key == 49)
			{
				int duoDuanIDByLingLi = GetDuoDuanIDByLingLi(num4);
				if (duoDuanIDByLingLi != 0)
				{
					JSONObject jSONObject3 = jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()];
					JSONObject jSONObject4 = new JSONObject();
					jSONObject4.SetField("id", jSONObject3["seid"].I);
					jSONObject4.SetField("value1", jSONObject3["value1"].I);
					jSONObject4.SetField("value2", jSONObject3["value2"].I);
					jSONObject4.SetField("value3", jSONObject3["value3"].I);
					jSONObject4.SetField("AttackType", GetAttackType(list, EquipType));
					jSONObject2.Add(jSONObject4);
				}
				continue;
			}
			if (lianQiHeCheng[key.ToString()]["seid"].I != 0)
			{
				JSONObject jSONObject5 = new JSONObject();
				jSONObject5.SetField("id", lianQiHeCheng[key.ToString()]["seid"].I);
				for (int i = 1; i < 3; i++)
				{
					int num5 = ((!lianQiHeCheng[key.ToString()]["fanbei"].HasItem(i)) ? 1 : num4);
					if (lianQiHeCheng[key.ToString()].HasField("intvalue" + i) && lianQiHeCheng[key.ToString()]["intvalue" + i].I != 0)
					{
						jSONObject5.SetField("value" + i, lianQiHeCheng[key.ToString()]["intvalue" + i].I * num5);
					}
				}
				for (int j = 1; j < 3; j++)
				{
					if (!lianQiHeCheng[key.ToString()].HasField("listvalue" + j) || lianQiHeCheng[key.ToString()]["listvalue" + j].list.Count == 0)
					{
						continue;
					}
					int num6 = ((!lianQiHeCheng[key.ToString()]["fanbei"].HasItem(j)) ? 1 : num4);
					JSONObject jSONObject6 = new JSONObject(JSONObject.Type.ARRAY);
					foreach (JSONObject item in lianQiHeCheng[key.ToString()]["listvalue" + j].list)
					{
						jSONObject6.Add(item.I * num6);
					}
					jSONObject5.SetField("value" + j, jSONObject6);
				}
				jSONObject2.Add(jSONObject5);
			}
			if (lianQiHeCheng[key.ToString()]["Itemseid"].I != 0)
			{
				JSONObject jSONObject7 = new JSONObject();
				int num7 = num4;
				jSONObject7.SetField("id", lianQiHeCheng[key.ToString()]["Itemseid"].I);
				JSONObject jSONObject8 = new JSONObject(JSONObject.Type.ARRAY);
				JSONObject jSONObject9 = new JSONObject(JSONObject.Type.ARRAY);
				for (int k = 0; k < lianQiHeCheng[key.ToString()]["Itemintvalue1"].Count; k++)
				{
					jSONObject8.Add(lianQiHeCheng[key.ToString()]["Itemintvalue1"][k].I);
					jSONObject9.Add(lianQiHeCheng[key.ToString()]["Itemintvalue2"][k].I * num7);
				}
				jSONObject7.SetField("value1", jSONObject8);
				jSONObject7.SetField("value2", jSONObject9);
				jSONObject.Add(jSONObject7);
			}
			num3 += lianQiHeCheng[key.ToString()]["HP"].I * num4;
		}
		if (LingWenType == 2 || LingWenType == 3)
		{
			string text2 = GetLingWenDesc(LingWenID);
			if (text2.Contains("造成<color=#fff227>x"))
			{
				text2 = text2.Replace("x", "");
			}
			text = text + text2 + "\n";
		}
		if (LingWenType == 4)
		{
			string text3 = GetLingWenDesc(LingWenID);
			if (EquipType != 1)
			{
				text3 = text3.Replace("使用", "生效");
			}
			text = text + text3 + "\n";
		}
		if (text.Contains("<color=#ff624d>"))
		{
			text = text.Replace("<color=#ff624d>", "");
		}
		if (text.Contains("<color=#fff227>"))
		{
			text = text.Replace("<color=#fff227>", "");
		}
		if (text.Contains("<color=#ff724d>"))
		{
			text = text.Replace("<color=#ff724d>", "");
		}
		if (text.Contains("<color=#f5e929>"))
		{
			text = text.Replace("<color=#f5e929>", "");
		}
		if (text.Contains("</color>"))
		{
			text = text.Replace("</color>", "");
		}
		ItemJson.AddField("SkillSeids", jSONObject2);
		if (jSONObject.list.Count > 0)
		{
			ItemJson.AddField("ItemSeids", jSONObject);
		}
		else if (num3 <= 0 && jSONObject2.list.Count < 2)
		{
			failCount++;
			if (LogFailInfo)
			{
				failInfoSB.AppendLine($"[随即装备]物品没有生成特性 {jSONObject2}");
				LogFailInfo = false;
				Debug.LogError((object)failInfoSB.ToString());
			}
			else
			{
				CreateRandomEquip(ref ItemID, ref ItemJson, equipQuality, targetShuXing, equipType, lingWenType, lingWenID);
			}
			return;
		}
		ItemJson.AddField("ItemID", ItemID);
		ItemJson.AddField("Name", RandomEquipName(EquipQuality, EquipType, num));
		ItemJson.AddField("SeidDesc", text);
		ItemJson.AddField("ItemIcon", NewGetEquipIconPath(EquipType, EquipQuality, attackType[0].I));
		if (num3 > 0)
		{
			ItemJson.AddField("Damage", num3);
		}
		ItemJson.AddField("quality", EquipQuality);
		ItemJson.AddField("QPingZhi", ShangXia);
		JSONObject jSONObject10 = new JSONObject();
		jSONObject10.Add(TargetShuXing);
		ItemJson.AddField("shuXingIdList", jSONObject10);
		ItemJson.AddField("qualitydesc", GetEquipQualityDesc(EquipQuality, ShangXia));
		ItemJson.AddField("Desc", GetEquipDesc(EquipType, EquipQuality, ShangXia, LingWenID, list, Maker));
		ItemJson.AddField("AttackType", attackType);
		ItemJson.AddField("Money", GetEquipPrice(EquipQuality, ShangXia));
		ItemJson.AddField("ItemFlag", GetEquipItemFlag(EquipType, EquipQuality, attackType));
		failCount = 0;
		LogFailInfo = false;
	}
}
