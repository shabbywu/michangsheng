using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class NPCFactory
{
	private Random random = new Random();

	private Dictionary<int, Dictionary<int, List<JSONObject>>> shopTypeDictionary = new Dictionary<int, Dictionary<int, List<JSONObject>>>();

	private Dictionary<int, Dictionary<int, List<JSONObject>>> typeDictionary = new Dictionary<int, Dictionary<int, List<JSONObject>>>();

	private Dictionary<int, List<int>> NpcXingGeDictionary = new Dictionary<int, List<int>>();

	private Dictionary<int, List<string>> NpcAuToCreateDictionary = new Dictionary<int, List<string>>();

	public bool isNewGame;

	public static object obj = new object();

	public void firstCreateNpcs()
	{
		Tools.instance.getPlayer();
		isNewGame = true;
		NpcJieSuanManager.inst.ImportantNpcBangDingDictionary = new Dictionary<int, int>();
		for (int i = 0; i < jsonData.instance.NPCChuShiHuaDate.Count; i++)
		{
			for (int j = 0; j < jsonData.instance.NPCChuShiHuaDate[i]["Level"].Count; j++)
			{
				for (int k = 0; k < jsonData.instance.NPCChuShiHuaDate[i]["Num"][j].I; k++)
				{
					for (int l = 0; l < jsonData.instance.NPCLeiXingDate.Count; l++)
					{
						int i2 = jsonData.instance.NPCChuShiHuaDate[i]["Level"][j].I;
						int i3 = jsonData.instance.NPCChuShiHuaDate[i]["LiuPai"].I;
						if (i2 == jsonData.instance.NPCLeiXingDate[l]["Level"].I && i3 == jsonData.instance.NPCLeiXingDate[l]["LiuPai"].I)
						{
							JSONObject npcDate = new JSONObject(jsonData.instance.NPCLeiXingDate[l].ToString());
							createNpc(npcDate, isImportant: false);
							break;
						}
					}
				}
			}
		}
		for (int m = 0; m < jsonData.instance.NPCImportantDate.Count; m++)
		{
			JSONObject improtantNpcDate = new JSONObject(jsonData.instance.NPCImportantDate[m].ToString());
			createImprotantNpc(improtantNpcDate);
		}
		foreach (JSONObject item in jsonData.instance.NpcHaiShangCreateData.list)
		{
			foreach (JSONObject item2 in jsonData.instance.NPCLeiXingDate.list)
			{
				if (item2["LiuPai"].I == item["LiuPai"].I && item2["Level"].I == item["level"].I)
				{
					JSONObject npcDate2 = new JSONObject(item2.ToString());
					int value = createNpc(npcDate2, isImportant: false);
					GlobalValue.Set(item["id"].I, value, "NPCFactory.firstCreateNpcs 海上NPC生成");
					break;
				}
			}
		}
	}

	public int CreateHaiShangNpc(int id)
	{
		int num = 0;
		JSONObject jSONObject = jsonData.instance.NpcHaiShangCreateData[id.ToString()];
		foreach (JSONObject item in jsonData.instance.NPCLeiXingDate.list)
		{
			if (item["LiuPai"].I == jSONObject["LiuPai"].I && item["Level"].I == jSONObject["level"].I)
			{
				JSONObject npcDate = new JSONObject(item.ToString());
				num = AfterCreateNpc(npcDate, isImportant: false, 0, isNewPlayer: false);
				GlobalValue.Set(jSONObject["id"].I, num, "NPCFactory.CreateHaiShangNpc 创建海上NPC");
				break;
			}
		}
		return num;
	}

	public void AuToCreateNpcs()
	{
		JSONObject npcCreateData = jsonData.instance.NpcCreateData;
		Tools.instance.getPlayer();
		if (NpcAuToCreateDictionary.Count < 1)
		{
			JSONObject nPCLeiXingDate = jsonData.instance.NPCLeiXingDate;
			foreach (string key in nPCLeiXingDate.keys)
			{
				if (nPCLeiXingDate[key]["Level"].I == 1)
				{
					int i = nPCLeiXingDate[key]["Type"].I;
					if (NpcAuToCreateDictionary.ContainsKey(i))
					{
						NpcAuToCreateDictionary[i].Add(key);
						continue;
					}
					NpcAuToCreateDictionary.Add(i, new List<string> { key });
				}
			}
		}
		int num = 0;
		string text = "";
		int num2 = 0;
		foreach (JSONObject item in npcCreateData.list)
		{
			num = item["NumA"].I;
			if (item["EventValue"].Count > 0 && GlobalValue.Get(item["EventValue"][0].I, "NPCFactory.AuToCreateNpcs 每10年自动生成NPC") == item["EventValue"][1].I)
			{
				num = item["NumB"].I;
			}
			num2 = item["id"].I;
			while (num > 0)
			{
				text = NpcAuToCreateDictionary[num2][getRandom(0, NpcAuToCreateDictionary[num2].Count - 1)];
				JSONObject npcDate = new JSONObject(jsonData.instance.NPCLeiXingDate[text].ToString());
				AfterCreateNpc(npcDate, isImportant: false, 0, isNewPlayer: false);
				num--;
			}
		}
	}

	public int CreateNpcByLiuPaiAndLevel(int liuPai, int level, int sex = 0)
	{
		foreach (JSONObject item in jsonData.instance.NPCLeiXingDate.list)
		{
			if (item["LiuPai"].I == liuPai && item["Level"].I == level)
			{
				JSONObject npcDate = new JSONObject(item.ToString());
				return AfterCreateNpc(npcDate, isImportant: false, 0, isNewPlayer: false, null, sex);
			}
		}
		Debug.LogError((object)"创建新Npc失败，不符合流派和境界");
		return 0;
	}

	public int createNpc(JSONObject npcDate, bool isImportant, int ZhiDingindex = 0, bool isNewPlayer = true, JSONObject importantJson = null)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jSONObject = new JSONObject();
		if (ZhiDingindex > 0)
		{
			jSONObject.SetField("id", ZhiDingindex);
		}
		else
		{
			jSONObject.SetField("id", player.NPCCreateIndex);
		}
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("StatusId", 1);
		jSONObject2.SetField("StatusTime", 60000);
		jSONObject.SetField("Status", jSONObject2);
		jSONObject.SetField("Name", "");
		jSONObject.SetField("IsTag", val: false);
		jSONObject.SetField("FirstName", npcDate["FirstName"].Str);
		jSONObject.SetField("face", 0);
		jSONObject.SetField("fightFace", 0);
		jSONObject.SetField("isImportant", isImportant);
		int val = getRandom(npcDate["NPCTag"][0].I, npcDate["NPCTag"][1].I);
		jSONObject.SetField("NPCTag", val);
		jSONObject.SetField("XingGe", getRandomXingGe(jsonData.instance.NPCTagDate[val.ToString()]["zhengxie"].I));
		jSONObject.SetField("ActionId", 1);
		jSONObject.SetField("IsKnowPlayer", val: false);
		jSONObject.SetField("QingFen", 0);
		jSONObject.SetField("CyList", JSONObject.arr);
		jSONObject.SetField("TuPoMiShu", JSONObject.arr);
		int i = npcDate["Type"].I;
		int i2 = npcDate["Level"].I;
		if (isImportant && importantJson.HasField("ChengHaoID"))
		{
			string str = jsonData.instance.NPCChengHaoData[importantJson["ChengHaoID"].I.ToString()]["ChengHao"].str;
			NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.SetField(importantJson["ChengHaoID"].I.ToString(), jSONObject["id"].I);
			jSONObject.SetField("Title", str.ToCN());
			jSONObject.SetField("ChengHaoID", importantJson["ChengHaoID"].I);
		}
		else
		{
			foreach (JSONObject item in jsonData.instance.NPCChengHaoData.list)
			{
				if (item["NPCType"].I == i && item["GongXian"].I == 0 && i2 >= item["Level"][0].I && i2 <= item["Level"][1].I)
				{
					jSONObject.SetField("Title", item["ChengHao"].str.ToCN());
					jSONObject.SetField("ChengHaoID", item["id"].I);
					break;
				}
			}
		}
		jSONObject.SetField("GongXian", 0);
		if (i == 3)
		{
			jSONObject.SetField("SexType", 2);
		}
		else
		{
			jSONObject.SetField("SexType", getRandom(1, 2));
		}
		jSONObject.SetField("Type", i);
		jSONObject.SetField("LiuPai", npcDate["LiuPai"].I);
		jSONObject.SetField("MenPai", npcDate["MengPai"].I);
		jSONObject.SetField("AvatarType", npcDate["AvatarType"].I);
		jSONObject.SetField("Level", i2);
		jSONObject.SetField("WuDaoValue", 0);
		jSONObject.SetField("WuDaoValueLevel", 0);
		jSONObject.SetField("EWWuDaoDian", 0);
		jSONObject.SetField("IsNeedHelp", val: false);
		if (isImportant)
		{
			jSONObject.SetField("BindingNpcID", importantJson["BindingNpcID"].I);
			jSONObject.SetField("SexType", importantJson["sex"].I);
			if (importantJson.HasField("ZhuJiTime"))
			{
				jSONObject.SetField("ZhuJiTime", importantJson["ZhuJiTime"].str);
				jSONObject.SetField("LianQiAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 4, "0001-1-1", importantJson["ZhuJiTime"].str));
			}
			if (importantJson.HasField("JinDanTime"))
			{
				jSONObject.SetField("JinDanTime", importantJson["JinDanTime"].str);
				if (jSONObject["Level"].I > 3)
				{
					jSONObject.SetField("ZhuJiAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 7, "0001-1-1", importantJson["JinDanTime"].str));
				}
				else
				{
					jSONObject.SetField("ZhuJiAddSpeed", GetEWaiXiuLianSpeed(4, 7, importantJson["ZhuJiTime"].str, importantJson["JinDanTime"].str));
				}
			}
			if (importantJson.HasField("YuanYingTime"))
			{
				jSONObject.SetField("YuanYingTime", importantJson["YuanYingTime"].str);
				if (jSONObject["Level"].I > 7)
				{
					jSONObject.SetField("JinDanAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 10, "0001-1-1", importantJson["YuanYingTime"].str));
				}
				else
				{
					jSONObject.SetField("JinDanAddSpeed", GetEWaiXiuLianSpeed(7, 10, importantJson["JinDanTime"].str, importantJson["YuanYingTime"].str));
				}
			}
			if (importantJson.HasField("HuaShengTime"))
			{
				jSONObject.SetField("HuaShengTime", importantJson["HuaShengTime"].str);
				if (jSONObject["Level"].I > 10)
				{
					jSONObject.SetField("YuanYingAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 13, "0001-1-1", importantJson["HuaShengTime"].str));
				}
				else
				{
					jSONObject.SetField("YuanYingAddSpeed", GetEWaiXiuLianSpeed(10, 13, importantJson["YuanYingTime"].str, importantJson["HuaShengTime"].str));
				}
			}
			NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(importantJson["BindingNpcID"].I, jSONObject["id"].I);
		}
		foreach (JSONObject item2 in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			if (i2 == item2["id"].I)
			{
				jSONObject.SetField("HP", getRandom(item2["HP"][0].I, item2["HP"][1].I));
				jSONObject.SetField("dunSu", getRandom(item2["dunSu"][0].I, item2["dunSu"][1].I));
				jSONObject.SetField("ziZhi", getRandom(item2["ziZhi"][0].I, item2["ziZhi"][1].I));
				jSONObject.SetField("wuXin", getRandom(item2["wuXin"][0].I, item2["wuXin"][1].I));
				jSONObject.SetField("shengShi", getRandom(item2["shengShi"][0].I, item2["shengShi"][1].I));
				jSONObject.SetField("shaQi", 0);
				jSONObject.SetField("shouYuan", getRandom(item2["shouYuan"][0].I, item2["shouYuan"][1].I));
				jSONObject.SetField("age", getRandom(item2["age"][0].I, item2["age"][1].I) * 12);
				jSONObject.SetField("exp", item2["xiuwei"].I);
				if (i2 <= 14)
				{
					jSONObject.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(i2 + 1).ToString()]["xiuwei"].I);
				}
				else
				{
					jSONObject.SetField("NextExp", 0);
				}
				jSONObject.SetField("equipWeapon", 0);
				jSONObject.SetField("equipList", new JSONObject());
				if (item2["equipWeapon"].I > 0)
				{
					int ItemID = 0;
					int i3 = item2["equipWeapon"].I;
					int i4 = npcDate["equipWeapon"][getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject ItemJson = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID, ref ItemJson, i4, null, i3);
					jSONObject["equipList"].SetField("Weapon1", ItemJson);
				}
				if (item2["equipWeapon2"].I > 0)
				{
					int ItemID2 = 0;
					int i5 = item2["equipWeapon2"].I;
					int i6 = npcDate["equipWeapon"][getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject ItemJson2 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID2, ref ItemJson2, i6, null, i5);
					jSONObject["equipList"].SetField("Weapon2", ItemJson2);
				}
				if (item2["equipClothing"].I > 0)
				{
					int ItemID3 = 0;
					int i7 = item2["equipClothing"].I;
					int i8 = npcDate["equipClothing"][getRandom(0, npcDate["equipClothing"].I)].I;
					JSONObject ItemJson3 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID3, ref ItemJson3, i8, null, i7);
					jSONObject["equipList"].SetField("Clothing", ItemJson3);
				}
				if (item2["equipRing"].I > 0)
				{
					int ItemID4 = 0;
					int i9 = item2["equipRing"].I;
					int i10 = npcDate["equipRing"][getRandom(0, npcDate["equipRing"].I)].I;
					JSONObject ItemJson4 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID4, ref ItemJson4, i10, null, i9);
					jSONObject["equipList"].SetField("Ring", ItemJson4);
				}
				jSONObject.SetField("equipWeaponPianHao", npcDate["equipWeapon"]);
				jSONObject.SetField("equipWeapon2PianHao", npcDate["equipWeapon"]);
				jSONObject.SetField("equipClothingPianHao", npcDate["equipClothing"]);
				jSONObject.SetField("equipRingPianHao", npcDate["equipRing"]);
				jSONObject.SetField("equipClothing", 0);
				jSONObject.SetField("equipRing", 0);
				jSONObject.SetField("LingGen", npcDate["LingGen"]);
				jSONObject.SetField("skills", npcDate["skills"]);
				jSONObject.SetField("JinDanType", npcDate["JinDanType"]);
				jSONObject.SetField("staticSkills", npcDate["staticSkills"]);
				jSONObject.SetField("xiuLianSpeed", getXiuLianSpeed(npcDate["staticSkills"], jSONObject["ziZhi"].I));
				jSONObject.SetField("yuanying", npcDate["yuanying"]);
				jSONObject.SetField("MoneyType", getRandom(item2["MoneyType"][0].I, item2["MoneyType"][1].I));
				jSONObject.SetField("IsRefresh", 0);
				jSONObject.SetField("dropType", 0);
				jSONObject.SetField("canjiaPaiMai", npcDate["canjiaPaiMai"].I);
				jSONObject.SetField("paimaifenzu", npcDate["paimaifenzu"]);
				jSONObject.SetField("wudaoType", npcDate["wudaoType"]);
				jSONObject.SetField("XinQuType", npcDate["XinQuType"]);
				jSONObject.SetField("gudingjiage", 0);
				jSONObject.SetField("sellPercent", 0);
				jSONObject.SetField("useItem", new JSONObject());
				jSONObject.SetField("NoteBook", new JSONObject());
				SetNpcWuDao(i2, npcDate["wudaoType"].I, jSONObject);
				break;
			}
		}
		UpNpcWuDaoByTag(jSONObject["NPCTag"].I, jSONObject);
		if (isImportant)
		{
			jSONObject.SetField("ziZhi", importantJson["zizhi"].I);
			jSONObject.SetField("wuXin", importantJson["wuxing"].I);
			jSONObject.SetField("age", importantJson["nianling"].I * 12);
			jSONObject.SetField("XingGe", importantJson["XingGe"].I);
			jSONObject.SetField("NPCTag", importantJson["NPCTag"].I);
		}
		if (ZhiDingindex > 0)
		{
			jsonData.instance.AvatarJsonData.SetField(ZhiDingindex.ToString(), jSONObject);
		}
		else
		{
			jsonData.instance.AvatarJsonData.SetField(player.NPCCreateIndex.ToString(), jSONObject);
			player.NPCCreateIndex++;
		}
		return jSONObject["id"].I;
	}

	public void SetNpcLevel(int npcId, int npcLevel)
	{
		if (!jsonData.instance.AvatarJsonData.HasField(npcId.ToString()))
		{
			Debug.LogError((object)$"不存在当前npcId {npcId}");
			return;
		}
		Tools.instance.getPlayer();
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		jSONObject.SetField("Level", npcLevel);
		int i = jSONObject["Level"].I;
		int i2 = jSONObject["LiuPai"].I;
		JSONObject jSONObject2 = null;
		for (int j = 0; j < jsonData.instance.NPCLeiXingDate.Count; j++)
		{
			if (i == jsonData.instance.NPCLeiXingDate[j]["Level"].I && i2 == jsonData.instance.NPCLeiXingDate[j]["LiuPai"].I)
			{
				jSONObject2 = new JSONObject(jsonData.instance.NPCLeiXingDate[j].ToString());
				break;
			}
		}
		foreach (JSONObject item in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			if (i == item["id"].I)
			{
				jSONObject.SetField("HP", getRandom(item["HP"][0].I, item["HP"][1].I));
				jSONObject.SetField("dunSu", getRandom(item["dunSu"][0].I, item["dunSu"][1].I));
				jSONObject.SetField("shengShi", getRandom(item["shengShi"][0].I, item["shengShi"][1].I));
				jSONObject.SetField("shaQi", 0);
				jSONObject.SetField("shouYuan", getRandom(item["shouYuan"][0].I, item["shouYuan"][1].I));
				jSONObject.SetField("age", getRandom(item["age"][0].I, item["age"][1].I) * 12);
				jSONObject.SetField("exp", item["xiuwei"].I);
				if (i <= 14)
				{
					jSONObject.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(i + 1).ToString()]["xiuwei"].I);
				}
				else
				{
					jSONObject.SetField("NextExp", 0);
				}
				jSONObject.SetField("equipList", new JSONObject());
				if (item["equipWeapon"].I > 0)
				{
					int ItemID = 0;
					int i3 = item["equipWeapon"].I;
					int i4 = jSONObject2["equipWeapon"][getRandom(0, jSONObject2["equipWeapon"].I)].I;
					JSONObject ItemJson = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID, ref ItemJson, i4, null, i3);
					jSONObject["equipList"].SetField("Weapon1", ItemJson);
				}
				if (item["equipWeapon2"].I > 0)
				{
					int ItemID2 = 0;
					int i5 = item["equipWeapon2"].I;
					int i6 = jSONObject2["equipWeapon"][getRandom(0, jSONObject2["equipWeapon"].I)].I;
					JSONObject ItemJson2 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID2, ref ItemJson2, i6, null, i5);
					jSONObject["equipList"].SetField("Weapon2", ItemJson2);
				}
				if (item["equipClothing"].I > 0)
				{
					int ItemID3 = 0;
					int i7 = item["equipClothing"].I;
					int i8 = jSONObject2["equipClothing"][getRandom(0, jSONObject2["equipClothing"].I)].I;
					JSONObject ItemJson3 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID3, ref ItemJson3, i8, null, i7);
					jSONObject["equipList"].SetField("Clothing", ItemJson3);
				}
				if (item["equipRing"].I > 0)
				{
					int ItemID4 = 0;
					int i9 = item["equipRing"].I;
					int i10 = jSONObject2["equipRing"][getRandom(0, jSONObject2["equipRing"].I)].I;
					JSONObject ItemJson4 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID4, ref ItemJson4, i10, null, i9);
					jSONObject["equipList"].SetField("Ring", ItemJson4);
				}
				jSONObject.SetField("yuanying", jSONObject2["yuanying"]);
				jSONObject.SetField("LingGen", jSONObject2["LingGen"]);
				jSONObject.SetField("skills", jSONObject2["skills"]);
				jSONObject.SetField("staticSkills", jSONObject2["staticSkills"]);
				jSONObject.SetField("MoneyType", getRandom(item["MoneyType"][0].I, item["MoneyType"][1].I));
				SetNpcWuDao(i, jSONObject2["wudaoType"].I, jSONObject);
				break;
			}
		}
		UpNpcWuDaoByTag(jSONObject["NPCTag"].I, jSONObject);
		FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jSONObject["id"].I, jSONObject);
	}

	public int AfterCreateNpc(JSONObject npcDate, bool isImportant, int ZhiDingindex = 0, bool isNewPlayer = true, JSONObject importantJson = null, int setSex = 0)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jSONObject = new JSONObject();
		if (ZhiDingindex > 0)
		{
			jSONObject.SetField("id", ZhiDingindex);
		}
		else
		{
			jSONObject.SetField("id", player.NPCCreateIndex);
		}
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("StatusId", 1);
		jSONObject2.SetField("StatusTime", 60000);
		jSONObject.SetField("Status", jSONObject2);
		jSONObject.SetField("Name", "");
		jSONObject.SetField("IsTag", val: false);
		jSONObject.SetField("FirstName", npcDate["FirstName"].Str);
		jSONObject.SetField("face", 0);
		jSONObject.SetField("fightFace", 0);
		jSONObject.SetField("CyList", JSONObject.arr);
		jSONObject.SetField("isImportant", isImportant);
		int val = getRandom(npcDate["NPCTag"][0].I, npcDate["NPCTag"][1].I);
		jSONObject.SetField("NPCTag", val);
		jSONObject.SetField("XingGe", getRandomXingGe(jsonData.instance.NPCTagDate[val.ToString()]["zhengxie"].I));
		jSONObject.SetField("ActionId", 1);
		jSONObject.SetField("IsKnowPlayer", val: false);
		jSONObject.SetField("QingFen", 0);
		jSONObject.SetField("TuPoMiShu", JSONObject.arr);
		int i = npcDate["Type"].I;
		int i2 = npcDate["Level"].I;
		if (isImportant && importantJson.HasField("ChengHaoID"))
		{
			string str = jsonData.instance.NPCChengHaoData[importantJson["ChengHaoID"].I.ToString()]["ChengHao"].str;
			NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.SetField(importantJson["ChengHaoID"].I.ToString(), jSONObject["id"].I);
			jSONObject.SetField("Title", str.ToCN());
			jSONObject.SetField("ChengHaoID", importantJson["ChengHaoID"].I);
		}
		else
		{
			foreach (JSONObject item in jsonData.instance.NPCChengHaoData.list)
			{
				if (item["NPCType"].I == i && item["GongXian"].I == 0 && i2 >= item["Level"][0].I && i2 <= item["Level"][1].I)
				{
					jSONObject.SetField("Title", item["ChengHao"].str.ToCN());
					jSONObject.SetField("ChengHaoID", item["id"].I);
					break;
				}
			}
		}
		jSONObject.SetField("GongXian", 0);
		if (i == 3)
		{
			jSONObject.SetField("SexType", 2);
		}
		else if (setSex != 0)
		{
			jSONObject.SetField("SexType", setSex);
		}
		else
		{
			jSONObject.SetField("SexType", getRandom(1, 2));
		}
		jSONObject.SetField("Type", i);
		jSONObject.SetField("LiuPai", npcDate["LiuPai"].I);
		jSONObject.SetField("MenPai", npcDate["MengPai"].I);
		jSONObject.SetField("AvatarType", npcDate["AvatarType"].I);
		jSONObject.SetField("Level", i2);
		jSONObject.SetField("WuDaoValue", 0);
		jSONObject.SetField("WuDaoValueLevel", 0);
		jSONObject.SetField("EWWuDaoDian", 0);
		jSONObject.SetField("IsNeedHelp", val: false);
		if (isImportant)
		{
			jSONObject.SetField("BindingNpcID", importantJson["BindingNpcID"].I);
			jSONObject.SetField("SexType", importantJson["sex"].I);
			if (importantJson.HasField("ZhuJiTime"))
			{
				jSONObject.SetField("ZhuJiTime", importantJson["ZhuJiTime"].str);
				jSONObject.SetField("LianQiAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 4, "0001-1-1", importantJson["ZhuJiTime"].str));
			}
			if (importantJson.HasField("JinDanTime"))
			{
				jSONObject.SetField("JinDanTime", importantJson["JinDanTime"].str);
				if (jSONObject["Level"].I > 3)
				{
					jSONObject.SetField("ZhuJiAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 7, "0001-1-1", importantJson["JinDanTime"].str));
				}
				else
				{
					jSONObject.SetField("ZhuJiAddSpeed", GetEWaiXiuLianSpeed(4, 7, importantJson["ZhuJiTime"].str, importantJson["JinDanTime"].str));
				}
			}
			if (importantJson.HasField("YuanYingTime"))
			{
				jSONObject.SetField("YuanYingTime", importantJson["YuanYingTime"].str);
				if (jSONObject["Level"].I > 7)
				{
					jSONObject.SetField("JinDanAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 10, "0001-1-1", importantJson["YuanYingTime"].str));
				}
				else
				{
					jSONObject.SetField("JinDanAddSpeed", GetEWaiXiuLianSpeed(7, 10, importantJson["JinDanTime"].str, importantJson["YuanYingTime"].str));
				}
			}
			if (importantJson.HasField("HuaShengTime"))
			{
				jSONObject.SetField("HuaShengTime", importantJson["HuaShengTime"].str);
				if (jSONObject["Level"].I > 11)
				{
					jSONObject.SetField("YuanYingAddSpeed", GetEWaiXiuLianSpeed(jSONObject["Level"].I, 13, "0001-1-1", importantJson["HuaShengTime"].str));
				}
				else
				{
					jSONObject.SetField("YuanYingAddSpeed", GetEWaiXiuLianSpeed(10, 13, importantJson["YuanYingTime"].str, importantJson["HuaShengTime"].str));
				}
			}
			NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(importantJson["BindingNpcID"].I, jSONObject["id"].I);
		}
		foreach (JSONObject item2 in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			if (i2 == item2["id"].I)
			{
				jSONObject.SetField("HP", getRandom(item2["HP"][0].I, item2["HP"][1].I));
				jSONObject.SetField("dunSu", getRandom(item2["dunSu"][0].I, item2["dunSu"][1].I));
				if (npcDate["MengPai"].I > 0)
				{
					jSONObject.SetField("ziZhi", getRandom(item2["ziZhi"][0].I, item2["ziZhi"][1].I) + 20);
					jSONObject.SetField("wuXin", getRandom(item2["wuXin"][0].I, item2["wuXin"][1].I) + 20);
				}
				else
				{
					jSONObject.SetField("ziZhi", getRandom(item2["ziZhi"][0].I, item2["ziZhi"][1].I));
					jSONObject.SetField("wuXin", getRandom(item2["wuXin"][0].I, item2["wuXin"][1].I));
				}
				jSONObject.SetField("shengShi", getRandom(item2["shengShi"][0].I, item2["shengShi"][1].I));
				jSONObject.SetField("shaQi", 0);
				jSONObject.SetField("shouYuan", getRandom(item2["shouYuan"][0].I, item2["shouYuan"][1].I));
				jSONObject.SetField("age", getRandom(item2["age"][0].I, item2["age"][1].I) * 12);
				jSONObject.SetField("exp", item2["xiuwei"].I);
				if (i2 <= 14)
				{
					jSONObject.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(i2 + 1).ToString()]["xiuwei"].I);
				}
				else
				{
					jSONObject.SetField("NextExp", 0);
				}
				jSONObject.SetField("equipWeapon", 0);
				jSONObject.SetField("equipList", new JSONObject());
				if (item2["equipWeapon"].I > 0)
				{
					int ItemID = 0;
					int i3 = item2["equipWeapon"].I;
					int i4 = npcDate["equipWeapon"][getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject ItemJson = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID, ref ItemJson, i4, null, i3);
					jSONObject["equipList"].SetField("Weapon1", ItemJson);
				}
				if (item2["equipWeapon2"].I > 0)
				{
					int ItemID2 = 0;
					int i5 = item2["equipWeapon2"].I;
					int i6 = npcDate["equipWeapon"][getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject ItemJson2 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID2, ref ItemJson2, i6, null, i5);
					jSONObject["equipList"].SetField("Weapon2", ItemJson2);
				}
				if (item2["equipClothing"].I > 0)
				{
					int ItemID3 = 0;
					int i7 = item2["equipClothing"].I;
					int i8 = npcDate["equipClothing"][getRandom(0, npcDate["equipClothing"].I)].I;
					JSONObject ItemJson3 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID3, ref ItemJson3, i8, null, i7);
					jSONObject["equipList"].SetField("Clothing", ItemJson3);
				}
				if (item2["equipRing"].I > 0)
				{
					int ItemID4 = 0;
					int i9 = item2["equipRing"].I;
					int i10 = npcDate["equipRing"][getRandom(0, npcDate["equipRing"].I)].I;
					JSONObject ItemJson4 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref ItemID4, ref ItemJson4, i10, null, i9);
					jSONObject["equipList"].SetField("Ring", ItemJson4);
				}
				jSONObject.SetField("equipWeaponPianHao", npcDate["equipWeapon"]);
				jSONObject.SetField("equipWeapon2PianHao", npcDate["equipWeapon"]);
				jSONObject.SetField("equipClothingPianHao", npcDate["equipClothing"]);
				jSONObject.SetField("equipRingPianHao", npcDate["equipRing"]);
				jSONObject.SetField("equipClothing", 0);
				jSONObject.SetField("equipRing", 0);
				jSONObject.SetField("LingGen", npcDate["LingGen"]);
				jSONObject.SetField("skills", npcDate["skills"]);
				jSONObject.SetField("JinDanType", npcDate["JinDanType"]);
				jSONObject.SetField("staticSkills", npcDate["staticSkills"]);
				jSONObject.SetField("xiuLianSpeed", getXiuLianSpeed(npcDate["staticSkills"], jSONObject["ziZhi"].I));
				jSONObject.SetField("yuanying", npcDate["yuanying"]);
				jSONObject.SetField("HuaShenLingYu", npcDate["HuaShenLingYu"]);
				jSONObject.SetField("MoneyType", getRandom(item2["MoneyType"][0].I, item2["MoneyType"][1].I));
				jSONObject.SetField("IsRefresh", 0);
				jSONObject.SetField("dropType", 0);
				jSONObject.SetField("canjiaPaiMai", npcDate["canjiaPaiMai"].I);
				jSONObject.SetField("paimaifenzu", npcDate["paimaifenzu"]);
				jSONObject.SetField("wudaoType", npcDate["wudaoType"]);
				jSONObject.SetField("XinQuType", npcDate["XinQuType"]);
				jSONObject.SetField("gudingjiage", 0);
				jSONObject.SetField("sellPercent", 0);
				jSONObject.SetField("useItem", new JSONObject());
				jSONObject.SetField("NoteBook", new JSONObject());
				SetNpcWuDao(i2, npcDate["wudaoType"].I, jSONObject);
				break;
			}
		}
		UpNpcWuDaoByTag(jSONObject["NPCTag"].I, jSONObject);
		if (isImportant)
		{
			jSONObject.SetField("ziZhi", importantJson["zizhi"].I);
			jSONObject.SetField("wuXin", importantJson["wuxing"].I);
			jSONObject.SetField("age", importantJson["nianling"].I * 12);
			jSONObject.SetField("XingGe", importantJson["XingGe"].I);
		}
		if (isImportant)
		{
			jsonData.instance.AvatarRandomJsonData.SetField(jSONObject["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[jSONObject["BindingNpcID"].I.ToString()]);
		}
		else
		{
			JSONObject jSONObject3 = jsonData.instance.randomAvatarFace(jSONObject);
			jsonData.instance.AvatarRandomJsonData.SetField(string.Concat(jSONObject["id"].I), jSONObject3.Copy());
		}
		FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jSONObject["id"].I, jSONObject);
		if (ZhiDingindex > 0)
		{
			jsonData.instance.AvatarJsonData.SetField(ZhiDingindex.ToString(), jSONObject);
		}
		else
		{
			jsonData.instance.AvatarJsonData.SetField(player.NPCCreateIndex.ToString(), jSONObject);
			player.NPCCreateIndex++;
		}
		return jSONObject["id"].I;
	}

	public int getXiuLianSpeed(JSONObject gongFaList, float zizhi)
	{
		float num = 0f;
		for (int i = 0; i < gongFaList.list.Count; i++)
		{
			if (num < (float)jsonData.instance.StaticSkillJsonData[gongFaList[i].I.ToString()]["Skill_Speed"].I)
			{
				num = jsonData.instance.StaticSkillJsonData[gongFaList[i].I.ToString()]["Skill_Speed"].I;
			}
		}
		num *= 1f + zizhi / 100f;
		return (int)num;
	}

	public void SetNpcWuDao(int level, int wudaoType, JSONObject npcDate)
	{
		for (int i = 0; i < jsonData.instance.NPCWuDaoJson.Count; i++)
		{
			if (jsonData.instance.NPCWuDaoJson[i]["lv"].I != level || jsonData.instance.NPCWuDaoJson[i]["Type"].I != wudaoType)
			{
				continue;
			}
			JSONObject jSONObject = jsonData.instance.NPCWuDaoJson[i];
			JSONObject jSONObject2 = new JSONObject();
			foreach (JSONObject item in jSONObject["wudaoID"].list)
			{
				jSONObject2.Add(item.I);
			}
			npcDate.SetField("wuDaoSkillList", jSONObject2);
			JSONObject jSONObject3 = new JSONObject();
			for (int j = 1; j <= 12; j++)
			{
				JSONObject jSONObject4 = new JSONObject();
				int val = ((j <= 10) ? j : (j + 10));
				jSONObject4.SetField("id", val);
				int i2 = jSONObject["value" + j].I;
				jSONObject4.SetField("level", i2);
				int val2 = 0;
				if (i2 != 0)
				{
					val2 = jsonData.instance.WuDaoJinJieJson[i2 - 1]["Max"].I;
				}
				jSONObject4.SetField("exp", val2);
				jSONObject3.SetField(val.ToString(), jSONObject4);
			}
			npcDate.SetField("wuDaoJson", jSONObject3);
			break;
		}
	}

	public void UpNpcWuDaoByTag(int tag, JSONObject npcData)
	{
		JSONObject jSONObject = jsonData.instance.NPCTagDate[tag.ToString()];
		if (jSONObject["WuDao"].Count >= 1)
		{
			int i = npcData["Level"].I;
			int num = i / 3;
			if (i % 3 == 0)
			{
				num--;
			}
			num++;
			for (int j = 0; j < jSONObject["WuDao"].Count; j++)
			{
				npcData["wuDaoJson"][jSONObject["WuDao"][j].I.ToString()].SetField("level", num);
				npcData["wuDaoJson"][jSONObject["WuDao"][j].I.ToString()].SetField("exp", jsonData.instance.WuDaoJinJieJson[num - 1]["Max"].I);
			}
		}
	}

	public void createImprotantNpc(JSONObject ImprotantNpcDate, bool isNewPlayer = true)
	{
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			int i2 = ImprotantNpcDate["level"].I;
			int i3 = ImprotantNpcDate["LiuPai"].I;
			if (i2 == jsonData.instance.NPCLeiXingDate[i]["Level"].I && i3 == jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I)
			{
				JSONObject jSONObject = new JSONObject();
				JSONObject npcDate = jsonData.instance.NPCLeiXingDate[i];
				jSONObject.SetField("BindingNpcID", ImprotantNpcDate["id"].I);
				jSONObject.SetField("sex", ImprotantNpcDate["sex"].I);
				if (ImprotantNpcDate["ChengHao"].I > 0)
				{
					jSONObject.SetField("ChengHaoID", ImprotantNpcDate["ChengHao"].I);
				}
				if (ImprotantNpcDate["ZhuJiTime"].str != "" && ImprotantNpcDate["ZhuJiTime"] != null)
				{
					jSONObject.SetField("ZhuJiTime", ImprotantNpcDate["ZhuJiTime"].str);
				}
				if (ImprotantNpcDate["JinDanTime"].str != "" && ImprotantNpcDate["JinDanTime"] != null)
				{
					jSONObject.SetField("JinDanTime", ImprotantNpcDate["JinDanTime"].str);
				}
				if (ImprotantNpcDate["YuanYingTime"].str != "" && ImprotantNpcDate["YuanYingTime"] != null)
				{
					jSONObject.SetField("YuanYingTime", ImprotantNpcDate["YuanYingTime"].str);
				}
				if (ImprotantNpcDate["HuaShengTime"].str != "" && ImprotantNpcDate["HuaShengTime"] != null)
				{
					jSONObject.SetField("HuaShengTime", ImprotantNpcDate["HuaShengTime"].str);
				}
				jSONObject.SetField("zizhi", ImprotantNpcDate["zizhi"].I);
				jSONObject.SetField("wuxing", ImprotantNpcDate["wuxing"].I);
				jSONObject.SetField("XingGe", ImprotantNpcDate["XingGe"].I);
				jSONObject.SetField("nianling", ImprotantNpcDate["nianling"].I);
				jSONObject.SetField("NPCTag", ImprotantNpcDate["NPCTag"].I);
				createNpc(npcDate, isImportant: true, 0, isNewPlayer, jSONObject);
				break;
			}
		}
	}

	public void AfterCreateImprotantNpc(JSONObject ImprotantNpcDate, bool isNewPlayer = true)
	{
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			int num = ImprotantNpcDate["level"].I;
			DateTime nowTime = Tools.instance.getPlayer().worldTimeMag.getNowTime();
			int i2 = ImprotantNpcDate["LiuPai"].I;
			if (ImprotantNpcDate["HuaShengTime"].str != "" && nowTime >= DateTime.Parse(ImprotantNpcDate["HuaShengTime"].str))
			{
				num = 13;
			}
			else if (ImprotantNpcDate["YuanYingTime"].str != "" && nowTime >= DateTime.Parse(ImprotantNpcDate["YuanYingTime"].str))
			{
				num = 10;
			}
			else if (ImprotantNpcDate["JinDanTime"].str != "" && nowTime >= DateTime.Parse(ImprotantNpcDate["JinDanTime"].str))
			{
				num = 7;
			}
			else if (ImprotantNpcDate["ZhuJiTime"].str != "" && nowTime >= DateTime.Parse(ImprotantNpcDate["ZhuJiTime"].str))
			{
				num = 4;
			}
			if (num == jsonData.instance.NPCLeiXingDate[i]["Level"].I && i2 == jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I)
			{
				JSONObject jSONObject = new JSONObject();
				JSONObject jSONObject2 = jsonData.instance.NPCLeiXingDate[i];
				jSONObject.SetField("BindingNpcID", ImprotantNpcDate["id"].I);
				jSONObject.SetField("sex", ImprotantNpcDate["sex"].I);
				if (ImprotantNpcDate["ChengHao"].I > 0)
				{
					jSONObject.SetField("ChengHaoID", ImprotantNpcDate["ChengHao"].I);
				}
				if (ImprotantNpcDate["ZhuJiTime"].str != "" && ImprotantNpcDate["ZhuJiTime"] != null)
				{
					jSONObject.SetField("ZhuJiTime", ImprotantNpcDate["ZhuJiTime"].str);
				}
				if (ImprotantNpcDate["JinDanTime"].str != "" && ImprotantNpcDate["JinDanTime"] != null)
				{
					jSONObject.SetField("JinDanTime", ImprotantNpcDate["JinDanTime"].str);
				}
				if (ImprotantNpcDate["YuanYingTime"].str != "" && ImprotantNpcDate["YuanYingTime"] != null)
				{
					jSONObject.SetField("YuanYingTime", ImprotantNpcDate["YuanYingTime"].str);
				}
				if (ImprotantNpcDate["HuaShengTime"].str != "" && ImprotantNpcDate["HuaShengTime"] != null)
				{
					jSONObject.SetField("HuaShengTime", ImprotantNpcDate["HuaShengTime"].str);
				}
				jSONObject.SetField("zizhi", ImprotantNpcDate["zizhi"].I);
				jSONObject.SetField("wuxing", ImprotantNpcDate["wuxing"].I);
				jSONObject.SetField("XingGe", ImprotantNpcDate["XingGe"].I);
				jSONObject.SetField("nianling", ImprotantNpcDate["nianling"].I);
				JSONObject npcDate = new JSONObject(jSONObject2.ToString());
				AfterCreateNpc(npcDate, isImportant: true, 0, isNewPlayer, jSONObject);
				break;
			}
		}
	}

	public int getRandom(int min, int max)
	{
		return random.Next(min, max + 1);
	}

	public void InitAutoCreateNpcBackpack(JSONObject jsondata, int avatarID, JSONObject data = null)
	{
		if (jsondata == null)
		{
			jsondata = new JSONObject();
		}
		jsondata.SetField(string.Concat(avatarID), new JSONObject());
		jsondata[string.Concat(avatarID)].SetField("Backpack", new JSONObject(JSONObject.Type.ARRAY));
		int num = 1;
		int num2 = 0;
		int num3 = 0;
		if (data == null)
		{
			num2 = jsonData.instance.AvatarJsonData[avatarID.ToString()]["Level"].I;
			num = (int)jsonData.instance.AvatarJsonData[string.Concat(avatarID)]["MoneyType"].n;
			num3 = jsonData.instance.AvatarJsonData[avatarID.ToString()]["NPCTag"].I;
		}
		else
		{
			num2 = data["Level"].I;
			num = data["MoneyType"].I;
			num3 = data["NPCTag"].I;
		}
		int num4 = num;
		int num5 = num;
		if (num < 50)
		{
			num4 = (int)jsonData.instance.AvatarMoneyJsonData[string.Concat(num)]["Max"].n - (int)jsonData.instance.AvatarMoneyJsonData[string.Concat(num)]["Min"].n;
			num5 = jsonData.instance.QuikeGetRandom() % num4;
			jsondata[string.Concat(avatarID)].SetField("money", (int)jsonData.instance.AvatarMoneyJsonData[string.Concat(num)]["Min"].n + num5);
		}
		jsondata[string.Concat(avatarID)].SetField("death", 0);
		JSONObject jSONObject = jsonData.instance.NpcBeiBaoTypeData[GetBeiBaoIdByTag(num3, num2).ToString()];
		int num6 = jsonData.instance.NPCChuShiShuZiDate[num2.ToString()]["bag"].I;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		List<int> list = new List<int>();
		while (num6 >= 0)
		{
			int index = getRandom(0, jSONObject["ShopType"].Count - 1);
			int i = jSONObject["ShopType"][index].I;
			int i2 = jSONObject["quality"][index].I;
			JSONObject randomItemByShopType = GetRandomItemByShopType(i, i2);
			if (randomItemByShopType == null)
			{
				continue;
			}
			int i3 = randomItemByShopType["id"].I;
			num6 -= jsonData.instance.ItemJsonData[i3.ToString()]["price"].I;
			if (randomItemByShopType["maxNum"].I > 1)
			{
				if (dictionary.ContainsKey(i3))
				{
					dictionary[i3]++;
				}
				else
				{
					dictionary.Add(i3, 1);
				}
			}
			else
			{
				list.Add(i3);
			}
		}
		foreach (int key in dictionary.Keys)
		{
			JSONObject jSONObject2 = jsonData.instance.setAvatarBackpack(Tools.getUUID(), key, dictionary[key], 1, 100, 1, Tools.CreateItemSeid(key));
			jsondata[avatarID.ToString()]["Backpack"].Add(jSONObject2);
		}
		for (int j = 0; j < list.Count; j++)
		{
			JSONObject jSONObject3 = jsonData.instance.setAvatarBackpack(Tools.getUUID(), list[j], 1, 1, 100, 1, Tools.CreateItemSeid(list[j]));
			jsondata[avatarID.ToString()]["Backpack"].Add(jSONObject3);
		}
		jsondata[avatarID.ToString()].SetField("CanSell", 1);
		jsondata[avatarID.ToString()].SetField("SellPercent", 100);
		jsondata[avatarID.ToString()].SetField("CanDrop", 1);
	}

	public int getRandomXingGe(int zhengXie)
	{
		if (!NpcXingGeDictionary.ContainsKey(zhengXie))
		{
			for (int i = 0; i < jsonData.instance.NpcXingGeDate.Count; i++)
			{
				if (NpcXingGeDictionary.ContainsKey(jsonData.instance.NpcXingGeDate[i]["zhengxie"].I))
				{
					NpcXingGeDictionary[jsonData.instance.NpcXingGeDate[i]["zhengxie"].I].Add(jsonData.instance.NpcXingGeDate[i]["id"].I);
					continue;
				}
				NpcXingGeDictionary.Add(jsonData.instance.NpcXingGeDate[i]["zhengxie"].I, new List<int> { jsonData.instance.NpcXingGeDate[i]["id"].I });
			}
		}
		return NpcXingGeDictionary[zhengXie][getRandom(0, NpcXingGeDictionary[zhengXie].Count - 1)];
	}

	public JSONObject GetRandomItemByShopType(int shopType, int quality)
	{
		lock (obj)
		{
			if (!shopTypeDictionary.ContainsKey(shopType))
			{
				JSONObject itemJsonData = jsonData.instance._ItemJsonData;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < itemJsonData.Count; i++)
				{
					num = itemJsonData[i]["ShopType"].I;
					num2 = itemJsonData[i]["quality"].I;
					if (shopTypeDictionary.ContainsKey(num))
					{
						if (shopTypeDictionary[num].ContainsKey(num2))
						{
							shopTypeDictionary[num][num2].Add(itemJsonData[i]);
							continue;
						}
						shopTypeDictionary[num].Add(num2, new List<JSONObject> { itemJsonData[i] });
					}
					else
					{
						Dictionary<int, List<JSONObject>> dictionary = new Dictionary<int, List<JSONObject>>();
						dictionary.Add(num2, new List<JSONObject> { itemJsonData[i] });
						shopTypeDictionary.Add(num, dictionary);
					}
				}
			}
			try
			{
				return shopTypeDictionary[shopType][quality][getRandom(0, shopTypeDictionary[shopType][quality].Count - 1)];
			}
			catch (Exception)
			{
				Debug.LogError((object)$"不存在shopType:{shopType},quality{quality}");
				return null;
			}
		}
	}

	public int GetBeiBaoIdByTag(int tag, int level)
	{
		JSONObject jSONObject = jsonData.instance.NPCTagDate[tag.ToString()]["BeiBaoType"];
		int i = jSONObject[getRandom(0, jSONObject.Count - 1)].I;
		foreach (JSONObject item in jsonData.instance.NpcBeiBaoTypeData.list)
		{
			if (item["BagTpye"].I == i && item["JinJie"].I == level)
			{
				return item["id"].I;
			}
		}
		Debug.LogError((object)"GetBeiBaoIdByTag方法报错");
		return 1;
	}

	public JSONObject GetRandomItemByType(int targetType, int quality)
	{
		if (!typeDictionary.ContainsKey(targetType))
		{
			JSONObject itemJsonData = jsonData.instance._ItemJsonData;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < itemJsonData.Count; i++)
			{
				num = itemJsonData[i]["type"].I;
				num2 = itemJsonData[i]["quality"].I;
				if (typeDictionary.ContainsKey(num))
				{
					if (typeDictionary[num].ContainsKey(num2))
					{
						typeDictionary[num][num2].Add(itemJsonData[i]);
						continue;
					}
					typeDictionary[num].Add(num2, new List<JSONObject> { itemJsonData[i] });
				}
				else
				{
					Dictionary<int, List<JSONObject>> dictionary = new Dictionary<int, List<JSONObject>>();
					dictionary.Add(num2, new List<JSONObject> { itemJsonData[i] });
					typeDictionary.Add(num, dictionary);
				}
			}
		}
		try
		{
			return typeDictionary[targetType][quality][getRandom(0, typeDictionary[targetType][quality].Count - 1)];
		}
		catch (Exception)
		{
			Debug.LogError((object)$"不存在Type:{targetType},quality{quality}");
			return null;
		}
	}

	public int GetEWaiXiuLianSpeed(int curLevel, int targetLevel, string _curTime, string _targetTime)
	{
		try
		{
			DateTime dateTime = DateTime.Parse(_curTime);
			int num = (DateTime.Parse(_targetTime) - dateTime).Days / 30;
			int num2 = 0;
			for (int i = curLevel; i <= targetLevel; i++)
			{
				num2 += jsonData.instance.NPCChuShiShuZiDate[i.ToString()]["xiuwei"].I;
			}
			return num2 / num;
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex);
		}
		return 0;
	}
}
