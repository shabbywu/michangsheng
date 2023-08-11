using System;
using System.Collections.Generic;
using System.Text;
using GUIPackage;
using JSONClass;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using YSGame.TianJiDaBi;

public class UINPCData : IComparable
{
	private static bool isDebugMode = false;

	public static Dictionary<int, UnityAction> ThreeSceneNPCTalkCache = new Dictionary<int, UnityAction>();

	public static Dictionary<int, UnityAction> ThreeSceneZhongYaoNPCTalkCache = new Dictionary<int, UnityAction>();

	public bool IsException;

	public int ID;

	public string UUID = "";

	public int Tag;

	public bool IsGuDingNPC;

	public bool IsZhongYaoNPC;

	public int ZhongYaoNPCID;

	public bool IsThreeSceneNPC;

	public bool IsBind;

	public string Name;

	public int Sex;

	public string Title;

	public int Age;

	public int HP;

	public int QingFen;

	public int Exp;

	public int Level;

	public int BigLevel;

	public string LevelStr;

	public int ZhuangTai;

	public string ZhuangTaiStr;

	public int ShouYuan;

	public int ZiZhi;

	public int WuXing;

	public int DunSu;

	public int ShenShi;

	public int Favor;

	public int FavorLevel;

	public float FavorPer;

	public bool IsNingZhouNPC;

	public bool IsKnowPlayer;

	public int XingGe;

	public int LiuPai;

	public int MenPai;

	public int ActionID;

	public bool IsTanChaUnlock;

	public bool IsNeedHelp;

	public bool IsTask;

	public bool IsSeaNPC;

	public bool IsTag;

	public int SeaEventID;

	public int NPCType;

	public bool ZhengXie;

	public int Face;

	public List<UINPCEventData> Events = new List<UINPCEventData>();

	public JSONObject json;

	public JSONObject Weapon1;

	public JSONObject Weapon2;

	public JSONObject Clothing;

	public JSONObject Ring;

	public bool IsDoubleWeapon;

	public List<int> StaticSkills = new List<int>();

	public int YuanYingStaticSkill;

	public List<int> Skills = new List<int>();

	public List<UINPCWuDaoData> WuDao = new List<UINPCWuDaoData>();

	public List<int> WuDaoSkills = new List<int>();

	public bool IsFight;

	public JSONObject BackpackJson;

	public List<item> Inventory = new List<item>();

	public List<UINPCQingJiaoSkillData.SData> ExQingJiaoSkills = new List<UINPCQingJiaoSkillData.SData>();

	public List<UINPCQingJiaoSkillData.SData> ExQingJiaoStaticSkills = new List<UINPCQingJiaoSkillData.SData>();

	private static Dictionary<int, string> _LevelDict = new Dictionary<int, string>();

	private static Dictionary<int, string> _ZhuangTaiDict = new Dictionary<int, string>();

	private static Dictionary<int, Dictionary<int, int>> _SkillDict = new Dictionary<int, Dictionary<int, int>>();

	public static Dictionary<int, UIWuDaoSkillData> _WuDaoSkillDict = new Dictionary<int, UIWuDaoSkillData>();

	private static Dictionary<int, List<int>> _WuDaoSkillTypeDict = new Dictionary<int, List<int>>();

	private static Dictionary<int, string> _EventDescDict = new Dictionary<int, string>();

	private static Dictionary<int, string> _EventGuDingDescDict = new Dictionary<int, string>();

	private static Dictionary<int, int> _EventTypeDict = new Dictionary<int, int>();

	private static Dictionary<int, string> _EventQiYuDescDict = new Dictionary<int, string>();

	private static Dictionary<int, bool> _ActionTaskDict = new Dictionary<int, bool>();

	private static bool _Inited;

	private static string[] fabaoleixing = new string[3] { "武器", "防具", "饰品" };

	private static string[] fabaopinjie = new string[5] { "符器", "法器", "法宝", "纯阳法宝", "通天灵宝" };

	private static string[] dajingjie = new string[5] { "炼气", "筑基", "金丹", "元婴", "化神" };

	private static string[] xiaojingjie = new string[3] { "初期", "中期", "后期" };

