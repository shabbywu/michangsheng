using System.Collections.Generic;
using GUIPackage;
using UnityEngine;

namespace YSGame.TuJian;

public static class TuJianDB
{
	public static bool _IsInited;

	public static Dictionary<string, Sprite> TuJianUISprite = new Dictionary<string, Sprite>();

	public static Dictionary<string, Sprite> TuJianRichTextSprite = new Dictionary<string, Sprite>();

	public static Dictionary<int, List<Dictionary<int, string>>> ItemTuJianFilterData = new Dictionary<int, List<Dictionary<int, string>>>();

	public static List<Dictionary<int, string>> RuleTuJianTypeNameData = new List<Dictionary<int, string>>();

	public static Dictionary<int, string> RuleTuJianTypeDescData = new Dictionary<int, string>();

	public static Dictionary<int, Dictionary<int, string>> RuleTuJianTypeChildDescData = new Dictionary<int, Dictionary<int, string>>();

	public static Dictionary<int, bool> RuleTuJianTypeDoubleSVData = new Dictionary<int, bool>();

	public static Dictionary<int, bool> RuleTuJianTypeHasChildData = new Dictionary<int, bool>();

	public static Dictionary<int, List<Dictionary<int, string>>> RuleTuJianFilterData = new Dictionary<int, List<Dictionary<int, string>>>();

	private static Dictionary<int, string> _LQWuWeiTypeName = new Dictionary<int, string>();

	private static Dictionary<int, string> _LQShuXingTypeName = new Dictionary<int, string>();

	private static Dictionary<string, string> _MapIDNameDict = new Dictionary<string, string>();

	private static Dictionary<string, int> _MapIDHighlightDict = new Dictionary<string, int>();

	public static Dictionary<int, DoubleItem> RuleDoubleData = new Dictionary<int, DoubleItem>();

	public static Dictionary<int, List<int>> RuleCiZhuiIndexData = new Dictionary<int, List<int>>();

	public static Dictionary<int, List<int>> RuleDoubleIndexData = new Dictionary<int, List<int>>();

	private static Dictionary<string, string> strTextData = new Dictionary<string, string>();

	private static JSONObject _zhonglei;

	private static JSONObject _chunwenben;

	private static JSONObject _tupianwenzi;

	private static JSONObject _zixiangtupianwenzi;

	private static JSONObject _yaoshou;

	private static List<Sprite> _ItemIconSpriteList = new List<Sprite>();

	private static List<Texture2D> _ItemIconTexList = new List<Texture2D>();

	private static List<Sprite> _ItemQualitySpriteList = new List<Sprite>();

	private static List<Texture2D> _ItemQualityTexList = new List<Texture2D>();

	private static List<Sprite> _ItemQualityUpSpriteList = new List<Sprite>();

	private static Dictionary<int, int> _ItemIconSpriteIndexDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _ItemQualitySpriteIndexDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _ItemQualityUpSpriteIndexDict = new Dictionary<int, int>();

	public static Dictionary<int, List<int>> YaoShouCaiLiaoChanChuData = new Dictionary<int, List<int>>();

	public static Dictionary<int, List<int>> YaoShouChanChuData = new Dictionary<int, List<int>>();

	public static string[] LevelNames = new string[5] { "炼气期", "筑基期", "金丹期", "元婴期", "化神期" };

	public static Dictionary<int, string> YaoShouLevelNameData = new Dictionary<int, string>();

	public static Dictionary<int, string> YaoShouQiXiMapData = new Dictionary<int, string>();

	public static Dictionary<int, string> YaoShouDescData = new Dictionary<int, string>();

	public static Dictionary<int, string> YaoShouNameData = new Dictionary<int, string>();

	private static Dictionary<int, string> YaoShouFacePathData = new Dictionary<int, string>();

	private static Dictionary<int, Sprite> YaoShouFaceSpriteData = new Dictionary<int, Sprite>();

	public static Dictionary<int, string> ShenTongMiShuNameData = new Dictionary<int, string>();

	public static Dictionary<int, string> ShenTongMiShuShuXingData = new Dictionary<int, string>();

