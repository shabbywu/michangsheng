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

namespace YSGame.EquipRandom
{
	// Token: 0x02000A85 RID: 2693
	public static class RandomEquip
	{
		// Token: 0x06004B80 RID: 19328 RVA: 0x00200712 File Offset: 0x001FE912
		private static void Init()
		{
			if (!RandomEquip.isInited)
			{
				RandomEquip.InitCaiLiaoData();
				RandomEquip.InitNameData();
				RandomEquip.InitHeChengBiaoData();
				RandomEquip.InitQualityLingLiData();
				RandomEquip.isInited = true;
			}
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00200738 File Offset: 0x001FE938
		private static void InitQualityLingLiData()
		{
			for (int i = 1; i <= 5; i++)
			{
				RandomEquip.QualityLingLiDict.Add(i, new Dictionary<int, int>());
				RandomEquip.QualityPriceDict.Add(i, new Dictionary<int, int>());
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.LianQiWuQiQuality)
			{
				RandomEquip.QualityLingLiDict[(int)keyValuePair.Value["quality"]].Add((int)keyValuePair.Value["shangxia"], (int)keyValuePair.Value["power"]);
				RandomEquip.QualityPriceDict[(int)keyValuePair.Value["quality"]].Add((int)keyValuePair.Value["shangxia"], (int)keyValuePair.Value["price"]);
			}
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00200858 File Offset: 0x001FEA58
		private static void InitHeChengBiaoData()
		{
			for (int i = 1; i <= 10; i++)
			{
				RandomEquip.HeChengBiaoDict.Add(i, new Dictionary<int, int>());
				RandomEquip.EquipTypeShuXingIDList.Add(i, new List<int>());
			}
			foreach (JSONObject jsonobject in jsonData.instance.LianQiHeCheng.list)
			{
				int i2 = jsonobject["zhonglei"].I;
				int i3 = jsonobject["ShuXingType"].I;
				RandomEquip.HeChengBiaoDict[i2].Add(i3, jsonobject["id"].I);
				RandomEquip.EquipTypeShuXingIDList[i2].Add(jsonobject["id"].I);
				RandomEquip.ShuXingCastDict.Add(jsonobject["id"].I, jsonobject["cast"].I);
				RandomEquip.ShuXingTypeDict.Add(jsonobject["id"].I, i3);
			}
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0020098C File Offset: 0x001FEB8C
		private static void InitNameData()
		{
			List<_FaBaoFirstNameJsonData> dataList = _FaBaoFirstNameJsonData.DataList;
			List<_FaBaoLastNameJsonData> dataList2 = _FaBaoLastNameJsonData.DataList;
			for (int i = 1; i <= 5; i++)
			{
				RandomEquip.FirstNameDict.Add(i, new Dictionary<int, List<_FaBaoFirstNameJsonData>>());
				RandomEquip.LastNameDict.Add(i, new Dictionary<int, List<_FaBaoLastNameJsonData>>());
			}
			foreach (_FaBaoFirstNameJsonData faBaoFirstNameJsonData in dataList)
			{
				for (int j = 0; j < faBaoFirstNameJsonData.quality.Count; j++)
				{
					for (int k = 0; k < faBaoFirstNameJsonData.Type.Count; k++)
					{
						int key = faBaoFirstNameJsonData.quality[j];
						int key2 = faBaoFirstNameJsonData.Type[k];
						string firstName = faBaoFirstNameJsonData.FirstName;
						if (!RandomEquip.FirstNameDict[key].ContainsKey(key2))
						{
							RandomEquip.FirstNameDict[key].Add(key2, new List<_FaBaoFirstNameJsonData>());
						}
						RandomEquip.FirstNameDict[key][key2].Add(faBaoFirstNameJsonData);
					}
				}
			}
			foreach (_FaBaoLastNameJsonData faBaoLastNameJsonData in dataList2)
			{
				for (int l = 0; l < faBaoLastNameJsonData.quality.Count; l++)
				{
					for (int m = 0; m < faBaoLastNameJsonData.Type.Count; m++)
					{
						int key3 = faBaoLastNameJsonData.quality[l];
						int key4 = faBaoLastNameJsonData.Type[m];
						string lastName = faBaoLastNameJsonData.LastName;
						if (!RandomEquip.LastNameDict[key3].ContainsKey(key4))
						{
							RandomEquip.LastNameDict[key3].Add(key4, new List<_FaBaoLastNameJsonData>());
						}
						RandomEquip.LastNameDict[key3][key4].Add(faBaoLastNameJsonData);
					}
				}
			}
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x00200BAC File Offset: 0x001FEDAC
		private static void InitCaiLiaoData()
		{
			JSONObject itemJsonData = jsonData.instance._ItemJsonData;
			for (int i = 1; i <= 6; i++)
			{
				RandomEquip.CaiLiaoDict.Add(i, new Dictionary<int, List<CaiLiao>>());
				RandomEquip.QualityShuXingCaiLiaoDict.Add(i, new Dictionary<int, List<JSONObject>>());
			}
			foreach (JSONObject jsonobject in itemJsonData.list)
			{
				if (jsonobject["type"].I == 8)
				{
					int i2 = jsonobject["ShuXingType"].I;
					int i3 = jsonobject["quality"].I;
					if (!RandomEquip.QualityShuXingCaiLiaoDict[i3].ContainsKey(i2))
					{
						RandomEquip.QualityShuXingCaiLiaoDict[i3].Add(i2, new List<JSONObject>());
					}
					if (!RandomEquip.CaiLiaoDict[i3].ContainsKey(i2))
					{
						RandomEquip.CaiLiaoDict[i3].Add(i2, new List<CaiLiao>());
					}
					RandomEquip.QualityShuXingCaiLiaoDict[i3][i2].Add(jsonobject);
					RandomEquip.CaiLiaoDict[i3][i2].Add(new CaiLiao(jsonobject));
				}
			}
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x00200D00 File Offset: 0x001FEF00
		private static int Range(int min, int max)
		{
			return RandomEquip.random.Next(min, max + 1);
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x00200D10 File Offset: 0x001FEF10
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

		// Token: 0x06004B87 RID: 19335 RVA: 0x00200D44 File Offset: 0x001FEF44
		private static JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
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

		// Token: 0x06004B88 RID: 19336 RVA: 0x00200D8C File Offset: 0x001FEF8C
		private static void SetLingWenSeid(JSONObject skillSeids, JSONObject itemSeid, int LingWenID, int equipType)
		{
			if (LingWenID == -1)
			{
				return;
			}
			JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()];
			if (jsonobject["type"].I == 1)
			{
				if (RandomEquip.LogFailInfo)
				{
					RandomEquip.failInfoSB.AppendLine("青龙直接返回");
				}
				return;
			}
			JSONObject jsonobject2 = new JSONObject();
			int num;
			if (equipType < 6)
			{
				num = 1;
			}
			else if (equipType < 8)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
			if (num == 1)
			{
				if (RandomEquip.LogFailInfo)
				{
					RandomEquip.failInfoSB.AppendLine("开始添加武器技能特性");
				}
				jsonobject2.SetField("id", jsonobject["seid"].I);
				if (jsonobject["seid"].I == 77)
				{
					jsonobject2.SetField("value1", jsonobject["listvalue1"]);
				}
				else if (jsonobject["seid"].I == 80)
				{
					jsonobject2.SetField("value1", jsonobject["listvalue1"]);
					jsonobject2.SetField("value2", jsonobject["listvalue2"]);
				}
				else if (jsonobject["seid"].I == 145)
				{
					jsonobject2.SetField("value1", jsonobject["listvalue1"][0]);
				}
				skillSeids.Add(jsonobject2);
				return;
			}
			if (num - 2 > 1)
			{
				return;
			}
			if (RandomEquip.LogFailInfo)
			{
				RandomEquip.failInfoSB.AppendLine("开始添加衣服和饰品(item)BUFF特性");
			}
			jsonobject2.SetField("id", jsonobject["Itemseid"].I);
			if (jsonobject["seid"].I == 62)
			{
				jsonobject2.SetField("value1", jsonobject["Itemintvalue1"]);
			}
			else
			{
				jsonobject2.SetField("value1", jsonobject["Itemintvalue1"]);
				jsonobject2.SetField("value2", jsonobject["Itemintvalue2"]);
			}
			itemSeid.Add(jsonobject2);
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x00200F74 File Offset: 0x001FF174
		private static string BuildNomalLingWenDesc(JSONObject obj)
		{
			string text = (obj["value3"].I == 1) ? "x" : "+";
			string result = "";
			switch (obj["type"].I)
			{
			case 1:
				result = string.Format("{0}<color=#fff227>x{1}</color>,灵力<color=#fff227>{2}{3}</color>", new object[]
				{
					obj["desc"].str,
					obj["value1"].I,
					text,
					obj["value4"].I
				});
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

		// Token: 0x06004B8A RID: 19338 RVA: 0x0020109C File Offset: 0x001FF29C
		private static string GetLingWenDesc(int LingWenID)
		{
			string result = "";
			if (LingWenID != -1)
			{
				JSONObject jsonobject = jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()];
				if (jsonobject["type"].I == 3)
				{
					result = Tools.Code64(jsonobject["desc"].str).Replace("获得", "获得<color=#ff624d>") + string.Format("</color>x{0}", jsonobject["value2"].I);
				}
				else
				{
					result = Regex.Split(RandomEquip.BuildNomalLingWenDesc(jsonobject), ",", RegexOptions.None)[0];
				}
			}
			return result;
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x00201140 File Offset: 0x001FF340
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

		// Token: 0x06004B8C RID: 19340 RVA: 0x002011EE File Offset: 0x001FF3EE
		private static string NewGetEquipIconPath(int EquipType, int Quality, int AttackType)
		{
			if (EquipType == -1)
			{
				return "";
			}
			return string.Format("LianQiIcon/MCS_fabao_{0}_{1}_{2}", RandomEquip.EquipTypePinYinList[EquipType - 1], AttackType, Quality);
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x00201220 File Offset: 0x001FF420
		private static JSONObject GetEquipItemFlag(int EquipType, int EquipQuality, JSONObject AttackType)
		{
			JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			int num;
			if (EquipType >= 1 && EquipType <= 5)
			{
				num = 1;
			}
			else if (EquipType >= 6 && EquipType < 8)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
			int num2 = num * 100 + EquipQuality;
			jsonobject.Add(num2);
			for (int i = 0; i < AttackType.Count; i++)
			{
				int val = num2 * 10 + AttackType[i].I;
				jsonobject.Add(val);
			}
			jsonobject.Add(num);
			return jsonobject;
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00201294 File Offset: 0x001FF494
		private static string GetEquipQualityDesc(int Quality, int ShangXia)
		{
			string result = "";
			string str = "";
			switch (ShangXia)
			{
			case 1:
				str = "下品";
				break;
			case 2:
				str = "中品";
				break;
			case 3:
				str = "上品";
				break;
			}
			switch (Quality)
			{
			case 1:
				result = str + "符器";
				break;
			case 2:
				result = str + "法器";
				break;
			case 3:
				result = str + "法宝";
				break;
			case 4:
				result = str + "纯阳法宝";
				break;
			case 5:
				result = str + "通天灵宝";
				break;
			}
			return result;
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0020133C File Offset: 0x001FF53C
		private static int RandomShuXingID()
		{
			JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
			return RandomEquip.Range(1, lianQiHeCheng.list.Count);
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x00201368 File Offset: 0x001FF568
		private static int RandomShuXingID(int EuqipType)
		{
			int index = RandomEquip.Range(0, RandomEquip.EquipTypeShuXingIDList[EuqipType].Count - 1);
			return RandomEquip.EquipTypeShuXingIDList[EuqipType][index];
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x0020139F File Offset: 0x001FF59F
		private static int GetItemIDByEquipType(int EquipType)
		{
			return 18000 + EquipType;
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x002013A8 File Offset: 0x001FF5A8
		private static int GetEquipTypeByShuXingID(int id)
		{
			return jsonData.instance.LianQiHeCheng[id.ToString()]["zhonglei"].I;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x002013D0 File Offset: 0x001FF5D0
		private static int RandomLingWenIDByLingWenType(int LingWenType, List<int> banAffix = null)
		{
			JSONObject lianQiLingWenBiao = jsonData.instance.LianQiLingWenBiao;
			List<int> list = new List<int>();
			foreach (JSONObject jsonobject in lianQiLingWenBiao.list)
			{
				if (jsonobject["type"].I == LingWenType)
				{
					if (banAffix != null)
					{
						List<int> list2 = jsonobject["Affix"].ToList();
						bool flag = false;
						foreach (int item in banAffix)
						{
							if (list2.Contains(item))
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
					list.Add(jsonobject["id"].I);
				}
			}
			return list[RandomEquip.Range(0, list.Count - 1)];
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x002014D0 File Offset: 0x001FF6D0
		private static JSONObject GetAttackType(List<CaiLiao> CaiLiaoList, int EquipType)
		{
			JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
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
						jsonobject2.Add(num2);
					}
					jsonobject.Add(num2);
				}
			}
			if (num > 0)
			{
				return jsonobject2;
			}
			return jsonobject;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x00201550 File Offset: 0x001FF750
		private static int GetEquipPrice(int EquipQuality, int ShangXia)
		{
			int result = 0;
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.LianQiWuQiQuality)
			{
				if ((int)keyValuePair.Value["quality"] == EquipQuality && (int)keyValuePair.Value["shangxia"] == ShangXia)
				{
					result = (int)keyValuePair.Value["price"];
				}
			}
			return result;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x002015E8 File Offset: 0x001FF7E8
		private static int GetDuoDuanIDByLingLi(int sum)
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

		// Token: 0x06004B97 RID: 19351 RVA: 0x0020164C File Offset: 0x001FF84C
		private static string GetCiTiaoDesc(int id, Dictionary<int, int> entryDictionary)
		{
			string text = "";
			if (id == 49)
			{
				int duoDuanIDByLingLi = RandomEquip.GetDuoDuanIDByLingLi(entryDictionary[id]);
				if (duoDuanIDByLingLi == 0)
				{
					return text;
				}
				return jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()]["desc"].str;
			}
			else
			{
				JSONObject jsonobject = jsonData.instance.LianQiHeCheng[id.ToString()];
				int num = entryDictionary[id];
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

		// Token: 0x06004B98 RID: 19352 RVA: 0x002017B4 File Offset: 0x001FF9B4
		private static List<CaiLiao> RandomCaiLiao(int EquipQuality, int TargetShuXing, ref int ShangXia)
		{
			List<CaiLiao> list = new List<CaiLiao>();
			int num = RandomEquip.ShuXingCastDict[TargetShuXing];
			int num2 = 0;
			foreach (KeyValuePair<string, JToken> keyValuePair in jsonData.instance.LianQiWuQiQuality)
			{
				if ((int)keyValuePair.Value["quality"] == EquipQuality && (int)keyValuePair.Value["shangxia"] == 1)
				{
					num2 = (int)keyValuePair.Value["power"];
					break;
				}
			}
			int num3 = 0;
			int num4 = RandomEquip.Range(0, 100);
			int num5;
			if (num4 < 10)
			{
				num5 = 2;
			}
			else if (num4 < 40)
			{
				num5 = 1;
			}
			else
			{
				num5 = 0;
			}
			int key = RandomEquip.ShuXingTypeDict[TargetShuXing];
			if (RandomEquip.CaiLiaoDict[EquipQuality][key].Count > 0)
			{
				int num6 = 0;
				int num7 = 0;
				int num8 = 0;
				while (list.Count < 10 || num6 < num || num6 < num2)
				{
					if (list.Count >= 10)
					{
						num7 = 1;
						if (RandomEquip.LogFailInfo)
						{
							RandomEquip.failInfoSB.AppendLine("[随机材料]灵力不够，去除一个材料重新随机");
						}
						num3 -= list[0].LingLi;
						list.RemoveAt(0);
						num8++;
						if (num8 > 100)
						{
							if (RandomEquip.LogFailInfo)
							{
								RandomEquip.failInfoSB.AppendLine("[随机材料]超过100次随机无法完成保底");
								break;
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
						if (RandomEquip.Range(0, 1) == 0)
						{
							num9 = EquipQuality + 1;
						}
						else
						{
							num9 = EquipQuality;
						}
					}
					if (num3 >= num)
					{
						if (num5 == 0)
						{
							num9--;
						}
						else if (num5 == 1 && RandomEquip.Range(0, 1) == 0)
						{
							num9--;
						}
					}
					num9 += num7;
					num9 = Mathf.Clamp(num9, 1, 6);
					int index = RandomEquip.Range(0, RandomEquip.CaiLiaoDict[num9][key].Count - 1);
					CaiLiao caiLiao = RandomEquip.CaiLiaoDict[num9][key][index];
					list.Add(RandomEquip.CaiLiaoDict[num9][key][index]);
					if (RandomEquip.LogFailInfo)
					{
						RandomEquip.failInfoSB.AppendLine(string.Format("[随机材料]添加了材料{0}", caiLiao));
					}
					num3 += caiLiao.LingLi;
					num6 = (int)(RandomEquip.CalcWuWeiBaiFenBi(list) * (float)num3);
					if (RandomEquip.LogFailInfo)
					{
						RandomEquip.failInfoSB.AppendLine(string.Format("[随机材料]当前材料削减后总灵力:{0}", num6));
					}
				}
			}
			int num10 = (int)(RandomEquip.CalcWuWeiBaiFenBi(list) * (float)num3);
			foreach (KeyValuePair<string, JToken> keyValuePair2 in jsonData.instance.LianQiWuQiQuality)
			{
				if (num10 < (int)keyValuePair2.Value["power"])
				{
					break;
				}
				ShangXia = (int)keyValuePair2.Value["shangxia"];
			}
			return list;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x00201AD4 File Offset: 0x001FFCD4
		public static string RandomEquipName(int EquipQuality, int EquipType, int ShuXingType)
		{
			return RandomEquip.BetterRandomEquipName(EquipQuality, EquipType, ShuXingType);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00201AE0 File Offset: 0x001FFCE0
		public static string NormalRandomEquipName(int EquipQuality, int EquipType, int ShuXingType)
		{
			List<_FaBaoFirstNameJsonData> list = RandomEquip.FirstNameDict[EquipQuality][ShuXingType];
			List<_FaBaoLastNameJsonData> list2 = RandomEquip.LastNameDict[EquipQuality][EquipType];
			int index = RandomEquip.Range(0, list.Count - 1);
			string firstName = list[index].FirstName;
			int index2 = RandomEquip.Range(0, list2.Count - 1);
			string lastName = list2[index2].LastName;
			return firstName + lastName + RandomEquip.EquipTypeNameList[EquipType - 1];
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x00201B60 File Offset: 0x001FFD60
		public static string BetterRandomEquipName(int EquipQuality, int EquipType, int ShuXingType)
		{
			string result;
			try
			{
				List<_FaBaoFirstNameJsonData> list = RandomEquip.FirstNameDict[EquipQuality][ShuXingType];
				List<_FaBaoLastNameJsonData> list2 = RandomEquip.LastNameDict[EquipQuality][EquipType];
				int index = RandomEquip.Range(0, list.Count - 1);
				_FaBaoFirstNameJsonData faBaoFirstNameJsonData = list[index];
				int index2 = RandomEquip.Range(0, list2.Count - 1);
				_FaBaoLastNameJsonData faBaoLastNameJsonData = list2[index2];
				string text;
				if (faBaoFirstNameJsonData.PosReverse == 1 || faBaoLastNameJsonData.PosReverse == 1)
				{
					text = faBaoLastNameJsonData.LastName + faBaoFirstNameJsonData.FirstName + RandomEquip.EquipTypeNameList[EquipType - 1];
				}
				else
				{
					text = faBaoFirstNameJsonData.FirstName + faBaoLastNameJsonData.LastName + RandomEquip.EquipTypeNameList[EquipType - 1];
				}
				result = text;
			}
			catch
			{
				string text2 = string.Format("错误{0}_{1}_{2}", EquipQuality, EquipType, ShuXingType);
				Debug.LogError(string.Format("炼器随机名字失败，目标装备品质{0} 目标装备类型{1} 目标装备属性{2}", EquipQuality, EquipType, ShuXingType));
				result = text2;
			}
			return result;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x00201C74 File Offset: 0x001FFE74
		public static void TestLogAllBetterRandomEquipName()
		{
			RandomEquip.Init();
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			List<string> list = new List<string>();
			for (int i = 1; i <= 5; i++)
			{
				stringBuilder.AppendLine(string.Format("[装备品质.{0}↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓]", i));
				List<int> list2 = RandomEquip.LastNameDict[i].Keys.ToList<int>();
				List<int> list3 = RandomEquip.FirstNameDict[i].Keys.ToList<int>();
				int num2 = 0;
				for (int j = 0; j < list2.Count; j++)
				{
					int num3 = list2[j];
					string text = RandomEquip.EquipTypeNameList[num3 - 1];
					int num4 = 0;
					for (int k = 0; k < list3.Count; k++)
					{
						int num5 = list3[k];
						StringBuilder stringBuilder2 = new StringBuilder();
						int num6 = 0;
						foreach (_FaBaoFirstNameJsonData faBaoFirstNameJsonData in RandomEquip.FirstNameDict[i][num5])
						{
							foreach (_FaBaoLastNameJsonData faBaoLastNameJsonData in RandomEquip.LastNameDict[i][num3])
							{
								string text2;
								if (faBaoFirstNameJsonData.PosReverse == 1 || faBaoLastNameJsonData.PosReverse == 1)
								{
									text2 = faBaoLastNameJsonData.LastName + faBaoFirstNameJsonData.FirstName + text;
								}
								else
								{
									text2 = faBaoFirstNameJsonData.FirstName + faBaoLastNameJsonData.LastName + text;
								}
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
						stringBuilder.AppendLine(string.Format("[装备属性.{0}.有{1}个名字组合]", num5, num6));
					}
					stringBuilder.AppendLine(string.Format("[装备类型.{0}.有{1}个名字组合]", text, num4));
				}
				stringBuilder.AppendLine(string.Format("[装备品质.{0}.有{1}个名字组合]\n", i, num2));
			}
			stopwatch.Stop();
			stringBuilder.AppendLine(string.Format("遍历完毕，全局一共有{0}个名字组合，{1}个不重复组合，遍历耗时{2}ms", num, list.Count, stopwatch.ElapsedMilliseconds));
			File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/装备随机名遍历结果.log", stringBuilder.ToString());
			stringBuilder.Clear();
			foreach (string str in list)
			{
				stringBuilder.Append(str + " ");
			}
			stringBuilder.AppendLine(string.Format("\n共有{0}个不重复组合", list.Count));
			File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/装备随机名遍历结果_不重复.log", stringBuilder.ToString());
			Debug.Log(string.Format("遍历完毕，全局一共有{0}个名字组合，{1}个不重复组合，遍历耗时{2}ms，日志已输出到桌面", num, list.Count, stopwatch.ElapsedMilliseconds));
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00201FF8 File Offset: 0x002001F8
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

		// Token: 0x06004B9E RID: 19358 RVA: 0x00202044 File Offset: 0x00200244
		private static Dictionary<int, int> CalcCiTiao(List<CaiLiao> data, int EquipType, int EquipQuality, int lingWenID, Avatar Maker)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			if (RandomEquip.LogFailInfo)
			{
				string text = "材料列表:\n";
				for (int i = 0; i < data.Count; i++)
				{
					text += string.Format("{0}\n", data[i]);
				}
				RandomEquip.failInfoSB.AppendLine(text);
			}
			for (int j = 0; j < data.Count; j++)
			{
				int num = data[j].ShuXingID(EquipType);
				if (num != 0)
				{
					if (num >= 49 && num <= 56)
					{
						if (dictionary.ContainsKey(49))
						{
							Dictionary<int, int> dictionary2 = dictionary;
							dictionary2[49] = dictionary2[49] + data[j].LingLi;
							if (RandomEquip.LogFailInfo)
							{
								RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料对entryDictionary[49]增加了{1}点灵力，目前有{2}灵力", j, data[j].LingLi, dictionary[49]));
							}
						}
						else
						{
							dictionary.Add(49, data[j].LingLi);
							if (RandomEquip.LogFailInfo)
							{
								RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料添加entryDictionary，key:49", j));
							}
						}
					}
					else if (num >= 1 && num <= 8)
					{
						if (dictionary.ContainsKey(1))
						{
							Dictionary<int, int> dictionary2 = dictionary;
							dictionary2[1] = dictionary2[1] + data[j].LingLi;
							if (RandomEquip.LogFailInfo)
							{
								RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料对entryDictionary[1]增加了{1}点灵力，目前有{2}灵力", j, data[j].LingLi, dictionary[1]));
							}
						}
						else
						{
							dictionary.Add(1, data[j].LingLi);
							if (RandomEquip.LogFailInfo)
							{
								RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料添加entryDictionary，key:1", j));
							}
						}
					}
					else if (dictionary.ContainsKey(num))
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int key = num;
						dictionary2[key] += data[j].LingLi;
						if (RandomEquip.LogFailInfo)
						{
							RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料对entryDictionary[{1}]增加了{2}点灵力，目前有{3}灵力", new object[]
							{
								j,
								num,
								data[j].LingLi,
								dictionary[num]
							}));
						}
					}
					else
					{
						dictionary.Add(num, data[j].LingLi);
						if (RandomEquip.LogFailInfo)
						{
							RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料添加entryDictionary，key:{1}", j, num));
						}
					}
				}
				else if (RandomEquip.LogFailInfo)
				{
					RandomEquip.failInfoSB.AppendLine(string.Format("{0}号材料属性ID为0", j));
				}
			}
			float num2 = RandomEquip.CalcWuWeiBaiFenBi(data);
			if (RandomEquip.LogFailInfo)
			{
				RandomEquip.failInfoSB.AppendLine(string.Format("五维削减百分比{0}", num2));
			}
			int num3 = -1;
			float num4 = -1f;
			bool flag = false;
			if (EquipQuality > 2)
			{
				flag = (RandomEquip.Range(0, 1000) % 20 == 0);
			}
			if (lingWenID > 0)
			{
				num3 = jsonData.instance.LianQiLingWenBiao[lingWenID.ToString()]["value3"].I;
				num4 = jsonData.instance.LianQiLingWenBiao[lingWenID.ToString()]["value4"].n;
			}
			JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
			Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
			foreach (int num5 in dictionary.Keys)
			{
				int num6 = dictionary[num5];
				num6 = (int)((float)num6 * num2);
				if (num5 == 49)
				{
					if (num6 >= jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I)
					{
						dictionary3.Add(num5, num6);
						if (RandomEquip.LogFailInfo)
						{
							RandomEquip.failInfoSB.AppendLine(string.Format("{0}达标，已有{1}，需求{2}", num5, num6, jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I));
						}
					}
					else if (RandomEquip.LogFailInfo)
					{
						RandomEquip.failInfoSB.AppendLine(string.Format("{0}不达标，已有{1}，需求{2}", num5, num6, jsonData.instance.LianQiDuoDuanShangHaiBiao["1"]["cast"].I));
					}
				}
				else
				{
					int i2 = lianQiHeCheng[num5.ToString()]["cast"].I;
					if (num6 >= i2)
					{
						dictionary3.Add(num5, num6);
						if (RandomEquip.LogFailInfo)
						{
							RandomEquip.failInfoSB.AppendLine(string.Format("{0}达标，已有{1}，需求{2}", num5, num6, i2));
						}
					}
					else if (RandomEquip.LogFailInfo)
					{
						RandomEquip.failInfoSB.AppendLine(string.Format("{0}不达标，已有{1}，需求{2}", num5, num6, i2));
					}
				}
			}
			if (dictionary3.Keys.Count == 0)
			{
				if (RandomEquip.LogFailInfo)
				{
					RandomEquip.failInfoSB.AppendLine("没有达标材料");
				}
				return dictionary3;
			}
			if (RandomEquip.LogFailInfo)
			{
				RandomEquip.failInfoSB.AppendLine("开始计算增幅");
			}
			Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
			foreach (int key2 in dictionary3.Keys)
			{
				int num7 = dictionary3[key2];
				if (lingWenID > 0)
				{
					if (num3 == 1)
					{
						num7 = (int)((float)num7 * num4);
					}
					else
					{
						num4 /= (float)dictionary3.Keys.Count;
						num7 = (int)((float)num7 + num4);
					}
				}
				if (flag)
				{
					num7 = (int)((float)num7 * 1.5f);
				}
				int i3 = lianQiHeCheng[key2.ToString()]["cast"].I;
				int value = num7 / i3;
				dictionary4.Add(key2, value);
			}
			return dictionary4;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x002026FC File Offset: 0x002008FC
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
			string equipQualityDesc = RandomEquip.GetEquipQualityDesc(EquipQuality, ShangXia);
			string text4 = RandomEquip.EquipTypeFullNameList[EquipType - 1];
			string text5;
			if (LingWenID != -1)
			{
				text5 = jsonData.instance.LianQiLingWenBiao[LingWenID.ToString()]["name"].Str + "灵纹,灵力更加强大，但对使用者也要求更高。";
			}
			else
			{
				text5 = "聚灵灵纹。";
			}
			string result;
			if (Maker == null)
			{
				result = string.Concat(new string[]
				{
					"不知何人将",
					text3,
					"等材料炼制的",
					equipQualityDesc,
					"，此",
					text4,
					"铭刻",
					text5
				});
			}
			else
			{
				result = string.Concat(new string[]
				{
					text,
					"于",
					text2,
					"年将",
					text3,
					"等材料炼制的",
					equipQualityDesc,
					"，此",
					text4,
					"铭刻",
					text5
				});
			}
			return result;
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00202878 File Offset: 0x00200A78
		public static int FindShuXingIDByEquipTypeAndShuXingType(int ShuXingType, int EquipType)
		{
			return RandomEquip.HeChengBiaoDict[EquipType][ShuXingType];
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0020288C File Offset: 0x00200A8C
		public static void CreateRandomEquip(ref int ItemID, ref JSONObject ItemJson, int EquipQuality = -1, int TargetShuXing = -1, int EquipType = -1, int LingWenType = -1, int LingWenID = -1, Avatar Maker = null)
		{
			if (RandomEquip.failCount >= 10)
			{
				RandomEquip.failInfoSB = new StringBuilder();
				RandomEquip.failInfoSB.AppendLine(string.Format("随机装备生成连续失败超过10次，请报告程序检查。目标品质:{0} 目标属性:{1} 目标装备类型:{2} 目标灵纹类型:{3} 目标灵纹ID:{4}", new object[]
				{
					EquipQuality,
					TargetShuXing,
					EquipType,
					LingWenType,
					LingWenID
				}));
				RandomEquip.LogFailInfo = true;
			}
			RandomEquip.Init();
			int equipQuality = EquipQuality;
			int targetShuXing = TargetShuXing;
			int equipType = EquipType;
			int lingWenType = LingWenType;
			int lingWenID = LingWenID;
			if (EquipQuality == -1)
			{
				EquipQuality = RandomEquip.Range(1, 5);
			}
			if (TargetShuXing != -1)
			{
				EquipType = RandomEquip.GetEquipTypeByShuXingID(TargetShuXing);
			}
			if (EquipType == -1)
			{
				TargetShuXing = RandomEquip.RandomShuXingID();
				EquipType = RandomEquip.GetEquipTypeByShuXingID(TargetShuXing);
			}
			else if (TargetShuXing == -1)
			{
				TargetShuXing = RandomEquip.RandomShuXingID(EquipType);
			}
			int num = jsonData.instance.LianQiHeCheng[TargetShuXing.ToString()]["ShuXingType"].I;
			if (EquipQuality < 2 && num % 2 == 0)
			{
				num--;
				TargetShuXing = RandomEquip.FindShuXingIDByEquipTypeAndShuXingType(num, EquipType);
			}
			ItemID = RandomEquip.GetItemIDByEquipType(EquipType);
			if (EquipQuality > 2)
			{
				if (LingWenID == -1)
				{
					if (LingWenType == -1)
					{
						if (EquipType < 6)
						{
							LingWenType = RandomEquip.Range(1, 3);
						}
						else
						{
							LingWenType = RandomEquip.Range(2, 3);
						}
					}
					int num2 = (int)jsonData.instance.LianQiEquipType[EquipType.ToString()]["zhonglei"];
					if (LingWenType == 3 && num2 == 2)
					{
						LingWenID = RandomEquip.RandomLingWenIDByLingWenType(LingWenType, new List<int>
						{
							8
						});
					}
					else
					{
						LingWenID = RandomEquip.RandomLingWenIDByLingWenType(LingWenType, null);
					}
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
			JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.ARRAY);
			ItemJson = Tools.CreateItemSeid(ItemID);
			int num3 = 0;
			int num4 = 0;
			List<CaiLiao> list = RandomEquip.RandomCaiLiao(EquipQuality, TargetShuXing, ref num4);
			JSONObject attackType = RandomEquip.GetAttackType(list, EquipType);
			jsonobject2.Add(RandomEquip.AddItemSeid(29, RandomEquip.GetItemCD(LingWenID), -9999));
			if (EquipQuality > 2)
			{
				if (RandomEquip.LogFailInfo)
				{
					RandomEquip.failInfoSB.AppendLine(string.Format("目标品质{0}符合条件，尝试设置灵纹，灵纹ID{1}", EquipQuality, LingWenID));
				}
				RandomEquip.SetLingWenSeid(jsonobject2, jsonobject, LingWenID, EquipType);
			}
			if (RandomEquip.LogFailInfo)
			{
				RandomEquip.failInfoSB.AppendLine("开始计算材料");
			}
			Dictionary<int, int> dictionary = RandomEquip.CalcCiTiao(list, EquipType, EquipQuality, LingWenID, Maker);
			if (dictionary.Count != 0)
			{
				JSONObject lianQiHeCheng = jsonData.instance.LianQiHeCheng;
				foreach (int num5 in dictionary.Keys)
				{
					if (RandomEquip.LogFailInfo)
					{
						RandomEquip.failInfoSB.AppendLine(string.Format("dic遍历中，key:{0}", num5));
					}
					string ciTiaoDesc = RandomEquip.GetCiTiaoDesc(num5, dictionary);
					if (ciTiaoDesc != "")
					{
						text += ciTiaoDesc;
						text += "\n";
					}
					int num6 = dictionary[num5];
					if (num5 == 49)
					{
						int duoDuanIDByLingLi = RandomEquip.GetDuoDuanIDByLingLi(num6);
						if (duoDuanIDByLingLi != 0)
						{
							JSONObject jsonobject3 = jsonData.instance.LianQiDuoDuanShangHaiBiao[duoDuanIDByLingLi.ToString()];
							JSONObject jsonobject4 = new JSONObject();
							jsonobject4.SetField("id", jsonobject3["seid"].I);
							jsonobject4.SetField("value1", jsonobject3["value1"].I);
							jsonobject4.SetField("value2", jsonobject3["value2"].I);
							jsonobject4.SetField("value3", jsonobject3["value3"].I);
							jsonobject4.SetField("AttackType", RandomEquip.GetAttackType(list, EquipType));
							jsonobject2.Add(jsonobject4);
						}
					}
					else
					{
						if (lianQiHeCheng[num5.ToString()]["seid"].I != 0)
						{
							JSONObject jsonobject5 = new JSONObject();
							jsonobject5.SetField("id", lianQiHeCheng[num5.ToString()]["seid"].I);
							for (int i = 1; i < 3; i++)
							{
								int num7 = lianQiHeCheng[num5.ToString()]["fanbei"].HasItem(i) ? num6 : 1;
								if (lianQiHeCheng[num5.ToString()].HasField("intvalue" + i) && lianQiHeCheng[num5.ToString()]["intvalue" + i].I != 0)
								{
									jsonobject5.SetField("value" + i, lianQiHeCheng[num5.ToString()]["intvalue" + i].I * num7);
								}
							}
							for (int j = 1; j < 3; j++)
							{
								if (lianQiHeCheng[num5.ToString()].HasField("listvalue" + j) && lianQiHeCheng[num5.ToString()]["listvalue" + j].list.Count != 0)
								{
									int num8 = lianQiHeCheng[num5.ToString()]["fanbei"].HasItem(j) ? num6 : 1;
									JSONObject jsonobject6 = new JSONObject(JSONObject.Type.ARRAY);
									foreach (JSONObject jsonobject7 in lianQiHeCheng[num5.ToString()]["listvalue" + j].list)
									{
										jsonobject6.Add(jsonobject7.I * num8);
									}
									jsonobject5.SetField("value" + j, jsonobject6);
								}
							}
							jsonobject2.Add(jsonobject5);
						}
						if (lianQiHeCheng[num5.ToString()]["Itemseid"].I != 0)
						{
							JSONObject jsonobject8 = new JSONObject();
							int num9 = num6;
							jsonobject8.SetField("id", lianQiHeCheng[num5.ToString()]["Itemseid"].I);
							JSONObject jsonobject9 = new JSONObject(JSONObject.Type.ARRAY);
							JSONObject jsonobject10 = new JSONObject(JSONObject.Type.ARRAY);
							for (int k = 0; k < lianQiHeCheng[num5.ToString()]["Itemintvalue1"].Count; k++)
							{
								jsonobject9.Add(lianQiHeCheng[num5.ToString()]["Itemintvalue1"][k].I);
								jsonobject10.Add(lianQiHeCheng[num5.ToString()]["Itemintvalue2"][k].I * num9);
							}
							jsonobject8.SetField("value1", jsonobject9);
							jsonobject8.SetField("value2", jsonobject10);
							jsonobject.Add(jsonobject8);
						}
						num3 += lianQiHeCheng[num5.ToString()]["HP"].I * num6;
					}
				}
				if (LingWenType == 2 || LingWenType == 3)
				{
					string text2 = RandomEquip.GetLingWenDesc(LingWenID);
					if (text2.Contains("造成<color=#fff227>x"))
					{
						text2 = text2.Replace("x", "");
					}
					text = text + text2 + "\n";
				}
				if (LingWenType == 4)
				{
					string text3 = RandomEquip.GetLingWenDesc(LingWenID);
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
				ItemJson.AddField("SkillSeids", jsonobject2);
				if (jsonobject.list.Count > 0)
				{
					ItemJson.AddField("ItemSeids", jsonobject);
				}
				else if (num3 <= 0 && jsonobject2.list.Count < 2)
				{
					RandomEquip.failCount++;
					if (RandomEquip.LogFailInfo)
					{
						RandomEquip.failInfoSB.AppendLine(string.Format("[随即装备]物品没有生成特性 {0}", jsonobject2));
						RandomEquip.LogFailInfo = false;
						Debug.LogError(RandomEquip.failInfoSB.ToString());
						return;
					}
					RandomEquip.CreateRandomEquip(ref ItemID, ref ItemJson, equipQuality, targetShuXing, equipType, lingWenType, lingWenID, null);
					return;
				}
				ItemJson.AddField("ItemID", ItemID);
				ItemJson.AddField("Name", RandomEquip.RandomEquipName(EquipQuality, EquipType, num));
				ItemJson.AddField("SeidDesc", text);
				ItemJson.AddField("ItemIcon", RandomEquip.NewGetEquipIconPath(EquipType, EquipQuality, attackType[0].I));
				if (num3 > 0)
				{
					ItemJson.AddField("Damage", num3);
				}
				ItemJson.AddField("quality", EquipQuality);
				ItemJson.AddField("QPingZhi", num4);
				JSONObject jsonobject11 = new JSONObject();
				jsonobject11.Add(TargetShuXing);
				ItemJson.AddField("shuXingIdList", jsonobject11);
				ItemJson.AddField("qualitydesc", RandomEquip.GetEquipQualityDesc(EquipQuality, num4));
				ItemJson.AddField("Desc", RandomEquip.GetEquipDesc(EquipType, EquipQuality, num4, LingWenID, list, Maker));
				ItemJson.AddField("AttackType", attackType);
				ItemJson.AddField("Money", RandomEquip.GetEquipPrice(EquipQuality, num4));
				ItemJson.AddField("ItemFlag", RandomEquip.GetEquipItemFlag(EquipType, EquipQuality, attackType));
				RandomEquip.failCount = 0;
				RandomEquip.LogFailInfo = false;
				return;
			}
			RandomEquip.failCount++;
			if (RandomEquip.LogFailInfo)
			{
				RandomEquip.failInfoSB.AppendLine("计算词条失败");
				RandomEquip.LogFailInfo = false;
				Debug.LogError(RandomEquip.failInfoSB.ToString());
				return;
			}
			RandomEquip.CreateRandomEquip(ref ItemID, ref ItemJson, equipQuality, targetShuXing, equipType, lingWenType, lingWenID, null);
		}

		// Token: 0x04004A9C RID: 19100
		private static int failCount;

		// Token: 0x04004A9D RID: 19101
		private static bool LogFailInfo;

		// Token: 0x04004A9E RID: 19102
		private static StringBuilder failInfoSB;

		// Token: 0x04004A9F RID: 19103
		private static bool isInited = false;

		// Token: 0x04004AA0 RID: 19104
		private static Dictionary<int, Dictionary<int, int>> QualityLingLiDict = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04004AA1 RID: 19105
		private static Dictionary<int, Dictionary<int, int>> QualityPriceDict = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04004AA2 RID: 19106
		private static Dictionary<int, Dictionary<int, int>> HeChengBiaoDict = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x04004AA3 RID: 19107
		private static Dictionary<int, List<int>> EquipTypeShuXingIDList = new Dictionary<int, List<int>>();

		// Token: 0x04004AA4 RID: 19108
		private static Dictionary<int, int> ShuXingCastDict = new Dictionary<int, int>();

		// Token: 0x04004AA5 RID: 19109
		private static Dictionary<int, int> ShuXingTypeDict = new Dictionary<int, int>();

		// Token: 0x04004AA6 RID: 19110
		private static Dictionary<int, Dictionary<int, List<_FaBaoFirstNameJsonData>>> FirstNameDict = new Dictionary<int, Dictionary<int, List<_FaBaoFirstNameJsonData>>>();

		// Token: 0x04004AA7 RID: 19111
		private static Dictionary<int, Dictionary<int, List<_FaBaoLastNameJsonData>>> LastNameDict = new Dictionary<int, Dictionary<int, List<_FaBaoLastNameJsonData>>>();

		// Token: 0x04004AA8 RID: 19112
		private static List<string> EquipTypeNameList = new List<string>
		{
			"剑",
			"钟",
			"环",
			"针",
			"匣",
			"袍",
			"甲",
			"珠",
			"令",
			"印"
		};

		// Token: 0x04004AA9 RID: 19113
		private static List<string> EquipTypeFullNameList = new List<string>
		{
			"剑",
			"钟",
			"环",
			"飞针",
			"匣",
			"法袍",
			"甲胄",
			"珠",
			"令",
			"印"
		};

		// Token: 0x04004AAA RID: 19114
		private static List<string> EquipTypePinYinList = new List<string>
		{
			"jian",
			"zhong",
			"huan",
			"zhen",
			"xia",
			"pao",
			"jia",
			"zhu",
			"ling",
			"yin"
		};

		// Token: 0x04004AAB RID: 19115
		private static Dictionary<int, Dictionary<int, List<JSONObject>>> QualityShuXingCaiLiaoDict = new Dictionary<int, Dictionary<int, List<JSONObject>>>();

		// Token: 0x04004AAC RID: 19116
		private static Dictionary<int, Dictionary<int, List<CaiLiao>>> CaiLiaoDict = new Dictionary<int, Dictionary<int, List<CaiLiao>>>();

		// Token: 0x04004AAD RID: 19117
		private static Random random = new Random();
	}
}
