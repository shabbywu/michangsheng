using System;
using System.Collections.Generic;
using UnityEngine;
using script.EventMsg;

public class NPCTuPo
{
	private Dictionary<int, int> itemTuPoLvDictionary = new Dictionary<int, int>();

	private Dictionary<int, int> itemTuPoFenShuDictionary = new Dictionary<int, int>();

	private Dictionary<int, int> itemTuPoUseNumDictionary = new Dictionary<int, int>();

	public NPCTuPo()
	{
		foreach (JSONObject item in jsonData.instance.NPCTuPuoDate.list)
		{
			for (int i = 0; i < item["TuPoItem"].Count; i++)
			{
				itemTuPoLvDictionary.Add(item["TuPoItem"][i].I, item["TiShengJiLv"][i].I);
				itemTuPoUseNumDictionary.Add(item["TuPoItem"][i].I, item["ShangXian"][i].I);
				if (item["id"].I <= 12)
				{
					itemTuPoFenShuDictionary.Add(item["TuPoItem"][i].I, item["TiShengJinDan"][i].I);
				}
			}
		}
	}

	public JSONObject NpcSmallToPo(JSONObject npcDate, int nextLevel)
	{
		JSONObject jSONObject = jsonData.instance.LevelUpDataJsonData[(nextLevel - 1).ToString()];
		npcDate.SetField("Level", nextLevel);
		if (nextLevel <= 14)
		{
			npcDate.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(nextLevel + 1).ToString()]["xiuwei"].I);
		}
		else
		{
			npcDate.SetField("NextExp", 0);
		}
		npcDate.SetField("HP", npcDate["HP"].I + jSONObject["AddHp"].I);
		npcDate.SetField("shengShi", npcDate["shengShi"].I + jSONObject["AddShenShi"].I);
		npcDate.SetField("dunSu", npcDate["dunSu"].I + jSONObject["AddDunSu"].I);
		JSONObject jSONObject2 = new JSONObject();
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			if (jsonData.instance.NPCLeiXingDate[i]["Type"].I == npcDate["Type"].I && jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I == npcDate["LiuPai"].I && jsonData.instance.NPCLeiXingDate[i]["Level"].I == nextLevel)
			{
				jSONObject2 = new JSONObject(jsonData.instance.NPCLeiXingDate[i].ToString());
				break;
			}
		}
		if (jSONObject2.keys.Count > 0)
		{
			npcDate.SetField("skills", jSONObject2["skills"]);
			npcDate.SetField("staticSkills", jSONObject2["staticSkills"]);
			npcDate.SetField("xiuLianSpeed", FactoryManager.inst.npcFactory.getXiuLianSpeed(npcDate["staticSkills"], npcDate["ziZhi"].I));
			npcDate.SetField("yuanying", jSONObject2["yuanying"]);
			if (jSONObject2["yuanying"].I > 0)
			{
				float num = jsonData.instance.StaticSkillJsonData[jSONObject2["yuanying"].I.ToString()]["Skill_Speed"].I;
				num *= 1f + (float)npcDate["ziZhi"].I / 100f;
				if (num > (float)npcDate["xiuLianSpeed"].I)
				{
					npcDate.SetField("xiuLianSpeed", num);
				}
				npcDate.SetField("XinQuType", jSONObject2["XinQuType"]);
			}
		}
		npcDate.SetField("equipWeaponPianHao", jSONObject2["equipWeapon"]);
		npcDate.SetField("equipWeapon2PianHao", jSONObject2["equipWeapon"]);
		npcDate.SetField("equipClothingPianHao", jSONObject2["equipClothing"]);
		npcDate.SetField("equipRingPianHao", jSONObject2["equipRing"]);
		NpcJieSuanManager.inst.npcNoteBook.NoteSmallTuPo(npcDate["id"].I);
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcDate["id"].I, 11);
		npcDate.SetField("exp", 0);
		return npcDate;
	}

	public bool IsCanSmallTuPo(int npcID = -1, JSONObject actionDate = null)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject = ((npcID != -1) ? jsonData.instance.AvatarJsonData[npcID.ToString()] : actionDate);
		int i = jSONObject["exp"].I;
		int num = jSONObject["Level"].I + 1;
		if (i >= jSONObject["NextExp"].I && jSONObject["NextExp"].I != 0 && num != 4 && num != 7 && num != 10 && num != 13 && num != 16)
		{
			return true;
		}
		return false;
	}

	public bool IsCanBigTuPo(int npcId)
	{
		int i = NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I;
		if (NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			JSONObject jSONObject = jsonData.instance.NPCTuPuoDate[i.ToString()];
			if (NpcJieSuanManager.inst.GetNpcShengYuTime(npcId) <= jSONObject["shengyushouyuan"].I)
			{
				return true;
			}
			int i2 = jSONObject["mubiaojilv"].I;
			if (GetNpcBigTuPoLv(npcId) >= i2)
			{
				return true;
			}
		}
		return false;
	}

	public int GetNpcBigTuPoLv(int npcId, bool isUse = false, bool isKuaiSu = false)
	{
		if (!NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return 0;
		}
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Level"].I;
		int npcBigLevel = NpcJieSuanManager.inst.GetNpcBigLevel(npcId);
		if (npcData["isImportant"].b)
		{
			if (npcBigLevel == 1 && npcData.HasField("ZhuJiTime"))
			{
				DateTime nowTime = NpcJieSuanManager.inst.GetNowTime();
				DateTime dateTime = DateTime.Parse(npcData["ZhuJiTime"].str);
				if (nowTime >= dateTime)
				{
					return 100;
				}
				return -1;
			}
			if (npcBigLevel == 2 && npcData.HasField("JinDanTime"))
			{
				DateTime nowTime2 = NpcJieSuanManager.inst.GetNowTime();
				DateTime dateTime2 = DateTime.Parse(npcData["JinDanTime"].str);
				if (nowTime2 >= dateTime2)
				{
					return 100;
				}
				return -1;
			}
			if (npcBigLevel == 3 && npcData.HasField("YuanYingTime"))
			{
				DateTime nowTime3 = NpcJieSuanManager.inst.GetNowTime();
				DateTime dateTime3 = DateTime.Parse(npcData["YuanYingTime"].str);
				if (nowTime3 >= dateTime3)
				{
					return 100;
				}
				return -1;
			}
		}
		JSONObject jSONObject = jsonData.instance.NPCTuPuoDate[i.ToString()];
		int num = jSONObject["jilv"].I;
		if (isKuaiSu)
		{
			switch (npcBigLevel)
			{
			case 1:
				num += 20;
				break;
			case 2:
				num += 10;
				break;
			case 3:
				num += 4;
				break;
			case 4:
				num = num;
				break;
			}
		}
		num += (int)(npcData["ziZhi"].f * jSONObject["ZiZhiJiaCheng"].f);
		JSONObject jSONObject2 = jSONObject["TuPoItem"];
		Dictionary<int, int> npcBaiBaoItemSum = NpcJieSuanManager.inst.GetNpcBaiBaoItemSum(npcId, jSONObject2.ToList());
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		for (int j = 0; j < jSONObject2.Count; j++)
		{
			num5 = jSONObject2[j].I;
			if (npcBaiBaoItemSum.ContainsKey(num5))
			{
				num2 = npcBaiBaoItemSum[num5];
				num3 = itemTuPoUseNumDictionary[num5];
				switch (jsonData.instance.ItemJsonData[num5.ToString()]["type"].I)
				{
				case 5:
					if (npcData["wuDaoSkillList"].ToList().Contains(2131))
					{
						num3 *= 2;
					}
					if (num2 > num3)
					{
						num2 = num3;
					}
					if (isUse)
					{
						for (int k = 0; k < num2; k++)
						{
							NpcJieSuanManager.inst.RemoveNpcItem(npcId, num5);
						}
					}
					num += itemTuPoLvDictionary[num5] * num2;
					break;
				case 3:
					num += itemTuPoLvDictionary[num5];
					if (!npcData["TuPoMiShu"].ToList().Contains(num5))
					{
						npcData["TuPoMiShu"].Add(num5);
						try
						{
							NpcJieSuanManager.inst.RemoveNpcItem(npcId, num5);
						}
						catch (Exception)
						{
							Debug.LogError((object)$"移除秘术出错,物品ID:{num5}");
						}
					}
					break;
				}
			}
			else if (npcData["TuPoMiShu"].ToList().Contains(num5))
			{
				num += itemTuPoLvDictionary[num5];
			}
		}
		return num;
	}

	public int GetNpcBigTuPoFenShu(int npcId, bool isUse = false, bool isKuaiSu = false)
	{
		if (!NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return 0;
		}
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int i = npcData["Level"].I;
		JSONObject jSONObject = jsonData.instance.NPCTuPuoDate[i.ToString()];
		int num = 0;
		if (npcData["isImportant"].b)
		{
			num = jSONObject["JinDanFen"][1].I;
		}
		else
		{
			num = NpcJieSuanManager.inst.getRandomInt(jSONObject["JinDanFen"][0].I, jSONObject["JinDanFen"][1].I);
			if (isKuaiSu)
			{
				switch (i)
				{
				case 3:
					num += 6;
					break;
				case 6:
					num += 10;
					break;
				case 9:
					num += 10;
					break;
				case 12:
					num += 5;
					break;
				}
			}
		}
		JSONObject jSONObject2 = jSONObject["TuPoItem"];
		Dictionary<int, int> npcBaiBaoItemSum = NpcJieSuanManager.inst.GetNpcBaiBaoItemSum(npcId, jSONObject2.ToList());
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		for (int j = 0; j < jSONObject2.Count; j++)
		{
			num5 = jSONObject2[j].I;
			if (npcBaiBaoItemSum.ContainsKey(num5))
			{
				num2 = npcBaiBaoItemSum[num5];
				num3 = itemTuPoUseNumDictionary[num5];
				switch (jsonData.instance.ItemJsonData[num5.ToString()]["type"].I)
				{
				case 5:
					if (npcData["wuDaoSkillList"].ToList().Contains(2131))
					{
						num3 *= 2;
					}
					if (num2 > num3)
					{
						num2 = num3;
					}
					if (isUse)
					{
						for (int k = 0; k < num2; k++)
						{
							NpcJieSuanManager.inst.RemoveNpcItem(npcId, num5);
						}
					}
					num += itemTuPoFenShuDictionary[num5] * num2;
					break;
				case 3:
					num += itemTuPoFenShuDictionary[num5];
					if (!npcData["TuPoMiShu"].ToList().Contains(num5))
					{
						npcData["TuPoMiShu"].Add(num5);
						NpcJieSuanManager.inst.RemoveNpcItem(npcId, num5);
					}
					break;
				}
			}
			else if (npcData["TuPoMiShu"].ToList().Contains(num5))
			{
				num += itemTuPoFenShuDictionary[num5];
			}
		}
		return num;
	}

	public void NpcBigTuPo(int npcId)
	{
		switch (NpcJieSuanManager.inst.GetNpcData(npcId)["Level"].I)
		{
		case 3:
			NpcTuPoZhuJi(npcId);
			break;
		case 6:
			NpcTuPoJinDan(npcId);
			break;
		case 9:
			NpcTuPoYuanYing(npcId);
			break;
		case 12:
			NpcTuPoHuaShen(npcId);
			break;
		}
	}

	public void NpcTuPoZhuJi(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		_ = npcData["Level"].I;
		int num = 0;
		num = GetNpcBigTuPoLv(npcId, isUse: false, isKuaiSu);
		if (npcData.HasField("ZhuJiTime"))
		{
			DateTime dateTime = DateTime.Parse(npcData["ZhuJiTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= dateTime))
			{
				return;
			}
			num = 100;
		}
		num += jsonData.instance.NPCTuPuoDate["3"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 26);
		if (npcData.HasField("AddToPoLv"))
		{
			num += npcData["AddToPoLv"].I;
		}
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["3"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 26);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		if (npcData.HasField("AddToPoLv"))
		{
			npcData.RemoveField("AddToPoLv");
		}
		int npcBigTuPoFenShu = GetNpcBigTuPoFenShu(npcId, isUse: true, isKuaiSu);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcBigTuPoFenShu);
		npcData.SetField("exp", 0);
		UpDateNpcData(npcId);
		NpcJieSuanManager.inst.npcNoteBook.NoteZhuJiSuccess(npcId);
	}

	public void NpcTuPoJinDan(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		_ = npcData["Level"].I;
		int num = 0;
		int num2 = 0;
		num = GetNpcBigTuPoLv(npcId, isUse: false, isKuaiSu);
		if (npcData.HasField("JinDanTime"))
		{
			DateTime dateTime = DateTime.Parse(npcData["JinDanTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= dateTime))
			{
				return;
			}
			num = 100;
		}
		if (npcData.HasField("AddToPoLv"))
		{
			num += npcData["AddToPoLv"].I;
		}
		if (npcData["wuDaoSkillList"].ToList().Contains(2112))
		{
			num += 30;
			num2 += 20;
		}
		num += jsonData.instance.NPCTuPuoDate["6"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 27);
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["6"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 27);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		if (npcData.HasField("AddToPoLv"))
		{
			npcData.RemoveField("AddToPoLv");
		}
		num2 += GetNpcBigTuPoFenShu(npcId, isUse: true, isKuaiSu);
		int num3 = num2 / 10 + 1;
		if (num3 > 9)
		{
			num3 = 9;
		}
		int jinDanId = GetJinDanId(npcId, num3);
		JSONObject jSONObject = jsonData.instance.JieDanBiao[jinDanId.ToString()];
		JSONObject jSONObject2 = new JSONObject();
		jSONObject2.SetField("JinDanId", jinDanId);
		jSONObject2.SetField("JinDanLv", num3);
		jSONObject2.SetField("JinDanAddSpeed", jSONObject["EXP"].I);
		jSONObject2.SetField("JinDanAddHp", jSONObject["HP"].I);
		npcData.SetField("JinDanData", jSONObject2);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, jSONObject["HP"].I);
		npcData.SetField("exp", 0);
		UpDateNpcData(npcId);
		NpcJieSuanManager.inst.npcNoteBook.NoteJinDanSuccess(npcId, num3);
		EventMag.Inst.SaveEvent(npcId, 1);
	}

	public void NpcTuPoYuanYing(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		_ = npcData["Level"].I;
		int num = 0;
		num = GetNpcBigTuPoLv(npcId, isUse: true, isKuaiSu);
		if (npcData.HasField("YuanYingTime"))
		{
			DateTime dateTime = DateTime.Parse(npcData["YuanYingTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= dateTime))
			{
				return;
			}
			num = 100;
		}
		num += jsonData.instance.NPCTuPuoDate["9"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 28);
		if (npcData.HasField("AddToPoLv"))
		{
			num += npcData["AddToPoLv"].I;
		}
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["9"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 28);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		if (npcData.HasField("AddToPoLv"))
		{
			npcData.RemoveField("AddToPoLv");
		}
		if (npcData.HasField("JinDanData"))
		{
			npcData["JinDanData"].SetField("JinDanAddSpeed", npcData["JinDanData"]["JinDanAddSpeed"].I * 2);
			NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcData["JinDanData"]["JinDanAddHp"].I);
		}
		int npcBigTuPoFenShu = GetNpcBigTuPoFenShu(npcId, isUse: true, isKuaiSu);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcBigTuPoFenShu * 10);
		NpcJieSuanManager.inst.npcNoteBook.NoteYuanYingSuccess(npcId);
		npcData.SetField("exp", 0);
		UpDateNpcData(npcId);
		EventMag.Inst.SaveEvent(npcId, 2);
	}

	public void NpcTuPoHuaShen(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		_ = npcData["Level"].I;
		int num = 0;
		num = GetNpcBigTuPoLv(npcId, isUse: true, isKuaiSu);
		if (npcData.HasField("HuaShengTime"))
		{
			DateTime dateTime = DateTime.Parse(npcData["HuaShengTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= dateTime))
			{
				return;
			}
			num = 100;
		}
		if (npcData.HasField("AddToPoLv"))
		{
			num += npcData["AddToPoLv"].I;
		}
		num += jsonData.instance.NPCTuPuoDate["12"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 29);
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["12"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 29);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		if (npcData.HasField("AddToPoLv"))
		{
			npcData.RemoveField("AddToPoLv");
		}
		int npcBigTuPoFenShu = GetNpcBigTuPoFenShu(npcId, isUse: true, isKuaiSu);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcBigTuPoFenShu * 1200);
		NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(npcId, npcBigTuPoFenShu * 6);
		NpcJieSuanManager.inst.npcSetField.AddNpcDunSu(npcId, npcBigTuPoFenShu * 3);
		NpcJieSuanManager.inst.npcSetField.AddNpcZhiZi(npcId, npcBigTuPoFenShu * 3);
		NpcJieSuanManager.inst.npcSetField.AddNpcWuXing(npcId, npcBigTuPoFenShu * 3);
		NpcJieSuanManager.inst.npcNoteBook.NoteHuaShenSuccess(npcId);
		npcData.SetField("exp", 0);
		UpDateNpcData(npcId);
		EventMag.Inst.SaveEvent(npcId, 3);
	}

	private void UpDateNpcData(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		JSONObject jSONObject = jsonData.instance.LevelUpDataJsonData[npcData["Level"].I.ToString()];
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, jSONObject["AddHp"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(npcId, jSONObject["AddShenShi"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcDunSu(npcId, jSONObject["AddDunSu"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcShouYuan(npcId, jSONObject["AddShouYuan"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcLevel(npcId, 1);
		JSONObject jSONObject2 = new JSONObject();
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			if (jsonData.instance.NPCLeiXingDate[i]["Type"].I == npcData["Type"].I && jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I == npcData["LiuPai"].I && jsonData.instance.NPCLeiXingDate[i]["Level"].I == npcData["Level"].I)
			{
				jSONObject2 = new JSONObject(jsonData.instance.NPCLeiXingDate[i].ToString());
				break;
			}
		}
		npcData.SetField("equipWeaponPianHao", jSONObject2["equipWeapon"]);
		npcData.SetField("equipWeapon2PianHao", jSONObject2["equipWeapon"]);
		npcData.SetField("equipClothingPianHao", jSONObject2["equipClothing"]);
		npcData.SetField("equipRingPianHao", jSONObject2["equipRing"]);
		if (jSONObject2.keys.Count > 0)
		{
			npcData.SetField("skills", jSONObject2["skills"]);
			npcData.SetField("staticSkills", jSONObject2["staticSkills"]);
			npcData.SetField("xiuLianSpeed", FactoryManager.inst.npcFactory.getXiuLianSpeed(npcData["staticSkills"], npcData["ziZhi"].I));
			npcData.SetField("yuanying", jSONObject2["yuanying"]);
			npcData.SetField("XinQuType", jSONObject2["XinQuType"].Copy());
			jsonData.instance.MonstarCreatInterstingType(npcId);
			npcData.SetField("HuaShenLingYu", jSONObject2["HuaShenLingYu"]);
		}
		NpcJieSuanManager.inst.UpdateNpcWuDao(npcId);
		int targetId = 0;
		if (NpcJieSuanManager.inst.npcChengHao.IsCanUpToChengHao(npcId, ref targetId))
		{
			NpcJieSuanManager.inst.npcChengHao.UpDateChengHao(npcId, targetId);
		}
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 10);
		AuToWuDaoSkill(npcId);
	}

	public void AuToWuDaoSkill(int npcId)
	{
		JSONObject jSONObject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		List<int> list = jSONObject["wuDaoSkillList"].ToList();
		JSONObject jSONObject2 = jSONObject["wuDaoJson"];
		int num = jSONObject["EWWuDaoDian"].I;
		int num2 = 0;
		int num3 = 0;
		if (num < 1)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NpcWuDaoChiData.list)
		{
			foreach (JSONObject item2 in item["wudaochi"].list)
			{
				if (list.Contains(item2.I) || num <= item["xiaohao"].I)
				{
					continue;
				}
				int i = jSONObject2[jsonData.instance.WuDaoJson[item2.ToString()]["Type"][0].I.ToString()]["level"].I;
				num2 = jsonData.instance.WuDaoJson[item2.ToString()]["Lv"].I;
				if (i > num2)
				{
					jSONObject["wuDaoSkillList"].Add(item2.I);
					num -= item["xiaohao"].I;
					jSONObject.SetField("EWWuDaoDian", num);
					if (num < 1)
					{
						return;
					}
				}
			}
		}
	}

	public int GetJinDanId(int npcId, int jinDanQuality)
	{
		JSONObject jSONObject = NpcJieSuanManager.inst.GetNpcData(npcId)["JinDanType"];
		foreach (JSONObject item in jsonData.instance.JieDanBiao.list)
		{
			if (jSONObject.Count == 2)
			{
				if (item["JinDanQuality"].I == jinDanQuality && item["JinDanType"].Count == 2 && item["JinDanType"][0].I == jSONObject[0].I && item["JinDanType"][1].I == jSONObject[1].I)
				{
					return item["id"].I;
				}
			}
			else if (item["JinDanQuality"].I == jinDanQuality && item["JinDanType"].Count == 1 && item["JinDanType"][0].I == jSONObject[0].I)
			{
				return item["id"].I;
			}
		}
		return 0;
	}

	public void NpcFlyToSky(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.GetNpcData(npcId);
		int npcBigTuPoLv = GetNpcBigTuPoLv(npcId);
		if (NpcJieSuanManager.inst.getRandomInt(0, 100) > npcBigTuPoLv)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(12, npcId);
			return;
		}
		npcData.SetField("IsFly", val: true);
		EventMag.Inst.SaveEvent(npcId, 4);
	}
}