	private static Dictionary<int, string> _TypeWuDaoName = new Dictionary<int, string>
	{
		{ 1, "金" },
		{ 2, "木" },
		{ 3, "水" },
		{ 4, "火" },
		{ 5, "土" },
		{ 6, "神" },
		{ 7, "体" },
		{ 8, "剑" },
		{ 9, "气" },
		{ 10, "阵" },
		{ 21, "丹" },
		{ 22, "器" }
	};

	private static void Init()
	{
		if (_Inited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.LevelUpDataJsonData.list)
		{
			_LevelDict.Add(item["id"].I, item["Name"].Str);
		}
		foreach (JSONObject item2 in jsonData.instance.NpcStatusDate.list)
		{
			_ZhuangTaiDict.Add(item2["id"].I, item2["ZhuangTaiInfo"].Str);
		}
		foreach (JSONObject item3 in jsonData.instance._skillJsonData.list)
		{
			int i = item3["Skill_ID"].I;
			int i2 = item3["Skill_Lv"].I;
			int i3 = item3["id"].I;
			if (_SkillDict.ContainsKey(i))
			{
				if (!ToolsEx.TryAdd(_SkillDict[i], i2, i3))
				{
					Debug.LogError((object)$"为NPC数据初始化技能数据时出错，在技能表，流水号{i3}，错误技能ID:{i}");
				}
			}
			else
			{
				ToolsEx.TryAdd(_SkillDict, i, new Dictionary<int, int> { { i2, i3 } });
			}
		}
		foreach (JSONObject item4 in jsonData.instance.WuDaoJson.list)
		{
			UIWuDaoSkillData uIWuDaoSkillData = new UIWuDaoSkillData();
			uIWuDaoSkillData.ID = item4["id"].I;
			uIWuDaoSkillData.Name = item4["name"].Str;
			uIWuDaoSkillData.WuDaoLv = item4["Lv"].I;
			_WuDaoSkillTypeDict.Add(uIWuDaoSkillData.ID, new List<int>());
			foreach (JSONObject item5 in item4["Type"].list)
			{
				uIWuDaoSkillData.WuDaoType.Add(item5.I);
				_WuDaoSkillTypeDict[uIWuDaoSkillData.ID].Add(item5.I);
			}
			uIWuDaoSkillData.Desc = item4["xiaoguo"].Str;
			_WuDaoSkillDict.Add(uIWuDaoSkillData.ID, uIWuDaoSkillData);
		}
		foreach (JSONObject item6 in jsonData.instance.NpcShiJianData.list)
		{
			_EventDescDict.Add(item6["id"].I, item6["ShiJianInfo"].Str);
			_EventTypeDict.Add(item6["id"].I, item6["ShiJianType"].I);
		}
		foreach (JSONObject item7 in jsonData.instance.NpcQiYuDate.list)
		{
			_EventQiYuDescDict.Add(item7["id"].I, item7["QiYuInfo"].Str);
		}
		foreach (JSONObject item8 in jsonData.instance.NpcImprotantEventData.list)
		{
			_EventGuDingDescDict.Add(item8["id"].I, item8["ShiJianInfo"].Str);
		}
		foreach (JSONObject item9 in jsonData.instance.NPCActionDate.list)
		{
			_ActionTaskDict.Add(item9["id"].I, item9["IsTask"].I == 1);
		}
		_Inited = true;
	}

	public UINPCData(int id, bool isThreeSceneNPC = false)
	{
		ID = id;
		IsThreeSceneNPC = isThreeSceneNPC;
	}

	public void SetID(int id)
	{
		ID = id;
		RefreshData();
	}

