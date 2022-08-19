using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public static class NPCEx
{
	// Token: 0x060012C1 RID: 4801 RVA: 0x000751A8 File Offset: 0x000733A8
	public static int NPCIDToNew(int npcid)
	{
		int result = npcid;
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcid))
		{
			result = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcid];
		}
		return result;
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x000751DC File Offset: 0x000733DC
	public static void AddNpcNextToPoLv(int npcId, int lv)
	{
		npcId = NPCEx.NPCIDToNew(npcId);
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		if (npcData == null)
		{
			Debug.LogError("不存在npc或已死亡:" + npcId);
			return;
		}
		npcData.SetField("AddToPoLv", lv);
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x00075224 File Offset: 0x00073424
	public static int NPCIDToOld(int npcid)
	{
		int result = npcid;
		foreach (KeyValuePair<int, int> keyValuePair in NpcJieSuanManager.inst.ImportantNpcBangDingDictionary)
		{
			if (keyValuePair.Value == npcid)
			{
				result = keyValuePair.Key;
			}
		}
		return result;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x0007528C File Offset: 0x0007348C
	public static void SetJSON(int npcid, string valueName, int value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
			return;
		}
		jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000752F4 File Offset: 0x000734F4
	public static void SetJSON(int npcid, string valueName, string value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
			return;
		}
		jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x0007535C File Offset: 0x0007355C
	public static void SetJSON(int npcid, string valueName, bool value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
			return;
		}
		jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000753C4 File Offset: 0x000735C4
	public static void SetMoney(int npcid, int money)
	{
		int num = NPCEx.NPCIDToNew(npcid);
		if (jsonData.instance.AvatarBackpackJsonData.HasField(num.ToString()))
		{
			jsonData.instance.AvatarBackpackJsonData[num.ToString()].SetField("money", money);
			return;
		}
		Debug.LogError(string.Format("设置NPC灵石出错，没有此NPC背包数据，NPCID:{0}", num));
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x00075427 File Offset: 0x00073627
	public static void SetNPCAction(int npcid, int actionID)
	{
		NPCEx.SetJSON(NPCEx.NPCIDToNew(npcid), "ActionId", actionID);
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x0007543C File Offset: 0x0007363C
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
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("time", eventTime);
		jsonobject.SetField("fungusshijian", eventDesc);
		jsonData.instance.AvatarJsonData[npcid.ToString()]["NoteBook"]["102"].Add(jsonobject);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x00075544 File Offset: 0x00073744
	public static void AddFavor(int npcid, int addCount, bool addQingFen = true, bool showTip = true)
	{
		int num = NPCEx.NPCIDToNew(npcid);
		if (addCount > 0 && addQingFen)
		{
			NPCEx.AddQingFen(num, addCount * 10, false);
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
				string str = (num3 > 0) ? ("提升了" + Math.Abs(num3)) : ("降低了" + Math.Abs(num3));
				PopTipIconType iconType = (num3 >= 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头;
				UIPopTip.Inst.Pop(jsonData.instance.AvatarRandomJsonData[npcid.ToString()]["Name"].Str + "对你的好感度" + str, iconType);
			}
			jsonData.instance.AvatarRandomJsonData[num.ToString()].SetField("HaoGanDu", num2);
			return;
		}
		Debug.LogError(string.Format("添加好感度异常，没有找到ID为{0}的NPC，传入的参数npcid:{1}，addCount:{2}", num, npcid, addCount));
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x00075684 File Offset: 0x00073884
	public static void AddQingFen(int npcid, int addCount, bool showTip = false)
	{
		int num = NPCEx.NPCIDToNew(npcid);
		if (jsonData.instance.AvatarJsonData.HasField(num.ToString()))
		{
			if (jsonData.instance.AvatarJsonData.HasField(num.ToString()))
			{
				int num2 = 0;
				if (jsonData.instance.AvatarJsonData[num.ToString()].HasField("QingFen"))
				{
					num2 = jsonData.instance.AvatarJsonData[num.ToString()]["QingFen"].I;
				}
				if (2147483647 - num2 < addCount)
				{
					addCount = int.MaxValue - num2;
				}
				int num3 = num2 + addCount;
				num3 = Mathf.Max(0, num3);
				if (showTip && addCount != 0)
				{
					string str = (addCount > 0) ? ("提升了" + Math.Abs(addCount)) : ("降低了" + Math.Abs(addCount));
					PopTipIconType iconType = (addCount > 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头;
					UIPopTip.Inst.Pop(jsonData.instance.AvatarRandomJsonData[npcid.ToString()]["Name"].Str + "对你的情分" + str, iconType);
				}
				jsonData.instance.AvatarJsonData[num.ToString()].SetField("QingFen", num3);
				return;
			}
			Debug.LogError(string.Format("添加情分异常，没有找到ID为{0}的NPC，传入的参数npcid:{1}，addCount:{2}", num, npcid, addCount));
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000757F8 File Offset: 0x000739F8
	public static int GetFavor(int npcid)
	{
		int num = NPCEx.NPCIDToNew(npcid);
		return jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x00075834 File Offset: 0x00073A34
	public static bool IsZhongYaoNPC(int npcid, out int oldid)
	{
		foreach (KeyValuePair<int, int> keyValuePair in NpcJieSuanManager.inst.ImportantNpcBangDingDictionary)
		{
			if (keyValuePair.Value == npcid)
			{
				oldid = keyValuePair.Key;
				return true;
			}
		}
		oldid = 0;
		return false;
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000758A4 File Offset: 0x00073AA4
	public static bool IsDeath(int npcid)
	{
		return NpcJieSuanManager.inst.IsDeath(npcid);
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000758B4 File Offset: 0x00073AB4
	public static void OpenTalk(int talkid)
	{
		GameObject gameObject = Resources.Load<GameObject>(string.Format("talkPrefab/TalkPrefab/Talk{0}", talkid));
		if (gameObject != null)
		{
			Object.Instantiate<GameObject>(gameObject);
			return;
		}
		Debug.LogError(string.Format("[OpenTalk]找不到Talk{0}", talkid));
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x00075900 File Offset: 0x00073B00
	public static int GetSeaNPCID(int staticId)
	{
		int num = GlobalValue.Get(staticId, "NPCEx.GetSeaNPCID 根据静态变量获取海上NPCID");
		int result;
		if (jsonData.instance.AvatarJsonData.HasField(num.ToString()))
		{
			result = num;
		}
		else
		{
			result = FactoryManager.inst.npcFactory.CreateHaiShangNpc(staticId);
		}
		return result;
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x00075947 File Offset: 0x00073B47
	public static int GetSeaNPCIDByEventID(int eventId)
	{
		return NPCEx.GetSeaNPCID((int)jsonData.instance.EndlessSeaNPCData[eventId.ToString()]["stvalue"][0]);
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x00075980 File Offset: 0x00073B80
	private static void InitAction101102103(int actionID)
	{
		NPCEx.Action101102103Dict.Clear();
		foreach (JSONObject jsonobject in jsonData.instance.NPCImportantDate.list)
		{
			int i = jsonobject["id"].I;
			int num = 0;
			switch (actionID)
			{
			case 101:
				if (jsonobject["DaShiXiong"].I != 0)
				{
					num = jsonobject["DaShiXiong"].I;
				}
				break;
			case 102:
				if (jsonobject["ZhangLao"].I != 0)
				{
					num = jsonobject["ZhangLao"].I;
				}
				break;
			case 103:
				if (jsonobject["ZhangMeng"].I != 0)
				{
					num = jsonobject["ZhangMeng"].I;
				}
				break;
			}
			if (num != 0)
			{
				NPCEx.Action101102103Dict.Add(i, num);
			}
		}
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x00075A8C File Offset: 0x00073C8C
	public static bool OpenAction101102103GuDingTalk(int oldNPCID, int actionID)
	{
		NPCEx.InitAction101102103(actionID);
		if (NPCEx.Action101102103Dict.ContainsKey(oldNPCID))
		{
			GlobalValue.Set(400, oldNPCID, "NPCEx.OpenAction101102103GuDingTalk");
			NPCEx.OpenTalk(NPCEx.Action101102103Dict[oldNPCID]);
			return true;
		}
		return false;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x00075AC4 File Offset: 0x00073CC4
	private static void InitQingFen()
	{
		if (!NPCEx.isQingFenInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NpcQingJiaoXiaoHaoData.list)
			{
				int key = jsonobject["Type"].I * 100 + jsonobject["quality"].I * 10 + jsonobject["typePinJie"].I;
				NPCEx.QingFenCostDict.Add(key, jsonobject["QingFen"].I);
			}
			NPCEx.isQingFenInited = true;
		}
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x00075B80 File Offset: 0x00073D80
	public static int GetQingFenCost(JSONObject skill, bool isGongFa)
	{
		NPCEx.InitQingFen();
		int result = 0;
		int num = 100;
		if (isGongFa)
		{
			num = 200;
		}
		num += skill["Skill_LV"].I * 10 + skill["typePinJie"].I;
		if (NPCEx.QingFenCostDict.ContainsKey(num))
		{
			result = NPCEx.QingFenCostDict[num];
		}
		else
		{
			Debug.LogError(string.Format("获取情分消耗出错，不存在复合ID{0}", num));
		}
		return result;
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x00075BF8 File Offset: 0x00073DF8
	public static void WarpToMap(int id)
	{
		int nowMapIndex = PlayerEx.Player.NowMapIndex;
		NPCEx.WarpToMap(id, nowMapIndex);
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x00075C18 File Offset: 0x00073E18
	public static void WarpToMap(int id, int mapIndex)
	{
		id = NPCEx.NPCIDToNew(id);
		if (NPCEx.IsDeath(id))
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
		foreach (KeyValuePair<int, List<int>> keyValuePair in bigMapNPCDictionary)
		{
			dictionary.Add(keyValuePair.Key, new List<int>());
			foreach (int num in keyValuePair.Value)
			{
				if (num != id)
				{
					dictionary[keyValuePair.Key].Add(num);
				}
			}
		}
		if (!dictionary.ContainsKey(key))
		{
			dictionary.Add(key, new List<int>());
		}
		dictionary[key].Add(id);
		NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Clear();
		foreach (KeyValuePair<int, List<int>> keyValuePair2 in dictionary)
		{
			NpcJieSuanManager.inst.npcMap.bigMapNPCDictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x00075D98 File Offset: 0x00073F98
	public static void WarpToScene(int id, string sceneName)
	{
		id = NPCEx.NPCIDToNew(id);
		if (NPCEx.IsDeath(id))
		{
			return;
		}
		if (!NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.ContainsKey(sceneName))
		{
			NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary.Add(sceneName, new List<int>());
		}
		foreach (KeyValuePair<string, List<int>> keyValuePair in NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary)
		{
			if (keyValuePair.Value.Contains(id))
			{
				keyValuePair.Value.Remove(id);
				break;
			}
		}
		if (!NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[sceneName].Contains(id))
		{
			NpcJieSuanManager.inst.npcMap.threeSenceNPCDictionary[sceneName].Add(id);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x00075E94 File Offset: 0x00074094
	public static void WarpToPlayerNowFuBen(int id, int mapIndex)
	{
		id = NPCEx.NPCIDToNew(id);
		if (NPCEx.IsDeath(id))
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
		foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary)
		{
			dictionary2.Add(keyValuePair.Key, new List<int>());
			foreach (int num in keyValuePair.Value)
			{
				if (num != id)
				{
					dictionary2[keyValuePair.Key].Add(num);
				}
			}
		}
		if (!dictionary2.ContainsKey(key))
		{
			dictionary2.Add(key, new List<int>());
		}
		dictionary2[key].Add(id);
		NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Clear();
		foreach (KeyValuePair<int, List<int>> keyValuePair2 in dictionary2)
		{
			NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Add(keyValuePair2.Key, keyValuePair2.Value);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x00076068 File Offset: 0x00074268
	public static void RemoveNPCFromNowFuBen(int id)
	{
		id = NPCEx.NPCIDToNew(id);
		if (NPCEx.IsDeath(id))
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
		foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary)
		{
			dictionary2.Add(keyValuePair.Key, new List<int>());
			foreach (int num in keyValuePair.Value)
			{
				if (num != id)
				{
					dictionary2[keyValuePair.Key].Add(num);
				}
			}
		}
		NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Clear();
		foreach (KeyValuePair<int, List<int>> keyValuePair2 in dictionary2)
		{
			NpcJieSuanManager.inst.npcMap.fuBenNPCDictionary[screenName].Add(keyValuePair2.Key, keyValuePair2.Value);
		}
		NpcJieSuanManager.inst.isUpDateNpcList = true;
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x00076200 File Offset: 0x00074400
	public static void ItemToNPCFromPlayer(UINPCData npc, item item, int count)
	{
		jsonData.instance.MonstarAddItem(npc.ID, item.UUID, item.itemID, count, item.Seid, 0);
		Tools.instance.RemoveItem(item.UUID, count);
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x00076237 File Offset: 0x00074437
	public static void ItemToPlayerFromNPC(UINPCData npc, item item, int count)
	{
		jsonData.instance.MonstarRemoveItem(npc.ID, item.UUID, count, item.Seid);
		PlayerEx.Player.addItem(item.itemID, count, item.Seid, false);
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x00076274 File Offset: 0x00074474
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

	// Token: 0x060012DE RID: 4830 RVA: 0x000762F0 File Offset: 0x000744F0
	public static int CalcQingFen(UINPCData npc, item item, int count, out bool isLaJi, out bool isJiXu, out int zengliJieGuo, out bool highLevel, out bool isXiHao)
	{
		int num = int.MaxValue;
		int jiaoYiPrice = item.GetJiaoYiPrice(npc.ID, true, false);
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

	// Token: 0x060012DF RID: 4831 RVA: 0x000763E4 File Offset: 0x000745E4
	public static void ZengLiToNPC(UINPCData npc, item item, int count)
	{
		Avatar player = PlayerEx.Player;
		int num = NPCEx.CalcZengLiX(npc);
		bool flag;
		bool flag2;
		int jieGuo;
		bool flag3;
		bool flag4;
		int num2 = NPCEx.CalcQingFen(npc, item, count, out flag, out flag2, out jieGuo, out flag3, out flag4);
		int num3 = player.ZengLi.TryGetField("DuoYuQingFen").TryGetField(npc.ID.ToString()).I;
		if (2147483647 - num3 < num2)
		{
			num3 = int.MaxValue - num2;
		}
		num2 += num3;
		int num4 = num2 / num;
		int num5 = num2 % num;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (!flag)
		{
			NPCEx.AddFavor(npc.ID, num4, false, true);
			NPCEx.AddQingFen(npc.ID, num2 - num3, false);
			if (!player.ZengLi.HasField("DuoYuQingFen"))
			{
				player.ZengLi.SetField("DuoYuQingFen", new JSONObject(JSONObject.Type.OBJECT));
			}
			player.ZengLi["DuoYuQingFen"].SetField(npc.ID.ToString(), num5);
			NPCEx.ItemToNPCFromPlayer(npc, item, count);
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
			NPCEx.ItemToNPCFromPlayer(npc, item, count);
		}
		zengLiArg.Item = item;
		zengLiArg.JieGuo = jieGuo;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsZengLiFinished = true;
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0007655C File Offset: 0x0007475C
	public static void SuoQuFromNPC(UINPCData npc, item item, int count)
	{
		NPCEx.SuoQuNpc = npc;
		NPCEx.SuoQuItem = item;
		NPCEx.SuoQuCount = count;
		bool suoQuIsLaJi;
		bool suoQuIsJiXu;
		int num2;
		bool suoQuHighLevel;
		bool suoQuIsXiHao;
		int num = NPCEx.CalcQingFen(npc, item, count, out suoQuIsLaJi, out suoQuIsJiXu, out num2, out suoQuHighLevel, out suoQuIsXiHao);
		int num3 = NPCEx.CalcZengLiX(NPCEx.SuoQuNpc);
		NPCEx.WeiXieHaoGan = num / num3;
		NPCEx.WeiXieQingFen = num;
		NPCEx.SuoQuHighLevel = suoQuHighLevel;
		NPCEx.SuoQuIsJiXu = suoQuIsJiXu;
		NPCEx.SuoQuIsLaJi = suoQuIsLaJi;
		NPCEx.SuoQuIsXiHao = suoQuIsXiHao;
		NPCEx.WeiXieFindFrind();
		NPCEx.FirstWeiXie();
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000765CC File Offset: 0x000747CC
	public static int CalcXingGe(int XingGe)
	{
		int result = 0;
		if (XingGe == 1 || XingGe == 2 || XingGe == 12 || XingGe == 15)
		{
			result = 2;
		}
		else if (XingGe == 8 || XingGe == 14 || XingGe == 18)
		{
			result = 1;
		}
		return result;
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x00076602 File Offset: 0x00074802
	private static int SuoQuRoll()
	{
		return PlayerEx.Player.RandomSeedNext() % 100;
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x00076614 File Offset: 0x00074814
	private static bool SuoQuRoll1(UINPCData npc)
	{
		int num = 20;
		int num2 = NPCEx.CalcXingGe(npc.XingGe);
		if (num2 == 1)
		{
			num = 33;
		}
		if (num2 == 2)
		{
			num = 10;
		}
		return num >= NPCEx.SuoQuRoll();
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x00076648 File Offset: 0x00074848
	private static bool SuoQuRoll2(UINPCData npc)
	{
		int num = 40;
		int num2 = NPCEx.CalcXingGe(npc.XingGe);
		if (num2 == 1)
		{
			num = 60;
		}
		if (num2 == 2)
		{
			num = 30;
		}
		return num >= NPCEx.SuoQuRoll();
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x0007667C File Offset: 0x0007487C
	private static void SuoQuChangeShengWang(int shengWang)
	{
		string arg;
		if (NPCEx.SuoQuNpc.IsNingZhouNPC)
		{
			PlayerEx.AddNingZhouShengWang(shengWang);
			arg = "宁州";
		}
		else
		{
			PlayerEx.AddSeaShengWang(shengWang);
			arg = "无尽之海";
		}
		string arg2 = (shengWang > 0) ? "增加" : "减少";
		PopTipIconType iconType = (shengWang > 0) ? PopTipIconType.上箭头 : PopTipIconType.下箭头;
		if (shengWang != 0)
		{
			UIPopTip.Inst.Pop(string.Format("{0}声望{1}了{2}", arg, arg2, shengWang), iconType);
		}
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000766F8 File Offset: 0x000748F8
	public static void FirstWeiXie()
	{
		NPCEx.IsFirstWeiXie = true;
		int num = PlayerEx.Player.getLevelType() - NPCEx.SuoQuNpc.BigLevel;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (num >= 2)
		{
			zengLiArg.JieGuo = 1;
		}
		else if (num == 1)
		{
			if (NPCEx.SuoQuHighLevel)
			{
				if (NPCEx.SuoQuIsXiHao)
				{
					if (NPCEx.SuoQuRoll1(NPCEx.SuoQuNpc))
					{
						if (NPCEx.SuoQuNpc.FavorLevel >= 5)
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
		int shengWang = (int)((float)NPCEx.WeiXieShengWang[NPCEx.SuoQuNpc.BigLevel - 1] * 0.15f);
		if (zengLiArg.JieGuo <= 4)
		{
			NPCEx.ItemToPlayerFromNPC(NPCEx.SuoQuNpc, NPCEx.SuoQuItem, NPCEx.SuoQuCount);
			int num2 = -(int)((float)NPCEx.WeiXieHaoGan * 1.3f);
			if (num2 == 0)
			{
				num2 = -1;
			}
			NPCEx.AddFavor(NPCEx.SuoQuNpc.ID, num2, true, true);
			NPCEx.AddQingFen(NPCEx.SuoQuNpc.ID, -NPCEx.WeiXieQingFen, false);
			NPCEx.SuoQuChangeShengWang(shengWang);
		}
		zengLiArg.Item = NPCEx.SuoQuItem;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsWeiXieFinished = true;
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x00076839 File Offset: 0x00074A39
	public static int CalcShenShiHaoGan()
	{
		if (NPCEx.SuoQuNpc.FavorLevel >= 5)
		{
			return 2;
		}
		return 0;
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x0007684C File Offset: 0x00074A4C
	public static void ShenShiWeiXie()
	{
		NPCEx.IsFirstWeiXie = false;
		Avatar player = PlayerEx.Player;
		bool flag = false;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (player.shengShi < NPCEx.SuoQuNpc.ShenShi)
		{
			zengLiArg.JieGuo = 12;
		}
		else if (player.shengShi >= NPCEx.SuoQuNpc.ShenShi + NPCEx.SuoQuNpc.BigLevel * 3)
		{
			if (NPCEx.SuoQuHighLevel)
			{
				if (NPCEx.SuoQuIsXiHao)
				{
					if (NPCEx.SuoQuRoll1(NPCEx.SuoQuNpc))
					{
						zengLiArg.JieGuo = NPCEx.CalcShenShiHaoGan();
					}
					else
					{
						zengLiArg.JieGuo = 6;
						flag = true;
					}
				}
				else
				{
					zengLiArg.JieGuo = NPCEx.CalcShenShiHaoGan();
				}
			}
			else
			{
				zengLiArg.JieGuo = 1;
			}
		}
		else if (NPCEx.SuoQuHighLevel)
		{
			if (NPCEx.SuoQuIsXiHao)
			{
				zengLiArg.JieGuo = 11;
			}
			else if (NPCEx.SuoQuRoll2(NPCEx.SuoQuNpc))
			{
				zengLiArg.JieGuo = NPCEx.CalcShenShiHaoGan();
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
		int num = (int)((float)NPCEx.WeiXieShengWang[NPCEx.SuoQuNpc.BigLevel - 1] * 0.15f);
		if (zengLiArg.JieGuo <= 4)
		{
			NPCEx.ItemToPlayerFromNPC(NPCEx.SuoQuNpc, NPCEx.SuoQuItem, NPCEx.SuoQuCount);
			int num2 = -(int)((float)NPCEx.WeiXieHaoGan * 1.3f);
			if (num2 == 0)
			{
				num2 = -1;
			}
			NPCEx.AddFavor(NPCEx.SuoQuNpc.ID, num2, true, true);
			NPCEx.AddQingFen(NPCEx.SuoQuNpc.ID, -NPCEx.WeiXieQingFen, false);
			NPCEx.SuoQuChangeShengWang(num);
		}
		else
		{
			NPCEx.AddFavor(NPCEx.SuoQuNpc.ID, -1, true, true);
			if (flag)
			{
				NPCEx.SuoQuChangeShengWang(num / 3);
			}
		}
		zengLiArg.Item = NPCEx.SuoQuItem;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsWeiXieFinished = true;
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x00076A00 File Offset: 0x00074C00
	public static void GuShiWeiXie(int friendIndex)
	{
		NPCEx.IsFirstWeiXie = false;
		NPCEx.WeiXieFriend = NPCEx.WeiXieFriends[friendIndex];
		ZengLiArg zengLiArg = new ZengLiArg();
		WeiXieArg weiXieArg = new WeiXieArg();
		weiXieArg.FriendID = NPCEx.WeiXieFriend.ID;
		weiXieArg.FriendName = NPCEx.WeiXieFriend.Name;
		weiXieArg.DiYu = (NPCEx.SuoQuNpc.IsNingZhouNPC ? "宁州" : "无尽之海");
		weiXieArg.FriendBigLevel = NPCEx.WeiXieFriend.BigLevel.ToBigLevelName();
		if (NPCEx.SuoQuNpc.ID == NPCEx.WeiXieFriend.ID)
		{
			zengLiArg.JieGuo = 16;
		}
		else if (NPCEx.WeiXieFriend.BigLevel > NPCEx.SuoQuNpc.BigLevel)
		{
			if (NPCEx.SuoQuNpc.IsNingZhouNPC == NPCEx.WeiXieFriend.IsNingZhouNPC)
			{
				if ((NPCEx.SuoQuNpc.MenPai == 0 && NPCEx.WeiXieFriend.MenPai == 0) || NPCEx.SuoQuNpc.MenPai != NPCEx.WeiXieFriend.MenPai)
				{
					zengLiArg.JieGuo = 3;
				}
				else if (NPCEx.WeiXieFriend.XingGe < 9)
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
			NPCEx.ItemToPlayerFromNPC(NPCEx.SuoQuNpc, NPCEx.SuoQuItem, NPCEx.SuoQuCount);
			int num = -(int)((float)NPCEx.WeiXieHaoGan * 1.1f);
			if (num == 0)
			{
				num = -1;
			}
			NPCEx.AddFavor(NPCEx.SuoQuNpc.ID, num, true, true);
			NPCEx.AddQingFen(NPCEx.SuoQuNpc.ID, -NPCEx.WeiXieQingFen, false);
			NPCEx.SuoQuChangeShengWang((int)((float)NPCEx.WeiXieShengWang[NPCEx.SuoQuNpc.BigLevel - 1] * 0.1f));
		}
		else if (zengLiArg.JieGuo != 16)
		{
			NPCEx.AddFavor(NPCEx.SuoQuNpc.ID, -1, true, true);
		}
		if (PlayerEx.IsTheather(NPCEx.WeiXieFriend.ID))
		{
			weiXieArg.SpecRel = "弟子";
		}
		else if (PlayerEx.IsDaoLv(NPCEx.WeiXieFriend.ID))
		{
			weiXieArg.SpecRel = "道侣";
		}
		else if (PlayerEx.IsBrother(NPCEx.WeiXieFriend.ID))
		{
			weiXieArg.SpecRel = "密友";
		}
		else
		{
			weiXieArg.SpecRel = "旧友";
		}
		zengLiArg.Item = NPCEx.SuoQuItem;
		UINPCJiaoHu.Inst.ZengLiArg = zengLiArg;
		UINPCJiaoHu.Inst.WeiXieArg = weiXieArg;
		UINPCJiaoHu.Inst.JiaoHuItemID = 0;
		UINPCJiaoHu.Inst.IsWeiXieFinished = true;
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x00076C70 File Offset: 0x00074E70
	public static void WeiXieFindFrind()
	{
		Avatar player = PlayerEx.Player;
		List<UINPCData> list = new List<UINPCData>();
		if (player.emailDateMag.cyNpcList.Count > 0)
		{
			foreach (int num in player.emailDateMag.cyNpcList)
			{
				if (!NpcJieSuanManager.inst.IsDeath(num))
				{
					UINPCData uinpcdata = new UINPCData(num, false);
					uinpcdata.RefreshData();
					if (uinpcdata.FavorLevel >= 6)
					{
						list.Add(uinpcdata);
					}
				}
			}
		}
		list.Sort();
		NPCEx.WeiXieFriends.Clear();
		if (list.Count > 0)
		{
			NPCEx.WeiXieFriendCount = Mathf.Min(3, list.Count);
			for (int i = 0; i < NPCEx.WeiXieFriendCount; i++)
			{
				NPCEx.WeiXieFriends.Add(list[i]);
			}
			return;
		}
		NPCEx.WeiXieFriendCount = 0;
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x00076D68 File Offset: 0x00074F68
	public static List<int> SearchLiuPaiNPC(int liuPai = 1, int jingjie = 1, bool canCreate = false)
	{
		JSONObject avatarJsonData = jsonData.instance.AvatarJsonData;
		List<int> list = new List<int>();
		foreach (string text in avatarJsonData.keys)
		{
			if (int.Parse(text) >= 20000 && avatarJsonData[text]["LiuPai"].I == liuPai && jingjie == avatarJsonData[text]["Level"].I)
			{
				list.Add(avatarJsonData[text]["id"].I);
			}
		}
		if (list.Count < 1 && canCreate)
		{
			int item = FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, jingjie, 0);
			list.Add(item);
		}
		return list;
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x00076E50 File Offset: 0x00075050
	public static int CreateLiuPaiNPC(int liuPai = 1, int jingjie = 1)
	{
		return FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, jingjie, 0);
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x00076E64 File Offset: 0x00075064
	public static void SetNPCExQingJiaoSkill(int npcid, int skillID)
	{
		NPCEx.NPCIDToNew(npcid);
		JSONObject jsonobject;
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField("ExQingJiaoSkills"))
		{
			jsonobject = jsonData.instance.AvatarJsonData[npcid.ToString()]["ExQingJiaoSkills"];
		}
		else
		{
			jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField("ExQingJiaoSkills", jsonobject);
		}
		if (!jsonobject.ToList().Contains(skillID))
		{
			jsonobject.Add(skillID);
		}
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x00076EFC File Offset: 0x000750FC
	public static void SetNPCExQingJiaoStaticSkill(int npcid, int liuShuiID)
	{
		NPCEx.NPCIDToNew(npcid);
		JSONObject jsonobject;
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField("ExQingJiaoStaticSkills"))
		{
			jsonobject = jsonData.instance.AvatarJsonData[npcid.ToString()]["ExQingJiaoStaticSkills"];
		}
		else
		{
			jsonobject = new JSONObject(JSONObject.Type.ARRAY);
			jsonData.instance.AvatarJsonData[npcid.ToString()].AddField("ExQingJiaoStaticSkills", jsonobject);
		}
		if (!jsonobject.ToList().Contains(liuShuiID))
		{
			jsonobject.Add(liuShuiID);
		}
	}

	// Token: 0x04000E5C RID: 3676
	private static Dictionary<int, int> Action101102103Dict = new Dictionary<int, int>();

	// Token: 0x04000E5D RID: 3677
	private static bool isQingFenInited;

	// Token: 0x04000E5E RID: 3678
	private static Dictionary<int, int> QingFenCostDict = new Dictionary<int, int>();

	// Token: 0x04000E5F RID: 3679
	private static string[] zengLiJieGuoStr = new string[]
	{
		"异常",
		"急需",
		"喜欢",
		"不喜欢",
		"垃圾"
	};

	// Token: 0x04000E60 RID: 3680
	private static Dictionary<int, int> suoQuFaverXiShu = new Dictionary<int, int>
	{
		{
			5,
			135
		},
		{
			6,
			130
		},
		{
			7,
			120
		},
		{
			8,
			105
		}
	};

	// Token: 0x04000E61 RID: 3681
	public static UINPCData SuoQuNpc;

	// Token: 0x04000E62 RID: 3682
	public static UINPCData WeiXieFriend;

	// Token: 0x04000E63 RID: 3683
	public static List<UINPCData> WeiXieFriends = new List<UINPCData>();

	// Token: 0x04000E64 RID: 3684
	public static item SuoQuItem;

	// Token: 0x04000E65 RID: 3685
	public static int SuoQuCount;

	// Token: 0x04000E66 RID: 3686
	public static int WeiXieHaoGan;

	// Token: 0x04000E67 RID: 3687
	public static int WeiXieQingFen;

	// Token: 0x04000E68 RID: 3688
	public static int WeiXieFriendCount;

	// Token: 0x04000E69 RID: 3689
	public static bool SuoQuHighLevel;

	// Token: 0x04000E6A RID: 3690
	public static bool SuoQuIsLaJi;

	// Token: 0x04000E6B RID: 3691
	public static bool SuoQuIsJiXu;

	// Token: 0x04000E6C RID: 3692
	public static bool SuoQuIsXiHao;

	// Token: 0x04000E6D RID: 3693
	public static bool IsFirstWeiXie;

	// Token: 0x04000E6E RID: 3694
	private static int[] WeiXieShengWang = new int[]
	{
		-10,
		-20,
		-30,
		-50,
		-70
	};
}
