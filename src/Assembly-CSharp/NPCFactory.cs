using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

// Token: 0x020003F0 RID: 1008
public class NPCFactory
{
	// Token: 0x06001B5A RID: 7002 RVA: 0x000F1E58 File Offset: 0x000F0058
	public void firstCreateNpcs()
	{
		Tools.instance.getPlayer();
		this.isNewGame = true;
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
							JSONObject npcDate = new JSONObject(jsonData.instance.NPCLeiXingDate[l].ToString(), -2, false, false);
							this.createNpc(npcDate, false, 0, true, null);
							break;
						}
					}
				}
			}
		}
		for (int m = 0; m < jsonData.instance.NPCImportantDate.Count; m++)
		{
			JSONObject improtantNpcDate = new JSONObject(jsonData.instance.NPCImportantDate[m].ToString(), -2, false, false);
			this.createImprotantNpc(improtantNpcDate, true);
		}
		foreach (JSONObject jsonobject in jsonData.instance.NpcHaiShangCreateData.list)
		{
			foreach (JSONObject jsonobject2 in jsonData.instance.NPCLeiXingDate.list)
			{
				if (jsonobject2["LiuPai"].I == jsonobject["LiuPai"].I && jsonobject2["Level"].I == jsonobject["level"].I)
				{
					JSONObject npcDate2 = new JSONObject(jsonobject2.ToString(), -2, false, false);
					int value = this.createNpc(npcDate2, false, 0, true, null);
					GlobalValue.Set(jsonobject["id"].I, value, "NPCFactory.firstCreateNpcs 海上NPC生成");
					break;
				}
			}
		}
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x000F2150 File Offset: 0x000F0350
	public int CreateHaiShangNpc(int id)
	{
		int num = 0;
		JSONObject jsonobject = jsonData.instance.NpcHaiShangCreateData[id.ToString()];
		foreach (JSONObject jsonobject2 in jsonData.instance.NPCLeiXingDate.list)
		{
			if (jsonobject2["LiuPai"].I == jsonobject["LiuPai"].I && jsonobject2["Level"].I == jsonobject["level"].I)
			{
				JSONObject npcDate = new JSONObject(jsonobject2.ToString(), -2, false, false);
				num = this.AfterCreateNpc(npcDate, false, 0, false, null, 0);
				GlobalValue.Set(jsonobject["id"].I, num, "NPCFactory.CreateHaiShangNpc 创建海上NPC");
				break;
			}
		}
		return num;
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x000F2248 File Offset: 0x000F0448
	public void AuToCreateNpcs()
	{
		JSONObject npcCreateData = jsonData.instance.NpcCreateData;
		Tools.instance.getPlayer();
		if (this.NpcAuToCreateDictionary.Count < 1)
		{
			JSONObject npcleiXingDate = jsonData.instance.NPCLeiXingDate;
			foreach (string text in npcleiXingDate.keys)
			{
				if (npcleiXingDate[text]["Level"].I == 1)
				{
					int i = npcleiXingDate[text]["Type"].I;
					if (this.NpcAuToCreateDictionary.ContainsKey(i))
					{
						this.NpcAuToCreateDictionary[i].Add(text);
					}
					else
					{
						this.NpcAuToCreateDictionary.Add(i, new List<string>
						{
							text
						});
					}
				}
			}
		}
		foreach (JSONObject jsonobject in npcCreateData.list)
		{
			int j = jsonobject["NumA"].I;
			if (jsonobject["EventValue"].Count > 0 && GlobalValue.Get(jsonobject["EventValue"][0].I, "NPCFactory.AuToCreateNpcs 每10年自动生成NPC") == jsonobject["EventValue"][1].I)
			{
				j = jsonobject["NumB"].I;
			}
			int i2 = jsonobject["id"].I;
			while (j > 0)
			{
				string index = this.NpcAuToCreateDictionary[i2][this.getRandom(0, this.NpcAuToCreateDictionary[i2].Count - 1)];
				JSONObject npcDate = new JSONObject(jsonData.instance.NPCLeiXingDate[index].ToString(), -2, false, false);
				this.AfterCreateNpc(npcDate, false, 0, false, null, 0);
				j--;
			}
		}
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x000F2490 File Offset: 0x000F0690
	public int CreateNpcByLiuPaiAndLevel(int liuPai, int level, int sex = 0)
	{
		foreach (JSONObject jsonobject in jsonData.instance.NPCLeiXingDate.list)
		{
			if (jsonobject["LiuPai"].I == liuPai && jsonobject["Level"].I == level)
			{
				JSONObject npcDate = new JSONObject(jsonobject.ToString(), -2, false, false);
				return this.AfterCreateNpc(npcDate, false, 0, false, null, sex);
			}
		}
		Debug.LogError("创建新Npc失败，不符合流派和境界");
		return 0;
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x000F2538 File Offset: 0x000F0738
	public int createNpc(JSONObject npcDate, bool isImportant, int ZhiDingindex = 0, bool isNewPlayer = true, JSONObject importantJson = null)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jsonobject = new JSONObject();
		if (ZhiDingindex > 0)
		{
			jsonobject.SetField("id", ZhiDingindex);
		}
		else
		{
			jsonobject.SetField("id", player.NPCCreateIndex);
		}
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("StatusId", 1);
		jsonobject2.SetField("StatusTime", 60000);
		jsonobject.SetField("Status", jsonobject2);
		jsonobject.SetField("Name", "");
		jsonobject.SetField("IsTag", false);
		jsonobject.SetField("FirstName", npcDate["FirstName"].Str);
		jsonobject.SetField("face", 0);
		jsonobject.SetField("fightFace", 0);
		jsonobject.SetField("isImportant", isImportant);
		int val = this.getRandom(npcDate["NPCTag"][0].I, npcDate["NPCTag"][1].I);
		jsonobject.SetField("NPCTag", val);
		jsonobject.SetField("XingGe", this.getRandomXingGe(jsonData.instance.NPCTagDate[val.ToString()]["zhengxie"].I));
		jsonobject.SetField("ActionId", 1);
		jsonobject.SetField("IsKnowPlayer", false);
		jsonobject.SetField("QingFen", 0);
		jsonobject.SetField("CyList", JSONObject.arr);
		jsonobject.SetField("TuPoMiShu", JSONObject.arr);
		int i = npcDate["Type"].I;
		int i2 = npcDate["Level"].I;
		if (isImportant && importantJson.HasField("ChengHaoID"))
		{
			string str = jsonData.instance.NPCChengHaoData[importantJson["ChengHaoID"].I.ToString()]["ChengHao"].str;
			NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.SetField(importantJson["ChengHaoID"].I.ToString(), jsonobject["id"].I);
			jsonobject.SetField("Title", str.ToCN());
			jsonobject.SetField("ChengHaoID", importantJson["ChengHaoID"].I);
		}
		else
		{
			foreach (JSONObject jsonobject3 in jsonData.instance.NPCChengHaoData.list)
			{
				if (jsonobject3["NPCType"].I == i && jsonobject3["GongXian"].I == 0 && i2 >= jsonobject3["Level"][0].I && i2 <= jsonobject3["Level"][1].I)
				{
					jsonobject.SetField("Title", jsonobject3["ChengHao"].str.ToCN());
					jsonobject.SetField("ChengHaoID", jsonobject3["id"].I);
					break;
				}
			}
		}
		jsonobject.SetField("GongXian", 0);
		if (i == 3)
		{
			jsonobject.SetField("SexType", 2);
		}
		else
		{
			jsonobject.SetField("SexType", this.getRandom(1, 2));
		}
		jsonobject.SetField("Type", i);
		jsonobject.SetField("LiuPai", npcDate["LiuPai"].I);
		jsonobject.SetField("MenPai", npcDate["MengPai"].I);
		jsonobject.SetField("AvatarType", npcDate["AvatarType"].I);
		jsonobject.SetField("Level", i2);
		jsonobject.SetField("WuDaoValue", 0);
		jsonobject.SetField("WuDaoValueLevel", 0);
		jsonobject.SetField("EWWuDaoDian", 0);
		jsonobject.SetField("IsNeedHelp", false);
		if (isImportant)
		{
			jsonobject.SetField("BindingNpcID", importantJson["BindingNpcID"].I);
			jsonobject.SetField("SexType", importantJson["sex"].I);
			if (importantJson.HasField("ZhuJiTime"))
			{
				jsonobject.SetField("ZhuJiTime", importantJson["ZhuJiTime"].str);
				jsonobject.SetField("LianQiAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 4, "0001-1-1", importantJson["ZhuJiTime"].str));
			}
			if (importantJson.HasField("JinDanTime"))
			{
				jsonobject.SetField("JinDanTime", importantJson["JinDanTime"].str);
				if (jsonobject["Level"].I > 3)
				{
					jsonobject.SetField("ZhuJiAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 7, "0001-1-1", importantJson["JinDanTime"].str));
				}
				else
				{
					jsonobject.SetField("ZhuJiAddSpeed", this.GetEWaiXiuLianSpeed(4, 7, importantJson["ZhuJiTime"].str, importantJson["JinDanTime"].str));
				}
			}
			if (importantJson.HasField("YuanYingTime"))
			{
				jsonobject.SetField("YuanYingTime", importantJson["YuanYingTime"].str);
				if (jsonobject["Level"].I > 7)
				{
					jsonobject.SetField("JinDanAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 10, "0001-1-1", importantJson["YuanYingTime"].str));
				}
				else
				{
					jsonobject.SetField("JinDanAddSpeed", this.GetEWaiXiuLianSpeed(7, 10, importantJson["JinDanTime"].str, importantJson["YuanYingTime"].str));
				}
			}
			if (importantJson.HasField("HuaShengTime"))
			{
				jsonobject.SetField("HuaShengTime", importantJson["HuaShengTime"].str);
				if (jsonobject["Level"].I > 10)
				{
					jsonobject.SetField("YuanYingAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 13, "0001-1-1", importantJson["HuaShengTime"].str));
				}
				else
				{
					jsonobject.SetField("YuanYingAddSpeed", this.GetEWaiXiuLianSpeed(10, 13, importantJson["YuanYingTime"].str, importantJson["HuaShengTime"].str));
				}
			}
			NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(importantJson["BindingNpcID"].I, jsonobject["id"].I);
		}
		foreach (JSONObject jsonobject4 in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			if (i2 == jsonobject4["id"].I)
			{
				jsonobject.SetField("HP", this.getRandom(jsonobject4["HP"][0].I, jsonobject4["HP"][1].I));
				jsonobject.SetField("dunSu", this.getRandom(jsonobject4["dunSu"][0].I, jsonobject4["dunSu"][1].I));
				jsonobject.SetField("ziZhi", this.getRandom(jsonobject4["ziZhi"][0].I, jsonobject4["ziZhi"][1].I));
				jsonobject.SetField("wuXin", this.getRandom(jsonobject4["wuXin"][0].I, jsonobject4["wuXin"][1].I));
				jsonobject.SetField("shengShi", this.getRandom(jsonobject4["shengShi"][0].I, jsonobject4["shengShi"][1].I));
				jsonobject.SetField("shaQi", 0);
				jsonobject.SetField("shouYuan", this.getRandom(jsonobject4["shouYuan"][0].I, jsonobject4["shouYuan"][1].I));
				jsonobject.SetField("age", this.getRandom(jsonobject4["age"][0].I, jsonobject4["age"][1].I) * 12);
				jsonobject.SetField("exp", jsonobject4["xiuwei"].I);
				if (i2 <= 14)
				{
					jsonobject.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(i2 + 1).ToString()]["xiuwei"].I);
				}
				else
				{
					jsonobject.SetField("NextExp", 0);
				}
				jsonobject.SetField("equipWeapon", 0);
				jsonobject.SetField("equipList", new JSONObject());
				if (jsonobject4["equipWeapon"].I > 0)
				{
					int num = 0;
					int i3 = jsonobject4["equipWeapon"].I;
					int i4 = npcDate["equipWeapon"][this.getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject obj = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num, ref obj, i4, null, i3);
					jsonobject["equipList"].SetField("Weapon1", obj);
				}
				if (jsonobject4["equipWeapon2"].I > 0)
				{
					int num2 = 0;
					int i5 = jsonobject4["equipWeapon2"].I;
					int i6 = npcDate["equipWeapon"][this.getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject obj2 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num2, ref obj2, i6, null, i5);
					jsonobject["equipList"].SetField("Weapon2", obj2);
				}
				if (jsonobject4["equipClothing"].I > 0)
				{
					int num3 = 0;
					int i7 = jsonobject4["equipClothing"].I;
					int i8 = npcDate["equipClothing"][this.getRandom(0, npcDate["equipClothing"].I)].I;
					JSONObject obj3 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num3, ref obj3, i8, null, i7);
					jsonobject["equipList"].SetField("Clothing", obj3);
				}
				if (jsonobject4["equipRing"].I > 0)
				{
					int num4 = 0;
					int i9 = jsonobject4["equipRing"].I;
					int i10 = npcDate["equipRing"][this.getRandom(0, npcDate["equipRing"].I)].I;
					JSONObject obj4 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num4, ref obj4, i10, null, i9);
					jsonobject["equipList"].SetField("Ring", obj4);
				}
				jsonobject.SetField("equipWeaponPianHao", npcDate["equipWeapon"]);
				jsonobject.SetField("equipWeapon2PianHao", npcDate["equipWeapon"]);
				jsonobject.SetField("equipClothingPianHao", npcDate["equipClothing"]);
				jsonobject.SetField("equipRingPianHao", npcDate["equipRing"]);
				jsonobject.SetField("equipClothing", 0);
				jsonobject.SetField("equipRing", 0);
				jsonobject.SetField("LingGen", npcDate["LingGen"]);
				jsonobject.SetField("skills", npcDate["skills"]);
				jsonobject.SetField("JinDanType", npcDate["JinDanType"]);
				jsonobject.SetField("staticSkills", npcDate["staticSkills"]);
				jsonobject.SetField("xiuLianSpeed", this.getXiuLianSpeed(npcDate["staticSkills"], (float)jsonobject["ziZhi"].I));
				jsonobject.SetField("yuanying", npcDate["yuanying"]);
				jsonobject.SetField("MoneyType", this.getRandom(jsonobject4["MoneyType"][0].I, jsonobject4["MoneyType"][1].I));
				jsonobject.SetField("IsRefresh", 0);
				jsonobject.SetField("dropType", 0);
				jsonobject.SetField("canjiaPaiMai", npcDate["canjiaPaiMai"].I);
				jsonobject.SetField("paimaifenzu", npcDate["paimaifenzu"]);
				jsonobject.SetField("wudaoType", npcDate["wudaoType"]);
				jsonobject.SetField("XinQuType", npcDate["XinQuType"]);
				jsonobject.SetField("gudingjiage", 0);
				jsonobject.SetField("sellPercent", 0);
				jsonobject.SetField("useItem", new JSONObject());
				jsonobject.SetField("NoteBook", new JSONObject());
				this.SetNpcWuDao(i2, npcDate["wudaoType"].I, jsonobject);
				break;
			}
		}
		this.UpNpcWuDaoByTag(jsonobject["NPCTag"].I, jsonobject);
		if (isImportant)
		{
			jsonobject.SetField("ziZhi", importantJson["zizhi"].I);
			jsonobject.SetField("wuXin", importantJson["wuxing"].I);
			jsonobject.SetField("age", importantJson["nianling"].I * 12);
			jsonobject.SetField("XingGe", importantJson["XingGe"].I);
			jsonobject.SetField("NPCTag", importantJson["NPCTag"].I);
		}
		if (ZhiDingindex > 0)
		{
			jsonData.instance.AvatarJsonData.SetField(ZhiDingindex.ToString(), jsonobject);
		}
		else
		{
			jsonData.instance.AvatarJsonData.SetField(player.NPCCreateIndex.ToString(), jsonobject);
			player.NPCCreateIndex++;
		}
		return jsonobject["id"].I;
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x000F342C File Offset: 0x000F162C
	public void SetNpcLevel(int npcId, int npcLevel)
	{
		if (!jsonData.instance.AvatarJsonData.HasField(npcId.ToString()))
		{
			Debug.LogError(string.Format("不存在当前npcId {0}", npcId));
			return;
		}
		Tools.instance.getPlayer();
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		jsonobject.SetField("Level", npcLevel);
		int i = jsonobject["Level"].I;
		int i2 = jsonobject["LiuPai"].I;
		JSONObject jsonobject2 = null;
		for (int j = 0; j < jsonData.instance.NPCLeiXingDate.Count; j++)
		{
			if (i == jsonData.instance.NPCLeiXingDate[j]["Level"].I && i2 == jsonData.instance.NPCLeiXingDate[j]["LiuPai"].I)
			{
				jsonobject2 = new JSONObject(jsonData.instance.NPCLeiXingDate[j].ToString(), -2, false, false);
				break;
			}
		}
		foreach (JSONObject jsonobject3 in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			if (i == jsonobject3["id"].I)
			{
				jsonobject.SetField("HP", this.getRandom(jsonobject3["HP"][0].I, jsonobject3["HP"][1].I));
				jsonobject.SetField("dunSu", this.getRandom(jsonobject3["dunSu"][0].I, jsonobject3["dunSu"][1].I));
				jsonobject.SetField("shengShi", this.getRandom(jsonobject3["shengShi"][0].I, jsonobject3["shengShi"][1].I));
				jsonobject.SetField("shaQi", 0);
				jsonobject.SetField("shouYuan", this.getRandom(jsonobject3["shouYuan"][0].I, jsonobject3["shouYuan"][1].I));
				jsonobject.SetField("age", this.getRandom(jsonobject3["age"][0].I, jsonobject3["age"][1].I) * 12);
				jsonobject.SetField("exp", jsonobject3["xiuwei"].I);
				if (i <= 14)
				{
					jsonobject.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(i + 1).ToString()]["xiuwei"].I);
				}
				else
				{
					jsonobject.SetField("NextExp", 0);
				}
				jsonobject.SetField("equipList", new JSONObject());
				if (jsonobject3["equipWeapon"].I > 0)
				{
					int num = 0;
					int i3 = jsonobject3["equipWeapon"].I;
					int i4 = jsonobject2["equipWeapon"][this.getRandom(0, jsonobject2["equipWeapon"].I)].I;
					JSONObject obj = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num, ref obj, i4, null, i3);
					jsonobject["equipList"].SetField("Weapon1", obj);
				}
				if (jsonobject3["equipWeapon2"].I > 0)
				{
					int num2 = 0;
					int i5 = jsonobject3["equipWeapon2"].I;
					int i6 = jsonobject2["equipWeapon"][this.getRandom(0, jsonobject2["equipWeapon"].I)].I;
					JSONObject obj2 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num2, ref obj2, i6, null, i5);
					jsonobject["equipList"].SetField("Weapon2", obj2);
				}
				if (jsonobject3["equipClothing"].I > 0)
				{
					int num3 = 0;
					int i7 = jsonobject3["equipClothing"].I;
					int i8 = jsonobject2["equipClothing"][this.getRandom(0, jsonobject2["equipClothing"].I)].I;
					JSONObject obj3 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num3, ref obj3, i8, null, i7);
					jsonobject["equipList"].SetField("Clothing", obj3);
				}
				if (jsonobject3["equipRing"].I > 0)
				{
					int num4 = 0;
					int i9 = jsonobject3["equipRing"].I;
					int i10 = jsonobject2["equipRing"][this.getRandom(0, jsonobject2["equipRing"].I)].I;
					JSONObject obj4 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num4, ref obj4, i10, null, i9);
					jsonobject["equipList"].SetField("Ring", obj4);
				}
				jsonobject.SetField("yuanying", jsonobject2["yuanying"]);
				jsonobject.SetField("LingGen", jsonobject2["LingGen"]);
				jsonobject.SetField("skills", jsonobject2["skills"]);
				jsonobject.SetField("staticSkills", jsonobject2["staticSkills"]);
				jsonobject.SetField("MoneyType", this.getRandom(jsonobject3["MoneyType"][0].I, jsonobject3["MoneyType"][1].I));
				this.SetNpcWuDao(i, jsonobject2["wudaoType"].I, jsonobject);
				break;
			}
		}
		this.UpNpcWuDaoByTag(jsonobject["NPCTag"].I, jsonobject);
		FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jsonobject["id"].I, jsonobject);
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x000F3A78 File Offset: 0x000F1C78
	public int AfterCreateNpc(JSONObject npcDate, bool isImportant, int ZhiDingindex = 0, bool isNewPlayer = true, JSONObject importantJson = null, int setSex = 0)
	{
		Avatar player = Tools.instance.getPlayer();
		JSONObject jsonobject = new JSONObject();
		if (ZhiDingindex > 0)
		{
			jsonobject.SetField("id", ZhiDingindex);
		}
		else
		{
			jsonobject.SetField("id", player.NPCCreateIndex);
		}
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("StatusId", 1);
		jsonobject2.SetField("StatusTime", 60000);
		jsonobject.SetField("Status", jsonobject2);
		jsonobject.SetField("Name", "");
		jsonobject.SetField("IsTag", false);
		jsonobject.SetField("FirstName", npcDate["FirstName"].Str);
		jsonobject.SetField("face", 0);
		jsonobject.SetField("fightFace", 0);
		jsonobject.SetField("CyList", JSONObject.arr);
		jsonobject.SetField("isImportant", isImportant);
		int val = this.getRandom(npcDate["NPCTag"][0].I, npcDate["NPCTag"][1].I);
		jsonobject.SetField("NPCTag", val);
		jsonobject.SetField("XingGe", this.getRandomXingGe(jsonData.instance.NPCTagDate[val.ToString()]["zhengxie"].I));
		jsonobject.SetField("ActionId", 1);
		jsonobject.SetField("IsKnowPlayer", false);
		jsonobject.SetField("QingFen", 0);
		jsonobject.SetField("TuPoMiShu", JSONObject.arr);
		int i = npcDate["Type"].I;
		int i2 = npcDate["Level"].I;
		if (isImportant && importantJson.HasField("ChengHaoID"))
		{
			string str = jsonData.instance.NPCChengHaoData[importantJson["ChengHaoID"].I.ToString()]["ChengHao"].str;
			NpcJieSuanManager.inst.npcChengHao.npcOnlyChengHao.SetField(importantJson["ChengHaoID"].I.ToString(), jsonobject["id"].I);
			jsonobject.SetField("Title", str.ToCN());
			jsonobject.SetField("ChengHaoID", importantJson["ChengHaoID"].I);
		}
		else
		{
			foreach (JSONObject jsonobject3 in jsonData.instance.NPCChengHaoData.list)
			{
				if (jsonobject3["NPCType"].I == i && jsonobject3["GongXian"].I == 0 && i2 >= jsonobject3["Level"][0].I && i2 <= jsonobject3["Level"][1].I)
				{
					jsonobject.SetField("Title", jsonobject3["ChengHao"].str.ToCN());
					jsonobject.SetField("ChengHaoID", jsonobject3["id"].I);
					break;
				}
			}
		}
		jsonobject.SetField("GongXian", 0);
		if (i == 3)
		{
			jsonobject.SetField("SexType", 2);
		}
		else if (setSex != 0)
		{
			jsonobject.SetField("SexType", setSex);
		}
		else
		{
			jsonobject.SetField("SexType", this.getRandom(1, 2));
		}
		jsonobject.SetField("Type", i);
		jsonobject.SetField("LiuPai", npcDate["LiuPai"].I);
		jsonobject.SetField("MenPai", npcDate["MengPai"].I);
		jsonobject.SetField("AvatarType", npcDate["AvatarType"].I);
		jsonobject.SetField("Level", i2);
		jsonobject.SetField("WuDaoValue", 0);
		jsonobject.SetField("WuDaoValueLevel", 0);
		jsonobject.SetField("EWWuDaoDian", 0);
		jsonobject.SetField("IsNeedHelp", false);
		if (isImportant)
		{
			jsonobject.SetField("BindingNpcID", importantJson["BindingNpcID"].I);
			jsonobject.SetField("SexType", importantJson["sex"].I);
			if (importantJson.HasField("ZhuJiTime"))
			{
				jsonobject.SetField("ZhuJiTime", importantJson["ZhuJiTime"].str);
				jsonobject.SetField("LianQiAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 4, "0001-1-1", importantJson["ZhuJiTime"].str));
			}
			if (importantJson.HasField("JinDanTime"))
			{
				jsonobject.SetField("JinDanTime", importantJson["JinDanTime"].str);
				if (jsonobject["Level"].I > 3)
				{
					jsonobject.SetField("ZhuJiAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 7, "0001-1-1", importantJson["JinDanTime"].str));
				}
				else
				{
					jsonobject.SetField("ZhuJiAddSpeed", this.GetEWaiXiuLianSpeed(4, 7, importantJson["ZhuJiTime"].str, importantJson["JinDanTime"].str));
				}
			}
			if (importantJson.HasField("YuanYingTime"))
			{
				jsonobject.SetField("YuanYingTime", importantJson["YuanYingTime"].str);
				if (jsonobject["Level"].I > 7)
				{
					jsonobject.SetField("JinDanAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 10, "0001-1-1", importantJson["YuanYingTime"].str));
				}
				else
				{
					jsonobject.SetField("JinDanAddSpeed", this.GetEWaiXiuLianSpeed(7, 10, importantJson["JinDanTime"].str, importantJson["YuanYingTime"].str));
				}
			}
			if (importantJson.HasField("HuaShengTime"))
			{
				jsonobject.SetField("HuaShengTime", importantJson["HuaShengTime"].str);
				if (jsonobject["Level"].I > 11)
				{
					jsonobject.SetField("YuanYingAddSpeed", this.GetEWaiXiuLianSpeed(jsonobject["Level"].I, 13, "0001-1-1", importantJson["HuaShengTime"].str));
				}
				else
				{
					jsonobject.SetField("YuanYingAddSpeed", this.GetEWaiXiuLianSpeed(10, 13, importantJson["YuanYingTime"].str, importantJson["HuaShengTime"].str));
				}
			}
			NpcJieSuanManager.inst.ImportantNpcBangDingDictionary.Add(importantJson["BindingNpcID"].I, jsonobject["id"].I);
		}
		foreach (JSONObject jsonobject4 in jsonData.instance.NPCChuShiShuZiDate.list)
		{
			if (i2 == jsonobject4["id"].I)
			{
				jsonobject.SetField("HP", this.getRandom(jsonobject4["HP"][0].I, jsonobject4["HP"][1].I));
				jsonobject.SetField("dunSu", this.getRandom(jsonobject4["dunSu"][0].I, jsonobject4["dunSu"][1].I));
				jsonobject.SetField("ziZhi", this.getRandom(jsonobject4["ziZhi"][0].I, jsonobject4["ziZhi"][1].I));
				jsonobject.SetField("wuXin", this.getRandom(jsonobject4["wuXin"][0].I, jsonobject4["wuXin"][1].I));
				jsonobject.SetField("shengShi", this.getRandom(jsonobject4["shengShi"][0].I, jsonobject4["shengShi"][1].I));
				jsonobject.SetField("shaQi", 0);
				jsonobject.SetField("shouYuan", this.getRandom(jsonobject4["shouYuan"][0].I, jsonobject4["shouYuan"][1].I));
				jsonobject.SetField("age", this.getRandom(jsonobject4["age"][0].I, jsonobject4["age"][1].I) * 12);
				jsonobject.SetField("exp", jsonobject4["xiuwei"].I);
				if (i2 <= 14)
				{
					jsonobject.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(i2 + 1).ToString()]["xiuwei"].I);
				}
				else
				{
					jsonobject.SetField("NextExp", 0);
				}
				jsonobject.SetField("equipWeapon", 0);
				jsonobject.SetField("equipList", new JSONObject());
				if (jsonobject4["equipWeapon"].I > 0)
				{
					int num = 0;
					int i3 = jsonobject4["equipWeapon"].I;
					int i4 = npcDate["equipWeapon"][this.getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject obj = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num, ref obj, i4, null, i3);
					jsonobject["equipList"].SetField("Weapon1", obj);
				}
				if (jsonobject4["equipWeapon2"].I > 0)
				{
					int num2 = 0;
					int i5 = jsonobject4["equipWeapon2"].I;
					int i6 = npcDate["equipWeapon"][this.getRandom(0, npcDate["equipWeapon"].I)].I;
					JSONObject obj2 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num2, ref obj2, i6, null, i5);
					jsonobject["equipList"].SetField("Weapon2", obj2);
				}
				if (jsonobject4["equipClothing"].I > 0)
				{
					int num3 = 0;
					int i7 = jsonobject4["equipClothing"].I;
					int i8 = npcDate["equipClothing"][this.getRandom(0, npcDate["equipClothing"].I)].I;
					JSONObject obj3 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num3, ref obj3, i8, null, i7);
					jsonobject["equipList"].SetField("Clothing", obj3);
				}
				if (jsonobject4["equipRing"].I > 0)
				{
					int num4 = 0;
					int i9 = jsonobject4["equipRing"].I;
					int i10 = npcDate["equipRing"][this.getRandom(0, npcDate["equipRing"].I)].I;
					JSONObject obj4 = new JSONObject();
					RandomNPCEquip.CreateLoveEquip(ref num4, ref obj4, i10, null, i9);
					jsonobject["equipList"].SetField("Ring", obj4);
				}
				jsonobject.SetField("equipWeaponPianHao", npcDate["equipWeapon"]);
				jsonobject.SetField("equipWeapon2PianHao", npcDate["equipWeapon"]);
				jsonobject.SetField("equipClothingPianHao", npcDate["equipClothing"]);
				jsonobject.SetField("equipRingPianHao", npcDate["equipRing"]);
				jsonobject.SetField("equipClothing", 0);
				jsonobject.SetField("equipRing", 0);
				jsonobject.SetField("LingGen", npcDate["LingGen"]);
				jsonobject.SetField("skills", npcDate["skills"]);
				jsonobject.SetField("JinDanType", npcDate["JinDanType"]);
				jsonobject.SetField("staticSkills", npcDate["staticSkills"]);
				jsonobject.SetField("xiuLianSpeed", this.getXiuLianSpeed(npcDate["staticSkills"], (float)jsonobject["ziZhi"].I));
				jsonobject.SetField("yuanying", npcDate["yuanying"]);
				jsonobject.SetField("HuaShenLingYu", npcDate["HuaShenLingYu"]);
				jsonobject.SetField("MoneyType", this.getRandom(jsonobject4["MoneyType"][0].I, jsonobject4["MoneyType"][1].I));
				jsonobject.SetField("IsRefresh", 0);
				jsonobject.SetField("dropType", 0);
				jsonobject.SetField("canjiaPaiMai", npcDate["canjiaPaiMai"].I);
				jsonobject.SetField("paimaifenzu", npcDate["paimaifenzu"]);
				jsonobject.SetField("wudaoType", npcDate["wudaoType"]);
				jsonobject.SetField("XinQuType", npcDate["XinQuType"]);
				jsonobject.SetField("gudingjiage", 0);
				jsonobject.SetField("sellPercent", 0);
				jsonobject.SetField("useItem", new JSONObject());
				jsonobject.SetField("NoteBook", new JSONObject());
				this.SetNpcWuDao(i2, npcDate["wudaoType"].I, jsonobject);
				break;
			}
		}
		this.UpNpcWuDaoByTag(jsonobject["NPCTag"].I, jsonobject);
		if (isImportant)
		{
			jsonobject.SetField("ziZhi", importantJson["zizhi"].I);
			jsonobject.SetField("wuXin", importantJson["wuxing"].I);
			jsonobject.SetField("age", importantJson["nianling"].I * 12);
			jsonobject.SetField("XingGe", importantJson["XingGe"].I);
		}
		if (isImportant)
		{
			jsonData.instance.AvatarRandomJsonData.SetField(jsonobject["id"].I.ToString(), jsonData.instance.AvatarRandomJsonData[jsonobject["BindingNpcID"].I.ToString()]);
		}
		else
		{
			JSONObject jsonobject5 = jsonData.instance.randomAvatarFace(jsonobject, jsonData.instance.AvatarRandomJsonData.HasField(string.Concat((int)jsonobject["id"].n)) ? jsonData.instance.AvatarRandomJsonData[((int)jsonobject["id"].n).ToString()] : null);
			jsonData.instance.AvatarRandomJsonData.SetField(string.Concat((int)jsonobject["id"].n), jsonobject5.Clone());
		}
		FactoryManager.inst.npcFactory.InitAutoCreateNpcBackpack(jsonData.instance.AvatarBackpackJsonData, jsonobject["id"].I, jsonobject);
		if (ZhiDingindex > 0)
		{
			jsonData.instance.AvatarJsonData.SetField(ZhiDingindex.ToString(), jsonobject);
		}
		else
		{
			jsonData.instance.AvatarJsonData.SetField(player.NPCCreateIndex.ToString(), jsonobject);
			player.NPCCreateIndex++;
		}
		return jsonobject["id"].I;
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x000F4A90 File Offset: 0x000F2C90
	public int getXiuLianSpeed(JSONObject gongFaList, float zizhi)
	{
		float num = 0f;
		for (int i = 0; i < gongFaList.list.Count; i++)
		{
			if (num < (float)jsonData.instance.StaticSkillJsonData[gongFaList[i].I.ToString()]["Skill_Speed"].I)
			{
				num = (float)jsonData.instance.StaticSkillJsonData[gongFaList[i].I.ToString()]["Skill_Speed"].I;
			}
		}
		num *= 1f + zizhi / 100f;
		return (int)num;
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x000F4B38 File Offset: 0x000F2D38
	public void SetNpcWuDao(int level, int wudaoType, JSONObject npcDate)
	{
		for (int i = 0; i < jsonData.instance.NPCWuDaoJson.Count; i++)
		{
			if (jsonData.instance.NPCWuDaoJson[i]["lv"].I == level && jsonData.instance.NPCWuDaoJson[i]["Type"].I == wudaoType)
			{
				JSONObject jsonobject = jsonData.instance.NPCWuDaoJson[i];
				JSONObject jsonobject2 = new JSONObject();
				foreach (JSONObject jsonobject3 in jsonobject["wudaoID"].list)
				{
					jsonobject2.Add(jsonobject3.I);
				}
				npcDate.SetField("wuDaoSkillList", jsonobject2);
				JSONObject jsonobject4 = new JSONObject();
				for (int j = 1; j <= 12; j++)
				{
					JSONObject jsonobject5 = new JSONObject();
					int val = (j <= 10) ? j : (j + 10);
					jsonobject5.SetField("id", val);
					int i2 = jsonobject["value" + j].I;
					jsonobject5.SetField("level", i2);
					int val2 = 0;
					if (i2 != 0)
					{
						val2 = jsonData.instance.WuDaoJinJieJson[i2 - 1]["Max"].I;
					}
					jsonobject5.SetField("exp", val2);
					jsonobject4.SetField(val.ToString(), jsonobject5);
				}
				npcDate.SetField("wuDaoJson", jsonobject4);
				return;
			}
		}
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x000F4CF0 File Offset: 0x000F2EF0
	public void UpNpcWuDaoByTag(int tag, JSONObject npcData)
	{
		JSONObject jsonobject = jsonData.instance.NPCTagDate[tag.ToString()];
		if (jsonobject["WuDao"].Count < 1)
		{
			return;
		}
		int i = npcData["Level"].I;
		int num = i / 3;
		if (i % 3 == 0)
		{
			num--;
		}
		num++;
		for (int j = 0; j < jsonobject["WuDao"].Count; j++)
		{
			npcData["wuDaoJson"][jsonobject["WuDao"][j].I.ToString()].SetField("level", num);
			npcData["wuDaoJson"][jsonobject["WuDao"][j].I.ToString()].SetField("exp", jsonData.instance.WuDaoJinJieJson[num - 1]["Max"].I);
		}
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x000F4DFC File Offset: 0x000F2FFC
	public void createImprotantNpc(JSONObject ImprotantNpcDate, bool isNewPlayer = true)
	{
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			int i2 = ImprotantNpcDate["level"].I;
			int i3 = ImprotantNpcDate["LiuPai"].I;
			if (i2 == jsonData.instance.NPCLeiXingDate[i]["Level"].I && i3 == jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I)
			{
				JSONObject jsonobject = new JSONObject();
				JSONObject npcDate = jsonData.instance.NPCLeiXingDate[i];
				jsonobject.SetField("BindingNpcID", ImprotantNpcDate["id"].I);
				jsonobject.SetField("sex", ImprotantNpcDate["sex"].I);
				if (ImprotantNpcDate["ChengHao"].I > 0)
				{
					jsonobject.SetField("ChengHaoID", ImprotantNpcDate["ChengHao"].I);
				}
				if (ImprotantNpcDate["ZhuJiTime"].str != "" && ImprotantNpcDate["ZhuJiTime"] != null)
				{
					jsonobject.SetField("ZhuJiTime", ImprotantNpcDate["ZhuJiTime"].str);
				}
				if (ImprotantNpcDate["JinDanTime"].str != "" && ImprotantNpcDate["JinDanTime"] != null)
				{
					jsonobject.SetField("JinDanTime", ImprotantNpcDate["JinDanTime"].str);
				}
				if (ImprotantNpcDate["YuanYingTime"].str != "" && ImprotantNpcDate["YuanYingTime"] != null)
				{
					jsonobject.SetField("YuanYingTime", ImprotantNpcDate["YuanYingTime"].str);
				}
				if (ImprotantNpcDate["HuaShengTime"].str != "" && ImprotantNpcDate["HuaShengTime"] != null)
				{
					jsonobject.SetField("HuaShengTime", ImprotantNpcDate["HuaShengTime"].str);
				}
				jsonobject.SetField("zizhi", ImprotantNpcDate["zizhi"].I);
				jsonobject.SetField("wuxing", ImprotantNpcDate["wuxing"].I);
				jsonobject.SetField("XingGe", ImprotantNpcDate["XingGe"].I);
				jsonobject.SetField("nianling", ImprotantNpcDate["nianling"].I);
				jsonobject.SetField("NPCTag", ImprotantNpcDate["NPCTag"].I);
				this.createNpc(npcDate, true, 0, isNewPlayer, jsonobject);
				return;
			}
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x000F50B4 File Offset: 0x000F32B4
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
				JSONObject jsonobject = new JSONObject();
				object obj = jsonData.instance.NPCLeiXingDate[i];
				jsonobject.SetField("BindingNpcID", ImprotantNpcDate["id"].I);
				jsonobject.SetField("sex", ImprotantNpcDate["sex"].I);
				if (ImprotantNpcDate["ChengHao"].I > 0)
				{
					jsonobject.SetField("ChengHaoID", ImprotantNpcDate["ChengHao"].I);
				}
				if (ImprotantNpcDate["ZhuJiTime"].str != "" && ImprotantNpcDate["ZhuJiTime"] != null)
				{
					jsonobject.SetField("ZhuJiTime", ImprotantNpcDate["ZhuJiTime"].str);
				}
				if (ImprotantNpcDate["JinDanTime"].str != "" && ImprotantNpcDate["JinDanTime"] != null)
				{
					jsonobject.SetField("JinDanTime", ImprotantNpcDate["JinDanTime"].str);
				}
				if (ImprotantNpcDate["YuanYingTime"].str != "" && ImprotantNpcDate["YuanYingTime"] != null)
				{
					jsonobject.SetField("YuanYingTime", ImprotantNpcDate["YuanYingTime"].str);
				}
				if (ImprotantNpcDate["HuaShengTime"].str != "" && ImprotantNpcDate["HuaShengTime"] != null)
				{
					jsonobject.SetField("HuaShengTime", ImprotantNpcDate["HuaShengTime"].str);
				}
				jsonobject.SetField("zizhi", ImprotantNpcDate["zizhi"].I);
				jsonobject.SetField("wuxing", ImprotantNpcDate["wuxing"].I);
				jsonobject.SetField("XingGe", ImprotantNpcDate["XingGe"].I);
				jsonobject.SetField("nianling", ImprotantNpcDate["nianling"].I);
				JSONObject npcDate = new JSONObject(obj.ToString(), -2, false, false);
				this.AfterCreateNpc(npcDate, true, 0, isNewPlayer, jsonobject, 0);
				return;
			}
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000170FB File Offset: 0x000152FB
	public int getRandom(int min, int max)
	{
		return this.random.Next(min, max + 1);
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x000F547C File Offset: 0x000F367C
	public void InitAutoCreateNpcBackpack(JSONObject jsondata, int avatarID, JSONObject data = null)
	{
		if (jsondata == null)
		{
			jsondata = new JSONObject();
		}
		jsondata.SetField(string.Concat(avatarID), new JSONObject());
		jsondata[string.Concat(avatarID)].SetField("Backpack", new JSONObject(JSONObject.Type.ARRAY));
		int i;
		int num;
		int i2;
		if (data == null)
		{
			i = jsonData.instance.AvatarJsonData[avatarID.ToString()]["Level"].I;
			num = (int)jsonData.instance.AvatarJsonData[string.Concat(avatarID)]["MoneyType"].n;
			i2 = jsonData.instance.AvatarJsonData[avatarID.ToString()]["NPCTag"].I;
		}
		else
		{
			i = data["Level"].I;
			num = data["MoneyType"].I;
			i2 = data["NPCTag"].I;
		}
		if (num < 50)
		{
			int num2 = (int)jsonData.instance.AvatarMoneyJsonData[string.Concat(num)]["Max"].n - (int)jsonData.instance.AvatarMoneyJsonData[string.Concat(num)]["Min"].n;
			int num3 = jsonData.instance.QuikeGetRandom() % num2;
			jsondata[string.Concat(avatarID)].SetField("money", (int)jsonData.instance.AvatarMoneyJsonData[string.Concat(num)]["Min"].n + num3);
		}
		jsondata[string.Concat(avatarID)].SetField("death", 0);
		JSONObject jsonobject = jsonData.instance.NpcBeiBaoTypeData[this.GetBeiBaoIdByTag(i2, i).ToString()];
		int j = jsonData.instance.NPCChuShiShuZiDate[i.ToString()]["bag"].I;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		List<int> list = new List<int>();
		while (j >= 0)
		{
			int index = this.getRandom(0, jsonobject["ShopType"].Count - 1);
			int i3 = jsonobject["ShopType"][index].I;
			int i4 = jsonobject["quality"][index].I;
			JSONObject randomItemByShopType = this.GetRandomItemByShopType(i3, i4);
			if (randomItemByShopType != null)
			{
				int i5 = randomItemByShopType["id"].I;
				j -= jsonData.instance.ItemJsonData[i5.ToString()]["price"].I;
				if (randomItemByShopType["maxNum"].I > 1)
				{
					if (dictionary.ContainsKey(i5))
					{
						Dictionary<int, int> dictionary2 = dictionary;
						int key = i5;
						dictionary2[key]++;
					}
					else
					{
						dictionary.Add(i5, 1);
					}
				}
				else
				{
					list.Add(i5);
				}
			}
		}
		foreach (int num4 in dictionary.Keys)
		{
			JSONObject obj = jsonData.instance.setAvatarBackpack(Tools.getUUID(), num4, dictionary[num4], 1, 100, 1, Tools.CreateItemSeid(num4), 0);
			jsondata[avatarID.ToString()]["Backpack"].Add(obj);
		}
		for (int k = 0; k < list.Count; k++)
		{
			JSONObject obj2 = jsonData.instance.setAvatarBackpack(Tools.getUUID(), list[k], 1, 1, 100, 1, Tools.CreateItemSeid(list[k]), 0);
			jsondata[avatarID.ToString()]["Backpack"].Add(obj2);
		}
		jsondata[avatarID.ToString()].SetField("CanSell", 1);
		jsondata[avatarID.ToString()].SetField("SellPercent", 100);
		jsondata[avatarID.ToString()].SetField("CanDrop", 1);
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x000F58E0 File Offset: 0x000F3AE0
	public int getRandomXingGe(int zhengXie)
	{
		if (!this.NpcXingGeDictionary.ContainsKey(zhengXie))
		{
			for (int i = 0; i < jsonData.instance.NpcXingGeDate.Count; i++)
			{
				if (this.NpcXingGeDictionary.ContainsKey(jsonData.instance.NpcXingGeDate[i]["zhengxie"].I))
				{
					this.NpcXingGeDictionary[jsonData.instance.NpcXingGeDate[i]["zhengxie"].I].Add(jsonData.instance.NpcXingGeDate[i]["id"].I);
				}
				else
				{
					this.NpcXingGeDictionary.Add(jsonData.instance.NpcXingGeDate[i]["zhengxie"].I, new List<int>
					{
						jsonData.instance.NpcXingGeDate[i]["id"].I
					});
				}
			}
		}
		return this.NpcXingGeDictionary[zhengXie][this.getRandom(0, this.NpcXingGeDictionary[zhengXie].Count - 1)];
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x000F5A1C File Offset: 0x000F3C1C
	public JSONObject GetRandomItemByShopType(int shopType, int quality)
	{
		if (!this.shopTypeDictionary.ContainsKey(shopType))
		{
			JSONObject itemJsonData = jsonData.instance._ItemJsonData;
			for (int i = 0; i < itemJsonData.Count; i++)
			{
				int i2 = itemJsonData[i]["ShopType"].I;
				int i3 = itemJsonData[i]["quality"].I;
				if (this.shopTypeDictionary.ContainsKey(i2))
				{
					if (this.shopTypeDictionary[i2].ContainsKey(i3))
					{
						this.shopTypeDictionary[i2][i3].Add(itemJsonData[i]);
					}
					else
					{
						this.shopTypeDictionary[i2].Add(i3, new List<JSONObject>
						{
							itemJsonData[i]
						});
					}
				}
				else
				{
					Dictionary<int, List<JSONObject>> dictionary = new Dictionary<int, List<JSONObject>>();
					dictionary.Add(i3, new List<JSONObject>
					{
						itemJsonData[i]
					});
					this.shopTypeDictionary.Add(i2, dictionary);
				}
			}
		}
		JSONObject result;
		try
		{
			result = this.shopTypeDictionary[shopType][quality][this.getRandom(0, this.shopTypeDictionary[shopType][quality].Count - 1)];
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("不存在shopType:{0},quality{1}", shopType, quality));
			result = null;
		}
		return result;
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x000F5B94 File Offset: 0x000F3D94
	public int GetBeiBaoIdByTag(int tag, int level)
	{
		JSONObject jsonobject = jsonData.instance.NPCTagDate[tag.ToString()]["BeiBaoType"];
		int i = jsonobject[this.getRandom(0, jsonobject.Count - 1)].I;
		foreach (JSONObject jsonobject2 in jsonData.instance.NpcBeiBaoTypeData.list)
		{
			if (jsonobject2["BagTpye"].I == i && jsonobject2["JinJie"].I == level)
			{
				return jsonobject2["id"].I;
			}
		}
		Debug.LogError("GetBeiBaoIdByTag方法报错");
		return 1;
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x000F5C70 File Offset: 0x000F3E70
	public JSONObject GetRandomItemByType(int targetType, int quality)
	{
		if (!this.typeDictionary.ContainsKey(targetType))
		{
			JSONObject itemJsonData = jsonData.instance._ItemJsonData;
			for (int i = 0; i < itemJsonData.Count; i++)
			{
				int i2 = itemJsonData[i]["type"].I;
				int i3 = itemJsonData[i]["quality"].I;
				if (this.typeDictionary.ContainsKey(i2))
				{
					if (this.typeDictionary[i2].ContainsKey(i3))
					{
						this.typeDictionary[i2][i3].Add(itemJsonData[i]);
					}
					else
					{
						this.typeDictionary[i2].Add(i3, new List<JSONObject>
						{
							itemJsonData[i]
						});
					}
				}
				else
				{
					Dictionary<int, List<JSONObject>> dictionary = new Dictionary<int, List<JSONObject>>();
					dictionary.Add(i3, new List<JSONObject>
					{
						itemJsonData[i]
					});
					this.typeDictionary.Add(i2, dictionary);
				}
			}
		}
		JSONObject result;
		try
		{
			result = this.typeDictionary[targetType][quality][this.getRandom(0, this.typeDictionary[targetType][quality].Count - 1)];
		}
		catch (Exception)
		{
			Debug.LogError(string.Format("不存在Type:{0},quality{1}", targetType, quality));
			result = null;
		}
		return result;
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x000F5DE8 File Offset: 0x000F3FE8
	public int GetEWaiXiuLianSpeed(int curLevel, int targetLevel, string _curTime, string _targetTime)
	{
		try
		{
			DateTime d = DateTime.Parse(_curTime);
			int num = (DateTime.Parse(_targetTime) - d).Days / 30;
			int num2 = 0;
			for (int i = curLevel; i <= targetLevel; i++)
			{
				num2 += jsonData.instance.NPCChuShiShuZiDate[i.ToString()]["xiuwei"].I;
			}
			return num2 / num;
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
		}
		return 0;
	}

	// Token: 0x04001715 RID: 5909
	private Random random = new Random();

	// Token: 0x04001716 RID: 5910
	private Dictionary<int, Dictionary<int, List<JSONObject>>> shopTypeDictionary = new Dictionary<int, Dictionary<int, List<JSONObject>>>();

	// Token: 0x04001717 RID: 5911
	private Dictionary<int, Dictionary<int, List<JSONObject>>> typeDictionary = new Dictionary<int, Dictionary<int, List<JSONObject>>>();

	// Token: 0x04001718 RID: 5912
	private Dictionary<int, List<int>> NpcXingGeDictionary = new Dictionary<int, List<int>>();

	// Token: 0x04001719 RID: 5913
	private Dictionary<int, List<string>> NpcAuToCreateDictionary = new Dictionary<int, List<string>>();

	// Token: 0x0400171A RID: 5914
	public bool isNewGame;
}