	public void RefreshData()
	{
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		Init();
		try
		{
			json = ID.NPCJson();
			if (jsonData.instance.AvatarRandomJsonData.HasField(ID.ToString()))
			{
				Name = jsonData.instance.AvatarRandomJsonData[ID.ToString()]["Name"].Str;
				Favor = jsonData.instance.AvatarRandomJsonData[ID.ToString()]["HaoGanDu"].I;
				if (UINPCJiaoHu.isDebugMode)
				{
					Name += ID;
				}
				Sex = json["SexType"].I;
				Title = json["Title"].Str;
				Age = json["age"].I / 12;
				HP = json["HP"].I;
				if (jsonData.instance.AvatarJsonData.HasField(ID.ToString()))
				{
					Face = jsonData.instance.AvatarJsonData[ID.ToString()]["face"].I;
				}
				IsTag = json.TryGetField("IsTag").b;
				if (isDebugMode)
				{
					Favor = Random.Range(-100, 300);
				}
				FavorLevel = UINPCHeadFavor.GetFavorLevel(Favor);
				Level = json["Level"].I;
				LevelStr = _LevelDict[Level];
				BigLevel = (Level - 1) / 3 + 1;
				int num = NPCEx.CalcZengLiX(this);
				int i = PlayerEx.Player.ZengLi.TryGetField("DuoYuQingFen").TryGetField(ID.ToString()).I;
				FavorPer = (float)i / (float)num;
				if (!json.HasField("isImportant"))
				{
					return;
				}
				if (json["isImportant"].b)
				{
					IsGuDingNPC = true;
				}
				IsZhongYaoNPC = NPCEx.IsZhongYaoNPC(ID, out ZhongYaoNPCID);
				_ = IsZhongYaoNPC;
				NPCType = json["Type"].I;
				Tag = json["NPCTag"].I;
				ZhengXie = jsonData.instance.NPCTagDate[Tag.ToString()]["zhengxie"].I == 1;
				Exp = json["exp"].I;
				QingFen = json["QingFen"].I;
				ZhuangTai = json["Status"]["StatusId"].I;
				ZhuangTaiStr = _ZhuangTaiDict[ZhuangTai];
				ShouYuan = json["shouYuan"].I;
				ZiZhi = json["ziZhi"].I;
				WuXing = json["wuXin"].I;
				DunSu = json["dunSu"].I;
				ShenShi = json["shengShi"].I;
				IsKnowPlayer = json["IsKnowPlayer"].b;
				if (json["paimaifenzu"].list.Count == 2 || json["paimaifenzu"].list[0].I == 2)
				{
					IsNingZhouNPC = false;
				}
				else
				{
					IsNingZhouNPC = true;
				}
				XingGe = json["XingGe"].I;
				LiuPai = json["LiuPai"].I;
				MenPai = json["MenPai"].I;
				ActionID = json["ActionId"].I;
				IsNeedHelp = json["IsNeedHelp"].b;
				IsTask = false;
				if (IsNeedHelp && !TianJiDaBiManager.IsOnSim)
				{
					Scene activeScene = SceneManager.GetActiveScene();
					if (!((Scene)(ref activeScene)).name.StartsWith("Sea") && FavorLevel >= 3 && _ActionTaskDict.ContainsKey(ActionID) && _ActionTaskDict[ActionID] && PlayerEx.Player.getLevelType() >= BigLevel - 1)
					{
						IsTask = true;
					}
				}
				if (json.HasField("isTanChaUnlock"))
				{
					IsTanChaUnlock = json["isTanChaUnlock"].b;
				}
				else
				{
					IsTanChaUnlock = false;
				}
				JSONObject jSONObject = json["equipList"];
				if (jSONObject.HasField("Weapon1"))
				{
					Weapon1 = jSONObject["Weapon1"];
					if (jSONObject.HasField("Weapon2"))
					{
						Weapon2 = jSONObject["Weapon2"];
						IsDoubleWeapon = true;
					}
				}
				if (jSONObject.HasField("Clothing"))
				{
					Clothing = jSONObject["Clothing"];
				}
				if (jSONObject.HasField("Ring"))
				{
					Ring = jSONObject["Ring"];
				}
				StaticSkills.Clear();
				foreach (JSONObject item2 in json["staticSkills"].list)
				{
					StaticSkills.Add(item2.I);
				}
				YuanYingStaticSkill = json["yuanying"].I;
				Skills.Clear();
				foreach (JSONObject item3 in json["skills"].list)
				{
					Skills.Add(_SkillDict[item3.I][BigLevel]);
				}
				if (json.HasField("ExQingJiaoStaticSkills"))
				{
					foreach (int staticSkillID in json["ExQingJiaoStaticSkills"].ToList())
					{
						UINPCQingJiaoSkillData.SData sData = UINPCQingJiaoSkillData.StaticSkillList.Find((UINPCQingJiaoSkillData.SData s) => s.ID == staticSkillID);
						if (sData != null)
						{
							ExQingJiaoStaticSkills.Add(sData);
						}
					}
				}
				if (json.HasField("ExQingJiaoSkills"))
				{
					foreach (int skillID in json["ExQingJiaoSkills"].ToList())
					{
						UINPCQingJiaoSkillData.SData sData2 = UINPCQingJiaoSkillData.StaticSkillList.Find((UINPCQingJiaoSkillData.SData s) => s.SkillID == skillID && s.Quality == BigLevel);
						if (sData2 != null)
						{
							ExQingJiaoSkills.Add(sData2);
						}
					}
				}
				WuDaoSkills.Clear();
				if (!json["wuDaoSkillList"].IsNull)
				{
					foreach (JSONObject item4 in json["wuDaoSkillList"].list)
					{
						WuDaoSkills.Add(item4.I);
					}
				}
				WuDao = new List<UINPCWuDaoData>();
				foreach (JSONObject item5 in json["wuDaoJson"].list)
				{
					UINPCWuDaoData uINPCWuDaoData = new UINPCWuDaoData();
					uINPCWuDaoData.ID = item5["id"].I;
					uINPCWuDaoData.Level = item5["level"].I;
					uINPCWuDaoData.Exp = item5["exp"].I;
					foreach (KeyValuePair<int, UIWuDaoSkillData> item6 in _WuDaoSkillDict)
					{
						if (WuDaoSkills.Contains(item6.Key) && item6.Value.WuDaoType.Contains(uINPCWuDaoData.ID))
						{
							uINPCWuDaoData.SkillIDList.Add(item6.Key);
							if (item6.Value.WuDaoLv > uINPCWuDaoData.Level)
							{
								Debug.LogError((object)$"解析NPC数据时出现问题，NPC的悟道技能超过了悟道等级，NPCID:{ID}，悟道技能ID:{item6.Key}，大道类型:{uINPCWuDaoData.ID}，请使用杂项功能测试->其他->检查NPC悟道配表 来查看具体情况");
							}
						}
					}
					WuDao.Add(uINPCWuDaoData);
				}
				WuDao.Sort();
				Events.Clear();
				ParseEvent();
				if (jsonData.instance.AvatarBackpackJsonData.HasField(ID.ToString()))
				{
					BackpackJson = jsonData.instance.AvatarBackpackJsonData[ID.ToString()];
					Inventory.Clear();
					if (BackpackJson["Backpack"].Count > 0)
					{
						foreach (JSONObject item7 in BackpackJson["Backpack"].list)
						{
							int i2 = item7["Num"].I;
							if (i2 > 0)
							{
								int i3 = item7["ItemID"].I;
								item item = null;
								item = new item(i3);
								item.UUID = item7["UUID"].str;
								if (item7.HasField("Seid"))
								{
									item.Seid = item7["Seid"];
								}
								else
								{
									item.Seid = new JSONObject(JSONObject.Type.OBJECT);
								}
								item.itemNum = i2;
								Inventory.Add(item);
							}
						}
					}
				}
				CalcDunSu();
				CalcShenShi();
				CalcHP();
				return;
			}
			Name = "获取失败";
			Debug.LogError((object)$"获取NPC {ID} 的名字和好感度失败，jsonData.instance.AvatarRandomJsonData中没有此ID");
			throw new Exception("NPC获取失败");
		}
		catch (Exception arg)
		{
			IsException = true;
			Debug.LogError((object)$"获取NPC数据时出错，目标的ID:{ID}，错误信息:\n{arg}");
		}
	}