	public static Dictionary<int, string> ShenTongMiShuDesc1Data = new Dictionary<int, string>();

	public static Dictionary<int, int> ShenTongMiShuQualityData = new Dictionary<int, int>();

	public static Dictionary<int, List<string>> ShenTongDesc2Data = new Dictionary<int, List<string>>();

	public static Dictionary<int, string> MiShuDesc2Data = new Dictionary<int, string>();

	public static Dictionary<int, List<int>> ShenTongMiShuCastData = new Dictionary<int, List<int>>();

	public static Dictionary<int, string> ShenTongMiShuPinJiData = new Dictionary<int, string>();

	public static Dictionary<int, string> GongFaNameData = new Dictionary<int, string>();

	public static Dictionary<int, string> GongFaPinJiData = new Dictionary<int, string>();

	public static Dictionary<int, string> GongFaShuXingData = new Dictionary<int, string>();

	public static Dictionary<int, int> GongFaSpeedData = new Dictionary<int, int>();

	public static Dictionary<int, string> GongFaDesc1Data = new Dictionary<int, string>();

	public static Dictionary<int, List<string>> GongFaDesc2Data = new Dictionary<int, List<string>>();

	public static Dictionary<int, int> GongFaQualityData = new Dictionary<int, int>();

	private static Dictionary<int, Sprite> _ShenTongMiShuSpriteData = new Dictionary<int, Sprite>();

	private static Dictionary<int, Sprite> _GongFaSpriteData = new Dictionary<int, Sprite>();

	private static Dictionary<int, Sprite> _SkillQualitySpriteData = new Dictionary<int, Sprite>();

	public static Dictionary<int, DanFangData> DanFangDataDict = new Dictionary<int, DanFangData>();

	public static Dictionary<int, string> YaoCaoTypeData = new Dictionary<int, string>();

	public static void InitDB()
	{
		if (!_IsInited)
		{
			Sprite[] array = Resources.LoadAll<Sprite>("NewUI/TuJian/TuJianUI");
			foreach (Sprite val in array)
			{
				ToolsEx.TryAdd(TuJianUISprite, ((Object)val).name, val);
				ToolsEx.TryAdd(TuJianRichTextSprite, ((Object)val).name, val);
			}
			InitStrText();
			jsonData.instance.init("Effect/json/d_TuJian.py.zhonglei", out _zhonglei);
			jsonData.instance.init("Effect/json/d_TuJian.py.chunwenben", out _chunwenben);
			jsonData.instance.init("Effect/json/d_TuJian.py.tupianwenzi", out _tupianwenzi);
			jsonData.instance.init("Effect/json/d_TuJian.py.zixiangtupianwenzi", out _zixiangtupianwenzi);
			jsonData.instance.init("Effect/json/d_TuJian.py.yaoshou", out _yaoshou);
			for (int j = 1; j <= 8; j++)
			{
				ToolsEx.TryAdd(ItemTuJianFilterData, j, new List<Dictionary<int, string>>());
			}
			InitItemTuJianData();
			InitLQWuWeiType();
			InitLQShuXingType();
			InitMapName();
			InitYaoShouData();
			InitRuleTuJianData();
			InitShenTongMiShu();
			InitGongFa();
			InitDanYao();
			_IsInited = true;
		}
	}

	public static Sprite GetRichTextSprite(string name)
	{
		if (TuJianRichTextSprite.ContainsKey(name))
		{
			return TuJianRichTextSprite[name];
		}
		Sprite val = ModResources.LoadSprite("NewUI/TuJian/Image/" + name);
		if ((Object)(object)val != (Object)null)
		{
			ToolsEx.TryAdd(TuJianRichTextSprite, name, val);
			return val;
		}
		return null;
	}

	public static string GetLQWuWeiTypeName(int wuWeiType)
	{
		return _LQWuWeiTypeName[wuWeiType];
	}

	public static string GetLQShuXingTypeName(int shuXingType)
	{
		return _LQShuXingTypeName[shuXingType];
	}

