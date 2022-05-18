using System;
using System.Collections.Generic;
using System.Text;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame.TianJiDaBi;

// Token: 0x02000382 RID: 898
public class UINPCData : IComparable
{
	// Token: 0x06001932 RID: 6450 RVA: 0x000DFA38 File Offset: 0x000DDC38
	private static void Init()
	{
		if (!UINPCData._Inited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.LevelUpDataJsonData.list)
			{
				UINPCData._LevelDict.Add(jsonobject["id"].I, jsonobject["Name"].Str);
			}
			foreach (JSONObject jsonobject2 in jsonData.instance.NpcStatusDate.list)
			{
				UINPCData._ZhuangTaiDict.Add(jsonobject2["id"].I, jsonobject2["ZhuangTaiInfo"].Str);
			}
			foreach (JSONObject jsonobject3 in jsonData.instance._skillJsonData.list)
			{
				int i = jsonobject3["Skill_ID"].I;
				int i2 = jsonobject3["Skill_Lv"].I;
				int i3 = jsonobject3["id"].I;
				if (UINPCData._SkillDict.ContainsKey(i))
				{
					if (!UINPCData._SkillDict[i].TryAdd(i2, i3, ""))
					{
						Debug.LogError(string.Format("为NPC数据初始化技能数据时出错，在技能表，流水号{0}，错误技能ID:{1}", i3, i));
					}
				}
				else
				{
					UINPCData._SkillDict.TryAdd(i, new Dictionary<int, int>
					{
						{
							i2,
							i3
						}
					}, "");
				}
			}
			foreach (JSONObject jsonobject4 in jsonData.instance.WuDaoJson.list)
			{
				UIWuDaoSkillData uiwuDaoSkillData = new UIWuDaoSkillData();
				uiwuDaoSkillData.ID = jsonobject4["id"].I;
				uiwuDaoSkillData.Name = jsonobject4["name"].Str;
				uiwuDaoSkillData.WuDaoLv = jsonobject4["Lv"].I;
				UINPCData._WuDaoSkillTypeDict.Add(uiwuDaoSkillData.ID, new List<int>());
				foreach (JSONObject jsonobject5 in jsonobject4["Type"].list)
				{
					uiwuDaoSkillData.WuDaoType.Add(jsonobject5.I);
					UINPCData._WuDaoSkillTypeDict[uiwuDaoSkillData.ID].Add(jsonobject5.I);
				}
				uiwuDaoSkillData.Desc = jsonobject4["xiaoguo"].Str;
				UINPCData._WuDaoSkillDict.Add(uiwuDaoSkillData.ID, uiwuDaoSkillData);
			}
			foreach (JSONObject jsonobject6 in jsonData.instance.NpcShiJianData.list)
			{
				UINPCData._EventDescDict.Add(jsonobject6["id"].I, jsonobject6["ShiJianInfo"].Str);
				UINPCData._EventTypeDict.Add(jsonobject6["id"].I, jsonobject6["ShiJianType"].I);
			}
			foreach (JSONObject jsonobject7 in jsonData.instance.NpcQiYuDate.list)
			{
				UINPCData._EventQiYuDescDict.Add(jsonobject7["id"].I, jsonobject7["QiYuInfo"].Str);
			}
			foreach (JSONObject jsonobject8 in jsonData.instance.NpcImprotantEventData.list)
			{
				UINPCData._EventGuDingDescDict.Add(jsonobject8["id"].I, jsonobject8["ShiJianInfo"].Str);
			}
			foreach (JSONObject jsonobject9 in jsonData.instance.NPCActionDate.list)
			{
				UINPCData._ActionTaskDict.Add(jsonobject9["id"].I, jsonobject9["IsTask"].I == 1);
			}
			UINPCData._Inited = true;
		}
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x000DFFCC File Offset: 0x000DE1CC
	public UINPCData(int id, bool isThreeSceneNPC = false)
	{
		this.ID = id;
		this.IsThreeSceneNPC = isThreeSceneNPC;
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x000159F6 File Offset: 0x00013BF6
	public void SetID(int id)
	{
		this.ID = id;
		this.RefreshData();
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x000E003C File Offset: 0x000DE23C
	public void RefreshData()
	{
		UINPCData.Init();
		try
		{
			this.json = this.ID.NPCJson();
			if (!jsonData.instance.AvatarRandomJsonData.HasField(this.ID.ToString()))
			{
				this.Name = "获取失败";
				Debug.LogError(string.Format("获取NPC {0} 的名字和好感度失败，jsonData.instance.AvatarRandomJsonData中没有此ID", this.ID));
				throw new Exception("NPC获取失败");
			}
			this.Name = jsonData.instance.AvatarRandomJsonData[this.ID.ToString()]["Name"].Str;
			this.Favor = jsonData.instance.AvatarRandomJsonData[this.ID.ToString()]["HaoGanDu"].I;
			if (UINPCJiaoHu.isDebugMode)
			{
				this.Name += this.ID.ToString();
			}
			this.Sex = this.json["SexType"].I;
			this.Title = this.json["Title"].Str;
			this.Age = this.json["age"].I / 12;
			this.HP = this.json["HP"].I;
			if (jsonData.instance.AvatarJsonData.HasField(this.ID.ToString()))
			{
				this.Face = jsonData.instance.AvatarJsonData[this.ID.ToString()]["face"].I;
			}
			this.IsTag = this.json.TryGetField("IsTag").b;
			if (UINPCData.isDebugMode)
			{
				this.Favor = Random.Range(-100, 300);
			}
			this.FavorLevel = UINPCHeadFavor.GetFavorLevel(this.Favor);
			this.Level = this.json["Level"].I;
			this.LevelStr = UINPCData._LevelDict[this.Level];
			this.BigLevel = (this.Level - 1) / 3 + 1;
			int num = NPCEx.CalcZengLiX(this);
			int i = PlayerEx.Player.ZengLi.TryGetField("DuoYuQingFen").TryGetField(this.ID.ToString()).I;
			this.FavorPer = (float)i / (float)num;
			if (this.json.HasField("isImportant"))
			{
				if (this.json["isImportant"].b)
				{
					this.IsGuDingNPC = true;
				}
				this.IsZhongYaoNPC = NPCEx.IsZhongYaoNPC(this.ID, out this.ZhongYaoNPCID);
				bool isZhongYaoNPC = this.IsZhongYaoNPC;
				this.NPCType = this.json["Type"].I;
				this.Tag = this.json["NPCTag"].I;
				this.ZhengXie = (jsonData.instance.NPCTagDate[this.Tag.ToString()]["zhengxie"].I == 1);
				this.Exp = this.json["exp"].I;
				this.QingFen = this.json["QingFen"].I;
				this.ZhuangTai = this.json["Status"]["StatusId"].I;
				this.ZhuangTaiStr = UINPCData._ZhuangTaiDict[this.ZhuangTai];
				this.ShouYuan = this.json["shouYuan"].I;
				this.ZiZhi = this.json["ziZhi"].I;
				this.WuXing = this.json["wuXin"].I;
				this.DunSu = this.json["dunSu"].I;
				this.ShenShi = this.json["shengShi"].I;
				this.IsKnowPlayer = this.json["IsKnowPlayer"].b;
				if (this.json["paimaifenzu"].list.Count == 2 || this.json["paimaifenzu"].list[0].I == 2)
				{
					this.IsNingZhouNPC = false;
				}
				else
				{
					this.IsNingZhouNPC = true;
				}
				this.XingGe = this.json["XingGe"].I;
				this.LiuPai = this.json["LiuPai"].I;
				this.MenPai = this.json["MenPai"].I;
				this.ActionID = this.json["ActionId"].I;
				this.IsNeedHelp = this.json["IsNeedHelp"].b;
				this.IsTask = false;
				if (this.IsNeedHelp && !TianJiDaBiManager.IsOnSim && !SceneManager.GetActiveScene().name.StartsWith("Sea") && this.FavorLevel >= 3 && UINPCData._ActionTaskDict.ContainsKey(this.ActionID) && UINPCData._ActionTaskDict[this.ActionID] && PlayerEx.Player.getLevelType() >= this.BigLevel - 1)
				{
					this.IsTask = true;
				}
				if (this.json.HasField("isTanChaUnlock"))
				{
					this.IsTanChaUnlock = this.json["isTanChaUnlock"].b;
				}
				else
				{
					this.IsTanChaUnlock = false;
				}
				JSONObject jsonobject = this.json["equipList"];
				if (jsonobject.HasField("Weapon1"))
				{
					this.Weapon1 = jsonobject["Weapon1"];
					if (jsonobject.HasField("Weapon2"))
					{
						this.Weapon2 = jsonobject["Weapon2"];
						this.IsDoubleWeapon = true;
					}
				}
				if (jsonobject.HasField("Clothing"))
				{
					this.Clothing = jsonobject["Clothing"];
				}
				if (jsonobject.HasField("Ring"))
				{
					this.Ring = jsonobject["Ring"];
				}
				this.StaticSkills.Clear();
				foreach (JSONObject jsonobject2 in this.json["staticSkills"].list)
				{
					this.StaticSkills.Add(jsonobject2.I);
				}
				this.YuanYingStaticSkill = this.json["yuanying"].I;
				this.Skills.Clear();
				foreach (JSONObject jsonobject3 in this.json["skills"].list)
				{
					this.Skills.Add(UINPCData._SkillDict[jsonobject3.I][this.BigLevel]);
				}
				this.WuDaoSkills.Clear();
				if (!this.json["wuDaoSkillList"].IsNull)
				{
					foreach (JSONObject jsonobject4 in this.json["wuDaoSkillList"].list)
					{
						this.WuDaoSkills.Add(jsonobject4.I);
					}
				}
				this.WuDao = new List<UINPCWuDaoData>();
				foreach (JSONObject jsonobject5 in this.json["wuDaoJson"].list)
				{
					UINPCWuDaoData uinpcwuDaoData = new UINPCWuDaoData();
					uinpcwuDaoData.ID = jsonobject5["id"].I;
					uinpcwuDaoData.Level = jsonobject5["level"].I;
					uinpcwuDaoData.Exp = jsonobject5["exp"].I;
					foreach (KeyValuePair<int, UIWuDaoSkillData> keyValuePair in UINPCData._WuDaoSkillDict)
					{
						if (this.WuDaoSkills.Contains(keyValuePair.Key) && keyValuePair.Value.WuDaoType.Contains(uinpcwuDaoData.ID))
						{
							uinpcwuDaoData.SkillIDList.Add(keyValuePair.Key);
							if (keyValuePair.Value.WuDaoLv > uinpcwuDaoData.Level)
							{
								Debug.LogError(string.Format("解析NPC数据时出现问题，NPC的悟道技能超过了悟道等级，NPCID:{0}，悟道技能ID:{1}，大道类型:{2}，请使用杂项功能测试->其他->检查NPC悟道配表 来查看具体情况", this.ID, keyValuePair.Key, uinpcwuDaoData.ID));
							}
						}
					}
					this.WuDao.Add(uinpcwuDaoData);
				}
				this.WuDao.Sort();
				this.Events.Clear();
				this.ParseEvent();
				if (jsonData.instance.AvatarBackpackJsonData.HasField(this.ID.ToString()))
				{
					this.BackpackJson = jsonData.instance.AvatarBackpackJsonData[this.ID.ToString()];
					this.Inventory.Clear();
					if (this.BackpackJson["Backpack"].Count > 0)
					{
						foreach (JSONObject jsonobject6 in this.BackpackJson["Backpack"].list)
						{
							int i2 = jsonobject6["Num"].I;
							if (i2 > 0)
							{
								item item = new item(jsonobject6["ItemID"].I);
								item.UUID = jsonobject6["UUID"].str;
								if (jsonobject6.HasField("Seid"))
								{
									item.Seid = jsonobject6["Seid"];
								}
								else
								{
									item.Seid = new JSONObject(JSONObject.Type.OBJECT);
								}
								item.itemNum = i2;
								this.Inventory.Add(item);
							}
						}
					}
				}
				this.CalcDunSu();
				this.CalcShenShi();
				this.CalcHP();
			}
		}
		catch (Exception arg)
		{
			this.IsException = true;
			Debug.LogError(string.Format("获取NPC数据时出错，目标的ID:{0}，错误信息:\n{1}", this.ID, arg));
		}
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x000E0B98 File Offset: 0x000DED98
	private void CalcDunSu()
	{
		int dunSu = this.DunSu;
		int num = this.CalcStaticSkillSeidSum(8);
		int num2 = this.CalcWuDaoSkillSeidSum(8);
		int num3 = this.CalcEquipSeidSum(8);
		int dunSu2 = dunSu + num + num2 + num3;
		this.DunSu = dunSu2;
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x000E0BD4 File Offset: 0x000DEDD4
	private void CalcShenShi()
	{
		int shenShi = this.ShenShi;
		int num = this.CalcStaticSkillSeidSum(2);
		int num2 = this.CalcWuDaoSkillSeidSum(2);
		int num3 = this.CalcEquipSeidSum(4);
		int shenShi2 = shenShi + num + num2 + num3;
		this.ShenShi = shenShi2;
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x000E0C10 File Offset: 0x000DEE10
	private void CalcHP()
	{
		int hp = this.HP;
		int num = this.CalcStaticSkillSeidSum(3);
		int num2 = this.CalcWuDaoSkillSeidSum(3);
		int num3 = this.CalcEquipSeidSum(3);
		int hp2 = hp + num + num2 + num3;
		this.HP = hp2;
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x000E0C4C File Offset: 0x000DEE4C
	private int CalcStaticSkillSeidSum(int seid)
	{
		int num = 0;
		foreach (int num2 in this.StaticSkills)
		{
			if (StaticSkillJsonData.DataDict.ContainsKey(num2))
			{
				StaticSkillJsonData staticSkillJsonData = StaticSkillJsonData.DataDict[num2];
				if (staticSkillJsonData.seid.Contains(seid))
				{
					JSONObject jsonobject = jsonData.instance.StaticSkillSeidJsonData[seid][staticSkillJsonData.id.ToString()];
					if (jsonobject != null)
					{
						num += jsonobject["value1"].I;
					}
					else
					{
						Debug.LogError(string.Format("检查NPC功法特性时出错，功法特性{0}的配表中不存在功法{1}的数据", seid, num2));
					}
				}
			}
			else
			{
				Debug.LogError(string.Format("检查NPC功法特性时出错，不存在功法{0}", num2));
			}
		}
		return num;
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x000E0D34 File Offset: 0x000DEF34
	private int CalcWuDaoSkillSeidSum(int seid)
	{
		int num = 0;
		foreach (int num2 in this.WuDaoSkills)
		{
			if (WuDaoJson.DataDict.ContainsKey(num2))
			{
				WuDaoJson wuDaoJson = WuDaoJson.DataDict[num2];
				if (wuDaoJson.seid.Contains(seid))
				{
					JSONObject jsonobject = jsonData.instance.WuDaoSeidJsonData[seid][wuDaoJson.id.ToString()];
					if (jsonobject != null)
					{
						num += jsonobject["value1"].I;
					}
					else
					{
						Debug.LogError(string.Format("检查NPC悟道特性时出错，悟道特性{0}的配表中不存在悟道技能{1}的数据", seid, num2));
					}
				}
			}
			else
			{
				Debug.LogError(string.Format("检查NPC悟道特性时出错，不存在悟道技能{0}", num2));
			}
		}
		return num;
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x000E0E1C File Offset: 0x000DF01C
	private int CalcEquipSeidSum(int seid)
	{
		int num = 0;
		int i = this.json["equipClothing"].I;
		int i2 = this.json["equipRing"].I;
		if (i > 0)
		{
			if (_ItemJsonData.DataDict.ContainsKey(i))
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[i];
				if (itemJsonData.seid.Contains(seid))
				{
					JSONObject jsonobject = jsonData.instance.EquipSeidJsonData[seid][itemJsonData.id.ToString()];
					if (jsonobject != null)
					{
						num += jsonobject["value1"].I;
					}
					else
					{
						Debug.LogError(string.Format("检查NPC衣服特性时出错，装备特性{0}的配表中不存在物品{1}的数据", seid, i));
					}
				}
			}
			else
			{
				Debug.LogError(string.Format("检查NPC衣服特性时出错，不存在物品{0}", i));
			}
		}
		if (i2 > 0)
		{
			if (_ItemJsonData.DataDict.ContainsKey(i2))
			{
				_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[i2];
				if (itemJsonData2.seid.Contains(seid))
				{
					JSONObject jsonobject2 = jsonData.instance.EquipSeidJsonData[seid][itemJsonData2.id.ToString()];
					if (jsonobject2 != null)
					{
						num += jsonobject2["value1"].I;
					}
					else
					{
						Debug.LogError(string.Format("检查NPC衣服特性时出错，装备特性{0}的配表中不存在物品{1}的数据", seid, i2));
					}
				}
			}
			else
			{
				Debug.LogError(string.Format("检查NPC饰品特性时出错，不存在物品{0}", i));
			}
		}
		return num;
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x000E0F8C File Offset: 0x000DF18C
	public void RefreshOldNpcData()
	{
		UINPCData.Init();
		try
		{
			this.json = this.ID.NPCJson();
			if (jsonData.instance.AvatarRandomJsonData.HasField(this.ID.ToString()))
			{
				this.Name = jsonData.instance.AvatarRandomJsonData[this.ID.ToString()]["Name"].Str;
				this.Favor = 0;
			}
			else
			{
				this.Name = "获取失败";
				Debug.LogError(string.Format("获取NPC {0} 的名字和好感度失败，jsonData.instance.AvatarRandomJsonData中没有此ID", this.ID));
			}
			if (UINPCJiaoHu.isDebugMode)
			{
				this.Name += this.ID.ToString();
			}
			this.Sex = this.json["SexType"].I;
			this.Title = this.json["Title"].Str;
			this.Age = this.json["age"].I / 12;
			this.HP = this.json["HP"].I;
			this.FavorLevel = UINPCHeadFavor.GetFavorLevel(this.Favor);
			this.Level = this.json["Level"].I;
			this.LevelStr = UINPCData._LevelDict[this.Level];
			this.BigLevel = (this.Level - 1) / 3 + 1;
			this.FavorPer = 0f;
			this.Exp = 0;
			this.QingFen = 0;
			this.ZhuangTai = 1;
			this.ZhuangTaiStr = UINPCData._ZhuangTaiDict[this.ZhuangTai];
			this.ShouYuan = this.json["shouYuan"].I;
			this.ZiZhi = this.json["ziZhi"].I;
			this.WuXing = this.json["wuXin"].I;
			this.DunSu = this.json["dunSu"].I;
			this.ShenShi = this.json["shengShi"].I;
			JSONObject jsonobject = this.json["equipList"];
			if (this.json["equipWeapon"].I > 0)
			{
				this.Weapon1 = new JSONObject();
				this.Weapon1.SetField("ItemID", this.json["equipWeapon"].I);
			}
			if (this.json["equipClothing"].I > 0)
			{
				this.Clothing = new JSONObject();
				this.Clothing.SetField("ItemID", this.json["equipClothing"].I);
			}
			if (this.json["equipRing"].I > 0)
			{
				this.Ring = new JSONObject();
				this.Ring.SetField("ItemID", this.json["equipRing"].I);
			}
			this.StaticSkills.Clear();
			foreach (JSONObject jsonobject2 in this.json["staticSkills"].list)
			{
				this.StaticSkills.Add(jsonobject2.I);
			}
			this.YuanYingStaticSkill = this.json["yuanying"].I;
			this.Skills.Clear();
			foreach (JSONObject jsonobject3 in this.json["skills"].list)
			{
				this.Skills.Add(UINPCData._SkillDict[jsonobject3.I][this.BigLevel]);
			}
			this.CalcDunSu();
			this.CalcShenShi();
			this.CalcHP();
		}
		catch (Exception arg)
		{
			this.IsException = true;
			Debug.LogError(string.Format("获取NPC数据时出错，目标的ID:{0}，错误信息:\n{1}", this.ID, arg));
		}
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x000E1420 File Offset: 0x000DF620
	public void ParseEvent()
	{
		JSONObject jsonobject = this.json["NoteBook"];
		if (!jsonobject.IsNull)
		{
			foreach (string text in jsonobject.keys)
			{
				int.Parse(text);
				foreach (JSONObject jsonobject2 in jsonobject[text].list)
				{
					UINPCEventData uinpceventData = new UINPCEventData();
					uinpceventData.EventDesc = "";
					uinpceventData.EventTime = DateTime.Parse(jsonobject2["time"].str);
					uinpceventData.EventTimeStr = jsonobject2["time"].str;
					if (text == "33")
					{
						UINPCEventData uinpceventData2 = uinpceventData;
						uinpceventData2.EventDesc += UINPCData._EventQiYuDescDict[jsonobject2["qiYuId"].I];
					}
					else if (text == "101")
					{
						UINPCEventData uinpceventData3 = uinpceventData;
						uinpceventData3.EventDesc += UINPCData._EventGuDingDescDict[jsonobject2["gudingshijian"].I];
					}
					else
					{
						int num = int.Parse(text);
						if (UINPCData._EventDescDict.ContainsKey(num))
						{
							UINPCEventData uinpceventData4 = uinpceventData;
							uinpceventData4.EventDesc += UINPCData._EventDescDict[num];
						}
						else
						{
							Debug.LogError(string.Format("解析重要事件出错，配表中没有id为{0}重要事件", num));
						}
					}
					foreach (string text2 in jsonobject2.keys)
					{
						if (text2 == "num")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{num}", jsonobject2[text2].I.ToString());
						}
						else if (text2 == "danyao")
						{
							JSONObject jsonobject3 = jsonobject2[text2].I.ItemJson();
							string str = jsonobject3["name"].Str;
							if (str == "废丹")
							{
								uinpceventData.EventDesc = "在炼制丹药时由于药引错误，药性未能中和，仅炼制成{Num}颗{danyao}。";
								uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{danyao}", str);
								uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{Num}", jsonobject2["num"].I.ToString());
								break;
							}
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{danyao}", str);
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{pinjie}", jsonobject3["quality"].I.ToCNNumber() + "品");
						}
						else if (text2 == "jingjie")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{jingjie}", UINPCData.dajingjie[(jsonobject2[text2].I - 1) / 3] + UINPCData.xiaojingjie[(jsonobject2[text2].I - 1) % 3]);
						}
						else if (text2 == "leixing")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{leixing}", UINPCData.fabaoleixing[jsonobject2[text2].I - 1]);
						}
						else if (text2 == "fabaopinjie")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{fabaopinjie}", UINPCData.fabaopinjie[jsonobject2[text2].I - 1]);
						}
						else if (text2 == "zhuangbei")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{zhuangbei}", jsonobject2[text2].Str);
						}
						else if (text2 == "item")
						{
							string str2 = jsonobject2[text2].I.ItemJson()["name"].Str;
							if (jsonobject2.HasField("itemName") && jsonobject2["itemName"].Str != "")
							{
								str2 = jsonobject2["itemName"].Str;
							}
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{item}", str2);
						}
						else if (text2 == "npcname")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{npcname}", jsonobject2[text2].Str);
						}
						else if (text2 == "fungusshijian")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{fungusshijian}", jsonobject2[text2].Str);
						}
						else if (text2 == "cnnum")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{cnnum}", jsonobject2[text2].I.ToCNNumber());
						}
						else if (text2 == "rank")
						{
							uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{rank}", jsonobject2[text2].I.ToCNNumber());
						}
					}
					uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{FirstName}", PlayerEx.Player.firstName);
					uinpceventData.EventDesc = uinpceventData.EventDesc.Replace("{LastName}", PlayerEx.Player.lastName);
					this.Events.Add(uinpceventData);
				}
			}
			try
			{
				this.Events.Sort();
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("排序NPC重要时间时出错");
				stringBuilder.AppendLine(ex.Message);
				stringBuilder.AppendLine(ex.StackTrace);
				foreach (UINPCEventData uinpceventData5 in this.Events)
				{
					stringBuilder.AppendLine(string.Format("{0} {1}", uinpceventData5.EventTime, uinpceventData5.EventDesc));
				}
				Debug.LogError(stringBuilder.ToString());
			}
		}
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x000E1B4C File Offset: 0x000DFD4C
	public static void CheckWuDaoError()
	{
		UINPCData.Init();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (JSONObject jsonobject in jsonData.instance.NPCWuDaoJson.list)
		{
			int i = jsonobject["id"].I;
			int i2 = jsonobject["Type"].I;
			int i3 = jsonobject["lv"].I;
			List<int> list = new List<int>();
			foreach (JSONObject jsonobject2 in jsonobject["wudaoID"].list)
			{
				list.Add(jsonobject2.I);
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int j = 1; j <= 12; j++)
			{
				int i4 = jsonobject[string.Format("value{0}", j)].I;
				if (j > 10)
				{
					dictionary.Add(j + 10, i4);
				}
				else
				{
					dictionary.Add(j, i4);
				}
			}
			bool flag = false;
			foreach (int num in list)
			{
				UIWuDaoSkillData uiwuDaoSkillData = UINPCData._WuDaoSkillDict[num];
				foreach (int num2 in uiwuDaoSkillData.WuDaoType)
				{
					if (uiwuDaoSkillData.WuDaoLv > dictionary[num2])
					{
						string text = string.Format("流水号:{0}，名字:{1}，悟道技能ID:{2}，大道类型:{3}{4}的等级{5}不够{6}，悟道类型{7}", new object[]
						{
							i,
							uiwuDaoSkillData.Name,
							num,
							UINPCData._TypeWuDaoName[num2],
							num2,
							dictionary[num2],
							uiwuDaoSkillData.WuDaoLv,
							i2
						});
						Debug.LogError(text);
						stringBuilder.AppendLine(text);
						flag = true;
					}
				}
			}
			if (flag)
			{
				stringBuilder.AppendLine();
			}
		}
		Debug.LogError(stringBuilder.ToString());
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000E1E10 File Offset: 0x000E0010
	public int CompareTo(object obj)
	{
		if (this.Level > ((UINPCData)obj).Level)
		{
			return 1;
		}
		if (this.Level != ((UINPCData)obj).Level)
		{
			return -1;
		}
		if (this.Exp > ((UINPCData)obj).Exp)
		{
			return 1;
		}
		if (this.Exp == ((UINPCData)obj).Exp)
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x04001402 RID: 5122
	private static bool isDebugMode = false;

	// Token: 0x04001403 RID: 5123
	public static Dictionary<int, UnityAction> ThreeSceneNPCTalkCache = new Dictionary<int, UnityAction>();

	// Token: 0x04001404 RID: 5124
	public static Dictionary<int, UnityAction> ThreeSceneZhongYaoNPCTalkCache = new Dictionary<int, UnityAction>();

	// Token: 0x04001405 RID: 5125
	public bool IsException;

	// Token: 0x04001406 RID: 5126
	public int ID;

	// Token: 0x04001407 RID: 5127
	public string UUID = "";

	// Token: 0x04001408 RID: 5128
	public int Tag;

	// Token: 0x04001409 RID: 5129
	public bool IsGuDingNPC;

	// Token: 0x0400140A RID: 5130
	public bool IsZhongYaoNPC;

	// Token: 0x0400140B RID: 5131
	public int ZhongYaoNPCID;

	// Token: 0x0400140C RID: 5132
	public bool IsThreeSceneNPC;

	// Token: 0x0400140D RID: 5133
	public bool IsBind;

	// Token: 0x0400140E RID: 5134
	public string Name;

	// Token: 0x0400140F RID: 5135
	public int Sex;

	// Token: 0x04001410 RID: 5136
	public string Title;

	// Token: 0x04001411 RID: 5137
	public int Age;

	// Token: 0x04001412 RID: 5138
	public int HP;

	// Token: 0x04001413 RID: 5139
	public int QingFen;

	// Token: 0x04001414 RID: 5140
	public int Exp;

	// Token: 0x04001415 RID: 5141
	public int Level;

	// Token: 0x04001416 RID: 5142
	public int BigLevel;

	// Token: 0x04001417 RID: 5143
	public string LevelStr;

	// Token: 0x04001418 RID: 5144
	public int ZhuangTai;

	// Token: 0x04001419 RID: 5145
	public string ZhuangTaiStr;

	// Token: 0x0400141A RID: 5146
	public int ShouYuan;

	// Token: 0x0400141B RID: 5147
	public int ZiZhi;

	// Token: 0x0400141C RID: 5148
	public int WuXing;

	// Token: 0x0400141D RID: 5149
	public int DunSu;

	// Token: 0x0400141E RID: 5150
	public int ShenShi;

	// Token: 0x0400141F RID: 5151
	public int Favor;

	// Token: 0x04001420 RID: 5152
	public int FavorLevel;

	// Token: 0x04001421 RID: 5153
	public float FavorPer;

	// Token: 0x04001422 RID: 5154
	public bool IsNingZhouNPC;

	// Token: 0x04001423 RID: 5155
	public bool IsKnowPlayer;

	// Token: 0x04001424 RID: 5156
	public int XingGe;

	// Token: 0x04001425 RID: 5157
	public int LiuPai;

	// Token: 0x04001426 RID: 5158
	public int MenPai;

	// Token: 0x04001427 RID: 5159
	public int ActionID;

	// Token: 0x04001428 RID: 5160
	public bool IsTanChaUnlock;

	// Token: 0x04001429 RID: 5161
	public bool IsNeedHelp;

	// Token: 0x0400142A RID: 5162
	public bool IsTask;

	// Token: 0x0400142B RID: 5163
	public bool IsSeaNPC;

	// Token: 0x0400142C RID: 5164
	public bool IsTag;

	// Token: 0x0400142D RID: 5165
	public int SeaEventID;

	// Token: 0x0400142E RID: 5166
	public int NPCType;

	// Token: 0x0400142F RID: 5167
	public bool ZhengXie;

	// Token: 0x04001430 RID: 5168
	public int Face;

	// Token: 0x04001431 RID: 5169
	public List<UINPCEventData> Events = new List<UINPCEventData>();

	// Token: 0x04001432 RID: 5170
	public JSONObject json;

	// Token: 0x04001433 RID: 5171
	public JSONObject Weapon1;

	// Token: 0x04001434 RID: 5172
	public JSONObject Weapon2;

	// Token: 0x04001435 RID: 5173
	public JSONObject Clothing;

	// Token: 0x04001436 RID: 5174
	public JSONObject Ring;

	// Token: 0x04001437 RID: 5175
	public bool IsDoubleWeapon;

	// Token: 0x04001438 RID: 5176
	public List<int> StaticSkills = new List<int>();

	// Token: 0x04001439 RID: 5177
	public int YuanYingStaticSkill;

	// Token: 0x0400143A RID: 5178
	public List<int> Skills = new List<int>();

	// Token: 0x0400143B RID: 5179
	public List<UINPCWuDaoData> WuDao = new List<UINPCWuDaoData>();

	// Token: 0x0400143C RID: 5180
	public List<int> WuDaoSkills = new List<int>();

	// Token: 0x0400143D RID: 5181
	public bool IsFight;

	// Token: 0x0400143E RID: 5182
	public JSONObject BackpackJson;

	// Token: 0x0400143F RID: 5183
	public List<item> Inventory = new List<item>();

	// Token: 0x04001440 RID: 5184
	private static Dictionary<int, string> _LevelDict = new Dictionary<int, string>();

	// Token: 0x04001441 RID: 5185
	private static Dictionary<int, string> _ZhuangTaiDict = new Dictionary<int, string>();

	// Token: 0x04001442 RID: 5186
	private static Dictionary<int, Dictionary<int, int>> _SkillDict = new Dictionary<int, Dictionary<int, int>>();

	// Token: 0x04001443 RID: 5187
	public static Dictionary<int, UIWuDaoSkillData> _WuDaoSkillDict = new Dictionary<int, UIWuDaoSkillData>();

	// Token: 0x04001444 RID: 5188
	private static Dictionary<int, List<int>> _WuDaoSkillTypeDict = new Dictionary<int, List<int>>();

	// Token: 0x04001445 RID: 5189
	private static Dictionary<int, string> _EventDescDict = new Dictionary<int, string>();

	// Token: 0x04001446 RID: 5190
	private static Dictionary<int, string> _EventGuDingDescDict = new Dictionary<int, string>();

	// Token: 0x04001447 RID: 5191
	private static Dictionary<int, int> _EventTypeDict = new Dictionary<int, int>();

	// Token: 0x04001448 RID: 5192
	private static Dictionary<int, string> _EventQiYuDescDict = new Dictionary<int, string>();

	// Token: 0x04001449 RID: 5193
	private static Dictionary<int, bool> _ActionTaskDict = new Dictionary<int, bool>();

	// Token: 0x0400144A RID: 5194
	private static bool _Inited;

	// Token: 0x0400144B RID: 5195
	private static string[] fabaoleixing = new string[]
	{
		"武器",
		"防具",
		"饰品"
	};

	// Token: 0x0400144C RID: 5196
	private static string[] fabaopinjie = new string[]
	{
		"符器",
		"法器",
		"法宝",
		"纯阳法宝",
		"通天灵宝"
	};

	// Token: 0x0400144D RID: 5197
	private static string[] dajingjie = new string[]
	{
		"炼气",
		"筑基",
		"金丹",
		"元婴",
		"化神"
	};

	// Token: 0x0400144E RID: 5198
	private static string[] xiaojingjie = new string[]
	{
		"初期",
		"中期",
		"后期"
	};

	// Token: 0x0400144F RID: 5199
	private static Dictionary<int, string> _TypeWuDaoName = new Dictionary<int, string>
	{
		{
			1,
			"金"
		},
		{
			2,
			"木"
		},
		{
			3,
			"水"
		},
		{
			4,
			"火"
		},
		{
			5,
			"土"
		},
		{
			6,
			"神"
		},
		{
			7,
			"体"
		},
		{
			8,
			"剑"
		},
		{
			9,
			"气"
		},
		{
			10,
			"阵"
		},
		{
			21,
			"丹"
		},
		{
			22,
			"器"
		}
	};
}