	private void CalcDunSu()
	{
		int dunSu = DunSu;
		int num = CalcStaticSkillSeidSum(8);
		int num2 = CalcWuDaoSkillSeidSum(8);
		int num3 = CalcEquipSeidSum(8);
		int dunSu2 = dunSu + num + num2 + num3;
		DunSu = dunSu2;
	}

	private void CalcShenShi()
	{
		int shenShi = ShenShi;
		int num = CalcStaticSkillSeidSum(2);
		int num2 = CalcWuDaoSkillSeidSum(2);
		int num3 = CalcEquipSeidSum(4);
		int shenShi2 = shenShi + num + num2 + num3;
		ShenShi = shenShi2;
	}

	private void CalcHP()
	{
		int hP = HP;
		int num = CalcStaticSkillSeidSum(3);
		int num2 = CalcWuDaoSkillSeidSum(3);
		int num3 = CalcEquipSeidSum(3);
		int hP2 = hP + num + num2 + num3;
		HP = hP2;
	}

	private int CalcStaticSkillSeidSum(int seid)
	{
		int num = 0;
		foreach (int staticSkill in StaticSkills)
		{
			if (StaticSkillJsonData.DataDict.ContainsKey(staticSkill))
			{
				StaticSkillJsonData staticSkillJsonData = StaticSkillJsonData.DataDict[staticSkill];
				if (staticSkillJsonData.seid.Contains(seid))
				{
					JSONObject jSONObject = jsonData.instance.StaticSkillSeidJsonData[seid][staticSkillJsonData.id.ToString()];
					if (jSONObject != null)
					{
						num += jSONObject["value1"].I;
					}
					else
					{
						Debug.LogError((object)$"检查NPC功法特性时出错，功法特性{seid}的配表中不存在功法{staticSkill}的数据");
					}
				}
			}
			else
			{
				Debug.LogError((object)$"检查NPC功法特性时出错，不存在功法{staticSkill}");
			}
		}
		return num;
	}

