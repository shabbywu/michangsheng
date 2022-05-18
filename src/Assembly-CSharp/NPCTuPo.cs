using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000344 RID: 836
public class NPCTuPo
{
	// Token: 0x06001899 RID: 6297 RVA: 0x000DBAA0 File Offset: 0x000D9CA0
	public NPCTuPo()
	{
		foreach (JSONObject jsonobject in jsonData.instance.NPCTuPuoDate.list)
		{
			for (int i = 0; i < jsonobject["TuPoItem"].Count; i++)
			{
				this.itemTuPoLvDictionary.Add(jsonobject["TuPoItem"][i].I, jsonobject["TiShengJiLv"][i].I);
				this.itemTuPoUseNumDictionary.Add(jsonobject["TuPoItem"][i].I, jsonobject["ShangXian"][i].I);
				if (jsonobject["id"].I <= 12)
				{
					this.itemTuPoFenShuDictionary.Add(jsonobject["TuPoItem"][i].I, jsonobject["TiShengJinDan"][i].I);
				}
			}
		}
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x000DBBFC File Offset: 0x000D9DFC
	public JSONObject NpcSmallToPo(JSONObject npcDate, int nextLevel)
	{
		JSONObject jsonobject = jsonData.instance.LevelUpDataJsonData[(nextLevel - 1).ToString()];
		npcDate.SetField("Level", nextLevel);
		if (nextLevel <= 14)
		{
			npcDate.SetField("NextExp", jsonData.instance.NPCChuShiShuZiDate[(nextLevel + 1).ToString()]["xiuwei"].I);
		}
		else
		{
			npcDate.SetField("NextExp", 0);
		}
		npcDate.SetField("HP", npcDate["HP"].I + jsonobject["AddHp"].I);
		npcDate.SetField("shengShi", npcDate["shengShi"].I + jsonobject["AddShenShi"].I);
		npcDate.SetField("dunSu", npcDate["dunSu"].I + jsonobject["AddDunSu"].I);
		JSONObject jsonobject2 = new JSONObject();
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			if (jsonData.instance.NPCLeiXingDate[i]["Type"].I == npcDate["Type"].I && jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I == npcDate["LiuPai"].I && jsonData.instance.NPCLeiXingDate[i]["Level"].I == nextLevel)
			{
				jsonobject2 = new JSONObject(jsonData.instance.NPCLeiXingDate[i].ToString(), -2, false, false);
				break;
			}
		}
		if (jsonobject2.keys.Count > 0)
		{
			npcDate.SetField("skills", jsonobject2["skills"]);
			npcDate.SetField("staticSkills", jsonobject2["staticSkills"]);
			npcDate.SetField("xiuLianSpeed", FactoryManager.inst.npcFactory.getXiuLianSpeed(npcDate["staticSkills"], (float)npcDate["ziZhi"].I));
			npcDate.SetField("yuanying", jsonobject2["yuanying"]);
			if (jsonobject2["yuanying"].I > 0)
			{
				float num = (float)jsonData.instance.StaticSkillJsonData[jsonobject2["yuanying"].I.ToString()]["Skill_Speed"].I;
				num *= 1f + (float)npcDate["ziZhi"].I / 100f;
				if (num > (float)npcDate["xiuLianSpeed"].I)
				{
					npcDate.SetField("xiuLianSpeed", num);
				}
				npcDate.SetField("XinQuType", jsonobject2["XinQuType"]);
			}
		}
		npcDate.SetField("equipWeaponPianHao", jsonobject2["equipWeapon"]);
		npcDate.SetField("equipWeapon2PianHao", jsonobject2["equipWeapon"]);
		npcDate.SetField("equipClothingPianHao", jsonobject2["equipClothing"]);
		npcDate.SetField("equipRingPianHao", jsonobject2["equipRing"]);
		NpcJieSuanManager.inst.npcNoteBook.NoteSmallTuPo(npcDate["id"].I);
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcDate["id"].I, 11);
		npcDate.SetField("exp", 0);
		return npcDate;
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x000DBFA0 File Offset: 0x000DA1A0
	public bool IsCanSmallTuPo(int npcID = -1, JSONObject actionDate = null)
	{
		JSONObject jsonobject = new JSONObject();
		if (npcID == -1)
		{
			jsonobject = actionDate;
		}
		else
		{
			jsonobject = jsonData.instance.AvatarJsonData[npcID.ToString()];
		}
		int i = jsonobject["exp"].I;
		int num = jsonobject["Level"].I + 1;
		return i >= jsonobject["NextExp"].I && jsonobject["NextExp"].I != 0 && num != 4 && num != 7 && num != 10 && num != 13 && num != 16;
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000DC034 File Offset: 0x000DA234
	public bool IsCanBigTuPo(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Level"].I;
		if (NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			JSONObject jsonobject = jsonData.instance.NPCTuPuoDate[i.ToString()];
			if (NpcJieSuanManager.inst.GetNpcShengYuTime(npcId) <= jsonobject["shengyushouyuan"].I)
			{
				return true;
			}
			int i2 = jsonobject["mubiaojilv"].I;
			if (this.GetNpcBigTuPoLv(npcId, false, false) >= i2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000DC0C8 File Offset: 0x000DA2C8
	public int GetNpcBigTuPoLv(int npcId, bool isUse = false, bool isKuaiSu = false)
	{
		if (!NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return 0;
		}
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["Level"].I;
		int npcBigLevel = NpcJieSuanManager.inst.getNpcBigLevel(npcId);
		if (npcData["isImportant"].b)
		{
			if (npcBigLevel == 1 && npcData.HasField("ZhuJiTime"))
			{
				DateTime nowTime = NpcJieSuanManager.inst.GetNowTime();
				DateTime t = DateTime.Parse(npcData["ZhuJiTime"].str);
				if (nowTime >= t)
				{
					return 100;
				}
				return -1;
			}
			else if (npcBigLevel == 2 && npcData.HasField("JinDanTime"))
			{
				DateTime nowTime2 = NpcJieSuanManager.inst.GetNowTime();
				DateTime t2 = DateTime.Parse(npcData["JinDanTime"].str);
				if (nowTime2 >= t2)
				{
					return 100;
				}
				return -1;
			}
			else if (npcBigLevel == 3 && npcData.HasField("YuanYingTime"))
			{
				DateTime nowTime3 = NpcJieSuanManager.inst.GetNowTime();
				DateTime t3 = DateTime.Parse(npcData["YuanYingTime"].str);
				if (nowTime3 >= t3)
				{
					return 100;
				}
				return -1;
			}
		}
		JSONObject jsonobject = jsonData.instance.NPCTuPuoDate[i.ToString()];
		int num = jsonobject["jilv"].I;
		if (isKuaiSu)
		{
			if (npcBigLevel == 1)
			{
				num += 20;
			}
			else if (npcBigLevel == 2)
			{
				num += 10;
			}
			else if (npcBigLevel == 3)
			{
				num += 4;
			}
			else if (npcBigLevel == 4)
			{
				num = num;
			}
		}
		num += (int)(npcData["ziZhi"].f * jsonobject["ZiZhiJiaCheng"].f);
		JSONObject jsonobject2 = jsonobject["TuPoItem"];
		Dictionary<int, int> npcBaiBaoItemSum = NpcJieSuanManager.inst.GetNpcBaiBaoItemSum(npcId, jsonobject2.ToList());
		int num2 = 0;
		int j = 0;
		while (j < jsonobject2.Count)
		{
			num2 = jsonobject2[j].I;
			if (!npcBaiBaoItemSum.ContainsKey(num2))
			{
				goto IL_301;
			}
			int num3 = npcBaiBaoItemSum[num2];
			int num4 = this.itemTuPoUseNumDictionary[num2];
			int i2 = jsonData.instance.ItemJsonData[num2.ToString()]["type"].I;
			if (i2 == 5)
			{
				if (npcData["wuDaoSkillList"].ToList().Contains(2131))
				{
					num4 *= 2;
				}
				if (num3 > num4)
				{
					num3 = num4;
				}
				if (isUse)
				{
					for (int k = 0; k < num3; k++)
					{
						NpcJieSuanManager.inst.RemoveNpcItem(npcId, num2);
					}
				}
				num += this.itemTuPoLvDictionary[num2] * num3;
			}
			else if (i2 == 3)
			{
				num += this.itemTuPoLvDictionary[num2];
				if (!npcData["TuPoMiShu"].ToList().Contains(num2))
				{
					npcData["TuPoMiShu"].Add(num2);
					try
					{
						NpcJieSuanManager.inst.RemoveNpcItem(npcId, num2);
						goto IL_32C;
					}
					catch (Exception)
					{
						Debug.LogError(string.Format("移除秘术出错,物品ID:{0}", num2));
						goto IL_32C;
					}
					goto IL_301;
				}
			}
			IL_32C:
			j++;
			continue;
			IL_301:
			if (npcData["TuPoMiShu"].ToList().Contains(num2))
			{
				num += this.itemTuPoLvDictionary[num2];
				goto IL_32C;
			}
			goto IL_32C;
		}
		return num;
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000DC428 File Offset: 0x000DA628
	public int GetNpcBigTuPoFenShu(int npcId, bool isUse = false, bool isKuaiSu = false)
	{
		if (!NpcJieSuanManager.inst.npcStatus.IsInTargetStatus(npcId, 2))
		{
			return 0;
		}
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["Level"].I;
		JSONObject jsonobject = jsonData.instance.NPCTuPuoDate[i.ToString()];
		int num;
		if (npcData["isImportant"].b)
		{
			num = jsonobject["JinDanFen"][1].I;
		}
		else
		{
			num = NpcJieSuanManager.inst.getRandomInt(jsonobject["JinDanFen"][0].I, jsonobject["JinDanFen"][1].I);
			if (isKuaiSu)
			{
				if (i == 3)
				{
					num += 6;
				}
				else if (i == 6)
				{
					num += 10;
				}
				else if (i == 9)
				{
					num += 10;
				}
				else if (i == 12)
				{
					num += 5;
				}
			}
		}
		JSONObject jsonobject2 = jsonobject["TuPoItem"];
		Dictionary<int, int> npcBaiBaoItemSum = NpcJieSuanManager.inst.GetNpcBaiBaoItemSum(npcId, jsonobject2.ToList());
		for (int j = 0; j < jsonobject2.Count; j++)
		{
			int i2 = jsonobject2[j].I;
			if (npcBaiBaoItemSum.ContainsKey(i2))
			{
				int num2 = npcBaiBaoItemSum[i2];
				int num3 = this.itemTuPoUseNumDictionary[i2];
				int i3 = jsonData.instance.ItemJsonData[i2.ToString()]["type"].I;
				if (i3 == 5)
				{
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
							NpcJieSuanManager.inst.RemoveNpcItem(npcId, i2);
						}
					}
					num += this.itemTuPoFenShuDictionary[i2] * num2;
				}
				else if (i3 == 3)
				{
					num += this.itemTuPoFenShuDictionary[i2];
					if (!npcData["TuPoMiShu"].ToList().Contains(i2))
					{
						npcData["TuPoMiShu"].Add(i2);
						NpcJieSuanManager.inst.RemoveNpcItem(npcId, i2);
					}
				}
			}
			else if (npcData["TuPoMiShu"].ToList().Contains(i2))
			{
				num += this.itemTuPoFenShuDictionary[i2];
			}
		}
		return num;
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x000DC698 File Offset: 0x000DA898
	public void NpcBigTuPo(int npcId)
	{
		int i = NpcJieSuanManager.inst.getNpcData(npcId)["Level"].I;
		if (i == 3)
		{
			this.NpcTuPoZhuJi(npcId, false);
			return;
		}
		if (i == 6)
		{
			this.NpcTuPoJinDan(npcId, false);
			return;
		}
		if (i == 9)
		{
			this.NpcTuPoYuanYing(npcId, false);
			return;
		}
		if (i == 12)
		{
			this.NpcTuPoHuaShen(npcId, false);
		}
	}

	// Token: 0x060018A0 RID: 6304 RVA: 0x000DC6F8 File Offset: 0x000DA8F8
	public void NpcTuPoZhuJi(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["Level"].I;
		int num = this.GetNpcBigTuPoLv(npcId, false, isKuaiSu);
		if (npcData.HasField("ZhuJiTime"))
		{
			DateTime t = DateTime.Parse(npcData["ZhuJiTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= t))
			{
				return;
			}
			num = 100;
		}
		num += jsonData.instance.NPCTuPuoDate["3"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 26);
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["3"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 26);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		int npcBigTuPoFenShu = this.GetNpcBigTuPoFenShu(npcId, true, isKuaiSu);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcBigTuPoFenShu);
		npcData.SetField("exp", 0);
		this.UpDateNpcData(npcId);
		NpcJieSuanManager.inst.npcNoteBook.NoteZhuJiSuccess(npcId);
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000DC848 File Offset: 0x000DAA48
	public void NpcTuPoJinDan(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["Level"].I;
		int num = 0;
		int num2 = this.GetNpcBigTuPoLv(npcId, false, isKuaiSu);
		if (npcData.HasField("JinDanTime"))
		{
			DateTime t = DateTime.Parse(npcData["JinDanTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= t))
			{
				return;
			}
			num2 = 100;
		}
		if (npcData["wuDaoSkillList"].ToList().Contains(2112))
		{
			num2 += 30;
			num += 20;
		}
		num2 += jsonData.instance.NPCTuPuoDate["6"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 27);
		if (num2 < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["6"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 27);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		num += this.GetNpcBigTuPoFenShu(npcId, true, isKuaiSu);
		int num3 = num / 10 + 1;
		if (num3 > 9)
		{
			num3 = 9;
		}
		int jinDanId = this.GetJinDanId(npcId, num3);
		JSONObject jsonobject = jsonData.instance.JieDanBiao[jinDanId.ToString()];
		JSONObject jsonobject2 = new JSONObject();
		jsonobject2.SetField("JinDanId", jinDanId);
		jsonobject2.SetField("JinDanLv", num3);
		jsonobject2.SetField("JinDanAddSpeed", jsonobject["EXP"].I);
		jsonobject2.SetField("JinDanAddHp", jsonobject["HP"].I);
		npcData.SetField("JinDanData", jsonobject2);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, jsonobject["HP"].I);
		npcData.SetField("exp", 0);
		this.UpDateNpcData(npcId);
		NpcJieSuanManager.inst.npcNoteBook.NoteJinDanSuccess(npcId, num3);
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000DCA70 File Offset: 0x000DAC70
	public void NpcTuPoYuanYing(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["Level"].I;
		int num = this.GetNpcBigTuPoLv(npcId, true, isKuaiSu);
		if (npcData.HasField("YuanYingTime"))
		{
			DateTime t = DateTime.Parse(npcData["YuanYingTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= t))
			{
				return;
			}
			num = 100;
		}
		num += jsonData.instance.NPCTuPuoDate["9"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 28);
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["9"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 28);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		if (npcData.HasField("JinDanData"))
		{
			npcData["JinDanData"].SetField("JinDanAddSpeed", npcData["JinDanData"]["JinDanAddSpeed"].I * 2);
			NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcData["JinDanData"]["JinDanAddHp"].I);
		}
		int npcBigTuPoFenShu = this.GetNpcBigTuPoFenShu(npcId, true, isKuaiSu);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcBigTuPoFenShu * 10);
		NpcJieSuanManager.inst.npcNoteBook.NoteYuanYingSuccess(npcId);
		npcData.SetField("exp", 0);
		this.UpDateNpcData(npcId);
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000DCC2C File Offset: 0x000DAE2C
	public void NpcTuPoHuaShen(int npcId, bool isKuaiSu = false)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int i = npcData["Level"].I;
		int num = this.GetNpcBigTuPoLv(npcId, true, isKuaiSu);
		if (npcData.HasField("HuaShengTime"))
		{
			DateTime t = DateTime.Parse(npcData["HuaShengTime"].str);
			if (!(NpcJieSuanManager.inst.GetNowTime() >= t))
			{
				return;
			}
			num = 100;
		}
		num += jsonData.instance.NPCTuPuoDate["12"]["FailAddLv"].I * NpcJieSuanManager.inst.npcNoteBook.GetEventCount(npcId, 29);
		if (num < NpcJieSuanManager.inst.getRandomInt(1, 100))
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcExp(npcId, -jsonData.instance.NPCTuPuoDate["12"]["sunshi"].I);
			NpcJieSuanManager.inst.npcNoteBook.NoteBigTuPoFail(npcId, 29);
			NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 1);
			return;
		}
		int npcBigTuPoFenShu = this.GetNpcBigTuPoFenShu(npcId, true, isKuaiSu);
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, npcBigTuPoFenShu * 1200);
		NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(npcId, npcBigTuPoFenShu * 6);
		NpcJieSuanManager.inst.npcSetField.AddNpcDunSu(npcId, npcBigTuPoFenShu * 3);
		NpcJieSuanManager.inst.npcSetField.AddNpcZhiZi(npcId, npcBigTuPoFenShu * 3);
		NpcJieSuanManager.inst.npcSetField.AddNpcWuXing(npcId, npcBigTuPoFenShu * 3);
		NpcJieSuanManager.inst.npcNoteBook.NoteHuaShenSuccess(npcId);
		npcData.SetField("exp", 0);
		this.UpDateNpcData(npcId);
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000DCDD0 File Offset: 0x000DAFD0
	private void UpDateNpcData(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		JSONObject jsonobject = jsonData.instance.LevelUpDataJsonData[npcData["Level"].I.ToString()];
		NpcJieSuanManager.inst.npcSetField.AddNpcHp(npcId, jsonobject["AddHp"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcShenShi(npcId, jsonobject["AddShenShi"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcDunSu(npcId, jsonobject["AddDunSu"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcShouYuan(npcId, jsonobject["AddShouYuan"].I);
		NpcJieSuanManager.inst.npcSetField.AddNpcLevel(npcId, 1);
		JSONObject jsonobject2 = new JSONObject();
		for (int i = 0; i < jsonData.instance.NPCLeiXingDate.Count; i++)
		{
			if (jsonData.instance.NPCLeiXingDate[i]["Type"].I == npcData["Type"].I && jsonData.instance.NPCLeiXingDate[i]["LiuPai"].I == npcData["LiuPai"].I && jsonData.instance.NPCLeiXingDate[i]["Level"].I == npcData["Level"].I)
			{
				jsonobject2 = new JSONObject(jsonData.instance.NPCLeiXingDate[i].ToString(), -2, false, false);
				break;
			}
		}
		npcData.SetField("equipWeaponPianHao", jsonobject2["equipWeapon"]);
		npcData.SetField("equipWeapon2PianHao", jsonobject2["equipWeapon"]);
		npcData.SetField("equipClothingPianHao", jsonobject2["equipClothing"]);
		npcData.SetField("equipRingPianHao", jsonobject2["equipRing"]);
		if (jsonobject2.keys.Count > 0)
		{
			npcData.SetField("skills", jsonobject2["skills"]);
			npcData.SetField("staticSkills", jsonobject2["staticSkills"]);
			npcData.SetField("xiuLianSpeed", FactoryManager.inst.npcFactory.getXiuLianSpeed(npcData["staticSkills"], (float)npcData["ziZhi"].I));
			npcData.SetField("yuanying", jsonobject2["yuanying"]);
			npcData.SetField("XinQuType", jsonobject2["XinQuType"]);
			npcData.SetField("HuaShenLingYu", jsonobject2["HuaShenLingYu"]);
		}
		NpcJieSuanManager.inst.UpdateNpcWuDao(npcId);
		int id = 0;
		if (NpcJieSuanManager.inst.npcChengHao.IsCanUpToChengHao(npcId, ref id))
		{
			NpcJieSuanManager.inst.npcChengHao.UpDateChengHao(npcId, id);
		}
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(npcId, 10);
		this.AuToWuDaoSkill(npcId);
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000DD0E0 File Offset: 0x000DB2E0
	public void AuToWuDaoSkill(int npcId)
	{
		JSONObject jsonobject = jsonData.instance.AvatarJsonData[npcId.ToString()];
		List<int> list = jsonobject["wuDaoSkillList"].ToList();
		JSONObject jsonobject2 = jsonobject["wuDaoJson"];
		int num = jsonobject["EWWuDaoDian"].I;
		if (num < 1)
		{
			return;
		}
		foreach (JSONObject jsonobject3 in jsonData.instance.NpcWuDaoChiData.list)
		{
			foreach (JSONObject jsonobject4 in jsonobject3["wudaochi"].list)
			{
				if (!list.Contains(jsonobject4.I) && num > jsonobject3["xiaohao"].I)
				{
					int i = jsonobject2[jsonData.instance.WuDaoJson[jsonobject4.ToString()]["Type"][0].I.ToString()]["level"].I;
					int i2 = jsonData.instance.WuDaoJson[jsonobject4.ToString()]["Lv"].I;
					if (i > i2)
					{
						jsonobject["wuDaoSkillList"].Add(jsonobject4.I);
						num -= jsonobject3["xiaohao"].I;
						jsonobject.SetField("EWWuDaoDian", num);
						if (num < 1)
						{
							return;
						}
					}
				}
			}
		}
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x000DD2D0 File Offset: 0x000DB4D0
	public int GetJinDanId(int npcId, int jinDanQuality)
	{
		JSONObject jsonobject = NpcJieSuanManager.inst.getNpcData(npcId)["JinDanType"];
		foreach (JSONObject jsonobject2 in jsonData.instance.JieDanBiao.list)
		{
			if (jsonobject.Count == 2)
			{
				if (jsonobject2["JinDanQuality"].I == jinDanQuality && jsonobject2["JinDanType"].Count == 2 && jsonobject2["JinDanType"][0].I == jsonobject[0].I && jsonobject2["JinDanType"][1].I == jsonobject[1].I)
				{
					return jsonobject2["id"].I;
				}
			}
			else if (jsonobject2["JinDanQuality"].I == jinDanQuality && jsonobject2["JinDanType"].Count == 1 && jsonobject2["JinDanType"][0].I == jsonobject[0].I)
			{
				return jsonobject2["id"].I;
			}
		}
		return 0;
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000DD444 File Offset: 0x000DB644
	public void NpcFlyToSky(int npcId)
	{
		JSONObject npcData = NpcJieSuanManager.inst.getNpcData(npcId);
		int npcBigTuPoLv = this.GetNpcBigTuPoLv(npcId, false, false);
		if (NpcJieSuanManager.inst.getRandomInt(0, 100) > npcBigTuPoLv)
		{
			NpcJieSuanManager.inst.npcDeath.SetNpcDeath(12, npcId, 0, false);
			return;
		}
		npcData.SetField("IsFly", true);
	}

	// Token: 0x0400139E RID: 5022
	private Dictionary<int, int> itemTuPoLvDictionary = new Dictionary<int, int>();

	// Token: 0x0400139F RID: 5023
	private Dictionary<int, int> itemTuPoFenShuDictionary = new Dictionary<int, int>();

	// Token: 0x040013A0 RID: 5024
	private Dictionary<int, int> itemTuPoUseNumDictionary = new Dictionary<int, int>();
}
