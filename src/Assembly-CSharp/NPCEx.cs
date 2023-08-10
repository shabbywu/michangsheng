using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public static class NPCEx
{
	private static Dictionary<int, int> Action101102103Dict = new Dictionary<int, int>();

	private static bool isQingFenInited;

	private static Dictionary<int, int> QingFenCostDict = new Dictionary<int, int>();

	private static string[] zengLiJieGuoStr = new string[5] { "异常", "急需", "喜欢", "不喜欢", "垃圾" };

	private static Dictionary<int, int> suoQuFaverXiShu = new Dictionary<int, int>
	{
		{ 5, 135 },
		{ 6, 130 },
		{ 7, 120 },
		{ 8, 105 }
	};

	public static UINPCData SuoQuNpc;

	public static UINPCData WeiXieFriend;

	public static List<UINPCData> WeiXieFriends = new List<UINPCData>();

	public static item SuoQuItem;

	public static int SuoQuCount;

	public static int WeiXieHaoGan;

	public static int WeiXieQingFen;

	public static int WeiXieFriendCount;

	public static bool SuoQuHighLevel;

	public static bool SuoQuIsLaJi;

	public static bool SuoQuIsJiXu;

	public static bool SuoQuIsXiHao;

	public static bool IsFirstWeiXie;

	private static int[] WeiXieShengWang = new int[5] { -10, -20, -30, -50, -70 };

	public static int NPCIDToNew(int npcid)
	{
		int result = npcid;
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcid))
		{
			result = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcid];
		}
		return result;
	}

	public static void AddNpcNextToPoLv(int npcId, int lv)
	{
		npcId = NPCIDToNew(npcId);
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		if (npcData == null)
		{
			Debug.LogError((object)("不存在npc或已死亡:" + npcId));
		}
		else
		{
			npcData.SetField("AddToPoLv", lv);
		}
	}

	public static int NPCIDToOld(int npcid)
	{
		int result = npcid;
		foreach (KeyValuePair<int, int> item in NpcJieSuanManager.inst.ImportantNpcBangDingDictionary)
		{
			if (item.Value == npcid)
			{
				result = item.Key;
			}
		}
		return result;
	}

	public static void SetJSON(int npcid, string valueName, int value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
		}
		else
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
		}
	}

	public static void SetJSON(int npcid, string valueName, string value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
		}
		else
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
		}
	}

	public static void SetJSON(int npcid, string valueName, bool value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
		}
		else
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
		}
	}

	public static void SetMoney(int npcid, int money)
	{
		int num = NPCIDToNew(npcid);
		if (jsonData.instance.AvatarBackpackJsonData.HasField(num.ToString()))
		{
			jsonData.instance.AvatarBackpackJsonData[num.ToString()].SetField("money", money);
		}
		else
		{
			Debug.LogError((object)$"设置NPC灵石出错，没有此NPC背包数据，NPCID:{num}");
		}
	}

	public static void SetNPCAction(int npcid, int actionID)
	{
		SetJSON(NPCIDToNew(npcid), "ActionId", actionID);
	}

	public static void AddEvent(int npcid, string eventTime, string eventDesc)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()]["NoteBook"].IsNull)
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()]["NoteBook"] = new JSONObject(JSONObject.Type.OBJECT);
		}
		if (!jsonData.instance.AvatarJsonData[npcid.ToString()]["NoteBook"].HasField("102"))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()]["NoteBook"].AddField("102", new JSONObject(JSONObject.Type.ARRAY));
		}
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("time", eventTime);
		jSONObject.SetField("fungusshijian", eventDesc);
		jsonData.instance.AvatarJsonData[npcid.ToString()]["NoteBook"]["102"].Add(jSONObject);
	}

	public static void AddFavor(int npcid, int addCount, bool addQingFen = true, bool showTip = true)
	{
		int num = NPCIDToNew(npcid);
		if (addCount > 0 && addQingFen)
		{
			AddQingFen(num, addCount * 10);
		}
		if (jsonData.instance.AvatarRandomJsonData.HasField(num.ToString()))
		{
			int i = jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
			int num2 = i + addCount;
			if (num2 > 200)
			{
				num2 = 200;
			}
			int num3 = num2 - i;
			if (showTip && num3 != 0)
			{
				string text = ((num3 > 0) ? ("提升了" + Math.Abs(num3)) : ("降低了" + Math.Abs(num3)));
				PopTipIconType iconType = ((num3 >= 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头);
				UIPopTip.Inst.Pop(jsonData.instance.AvatarRandomJsonData[npcid.ToString()]["Name"].Str + "对你的好感度" + text, iconType);
			}
			jsonData.instance.AvatarRandomJsonData[num.ToString()].SetField("HaoGanDu", num2);
		}
		else
		{
			Debug.LogError((object)$"添加好感度异常，没有找到ID为{num}的NPC，传入的参数npcid:{npcid}，addCount:{addCount}");
		}
	}

	public static void AddQingFen(int npcid, int addCount, bool showTip = false)
	{
		int num = NPCIDToNew(npcid);
		if (!jsonData.instance.AvatarJsonData.HasField(num.ToString()))
		{
			return;
		}
		if (jsonData.instance.AvatarJsonData.HasField(num.ToString()))
		{
			int num2 = 0;
			if (jsonData.instance.AvatarJsonData[num.ToString()].HasField("QingFen"))
			{
				num2 = jsonData.instance.AvatarJsonData[num.ToString()]["QingFen"].I;
			}
			if (int.MaxValue - num2 < addCount)
			{
				addCount = int.MaxValue - num2;
			}
			int num3 = num2 + addCount;
			num3 = Mathf.Max(0, num3);
			if (showTip && addCount != 0)
			{
				string text = ((addCount > 0) ? ("提升了" + Math.Abs(addCount)) : ("降低了" + Math.Abs(addCount)));
				PopTipIconType iconType = ((addCount > 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头);
				UIPopTip.Inst.Pop(jsonData.instance.AvatarRandomJsonData[npcid.ToString()]["Name"].Str + "对你的情分" + text, iconType);
			}
			jsonData.instance.AvatarJsonData[num.ToString()].SetField("QingFen", num3);
		}
		else
		{
			Debug.LogError((object)$"添加情分异常，没有找到ID为{num}的NPC，传入的参数npcid:{npcid}，addCount:{addCount}");
		}
	}

	public static int GetFavor(int npcid)
	{
		int num = NPCIDToNew(npcid);
		return jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
	}

	public static bool IsZhongYaoNPC(int npcid, out int oldid)
	{
		foreach (KeyValuePair<int, int> item in NpcJieSuanManager.inst.ImportantNpcBangDingDictionary)
		{
			if (item.Value == npcid)
			{
				oldid = item.Key;
				return true;
			}
		}
		oldid = 0;
		return false;
	}

	public static bool IsDeath(int npcid)
	{
		return NpcJieSuanManager.inst.IsDeath(npcid);
	}

	public static void OpenTalk(int talkid)
	{
		GameObject val = Resources.Load<GameObject>($"talkPrefab/TalkPrefab/Talk{talkid}");
		if ((Object)(object)val != (Object)null)
		{
			Object.Instantiate<GameObject>(val);
		}
		else
		{
			Debug.LogError((object)$"[OpenTalk]找不到Talk{talkid}");
		}
	}

	public static int GetSeaNPCID(int staticId)
	{
		int result = GlobalValue.Get(staticId, "NPCEx.GetSeaNPCID 根据静态变量获取海上NPCID");
		if (jsonData.instance.AvatarJsonData.HasField(result.ToString()))
		{
			return result;
		}
		return FactoryManager.inst.npcFactory.CreateHaiShangNpc(staticId);
	}

	public static int GetSeaNPCIDByEventID(int eventId)
	{
		return GetSeaNPCID((int)jsonData.instance.EndlessSeaNPCData[eventId.ToString()][(object)"stvalue"][(object)0]);
	}

	private static void InitAction101102103(int actionID)
	{
		Action101102103Dict.Clear();
		foreach (JSONObject item in jsonData.instance.NPCImportantDate.list)
		{
			int i = item["id"].I;
			int num = 0;
			switch (actionID)
			{
			case 101:
				if (item["DaShiXiong"].I != 0)
				{
					num = item["DaShiXiong"].I;
				}
				break;
			case 102:
				if (item["ZhangLao"].I != 0)
				{
					num = item["ZhangLao"].I;
				}
				break;
			case 103:
				if (item["ZhangMeng"].I != 0)
				{
					num = item["ZhangMeng"].I;
				}
				break;
			}
			if (num != 0)
			{
				Action101102103Dict.Add(i, num);
			}
		}
	}

	public static bool OpenAction101102103GuDingTalk(int oldNPCID, int actionID)
	{
		InitAction101102103(actionID);
		if (Action101102103Dict.ContainsKey(oldNPCID))
		{
			GlobalValue.Set(400, oldNPCID, "NPCEx.OpenAction101102103GuDingTalk");
			OpenTalk(Action101102103Dict[oldNPCID]);
			return true;
		}
		return false;
	}

	private static void InitQingFen()
	{
		if (isQingFenInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NpcQingJiaoXiaoHaoData.list)
		{
			int key = item["Type"].I * 100 + item["quality"].I * 10 + item["typePinJie"].I;
			QingFenCostDict.Add(key, item["QingFen"].I);
		}
		isQingFenInited = true;
	}

	public static int GetQingFenCost(JSONObject skill, bool isGongFa)
	{
		InitQingFen();
		int result = 0;
		int num = 100;
		if (isGongFa)
		{
			num = 200;
		}
		num += skill["Skill_LV"].I * 10 + skill["typePinJie"].I;
		if (QingFenCostDict.ContainsKey(num))
		{
			result = QingFenCostDict[num];
		}
		else
		{
			Debug.LogError((object)$"获取情分消耗出错，不存在复合ID{num}");
		}
		return result;
	}

	public static void WarpToMap(int id)
	{
		int nowMapIndex = PlayerEx.Player.NowMapIndex;
		WarpToMap(id, nowMapIndex);
	}

	public static void WarpToMap(int id, int mapIndex)
	{
		id = NPCIDToNew(id);
		if (IsDeath(id))
		{
			return;
		}
		int key = PlayerEx.Player.NowMapIndex;
		if (mapIndex != 0)
		{
			key = mapIndex;
		}
		Dictionary<int, List<int>> bigMapNPCDictionary = NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary;
		Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
		foreach (KeyValuePair<int, List<int>> item in bigMapNPCDictionary)
		{
			dictionary.Add(item.Key, new List<int>());
			foreach (int item2 in item.Value)
			{
				if (item2 != id)
				{
					dictionary[item.Key].Add(item2);
				}
			}
		}
		if (!dictionary.ContainsKey(key))
		{
			dictionary.Add(key, new List<int>());
		}
		dictionary[key].Add(id);
		NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Clear();
		foreach (KeyValuePair<int, List<int>> item3 in dictionary)
		{
			NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Add(item3.Key, item3.Value);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	public static void WarpToScene(int id, string sceneName)
	{
		id = NPCIDToNew(id);
		if (IsDeath(id))
		{
			return;
		}
		if (!NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.ContainsKey(sceneName))
		{
			NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.Add(sceneName, new List<int>());
		}
		foreach (KeyValuePair<string, List<int>> item in NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary)
		{
			if (item.Value.Contains(id))
			{
				item.Value.Remove(id);
				break;
			}
		}
		if (!NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[sceneName].Contains(id))
		{
			NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[sceneName].Add(id);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	public static void WarpToPlayerNowFuBen(int id, int mapIndex)
	{
		id = NPCIDToNew(id);
		if (IsDeath(id))
		{
			return;
		}
		string screenName = Tools.getScreenName();
		int key = PlayerEx.Player.fubenContorl[screenName].NowIndex;
		if (mapIndex != 0)
		{
			key = mapIndex;
		}
		if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(screenName))
		{
			NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.Add(screenName, new Dictionary<int, List<int>>());
		}
		Dictionary<int, List<int>> dictionary = NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName];
		Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
		foreach (KeyValuePair<int, List<int>> item in dictionary)
		{
			dictionary2.Add(item.Key, new List<int>());
			foreach (int item2 in item.Value)
			{
				if (item2 != id)
				{
					dictionary2[item.Key].Add(item2);
				}
			}
		}
		if (!dictionary2.ContainsKey(key))
		{
			dictionary2.Add(key, new List<int>());
		}
		dictionary2[key].Add(id);
		NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Clear();
		foreach (KeyValuePair<int, List<int>> item3 in dictionary2)
		{
			NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Add(item3.Key, item3.Value);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	public static void RemoveNPCFromNowFuBen(int id)
	{
		id = NPCIDToNew(id);
		if (IsDeath(id))
		{
			return;
		}
		string screenName = Tools.getScreenName();
		if (!NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.ContainsKey(screenName))
		{
			NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary.Add(screenName, new Dictionary<int, List<int>>());
		}
		Dictionary<int, List<int>> dictionary = NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName];
		Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
		foreach (KeyValuePair<int, List<int>> item in dictionary)
		{
			dictionary2.Add(item.Key, new List<int>());
			foreach (int item2 in item.Value)
			{
				if (item2 != id)
				{
					dictionary2[item.Key].Add(item2);
				}
			}
		}
		NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Clear();
		foreach (KeyValuePair<int, List<int>> item3 in dictionary2)
		{
			NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Add(item3.Key, item3.Value);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	public static void ItemToNPCFromPlayer(UINPCData npc, item item, int count)
	{
		jsonData.instance.MonstarAddItem(npc.ID, item.UUID, item.itemID, count, item.Seid);
		Tools.instance.RemoveItem(item.UUID, count);
	}

	public static void ItemToPlayerFromNPC(UINPCData npc, item item, int count)
	{
		jsonData.instance.MonstarRemoveItem(npc.ID, item.UUID, count, item.Seid);
		PlayerEx.Player.addItem(item.itemID, count, item.Seid);
	}

	public static int CalcZengLiX(UINPCData npc)
	{
		Avatar player = PlayerEx.Player;
		int i = jsonData.instance.NpcLevelShouYiDate[npc.Level.ToString()]["ZengLi"].I;
		int i2 = jsonData.instance.NpcHaoGanDuData[npc.FavorLevel.ToString()]["XiShu"].I;
		int num = 1 + i2 - player.getLevelType();
		if (num < 1)
		{
			num = 1;
		}
		return i * num;
	}

	public static int CalcQingFen(UINPCData npc, item item, int count, out bool isLaJi, out bool isJiXu, out int zengliJieGuo, out bool highLevel, out bool isXiHao)
	{
		int num = int.MaxValue;
		int jiaoYiPrice = item.GetJiaoYiPrice(npc.ID, isPlayer: true);
		if (jiaoYiPrice > 0)
		{
			num = int.MaxValue / jiaoYiPrice;
		}
		if (count > num)
		{
			count = num;
		}
		int num2 = jiaoYiPrice * count;
		item.CalcNPCZhuangTai(npc.ID, out isJiXu, out isLaJi);
		if (jsonData.instance.GetMonstarInterestingItem(npc.ID, item.itemID, item.Seid) > 0)
		{
			zengliJieGuo = 2;
			isXiHao = true;
		}
		else
		{
			zengliJieGuo = 3;
			isXiHao = false;
		}
		if (isLaJi)
		{
			zengliJieGuo = 4;
		}
		if (isJiXu)
		{
			zengliJieGuo = 1;
		}
		highLevel = false;
		if (zengliJieGuo != 1 && zengliJieGuo != 2)
		{
			int num3 = item.quality;
			if (item.itemtype == 3 || item.itemtype == 4)
			{
				num3 = num3 * 3 - 2;
			}
			else if (item.itemtype == 0 || item.itemtype == 1 || item.itemtype == 2)
			{
				num3++;
			}
			if (num3 <= npc.BigLevel)
			{
				num2 /= 2;
				highLevel = false;
			}
			else
			{
				highLevel = true;
			}
		}
		return num2;
	}

	public static void ZengLiToNPC(UINPCData npc, item item, int count)
	{
		Avatar player = PlayerEx.Player;
		int num = CalcZengLiX(npc);
		int num2 = CalcQingFen(npc, item, count, out var isLaJi, out var _, out var zengliJieGuo, out var _, out var _);
		int num3 = player.ZengLi.TryGetField("DuoYuQingFen").TryGetField(npc.ID.ToString()).I;
		if (int.MaxValue - num3 < num2)
		{
			num3 = int.MaxValue - num2;
		}
		num2 += num3;
		int num4 = num2 / num;
		int num5 = num2 % num;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (!isLaJi)
		{
			AddFavor(npc.ID, num4, addQingFen: false);
			AddQingFen(npc.ID, num2 - num3);
			if (!player.ZengLi.HasField("DuoYuQingFen"))
			{
				player.ZengLi.SetField("DuoYuQingFen", new JSONObject(JSONObject.Type.OBJECT));
			}
			player.ZengLi["DuoYuQingFen"].SetField(npc.ID.ToString(), num5);
			ItemToNPCFromPlayer(npc, item, count);
			if (num2 - num3 > 0)
			{
				UIPopTip.Inst.Pop("你和" + npc.Name + "的情分提升了", PopTipIconType.上箭头);
			}
			zengLiArg.AddFavor = num4;
			zengLiArg.AddQingFen = num2 - num3;
			zengLiArg.DuoYuQingFen = num5;
		}
		else
		{
			ItemToNPCFromPlayer(npc, item, count);
		}
		zengLiArg.Item = item;
		zengLiArg.JieGuo = zengliJieGuo;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsZengLiFinished = true;
	}

	public static void SuoQuFromNPC(UINPCData npc, item item, int count)
	{
		SuoQuNpc = npc;
		SuoQuItem = item;
		SuoQuCount = count;
		bool isLaJi;
		bool isJiXu;
		int zengliJieGuo;
		bool highLevel;
		bool isXiHao;
		int num = CalcQingFen(npc, item, count, out isLaJi, out isJiXu, out zengliJieGuo, out highLevel, out isXiHao);
		int num2 = CalcZengLiX(SuoQuNpc);
		WeiXieHaoGan = num / num2;
		WeiXieQingFen = num;
		SuoQuHighLevel = highLevel;
		SuoQuIsJiXu = isJiXu;
		SuoQuIsLaJi = isLaJi;
		SuoQuIsXiHao = isXiHao;
		WeiXieFindFrind();
		FirstWeiXie();
	}

	public static int CalcXingGe(int XingGe)
	{
		int result = 0;
		switch (XingGe)
		{
		case 1:
		case 2:
		case 12:
		case 15:
			result = 2;
			break;
		case 8:
		case 14:
		case 18:
			result = 1;
			break;
		}
		return result;
	}

	private static int SuoQuRoll()
	{
		return PlayerEx.Player.RandomSeedNext() % 100;
	}

	private static bool SuoQuRoll1(UINPCData npc)
	{
		int num = 20;
		int num2 = CalcXingGe(npc.XingGe);
		if (num2 == 1)
		{
			num = 33;
		}
		if (num2 == 2)
		{
			num = 10;
		}
		return num >= SuoQuRoll();
	}

	private static bool SuoQuRoll2(UINPCData npc)
	{
		int num = 40;
		int num2 = CalcXingGe(npc.XingGe);
		if (num2 == 1)
		{
			num = 60;
		}
		if (num2 == 2)
		{
			num = 30;
		}
		return num >= SuoQuRoll();
	}

	private static void SuoQuChangeShengWang(int shengWang)
	{
		string text = "";
		string text2 = "";
		if (SuoQuNpc.IsNingZhouNPC)
		{
			PlayerEx.AddNingZhouShengWang(shengWang);
			text = "宁州";
		}
		else
		{
			PlayerEx.AddSeaShengWang(shengWang);
			text = "无尽之海";
		}
		text2 = ((shengWang > 0) ? "增加" : "减少");
		PopTipIconType iconType = ((shengWang > 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头);
		if (shengWang != 0)
		{
			UIPopTip.Inst.Pop($"{text}声望{text2}了{shengWang}", iconType);
		}
	}

	public static void FirstWeiXie()
	{
		IsFirstWeiXie = true;
		int num = PlayerEx.Player.getLevelType() - SuoQuNpc.BigLevel;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (num >= 2)
		{
			zengLiArg.JieGuo = 1;
		}
		else if (num == 1)
		{
			if (SuoQuHighLevel)
			{
				if (SuoQuIsXiHao)
				{
					if (SuoQuRoll1(SuoQuNpc))
					{
						if (SuoQuNpc.FavorLevel >= 5)
						{
							zengLiArg.JieGuo = 2;
						}
						else
						{
							zengLiArg.JieGuo = 0;
						}
					}
					else
					{
						zengLiArg.JieGuo = 6;
					}
				}
				else
				{
					zengLiArg.JieGuo = 1;
				}
			}
			else
			{
				zengLiArg.JieGuo = 1;
			}
		}
		else
		{
			zengLiArg.JieGuo = 5;
		}
		int shengWang = (int)((float)WeiXieShengWang[SuoQuNpc.BigLevel - 1] * 0.15f);
		if (zengLiArg.JieGuo <= 4)
		{
			ItemToPlayerFromNPC(SuoQuNpc, SuoQuItem, SuoQuCount);
			int num2 = -(int)((float)WeiXieHaoGan * 1.3f);
			if (num2 == 0)
			{
				num2 = -1;
			}
			AddFavor(SuoQuNpc.ID, num2);
			AddQingFen(SuoQuNpc.ID, -WeiXieQingFen);
			SuoQuChangeShengWang(shengWang);
		}
		zengLiArg.Item = SuoQuItem;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsWeiXieFinished = true;
	}

	public static int CalcShenShiHaoGan()
	{
		if (SuoQuNpc.FavorLevel >= 5)
		{
			return 2;
		}
		return 0;
	}

	public static void ShenShiWeiXie()
	{
		IsFirstWeiXie = false;
		Avatar player = PlayerEx.Player;
		bool flag = false;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (player.shengShi < SuoQuNpc.ShenShi)
		{
			zengLiArg.JieGuo = 12;
		}
		else if (player.shengShi >= SuoQuNpc.ShenShi + SuoQuNpc.BigLevel * 3)
		{
			if (SuoQuHighLevel)
			{
				if (SuoQuIsXiHao)
				{
					if (SuoQuRoll1(SuoQuNpc))
					{
						zengLiArg.JieGuo = CalcShenShiHaoGan();
					}
					else
					{
						zengLiArg.JieGuo = 6;
						flag = true;
					}
				}
				else
				{
					zengLiArg.JieGuo = CalcShenShiHaoGan();
				}
			}
			else
			{
				zengLiArg.JieGuo = 1;
			}
		}
		else if (SuoQuHighLevel)
		{
			if (SuoQuIsXiHao)
			{
				zengLiArg.JieGuo = 11;
			}
			else if (SuoQuRoll2(SuoQuNpc))
			{
				zengLiArg.JieGuo = CalcShenShiHaoGan();
			}
			else
			{
				zengLiArg.JieGuo = 7;
				flag = true;
			}
		}
		else
		{
			zengLiArg.JieGuo = 1;
		}
		int num = (int)((float)WeiXieShengWang[SuoQuNpc.BigLevel - 1] * 0.15f);
		if (zengLiArg.JieGuo <= 4)
		{
			ItemToPlayerFromNPC(SuoQuNpc, SuoQuItem, SuoQuCount);
			int num2 = -(int)((float)WeiXieHaoGan * 1.3f);
			if (num2 == 0)
			{
				num2 = -1;
			}
			AddFavor(SuoQuNpc.ID, num2);
			AddQingFen(SuoQuNpc.ID, -WeiXieQingFen);
			SuoQuChangeShengWang(num);
		}
		else
		{
			AddFavor(SuoQuNpc.ID, -1);
			if (flag)
			{
				SuoQuChangeShengWang(num / 3);
			}
		}
		zengLiArg.Item = SuoQuItem;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsWeiXieFinished = true;
	}

	public static void GuShiWeiXie(int friendIndex)
	{
		IsFirstWeiXie = false;
		WeiXieFriend = WeiXieFriends[friendIndex];
		ZengLiArg zengLiArg = new ZengLiArg();
		WeiXieArg weiXieArg = new WeiXieArg();
		weiXieArg.FriendID = WeiXieFriend.ID;
		weiXieArg.FriendName = WeiXieFriend.Name;
		weiXieArg.DiYu = (SuoQuNpc.IsNingZhouNPC ? "宁州" : "无尽之海");
		weiXieArg.FriendBigLevel = WeiXieFriend.BigLevel.ToBigLevelName();
		if (SuoQuNpc.ID == WeiXieFriend.ID)
		{
			zengLiArg.JieGuo = 16;
		}
		else if (WeiXieFriend.BigLevel > SuoQuNpc.BigLevel)
		{
			if (SuoQuNpc.IsNingZhouNPC == WeiXieFriend.IsNingZhouNPC)
			{
				if ((SuoQuNpc.MenPai == 0 && WeiXieFriend.MenPai == 0) || SuoQuNpc.MenPai != WeiXieFriend.MenPai)
				{
					zengLiArg.JieGuo = 3;
				}
				else if (WeiXieFriend.XingGe < 9)
				{
					zengLiArg.JieGuo = 14;
				}
				else
				{
					zengLiArg.JieGuo = 4;
				}
			}
			else
			{
				zengLiArg.JieGuo = 13;
			}
		}
		else
		{
			zengLiArg.JieGuo = 15;
		}
		if (zengLiArg.JieGuo <= 4)
		{
			ItemToPlayerFromNPC(SuoQuNpc, SuoQuItem, SuoQuCount);
			int num = -(int)((float)WeiXieHaoGan * 1.1f);
			if (num == 0)
			{
				num = -1;
			}
			AddFavor(SuoQuNpc.ID, num);
			AddQingFen(SuoQuNpc.ID, -WeiXieQingFen);
			SuoQuChangeShengWang((int)((float)WeiXieShengWang[SuoQuNpc.BigLevel - 1] * 0.1f));
		}
		else if (zengLiArg.JieGuo != 16)
		{
			AddFavor(SuoQuNpc.ID, -1);
		}
		if (PlayerEx.IsTheather(WeiXieFriend.ID))
		{
			weiXieArg.SpecRel = "弟子";
		}
		else if (PlayerEx.IsDaoLv(WeiXieFriend.ID))
		{
			weiXieArg.SpecRel = "道侣";
		}
		else if (PlayerEx.IsBrother(WeiXieFriend.ID))
		{
			weiXieArg.SpecRel = "密友";
		}
		else
		{
			weiXieArg.SpecRel = "旧友";
		}
		zengLiArg.Item = SuoQuItem;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.WeiXieArg = weiXieArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsWeiXieFinished = true;
	}

	public static void WeiXieFindFrind()
	{
		Avatar player = PlayerEx.Player;
		List<UINPCData> list = new List<UINPCData>();
		if (player.emailDateMag.cyNpcList.Count > 0)
		{
			foreach (int cyNpc in player.emailDateMag.cyNpcList)
			{
				if (!NpcJieSuanManager.inst.IsDeath(cyNpc))
				{
					UINPCData uINPCData = new UINPCData(cyNpc);
					uINPCData.RefreshData();
					if (uINPCData.FavorLevel >= 6)
					{
						list.Add(uINPCData);
					}
				}
			}
		}
		list.Sort();
		WeiXieFriends.Clear();
		if (list.Count > 0)
		{
			WeiXieFriendCount = Mathf.Min(3, list.Count);
			for (int i = 0; i < WeiXieFriendCount; i++)
			{
				WeiXieFriends.Add(list[i]);
			}
		}
		else
		{
			WeiXieFriendCount = 0;
		}
	}

	public static List<int> SearchLiuPaiNPC(int liuPai = 1, int jingjie = 1, bool canCreate = false)
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		List<int> list = new List<int>();
		foreach (string key in avatarJsonData.keys)
		{
			if (int.Parse(key) >= 20000 && avatarJsonData[key]["LiuPai"].I == liuPai && jingjie == avatarJsonData[key]["Level"].I)
			{
				list.Add(avatarJsonData[key]["id"].I);
			}
		}
		if (list.Count < 1 && canCreate)
		{
			int item = FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, jingjie);
			list.Add(item);
		}
		return list;
	}

	public static int CreateLiuPaiNPC(int liuPai = 1, int jingjie = 1)
	{
		return FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, jingjie);
	}

	public static void SetNPCExQingJiaoSkill(int npcid, int skillID)
	{
		NPCIDToNew(npcid);
		JSONObject jSONObject = null;
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField("ExQingJiaoSkills"))
		{
			jSONObject = jsonData.instance.AvatarJsonData[npcid.ToString()]["ExQingJiaoSkills"];
		}
		else
		{
			jSONObject = new JSONObject(JSONObject.Type.ARRAY);
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField("ExQingJiaoSkills", jSONObject);
		}
		if (!jSONObject.ToList().Contains(skillID))
		{
			jSONObject.Add(skillID);
		}
	}

	public static void SetNPCExQingJiaoStaticSkill(int npcid, int liuShuiID)
	{
		NPCIDToNew(npcid);
		JSONObject jSONObject = null;
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField("ExQingJiaoStaticSkills"))
		{
			jSONObject = jsonData.instance.AvatarJsonData[npcid.ToString()]["ExQingJiaoStaticSkills"];
		}
		else
		{
			jSONObject = new JSONObject(JSONObject.Type.ARRAY);
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField("ExQingJiaoStaticSkills", jSONObject);
		}
		if (!jSONObject.ToList().Contains(liuShuiID))
		{
			jSONObject.Add(liuShuiID);
		}
	}
}