	private static void InitItemTuJianData()
	{
		foreach (JSONObject item in jsonData.instance._ItemJsonData.list)
		{
			if (item.HasField("TuJianType"))
			{
				int i = item["TuJianType"].I;
				if (i <= 4 && i > 0)
				{
					ItemTuJianFilterData[i].Add(new Dictionary<int, string> { 
					{
						item["id"].I,
						item["name"].Str
					} });
				}
			}
		}
	}

	private static void InitRuleTuJianData()
	{
		foreach (JSONObject item in _zhonglei.list)
		{
			int i = item["id"].I;
			if (item["type1"].I != 2)
			{
				continue;
			}
			RuleTuJianTypeNameData.Add(new Dictionary<int, string> { 
			{
				i,
				item["name"].Str
			} });
			ToolsEx.TryAdd(RuleTuJianFilterData, i, new List<Dictionary<int, string>>());
			if (item["type2"].I == 1)
			{
				ToolsEx.TryAdd(RuleTuJianTypeDoubleSVData, i, value: true);
				if (i != 101)
				{
					ToolsEx.TryAdd(RuleDoubleIndexData, i, new List<int>());
				}
			}
			else if (item["type2"].I == 2)
			{
				ToolsEx.TryAdd(RuleTuJianTypeDoubleSVData, i, value: false);
				ToolsEx.TryAdd(RuleTuJianTypeHasChildData, i, value: false);
			}
			else if (item["type2"].I == 3)
			{
				ToolsEx.TryAdd(RuleTuJianTypeDoubleSVData, i, value: false);
				ToolsEx.TryAdd(RuleTuJianTypeHasChildData, i, value: true);
			}
		}
		RuleTuJianFilterData[101].Add(new Dictionary<int, string> { { 1, "增益状态" } });
		RuleTuJianFilterData[101].Add(new Dictionary<int, string> { { 2, "负面状态" } });
		RuleTuJianFilterData[101].Add(new Dictionary<int, string> { { 3, "特殊状态" } });
		RuleTuJianFilterData[101].Add(new Dictionary<int, string> { { 4, "指示物" } });
		RuleTuJianFilterData[101].Add(new Dictionary<int, string> { { 5, "词缀" } });
		for (int j = 1; j <= 5; j++)
		{
			ToolsEx.TryAdd(RuleCiZhuiIndexData, j, new List<int>());
		}
		foreach (JSONObject item2 in _chunwenben.list)
		{
			int i2 = item2["typenum"].I;
			int i3 = item2["type"].I;
			int i4 = item2["id"].I;
			ToolsEx.TryAdd(RuleDoubleData, i4, new DoubleItem(item2["name2"].Str, item2["descr"].Str));
			if (i2 == 101)
			{
				RuleCiZhuiIndexData[i3].Add(i4);
			}
			else if (RuleDoubleIndexData.ContainsKey(i2))
			{
				RuleDoubleIndexData[i2].Add(i4);
			}
			else
			{
				Debug.LogError((object)$"规则图鉴初始化出错，没有大类{i2}，请检查规则图鉴配表");
			}
		}
		foreach (JSONObject item3 in _tupianwenzi.list)
		{
			int i5 = item3["typenum"].I;
			ToolsEx.TryAdd(RuleTuJianTypeDescData, i5, item3["descr"].Str);
		}
		foreach (JSONObject item4 in _zixiangtupianwenzi.list)
		{
			int i6 = item4["typenum"].I;
			if (!RuleTuJianFilterData.ContainsKey(i6))
			{
				RuleTuJianFilterData.Add(i6, new List<Dictionary<int, string>>());
			}
			int i7 = item4["subtypenum"].I;
			string str = item4["subname"].Str;
			RuleTuJianFilterData[i6].Add(new Dictionary<int, string> { { i7, str } });
			if (!RuleTuJianTypeChildDescData.ContainsKey(i6))
			{
				RuleTuJianTypeChildDescData.Add(i6, new Dictionary<int, string>());
			}
			RuleTuJianTypeChildDescData[i6].Add(i7, item4["descr"].Str);
		}
	}

	private static void InitStrText()
	{
		foreach (JSONObject item in jsonData.instance.StrTextJsonData.list)
		{
			ToolsEx.TryAdd(strTextData, item["StrID"].str, item["ChinaText"].Str);
		}
	}