	private int CalcWuDaoSkillSeidSum(int seid)
	{
		int num = 0;
		foreach (int wuDaoSkill in WuDaoSkills)
		{
			if (WuDaoJson.DataDict.ContainsKey(wuDaoSkill))
			{
				WuDaoJson wuDaoJson = WuDaoJson.DataDict[wuDaoSkill];
				if (wuDaoJson.seid.Contains(seid))
				{
					JSONObject jSONObject = jsonData.instance.WuDaoSeidJsonData[seid][wuDaoJson.id.ToString()];
					if (jSONObject != null)
					{
						num += jSONObject["value1"].I;
					}
					else
					{
						Debug.LogError((object)$"检查NPC悟道特性时出错，悟道特性{seid}的配表中不存在悟道技能{wuDaoSkill}的数据");
					}
				}
			}
			else
			{
				Debug.LogError((object)$"检查NPC悟道特性时出错，不存在悟道技能{wuDaoSkill}");
			}
		}
		return num;
	}

	private int CalcEquipSeidSum(int seid)
	{
		int num = 0;
		int i = json["equipClothing"].I;
		int i2 = json["equipRing"].I;
		if (i > 0)
		{
			if (_ItemJsonData.DataDict.ContainsKey(i))
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[i];
				if (itemJsonData.seid.Contains(seid))
				{
					JSONObject jSONObject = jsonData.instance.EquipSeidJsonData[seid][itemJsonData.id.ToString()];
					if (jSONObject != null)
					{
						num += jSONObject["value1"].I;
					}
					else
					{
						Debug.LogError((object)$"检查NPC衣服特性时出错，装备特性{seid}的配表中不存在物品{i}的数据");
					}
				}
			}
			else
			{
				Debug.LogError((object)$"检查NPC衣服特性时出错，不存在物品{i}");
			}
		}
		if (i2 > 0)
		{
			if (_ItemJsonData.DataDict.ContainsKey(i2))
			{
				_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[i2];
				if (itemJsonData2.seid.Contains(seid))
				{
					JSONObject jSONObject2 = jsonData.instance.EquipSeidJsonData[seid][itemJsonData2.id.ToString()];
					if (jSONObject2 != null)
					{
						num += jSONObject2["value1"].I;
					}
					else
					{
						Debug.LogError((object)$"检查NPC衣服特性时出错，装备特性{seid}的配表中不存在物品{i2}的数据");
					}
				}
			}
			else
			{
				Debug.LogError((object)$"检查NPC饰品特性时出错，不存在物品{i}");
			}
		}
		return num;
	}

	public void RefreshOldNpcData()
	{
		Init();
		try
		{
			json = ID.NPCJson();
			if (jsonData.instance.AvatarRandomJsonData.HasField(ID.ToString()))
			{
				Name = jsonData.instance.AvatarRandomJsonData[ID.ToString()]["Name"].Str;
				Favor = 0;
			}
			else
			{
				Name = "获取失败";
				Debug.LogError((object)$"获取NPC {ID} 的名字和好感度失败，jsonData.instance.AvatarRandomJsonData中没有此ID");
			}
			if (UINPCJiaoHu.isDebugMode)
			{
				Name += ID;
			}
			Sex = json["SexType"].I;
			Title = json["Title"].Str;
			Age = json["age"].I / 12;
			HP = json["HP"].I;
			FavorLevel = UINPCHeadFavor.GetFavorLevel(Favor);
			Level = json["Level"].I;
			LevelStr = _LevelDict[Level];
			BigLevel = (Level - 1) / 3 + 1;
			FavorPer = 0f;
			Exp = 0;
			QingFen = 0;
			ZhuangTai = 1;
			ZhuangTaiStr = _ZhuangTaiDict[ZhuangTai];
			ShouYuan = json["shouYuan"].I;
			ZiZhi = json["ziZhi"].I;
			WuXing = json["wuXin"].I;
			DunSu = json["dunSu"].I;
			ShenShi = json["shengShi"].I;
			_ = json["equipList"];
			if (json["equipWeapon"].I > 0)
			{
				Weapon1 = new JSONObject();
				Weapon1.SetField("ItemID", json["equipWeapon"].I);
			}
			if (json["equipClothing"].I > 0)
			{
				Clothing = new JSONObject();
				Clothing.SetField("ItemID", json["equipClothing"].I);
			}
			if (json["equipRing"].I > 0)
			{
				Ring = new JSONObject();
				Ring.SetField("ItemID", json["equipRing"].I);
			}
			StaticSkills.Clear();
			foreach (JSONObject item in json["staticSkills"].list)
			{
				StaticSkills.Add(item.I);
			}
			YuanYingStaticSkill = json["yuanying"].I;
			Skills.Clear();
			foreach (JSONObject item2 in json["skills"].list)
			{
				Skills.Add(_SkillDict[item2.I][BigLevel]);
			}
			CalcDunSu();
			CalcShenShi();
			CalcHP();
		}
		catch (Exception arg)
		{
			IsException = true;
			Debug.LogError((object)$"获取NPC数据时出错，目标的ID:{ID}，错误信息:\n{arg}");
		}
	}

	public void ParseEvent()
	{
		JSONObject jSONObject = json["NoteBook"];
		if (jSONObject.IsNull)
		{
			return;
		}
		foreach (string key in jSONObject.keys)
		{
			int.Parse(key);
			foreach (JSONObject item in jSONObject[key].list)
			{
				UINPCEventData uINPCEventData = new UINPCEventData();
				uINPCEventData.EventDesc = "";
				uINPCEventData.EventTime = DateTime.Parse(item["time"].str);
				uINPCEventData.EventTimeStr = item["time"].str;
				if (key == "33")
				{
					uINPCEventData.EventDesc += _EventQiYuDescDict[item["qiYuId"].I];
				}
				else if (key == "101")
				{
					uINPCEventData.EventDesc += _EventGuDingDescDict[item["gudingshijian"].I];
				}
				else
				{
					int num = int.Parse(key);
					if (_EventDescDict.ContainsKey(num))
					{
						uINPCEventData.EventDesc += _EventDescDict[num];
					}
					else
					{
						Debug.LogError((object)$"解析重要事件出错，配表中没有id为{num}重要事件");
					}
				}
				foreach (string key2 in item.keys)
				{
					switch (key2)
					{
					case "num":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{num}", item[key2].I.ToString());
						break;
					case "danyao":
					{
						JSONObject jSONObject2 = item[key2].I.ItemJson();
						string str2 = jSONObject2["name"].Str;
						if (str2 == "废丹")
						{
							uINPCEventData.EventDesc = "在炼制丹药时由于药引错误，药性未能中和，仅炼制成{Num}颗{danyao}。";
							uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{danyao}", str2);
							uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{Num}", item["num"].I.ToString());
							goto end_IL_0554;
						}
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{danyao}", str2);
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{pinjie}", jSONObject2["quality"].I.ToCNNumber() + "品");
						break;
					}
					case "jingjie":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{jingjie}", dajingjie[(item[key2].I - 1) / 3] + xiaojingjie[(item[key2].I - 1) % 3]);
						break;
					case "leixing":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{leixing}", fabaoleixing[item[key2].I - 1]);
						break;
					case "fabaopinjie":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{fabaopinjie}", fabaopinjie[item[key2].I - 1]);
						break;
					case "zhuangbei":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{zhuangbei}", item[key2].Str);
						break;
					case "item":
					{
						string str = item[key2].I.ItemJson()["name"].Str;
						if (item.HasField("itemName") && item["itemName"].Str != "")
						{
							str = item["itemName"].Str;
						}
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{item}", str);
						break;
					}
					case "npcname":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{npcname}", item[key2].Str);
						break;
					case "fungusshijian":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{fungusshijian}", item[key2].Str);
						break;
					case "cnnum":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{cnnum}", item[key2].I.ToCNNumber());
						break;
					case "rank":
						uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{rank}", item[key2].I.ToCNNumber());
						break;
					}
					continue;
					end_IL_0554:
					break;
				}
				uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{FirstName}", PlayerEx.Player.firstName);
				uINPCEventData.EventDesc = uINPCEventData.EventDesc.Replace("{LastName}", PlayerEx.Player.lastName);
				Events.Add(uINPCEventData);
			}
		}
		try
		{
			Events.Sort();
		}
		catch (Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("排序NPC重要时间时出错");
			stringBuilder.AppendLine(ex.Message);
			stringBuilder.AppendLine(ex.StackTrace);
			foreach (UINPCEventData @event in Events)
			{
				stringBuilder.AppendLine($"{@event.EventTime} {@event.EventDesc}");
			}
			Debug.LogError((object)stringBuilder.ToString());
		}
	}

	public static void CheckWuDaoError()
	{
		Init();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (JSONObject item in jsonData.instance.NPCWuDaoJson.list)
		{
			int i = item["id"].I;
			int i2 = item["Type"].I;
			_ = item["lv"].I;
			List<int> list = new List<int>();
			foreach (JSONObject item2 in item["wudaoID"].list)
			{
				list.Add(item2.I);
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int j = 1; j <= 12; j++)
			{
				int i3 = item[$"value{j}"].I;
				if (j > 10)
				{
					dictionary.Add(j + 10, i3);
				}
				else
				{
					dictionary.Add(j, i3);
				}
			}
			bool flag = false;
			foreach (int item3 in list)
			{
				UIWuDaoSkillData uIWuDaoSkillData = _WuDaoSkillDict[item3];
				foreach (int item4 in uIWuDaoSkillData.WuDaoType)
				{
					if (uIWuDaoSkillData.WuDaoLv > dictionary[item4])
					{
						string text = $"流水号:{i}，名字:{uIWuDaoSkillData.Name}，悟道技能ID:{item3}，大道类型:{_TypeWuDaoName[item4]}{item4}的等级{dictionary[item4]}不够{uIWuDaoSkillData.WuDaoLv}，悟道类型{i2}";
						Debug.LogError((object)text);
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
		Debug.LogError((object)stringBuilder.ToString());
	}

	public int CompareTo(object obj)
	{
		if (Level > ((UINPCData)obj).Level)
		{
			return 1;
		}
		if (Level == ((UINPCData)obj).Level)
		{
			if (Exp > ((UINPCData)obj).Exp)
			{
				return 1;
			}
			if (Exp == ((UINPCData)obj).Exp)
			{
				return 0;
			}
			return -1;
		}
		return -1;
	}
}
