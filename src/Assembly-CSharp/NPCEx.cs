using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020002CC RID: 716
public static class NPCEx
{
	// Token: 0x06001579 RID: 5497 RVA: 0x000C2830 File Offset: 0x000C0A30
	public static int NPCIDToNew(int npcid)
	{
		int result = npcid;
		if (NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.ContainsKey(npcid))
		{
			result = NpcJieSuanManager.inst.ImportantNpcBangDingDictionary[npcid];
		}
		return result;
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000C2864 File Offset: 0x000C0A64
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

	// Token: 0x0600157B RID: 5499 RVA: 0x000C28CC File Offset: 0x000C0ACC
	public static void SetJSON(int npcid, string valueName, int value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
			return;
		}
		jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000C2934 File Offset: 0x000C0B34
	public static void SetJSON(int npcid, string valueName, string value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
			return;
		}
		jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x000C299C File Offset: 0x000C0B9C
	public static void SetJSON(int npcid, string valueName, bool value)
	{
		if (jsonData.instance.AvatarJsonData[npcid.ToString()].HasField(valueName))
		{
			jsonData.instance.AvatarJsonData[npcid.ToString()].SetField(valueName, value);
			return;
		}
		jsonData.instance.AvatarJsonData[npcid.ToString()].AddField(valueName, value);
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000C2A04 File Offset: 0x000C0C04
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

	// Token: 0x0600157F RID: 5503 RVA: 0x0001370C File Offset: 0x0001190C
	public static void SetNPCAction(int npcid, int actionID)
	{
		NPCEx.SetJSON(NPCEx.NPCIDToNew(npcid), "ActionId", actionID);
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000C2A68 File Offset: 0x000C0C68
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

	// Token: 0x06001581 RID: 5505 RVA: 0x000C2B70 File Offset: 0x000C0D70
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

	// Token: 0x06001582 RID: 5506 RVA: 0x000C2CB0 File Offset: 0x000C0EB0
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

	// Token: 0x06001583 RID: 5507 RVA: 0x000C2E10 File Offset: 0x000C1010
	public static int GetFavor(int npcid)
	{
		int num = NPCEx.NPCIDToNew(npcid);
		return jsonData.instance.AvatarRandomJsonData[num.ToString()]["HaoGanDu"].I;
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000C2E4C File Offset: 0x000C104C
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

	// Token: 0x06001585 RID: 5509 RVA: 0x0001371F File Offset: 0x0001191F
	public static bool IsDeath(int npcid)
	{
		return NpcJieSuanManager.inst.IsDeath(npcid);
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x000C2EBC File Offset: 0x000C10BC
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

	// Token: 0x06001587 RID: 5511 RVA: 0x000C2F08 File Offset: 0x000C1108
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

	// Token: 0x06001588 RID: 5512 RVA: 0x0001372C File Offset: 0x0001192C
	public static int GetSeaNPCIDByEventID(int eventId)
	{
		return NPCEx.GetSeaNPCID((int)jsonData.instance.EndlessSeaNPCData[eventId.ToString()]["stvalue"][0]);
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x000C2F50 File Offset: 0x000C1150
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

	// Token: 0x0600158A RID: 5514 RVA: 0x00013763 File Offset: 0x00011963
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

	// Token: 0x0600158B RID: 5515 RVA: 0x000C305C File Offset: 0x000C125C
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

	// Token: 0x0600158C RID: 5516 RVA: 0x000C3118 File Offset: 0x000C1318
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

	// Token: 0x0600158D RID: 5517 RVA: 0x000C3190 File Offset: 0x000C1390
	public static void WarpToMap(int id)
	{
		int nowMapIndex = PlayerEx.Player.NowMapIndex;
		NPCEx.WarpToMap(id, nowMapIndex);
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x000C31B0 File Offset: 0x000C13B0
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

	// Token: 0x0600158F RID: 5519 RVA: 0x000C3330 File Offset: 0x000C1530
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

	// Token: 0x06001590 RID: 5520 RVA: 0x000C342C File Offset: 0x000C162C
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

	// Token: 0x06001591 RID: 5521 RVA: 0x000C3600 File Offset: 0x000C1800
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

	// Token: 0x06001592 RID: 5522 RVA: 0x0001379B File Offset: 0x0001199B
	public static void ItemToNPCFromPlayer(UINPCData npc, item item, int count)
	{
		jsonData.instance.MonstarAddItem(npc.ID, item.UUID, item.itemID, count, item.Seid, 0);
		Tools.instance.RemoveItem(item.UUID, count);
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000137D2 File Offset: 0x000119D2
	public static void ItemToPlayerFromNPC(UINPCData npc, item item, int count)
	{
		jsonData.instance.MonstarRemoveItem(npc.ID, item.UUID, count, item.Seid);
		PlayerEx.Player.addItem(item.itemID, count, item.Seid, false);
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000C3798 File Offset: 0x000C1998
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

	// Token: 0x06001595 RID: 5525 RVA: 0x000C3814 File Offset: 0x000C1A14
	public static int CalcQingFen(UINPCData npc, item item, int count, out bool isLaJi, out bool isJiXu, out int zengliJieGuo, out bool highLevel, out bool isXiHao)
	{
		int num = item.GetJiaoYiPrice(npc.ID, true, false) * count;
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
			int num2 = item.quality;
			if (item.itemtype == 3 || item.itemtype == 4)
			{
				num2 = num2 * 3 - 2;
			}
			else if (item.itemtype == 0 || item.itemtype == 1 || item.itemtype == 2)
			{
				num2++;
			}
			if (num2 <= npc.BigLevel)
			{
				num /= 2;
				highLevel = false;
			}
			else
			{
				highLevel = true;
			}
		}
		return num;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000C38EC File Offset: 0x000C1AEC
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
		int i = player.ZengLi.TryGetField("DuoYuQingFen").TryGetField(npc.ID.ToString()).I;
		num2 += i;
		int num3 = num2 / num;
		int num4 = num2 % num;
		ZengLiArg zengLiArg = new ZengLiArg();
		if (!flag)
		{
			NPCEx.AddFavor(npc.ID, num3, false, true);
			NPCEx.AddQingFen(npc.ID, num2 - i, false);
			if (!player.ZengLi.HasField("DuoYuQingFen"))
			{
				player.ZengLi.SetField("DuoYuQingFen", new JSONObject(JSONObject.Type.OBJECT));
			}
			player.ZengLi["DuoYuQingFen"].SetField(npc.ID.ToString(), num4);
			NPCEx.ItemToNPCFromPlayer(npc, item, count);
			if (num2 - i > 0)
			{
				UIPopTip.Inst.Pop("你和" + npc.Name + "的情分提升了", PopTipIconType.上箭头);
			}
			zengLiArg.AddFavor = num3;
			zengLiArg.AddQingFen = num2 - i;
			zengLiArg.DuoYuQingFen = num4;
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

	// Token: 0x06001597 RID: 5527 RVA: 0x000C3A50 File Offset: 0x000C1C50
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

	// Token: 0x06001598 RID: 5528 RVA: 0x000C3AC0 File Offset: 0x000C1CC0
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

	// Token: 0x06001599 RID: 5529 RVA: 0x0001380E File Offset: 0x00011A0E
	private static int SuoQuRoll()
	{
		return PlayerEx.Player.RandomSeedNext() % 100;
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000C3AF8 File Offset: 0x000C1CF8
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

	// Token: 0x0600159B RID: 5531 RVA: 0x000C3B2C File Offset: 0x000C1D2C
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

	// Token: 0x0600159C RID: 5532 RVA: 0x000C3B60 File Offset: 0x000C1D60
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

	// Token: 0x0600159D RID: 5533 RVA: 0x000C3BDC File Offset: 0x000C1DDC
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

	// Token: 0x0600159E RID: 5534 RVA: 0x0001381D File Offset: 0x00011A1D
	public static int CalcShenShiHaoGan()
	{
		if (NPCEx.SuoQuNpc.FavorLevel >= 5)
		{
			return 2;
		}
		return 0;
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000C3D20 File Offset: 0x000C1F20
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

	// Token: 0x060015A0 RID: 5536 RVA: 0x000C3ED4 File Offset: 0x000C20D4
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

	// Token: 0x060015A1 RID: 5537 RVA: 0x000C4144 File Offset: 0x000C2344
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

	// Token: 0x060015A2 RID: 5538 RVA: 0x000C423C File Offset: 0x000C243C
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

	// Token: 0x060015A3 RID: 5539 RVA: 0x0001382F File Offset: 0x00011A2F
	public static int CreateLiuPaiNPC(int liuPai = 1, int jingjie = 1)
	{
		return FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liuPai, jingjie, 0);
	}

	// Token: 0x0400119D RID: 4509
	private static Dictionary<int, int> Action101102103Dict = new Dictionary<int, int>();

	// Token: 0x0400119E RID: 4510
	private static bool isQingFenInited;

	// Token: 0x0400119F RID: 4511
	private static Dictionary<int, int> QingFenCostDict = new Dictionary<int, int>();

	// Token: 0x040011A0 RID: 4512
	private static string[] zengLiJieGuoStr = new string[]
	{
		"异常",
		"急需",
		"喜欢",
		"不喜欢",
		"垃圾"
	};

	// Token: 0x040011A1 RID: 4513
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

	// Token: 0x040011A2 RID: 4514
	public static UINPCData SuoQuNpc;

	// Token: 0x040011A3 RID: 4515
	public static UINPCData WeiXieFriend;

	// Token: 0x040011A4 RID: 4516
	public static List<UINPCData> WeiXieFriends = new List<UINPCData>();

	// Token: 0x040011A5 RID: 4517
	public static item SuoQuItem;

	// Token: 0x040011A6 RID: 4518
	public static int SuoQuCount;

	// Token: 0x040011A7 RID: 4519
	public static int WeiXieHaoGan;

	// Token: 0x040011A8 RID: 4520
	public static int WeiXieQingFen;

	// Token: 0x040011A9 RID: 4521
	public static int WeiXieFriendCount;

	// Token: 0x040011AA RID: 4522
	public static bool SuoQuHighLevel;

	// Token: 0x040011AB RID: 4523
	public static bool SuoQuIsLaJi;

	// Token: 0x040011AC RID: 4524
	public static bool SuoQuIsJiXu;

	// Token: 0x040011AD RID: 4525
	public static bool SuoQuIsXiHao;

	// Token: 0x040011AE RID: 4526
	public static bool IsFirstWeiXie;

	// Token: 0x040011AF RID: 4527
	private static int[] WeiXieShengWang = new int[]
	{
		-10,
		-20,
		-30,
		-50,
		-70
	};
}