	private static void InitLQWuWeiType()
	{
		foreach (JSONObject item in jsonData.instance.LianQiWuWeiBiao.list)
		{
			ToolsEx.TryAdd(_LQWuWeiTypeName, item["id"].I, item["desc"].Str);
		}
	}

	private static void InitLQShuXingType()
	{
		foreach (JSONObject item in jsonData.instance.LianQiShuXinLeiBie.list)
		{
			ToolsEx.TryAdd(_LQShuXingTypeName, item["id"].I, item["desc"].Str);
		}
	}

	public static Sprite GetItemQualitySprite(int id)
	{
		if (!_ItemQualitySpriteIndexDict.ContainsKey(id))
		{
			LoadItemSprite(id);
		}
		return _ItemQualitySpriteList[_ItemQualitySpriteIndexDict[id]];
	}

	public static Sprite GetItemQualityUpSprite(int id)
	{
		if (!_ItemQualityUpSpriteIndexDict.ContainsKey(id))
		{
			LoadItemSprite(id);
		}
		return _ItemQualityUpSpriteList[_ItemQualityUpSpriteIndexDict[id]];
	}

	public static Sprite GetItemIconSprite(int id)
	{
		if (!_ItemIconSpriteIndexDict.ContainsKey(id))
		{
			LoadItemSprite(id);
		}
		return _ItemIconSpriteList[_ItemIconSpriteIndexDict[id]];
	}

