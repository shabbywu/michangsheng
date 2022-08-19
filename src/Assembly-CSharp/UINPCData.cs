using System;
using System.Collections.Generic;
using System.Text;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame.TianJiDaBi;

// Token: 0x0200026A RID: 618
public class UINPCData : IComparable
{
	// Token: 0x06001680 RID: 5760 RVA: 0x00097924 File Offset: 0x00095B24
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

	// Token: 0x06001681 RID: 5761 RVA: 0x00097EB8 File Offset: 0x000960B8
	public UINPCData(int id, bool isThreeSceneNPC = false)
	{
		this.ID = id;
		this.IsThreeSceneNPC = isThreeSceneNPC;
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x00097F3C File Offset: 0x0009613C
	public void SetID(int id)
	{
		this.ID = id;
		this.RefreshData();
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x00097F4C File Offset: 0x0009614C
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
				if (this.json.HasField("ExQingJiaoStaticSkills"))
				{
					using (List<int>.Enumerator enumerator2 = this.json["ExQingJiaoStaticSkills"].ToList().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int staticSkillID = enumerator2.Current;
							UINPCQingJiaoSkillData.SData sdata = UINPCQingJiaoSkillData.StaticSkillList.Find((UINPCQingJiaoSkillData.SData s) => s.ID == staticSkillID);
							if (sdata != null)
							{
								this.ExQingJiaoStaticSkills.Add(sdata);
							}
						}
					}
				}
				if (this.json.HasField("ExQingJiaoSkills"))
				{
					using (List<int>.Enumerator enumerator2 = this.json["ExQingJiaoSkills"].ToList().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int skillID = enumerator2.Current;
							UINPCQingJiaoSkillData.SData sdata2 = UINPCQingJiaoSkillData.StaticSkillList.Find((UINPCQingJiaoSkillData.SData s) => s.SkillID == skillID && s.Quality == this.BigLevel);
							if (sdata2 != null)
							{
								this.ExQingJiaoSkills.Add(sdata2);
							}
						}
					}
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

	// Token: 0x06001684 RID: 5764 RVA: 0x00098BF0 File Offset: 0x00096DF0
	private void CalcDunSu()
	{
		int dunSu = this.DunSu;
		int num = this.CalcStaticSkillSeidSum(8);
		int num2 = this.CalcWuDaoSkillSeidSum(8);
		int num3 = this.CalcEquipSeidSum(8);
		int dunSu2 = dunSu + num + num2 + num3;
		this.DunSu = dunSu2;
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x00098C2C File Offset: 0x00096E2C
	private void CalcShenShi()
	{
		int shenShi = this.ShenShi;
		int num = this.CalcStaticSkillSeidSum(2);
		int num2 = this.CalcWuDaoSkillSeidSum(2);
		int num3 = this.CalcEquipSeidSum(4);
		int shenShi2 = shenShi + num + num2 + num3;
		this.ShenShi = shenShi2;
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x00098C68 File Offset: 0x00096E68
	private void CalcHP()
	{
		int hp = this.HP;
		int num = this.CalcStaticSkillSeidSum(3);
		int num2 = this.CalcWuDaoSkillSeidSum(3);
		int num3 = this.CalcEquipSeidSum(3);
		int hp2 = hp + num + num2 + num3;
		this.HP = hp2;
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x00098CA4 File Offset: 0x00096EA4
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

	// Token: 0x06001688 RID: 5768 RVA: 0x00098D8C File Offset: 0x00096F8C
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

	// Token: 0x06001689 RID: 5769 RVA: 0x00098E74 File Offset: 0x00097074
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

	// Token: 0x0600168A RID: 5770 RVA: 0x00098FE4 File Offset: 0x000971E4
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

	// Token: 0x0600168B RID: 5771 RVA: 0x00099478 File Offset: 0x00097678
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

	// Token: 0x0600168C RID: 5772 RVA: 0x00099BA4 File Offset: 0x00097DA4
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

	// Token: 0x0600168D RID: 5773 RVA: 0x00099E68 File Offset: 0x00098068
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

	// Token: 0x040010B0 RID: 4272
	private static bool isDebugMode = false;

	// Token: 0x040010B1 RID: 4273
	public static Dictionary<int, UnityAction> ThreeSceneNPCTalkCache = new Dictionary<int, UnityAction>();

	// Token: 0x040010B2 RID: 4274
	public static Dictionary<int, UnityAction> ThreeSceneZhongYaoNPCTalkCache = new Dictionary<int, UnityAction>();

	// Token: 0x040010B3 RID: 4275
	public bool IsException;

	// Token: 0x040010B4 RID: 4276
	public int ID;

	// Token: 0x040010B5 RID: 4277
	public string UUID = "";

	// Token: 0x040010B6 RID: 4278
	public int Tag;

	// Token: 0x040010B7 RID: 4279
	public bool IsGuDingNPC;

	// Token: 0x040010B8 RID: 4280
	public bool IsZhongYaoNPC;

	// Token: 0x040010B9 RID: 4281
	public int ZhongYaoNPCID;

	// Token: 0x040010BA RID: 4282
	public bool IsThreeSceneNPC;

	// Token: 0x040010BB RID: 4283
	public bool IsBind;

	// Token: 0x040010BC RID: 4284
	public string Name;

	// Token: 0x040010BD RID: 4285
	public int Sex;

	// Token: 0x040010BE RID: 4286
	public string Title;

	// Token: 0x040010BF RID: 4287
	public int Age;

	// Token: 0x040010C0 RID: 4288
	public int HP;

	// Token: 0x040010C1 RID: 4289
	public int QingFen;

	// Token: 0x040010C2 RID: 4290
	public int Exp;

	// Token: 0x040010C3 RID: 4291
	public int Level;

	// Token: 0x040010C4 RID: 4292
	public int BigLevel;

	// Token: 0x040010C5 RID: 4293
	public string LevelStr;

	// Token: 0x040010C6 RID: 4294
	public int ZhuangTai;

	// Token: 0x040010C7 RID: 4295
	public string ZhuangTaiStr;

	// Token: 0x040010C8 RID: 4296
	public int ShouYuan;

	// Token: 0x040010C9 RID: 4297
	public int ZiZhi;

	// Token: 0x040010CA RID: 4298
	public int WuXing;

	// Token: 0x040010CB RID: 4299
	public int DunSu;

	// Token: 0x040010CC RID: 4300
	public int ShenShi;

	// Token: 0x040010CD RID: 4301
	public int Favor;

	// Token: 0x040010CE RID: 4302
	public int FavorLevel;

	// Token: 0x040010CF RID: 4303
	public float FavorPer;

	// Token: 0x040010D0 RID: 4304
	public bool IsNingZhouNPC;

	// Token: 0x040010D1 RID: 4305
	public bool IsKnowPlayer;

	// Token: 0x040010D2 RID: 4306
	public int XingGe;

	// Token: 0x040010D3 RID: 4307
	public int LiuPai;

	// Token: 0x040010D4 RID: 4308
	public int MenPai;

	// Token: 0x040010D5 RID: 4309
	public int ActionID;

	// Token: 0x040010D6 RID: 4310
	public bool IsTanChaUnlock;

	// Token: 0x040010D7 RID: 4311
	public bool IsNeedHelp;

	// Token: 0x040010D8 RID: 4312
	public bool IsTask;

	// Token: 0x040010D9 RID: 4313
	public bool IsSeaNPC;

	// Token: 0x040010DA RID: 4314
	public bool IsTag;

	// Token: 0x040010DB RID: 4315
	public int SeaEventID;

	// Token: 0x040010DC RID: 4316
	public int NPCType;

	// Token: 0x040010DD RID: 4317
	public bool ZhengXie;

	// Token: 0x040010DE RID: 4318
	public int Face;

	// Token: 0x040010DF RID: 4319
	public List<UINPCEventData> Events = new List<UINPCEventData>();

	// Token: 0x040010E0 RID: 4320
	public JSONObject json;

	// Token: 0x040010E1 RID: 4321
	public JSONObject Weapon1;

	// Token: 0x040010E2 RID: 4322
	public JSONObject Weapon2;

	// Token: 0x040010E3 RID: 4323
	public JSONObject Clothing;

	// Token: 0x040010E4 RID: 4324
	public JSONObject Ring;

	// Token: 0x040010E5 RID: 4325
	public bool IsDoubleWeapon;

	// Token: 0x040010E6 RID: 4326
	public List<int> StaticSkills = new List<int>();

	// Token: 0x040010E7 RID: 4327
	public int YuanYingStaticSkill;

	// Token: 0x040010E8 RID: 4328
	public List<int> Skills = new List<int>();

	// Token: 0x040010E9 RID: 4329
	public List<UINPCWuDaoData> WuDao = new List<UINPCWuDaoData>();

	// Token: 0x040010EA RID: 4330
	public List<int> WuDaoSkills = new List<int>();

	// Token: 0x040010EB RID: 4331
	public bool IsFight;

	// Token: 0x040010EC RID: 4332
	public JSONObject BackpackJson;

	// Token: 0x040010ED RID: 4333
	public List<item> Inventory = new List<item>();

	// Token: 0x040010EE RID: 4334
	public List<UINPCQingJiaoSkillData.SData> ExQingJiaoSkills = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x040010EF RID: 4335
	public List<UINPCQingJiaoSkillData.SData> ExQingJiaoStaticSkills = new List<UINPCQingJiaoSkillData.SData>();

	// Token: 0x040010F0 RID: 4336
	private static Dictionary<int, string> _LevelDict = new Dictionary<int, string>();

	// Token: 0x040010F1 RID: 4337
	private static Dictionary<int, string> _ZhuangTaiDict = new Dictionary<int, string>();

	// Token: 0x040010F2 RID: 4338
	private static Dictionary<int, Dictionary<int, int>> _SkillDict = new Dictionary<int, Dictionary<int, int>>();

	// Token: 0x040010F3 RID: 4339
	public static Dictionary<int, UIWuDaoSkillData> _WuDaoSkillDict = new Dictionary<int, UIWuDaoSkillData>();

	// Token: 0x040010F4 RID: 4340
	private static Dictionary<int, List<int>> _WuDaoSkillTypeDict = new Dictionary<int, List<int>>();

	// Token: 0x040010F5 RID: 4341
	private static Dictionary<int, string> _EventDescDict = new Dictionary<int, string>();

	// Token: 0x040010F6 RID: 4342
	private static Dictionary<int, string> _EventGuDingDescDict = new Dictionary<int, string>();

	// Token: 0x040010F7 RID: 4343
	private static Dictionary<int, int> _EventTypeDict = new Dictionary<int, int>();

	// Token: 0x040010F8 RID: 4344
	private static Dictionary<int, string> _EventQiYuDescDict = new Dictionary<int, string>();

	// Token: 0x040010F9 RID: 4345
	private static Dictionary<int, bool> _ActionTaskDict = new Dictionary<int, bool>();

	// Token: 0x040010FA RID: 4346
	private static bool _Inited;

	// Token: 0x040010FB RID: 4347
	private static string[] fabaoleixing = new string[]
	{
		"武器",
		"防具",
		"饰品"
	};

	// Token: 0x040010FC RID: 4348
	private static string[] fabaopinjie = new string[]
	{
		"符器",
		"法器",
		"法宝",
		"纯阳法宝",
		"通天灵宝"
	};

	// Token: 0x040010FD RID: 4349
	private static string[] dajingjie = new string[]
	{
		"炼气",
		"筑基",
		"金丹",
		"元婴",
		"化神"
	};

	// Token: 0x040010FE RID: 4350
	private static string[] xiaojingjie = new string[]
	{
		"初期",
		"中期",
		"后期"
	};

	// Token: 0x040010FF RID: 4351
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