	private static void LoadItemSprite(int id)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		item temp = new item(id);
		if (!_ItemIconTexList.Contains(temp.itemIcon))
		{
			_ItemIconTexList.Add(temp.itemIcon);
			Sprite item = Sprite.Create(temp.itemIcon, new Rect(0f, 0f, (float)((Texture)temp.itemIcon).width, (float)((Texture)temp.itemIcon).height), new Vector2(0.5f, 0.5f));
			_ItemIconSpriteList.Add(item);
			ToolsEx.TryAdd(_ItemIconSpriteIndexDict, id, _ItemIconSpriteList.Count - 1);
		}
		else
		{
			int value = _ItemIconTexList.FindIndex((Texture2D t) => (Object)(object)t == (Object)(object)temp.itemIcon);
			ToolsEx.TryAdd(_ItemIconSpriteIndexDict, id, value);
		}
		if (!_ItemQualityTexList.Contains(temp.itemPingZhi))
		{
			_ItemQualityTexList.Add(temp.itemPingZhi);
			Sprite item2 = Sprite.Create(temp.itemPingZhi, new Rect(0f, 0f, (float)((Texture)temp.itemPingZhi).width, (float)((Texture)temp.itemPingZhi).height), new Vector2(0.5f, 0.5f));
			_ItemQualitySpriteList.Add(item2);
			ToolsEx.TryAdd(_ItemQualitySpriteIndexDict, id, _ItemQualitySpriteList.Count - 1);
		}
		else
		{
			int value2 = _ItemQualityTexList.FindIndex((Texture2D t) => (Object)(object)t == (Object)(object)temp.itemPingZhi);
			ToolsEx.TryAdd(_ItemQualitySpriteIndexDict, id, value2);
		}
		if (!_ItemQualityUpSpriteList.Contains(temp.itemPingZhiUP))
		{
			_ItemQualityUpSpriteList.Add(temp.itemPingZhiUP);
			ToolsEx.TryAdd(_ItemQualityUpSpriteIndexDict, id, _ItemQualityUpSpriteList.Count - 1);
			return;
		}
		int value3 = _ItemQualityUpSpriteList.FindIndex((Sprite s) => (Object)(object)s == (Object)(object)temp.itemPingZhiUP);
		ToolsEx.TryAdd(_ItemQualityUpSpriteIndexDict, id, value3);
	}

	private static void InitMapName()
	{
		foreach (JSONObject item in jsonData.instance.SceneNameJsonData.list)
		{
			ToolsEx.TryAdd(_MapIDNameDict, item["id"].str, item["EventName"].Str);
			ToolsEx.TryAdd(_MapIDHighlightDict, item["id"].str, item["HighlightID"].I);
		}
	}

	public static string GetMapNameByID(string mapID)
	{
		if (TuJianManager.IsDebugMode)
		{
			return _MapIDNameDict[mapID] + "-" + mapID;
		}
		return _MapIDNameDict[mapID];
	}

	public static int GetMapHighlightIDByMapID(string mapID)
	{
		return _MapIDHighlightDict[mapID];
	}

	public static Sprite GetYaoShouFace(int id)
	{
		Sprite val = null;
		if (!YaoShouFaceSpriteData.ContainsKey(id))
		{
			if (YaoShouFacePathData.ContainsKey(id))
			{
				val = ModResources.LoadSprite(YaoShouFacePathData[id]);
				ToolsEx.TryAdd(YaoShouFaceSpriteData, id, val);
			}
			else
			{
				Debug.LogError((object)"[图鉴]加载妖兽形象出错，没有找到形象");
			}
		}
		else
		{
			val = YaoShouFaceSpriteData[id];
		}
		return val;
	}

	private static void InitYaoShouData()
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		foreach (JSONObject item in _yaoshou.list)
		{
			int i = item["avatarid"].I;
			JSONObject jSONObject = avatarJsonData[i.ToString()];
			int i2 = jSONObject["fightFace"].I;
			string str = jSONObject["Name"].Str;
			string str2 = item["descr"].Str;
			string value = LevelNames[(jSONObject["Level"].I - 1) / 3];
			string str3 = item["chanchu"].str;
			string str4 = item["qixidi"].str;
			ItemTuJianFilterData[5].Add(new Dictionary<int, string> { { i, str } });
			ToolsEx.TryAdd(YaoShouNameData, i, str);
			ToolsEx.TryAdd(YaoShouLevelNameData, i, value);
			ToolsEx.TryAdd(YaoShouQiXiMapData, i, str4);
			string[] array = str3.Split(new char[1] { ',' });
			for (int j = 0; j < array.Length; j++)
			{
				int num = int.Parse(array[j]);
				if (!YaoShouCaiLiaoChanChuData.ContainsKey(num))
				{
					ToolsEx.TryAdd(YaoShouCaiLiaoChanChuData, num, new List<int>());
				}
				if (!YaoShouChanChuData.ContainsKey(i))
				{
					ToolsEx.TryAdd(YaoShouChanChuData, i, new List<int>());
				}
				YaoShouCaiLiaoChanChuData[num].Add(i);
				YaoShouChanChuData[i].Add(num);
			}
			ToolsEx.TryAdd(YaoShouDescData, i, str2);
			ToolsEx.TryAdd(YaoShouFacePathData, i, $"Effect/Prefab/gameEntity/Avater/Avater{i2}/{i2}");
		}
	}

	private static void InitShenTongMiShu()
	{
		foreach (JSONObject item in jsonData.instance._ItemJsonData.list)
		{
			if (item["id"].I < jsonData.QingJiaoItemIDSegment && item["type"].I == 3)
			{
				int key = (int)float.Parse(item["desc"].str);
				string str = item["desc2"].Str;
				ShenTongMiShuDesc1Data[key] = str;
			}
		}
		JSONObject skillJsonData = jsonData.instance._skillJsonData;
		for (int i = 0; i < skillJsonData.list.Count; i++)
		{
			JSONObject jSONObject = skillJsonData.list[i];
			int i2 = jSONObject["TuJianType"].I;
			if (i2 != 6 && i2 != 8)
			{
				continue;
			}
			int i3 = jSONObject["Skill_ID"].I;
			int i4 = jSONObject["Skill_LV"].I;
			int i5 = jSONObject["typePinJie"].I;
			string text = strTextData[$"pingjie{i4}"];
			string text2 = strTextData[$"shangzhongxia{i5}"];
			ShenTongMiShuPinJiData[i3] = text + text2;
			ShenTongMiShuQualityData[i3] = i4;
			if (!ShenTongMiShuNameData.ContainsKey(i3))
			{
				string value = jSONObject["name"].Str.RemoveNumber();
				ItemTuJianFilterData[i2].Add(new Dictionary<int, string> { { i3, value } });
				ShenTongMiShuNameData[i3] = value;
			}
			if (!ShenTongMiShuShuXingData.ContainsKey(i3))
			{
				string text3 = "";
				foreach (JSONObject item2 in jSONObject["AttackType"].list)
				{
					text3 += strTextData[$"xibie{item2.I}"];
				}
				ShenTongMiShuShuXingData[i3] = text3;
			}
			if (i2 == 6)
			{
				if (!ShenTongDesc2Data.ContainsKey(i3))
				{
					List<string> list = new List<string>();
					for (int j = 0; j < 5; j++)
					{
						list.Add("无");
					}
					ShenTongDesc2Data[i3] = list;
				}
				string str2 = jSONObject["TuJiandescr"].Str;
				str2 = str2.Replace("（attack）", jSONObject["HP"].I.ToString());
				int i6 = jSONObject["Skill_Lv"].I;
				ShenTongDesc2Data[i3][i6 - 1] = str2;
			}
			else
			{
				MiShuDesc2Data[i3] = jSONObject["TuJiandescr"].Str;
			}
			List<int> list2 = new List<int>();
			foreach (JSONObject item3 in jSONObject["skill_CastType"].list)
			{
				list2.Add(item3.I);
			}
			List<int> list3 = new List<int>();
			foreach (JSONObject item4 in jSONObject["skill_Cast"].list)
			{
				list3.Add(item4.I);
			}
			List<int> list4 = new List<int>();
			foreach (JSONObject item5 in jSONObject["skill_SameCastNum"].list)
			{
				list4.Add(item5.I);
			}
			List<int> list5 = new List<int>();
			for (int k = 0; k < list3.Count; k++)
			{
				for (int l = 0; l < list3[k]; l++)
				{
					list5.Add(list2[k]);
				}
				list5.Add(9);
			}
			for (int m = 0; m < list4.Count; m++)
			{
				for (int n = 0; n < list4[m]; n++)
				{
					list5.Add(8);
				}
				list5.Add(9);
			}
			if (list5.Count > 0 && list5[list5.Count - 1] == 9)
			{
				list5.RemoveAt(list5.Count - 1);
			}
			ShenTongMiShuCastData[i3] = list5;
			if (!ShenTongMiShuDesc1Data.ContainsKey(i3))
			{
				Debug.Log((object)$"[图鉴]不存在神通秘术{i3}的描述1，需反馈策划");
			}
		}
	}

	private static void InitGongFa()
	{
		foreach (JSONObject item in jsonData.instance._ItemJsonData.list)
		{
			if (item["id"].I < jsonData.QingJiaoItemIDSegment && item["type"].I == 4)
			{
				int key = (int)float.Parse(item["desc"].str);
				string str = item["desc2"].Str;
				GongFaDesc1Data.TryAdd(key, str, "[TuJianDB.InitGongFa]添加功法的描述1时，尝试对已有的SkillID进行添加，SkillID:{0}，已有Value:{1}，新加Value:{2}");
			}
		}
		JSONObject staticSkillJsonData = jsonData.instance.StaticSkillJsonData;
		for (int i = 0; i < staticSkillJsonData.list.Count; i++)
		{
			JSONObject jSONObject = staticSkillJsonData.list[i];
			int i2 = jSONObject["TuJianType"].I;
			if (i2 == 7)
			{
				int i3 = jSONObject["Skill_ID"].I;
				int i4 = jSONObject["Skill_LV"].I;
				int i5 = jSONObject["typePinJie"].I;
				string text = strTextData[$"pingjie{i4}"];
				string text2 = strTextData[$"shangzhongxia{i5}"];
				ToolsEx.TryAdd(GongFaQualityData, i3, i4);
				ToolsEx.TryAdd(GongFaPinJiData, i3, text + text2);
				string value = jSONObject["name"].Str.RemoveNumber();
				ItemTuJianFilterData[i2].Add(new Dictionary<int, string> { { i3, value } });
				ToolsEx.TryAdd(GongFaNameData, i3, value);
				int i6 = jSONObject["AttackType"].I;
				string value2 = strTextData[$"gongfaleibie{i6}"];
				ToolsEx.TryAdd(GongFaShuXingData, i3, value2);
				ToolsEx.TryAdd(GongFaSpeedData, i3, jSONObject["Skill_Speed"].I);
				List<string> list = new List<string>();
				for (int j = 0; j < 5; j++)
				{
					string str2 = staticSkillJsonData.list[i + j]["TuJiandescr"].Str;
					list.Add(str2);
				}
				ToolsEx.TryAdd(GongFaDesc2Data, i3, list);
				if (!GongFaDesc1Data.ContainsKey(i3))
				{
					Debug.Log((object)$"[图鉴]不存在功法{i3}的描述1，需反馈策划");
				}
				i += 4;
			}
		}
	}

	public static Sprite GetShenTongMiShuSprite(int id)
	{
		if (!_ShenTongMiShuSpriteData.ContainsKey(1))
		{
			ToolsEx.TryAdd(_ShenTongMiShuSpriteData, 1, ResManager.inst.LoadSprite("Skill Icon/1"));
		}
		if (!_ShenTongMiShuSpriteData.ContainsKey(id))
		{
			Sprite val = ResManager.inst.LoadSprite("Skill Icon/" + id);
			if (Object.op_Implicit((Object)(object)val))
			{
				ToolsEx.TryAdd(_ShenTongMiShuSpriteData, id, val);
			}
			else
			{
				ToolsEx.TryAdd(_ShenTongMiShuSpriteData, id, _ShenTongMiShuSpriteData[1]);
			}
		}
		return _ShenTongMiShuSpriteData[id];
	}

	public static Sprite GetGongFaSprite(int id)
	{
		if (!_GongFaSpriteData.ContainsKey(1))
		{
			ToolsEx.TryAdd(_GongFaSpriteData, 1, ResManager.inst.LoadSprite("StaticSkill Icon/1"));
		}
		if (!_GongFaSpriteData.ContainsKey(id))
		{
			Sprite val = ResManager.inst.LoadSprite("StaticSkill Icon/" + id);
			if (Object.op_Implicit((Object)(object)val))
			{
				ToolsEx.TryAdd(_GongFaSpriteData, id, val);
			}
			else
			{
				ToolsEx.TryAdd(_GongFaSpriteData, id, _GongFaSpriteData[1]);
			}
		}
		return _GongFaSpriteData[id];
	}

	public static Sprite GetSkillQualitySprite(int quality)
	{
		if (!_SkillQualitySpriteData.ContainsKey(quality))
		{
			ToolsEx.TryAdd(_SkillQualitySpriteData, quality, ResManager.inst.LoadSprite("Ui Icon/tab/skill" + quality));
		}
		return _SkillQualitySpriteData[quality];
	}

	private static void InitDanYao()
	{
		foreach (JSONObject item in jsonData.instance.LianDanDanFangBiao.list)
		{
			DanFangData danFangData = new DanFangData();
			danFangData.ItemID = item["ItemID"].I;
			danFangData.YaoYinID = item["value1"].I;
			danFangData.ZhuYao1ID = item["value2"].I;
			danFangData.ZhuYao2ID = item["value3"].I;
			danFangData.FuYao1ID = item["value4"].I;
			danFangData.FuYao2ID = item["value5"].I;
			danFangData.YaoYinCount = item["num1"].I;
			danFangData.ZhuYao1Count = item["num2"].I;
			danFangData.ZhuYao2Count = item["num3"].I;
			danFangData.FuYao1Count = item["num4"].I;
			danFangData.FuYao2Count = item["num5"].I;
			danFangData.CastTime = item["castTime"].I;
			danFangData.CalcYaoCaiTypeCount();
			ToolsEx.TryAdd(DanFangDataDict, danFangData.ItemID, danFangData);
		}
		foreach (JSONObject item2 in jsonData.instance.LianDanItemLeiXin.list)
		{
			ToolsEx.TryAdd(YaoCaoTypeData, item2["id"].I, item2["name"].Str);
		}
	}
}
