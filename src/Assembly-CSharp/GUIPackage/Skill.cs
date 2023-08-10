using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Fungus;
using JSONClass;
using KBEngine;
using UnityEngine;
using UnityEngine.Events;
using YSGame;
using YSGame.Fight;
using script.world_script;

namespace GUIPackage;

[Serializable]
public class Skill
{
	public enum ALLSkill
	{
		SKILL34 = 34,
		SKILL65 = 65,
		SKILL66 = 66,
		SKILL68 = 68,
		SKILL69 = 69,
		SKILL97 = 97
	}

	public string skill_Name;

	public int skill_ID = -1;

	public int SkillID;

	public int Skill_Lv;

	public string skill_Desc;

	public Avatar attack;

	public Avatar target;

	public int type;

	private Texture2D _skill_Icon;

	private Texture2D _SkillPingZhi;

	private Sprite _skillIconSprite;

	private Sprite _SkillPingZhiSprite;

	private Sprite _newSkillPingZhiSprite;

	public int ColorIndex;

	public int skill_level;

	public int Max_level;

	public float CoolDown;

	public float CurCD;

	public int Damage;

	public int SkillQuality;

	public string weaponuuid = "";

	public Dictionary<int, int> skillCast = new Dictionary<int, int>();

	public Dictionary<int, int> skillSameCast = new Dictionary<int, int>();

	public List<LateDamage> LateDamages = new List<LateDamage>();

	public List<int> NowSkillSeid = new List<int>();

	public Dictionary<int, JSONObject> SkillSeidList = new Dictionary<int, JSONObject>();

	public JSONObject ItemAddSeid;

	public Dictionary<int, int> nowSkillUseCard = new Dictionary<int, int>();

	public Dictionary<int, bool> nowSkillIsChuFa = new Dictionary<int, bool>();

	private static Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

	public bool IsStaticSkill;

	private bool initedImage;

	private static Dictionary<int, List<int>> seid166Dict = new Dictionary<int, List<int>>
	{
		{
			1,
			new List<int> { 1780, 1800, 1820 }
		},
		{
			2,
			new List<int> { 1950, 2000, 2050 }
		},
		{
			3,
			new List<int> { 2300, 2400, 2500 }
		},
		{
			4,
			new List<int> { 2800, 3000, 3200 }
		},
		{
			5,
			new List<int> { 3500, 4000, 4500 }
		}
	};

	private static List<int> seid166BanList = new List<int> { 1, 117, 218, 304, 314 };

	public Texture2D skill_Icon
	{
		get
		{
			InitImage();
			return _skill_Icon;
		}
		set
		{
			_skill_Icon = value;
		}
	}

	public Texture2D SkillPingZhi
	{
		get
		{
			InitImage();
			return _SkillPingZhi;
		}
		set
		{
			_SkillPingZhi = value;
		}
	}

	public Sprite skillIconSprite
	{
		get
		{
			InitImage();
			return _skillIconSprite;
		}
		set
		{
			_skillIconSprite = value;
		}
	}

	public Sprite SkillPingZhiSprite
	{
		get
		{
			InitImage();
			return _SkillPingZhiSprite;
		}
		set
		{
			_SkillPingZhiSprite = value;
		}
	}

	public Sprite newSkillPingZhiSprite
	{
		get
		{
			InitImage();
			return _newSkillPingZhiSprite;
		}
		set
		{
			_newSkillPingZhiSprite = value;
		}
	}

	private static void InitMethod(string methodName)
	{
		if (!methodDict.ContainsKey(methodName))
		{
			MethodInfo method = typeof(Skill).GetMethod(methodName);
			methodDict.Add(methodName, method);
		}
	}

	public static bool IsSkillType(int skillID, int SkillType)
	{
		bool result = false;
		foreach (JSONObject item in jsonData.instance.skillJsonData[string.Concat(skillID)]["AttackType"].list)
		{
			if (SkillType == (int)item.n)
			{
				result = true;
			}
		}
		return result;
	}

	public Dictionary<int, int> getSkillCast(Avatar _attaker)
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>(skillCast);
		foreach (int item in _skillJsonData.DataDict[skill_ID].seid)
		{
			if (item == 97)
			{
				List<int> list = _attaker.fightTemp.NowRoundUsedSkills.FindAll((int aaa) => aaa == skill_ID);
				int key = (int)getSeidJson(97)["value1"].n;
				if (dictionary.ContainsKey(key))
				{
					dictionary[key] += list.Count;
				}
				else if (list.Count > 0)
				{
					dictionary[key] = list.Count;
				}
			}
		}
		List<List<int>> buffBySeid = _attaker.buffmag.getBuffBySeid(140);
		if (buffBySeid.Count > 0)
		{
			foreach (List<int> item2 in buffBySeid)
			{
				if (skillCast.Count == 0)
				{
					break;
				}
				int key2 = item2[2];
				List<int> list2 = new List<int>();
				SetSkillFlag(list2, 0, skill_ID);
				if (!jsonData.instance.Buff[key2].CanRealized(_attaker, list2, item2))
				{
					continue;
				}
				BuffSeidJsonData140 buffSeidJsonData = BuffSeidJsonData140.DataDict[key2];
				if (IsSkillType(skill_ID, buffSeidJsonData.value1))
				{
					dictionary[buffSeidJsonData.value3] += buffSeidJsonData.value2;
					if (dictionary[buffSeidJsonData.value3] < 0)
					{
						dictionary[buffSeidJsonData.value3] = 0;
					}
				}
			}
		}
		List<List<int>> buffBySeid2 = _attaker.buffmag.getBuffBySeid(144);
		if (buffBySeid2.Count > 0)
		{
			foreach (List<int> item3 in buffBySeid2)
			{
				int key3 = item3[2];
				List<int> list3 = new List<int>();
				SetSkillFlag(list3, 0, skill_ID);
				if (!jsonData.instance.Buff[key3].CanRealized(_attaker, list3, item3))
				{
					continue;
				}
				BuffSeidJsonData144 buffSeidJsonData2 = BuffSeidJsonData144.DataDict[key3];
				if (_skillJsonData.DataDict[skill_ID].Skill_ID == buffSeidJsonData2.value1)
				{
					dictionary[buffSeidJsonData2.value3] += buffSeidJsonData2.value2;
					if (dictionary[buffSeidJsonData2.value3] < 0)
					{
						dictionary[buffSeidJsonData2.value3] = 0;
					}
				}
			}
		}
		return dictionary;
	}

	public Skill(int id, int level, int max)
	{
		skillInit(id, level, max);
	}

	private void InitImage()
	{
		if (initedImage)
		{
			return;
		}
		initedImage = true;
		if (IsStaticSkill)
		{
			Texture2D val = ResManager.inst.LoadTexture2D("StaticSkill Icon/" + Tools.instance.getStaticSkillIDByKey(skill_ID));
			if (Object.op_Implicit((Object)(object)val))
			{
				skill_Icon = val;
			}
			else
			{
				skill_Icon = ResManager.inst.LoadTexture2D("StaticSkill Icon/0");
			}
			Sprite val2 = ResManager.inst.LoadSprite("StaticSkill Icon/" + Tools.instance.getStaticSkillIDByKey(skill_ID));
			if (Object.op_Implicit((Object)(object)val2))
			{
				skillIconSprite = val2;
			}
			else
			{
				skillIconSprite = ResManager.inst.LoadSprite("StaticSkill Icon/0");
			}
		}
		else
		{
			Texture2D val3 = ResManager.inst.LoadTexture2D("Skill Icon/" + Tools.instance.getSkillIDByKey(skill_ID));
			if (Object.op_Implicit((Object)(object)val3))
			{
				skill_Icon = val3;
			}
			else
			{
				skill_Icon = ResManager.inst.LoadTexture2D("Skill Icon/0");
			}
			Sprite val4 = ResManager.inst.LoadSprite("Skill Icon/" + Tools.instance.getSkillIDByKey(skill_ID));
			if (Object.op_Implicit((Object)(object)val4))
			{
				skillIconSprite = val4;
			}
			else
			{
				skillIconSprite = ResManager.inst.LoadSprite("Skill Icon/0");
			}
		}
		SkillPingZhi = ResManager.inst.LoadTexture2D("Ui Icon/tab/skill" + SkillQuality);
		SkillPingZhiSprite = ResManager.inst.LoadSprite("Ui Icon/tab/skill" + SkillQuality);
		newSkillPingZhiSprite = ResManager.inst.LoadSprite("NewUI/Inventory/Icon/MCS_icon_qualityup_" + SkillQuality * 2);
	}

	public void skillInit(int id, int level, int max)
	{
		if (!_skillJsonData.DataDict.ContainsKey(id))
		{
			throw new Exception($"技能表中不存在id为{id}的技能，请检查配表");
		}
		IsStaticSkill = false;
		_skillJsonData skillJsonData = _skillJsonData.DataDict[id];
		if (skillJsonData != null)
		{
			skill_Name = skillJsonData.name;
			skill_ID = id;
			SkillID = skillJsonData.Skill_ID;
			Skill_Lv = skillJsonData.Skill_Lv;
			if (Skill_Lv < 1 || Skill_Lv > 5)
			{
				throw new Exception($"技能表中id为[{id}] 名字为[{skill_Name}]的技能出现了等级不正确异常，Skill_Lv必须在1-5之间，当前值为:{Skill_Lv}，请检查配表");
			}
			skill_Desc = skillJsonData.descr;
			skill_level = level;
			Max_level = max;
			CoolDown = 10000f;
			CurCD = 0f;
			Damage = skillJsonData.HP;
			skillCast = new Dictionary<int, int>();
			if (skillJsonData.skill_CastType.Count != skillJsonData.skill_Cast.Count)
			{
				throw new Exception($"技能表中id为[{id}] 名字为[{skill_Name}]的技能出现了数组越界异常，skill_CastType与skill_Cast的数组长度必须保持一致，请检查配表");
			}
			for (int i = 0; i < skillJsonData.skill_CastType.Count; i++)
			{
				int key = skillJsonData.skill_CastType[i];
				skillCast.Add(key, skillJsonData.skill_Cast[i]);
			}
			skillSameCast = new Dictionary<int, int>();
			for (int j = 0; j < skillJsonData.skill_SameCastNum.Count; j++)
			{
				skillSameCast.Add(j, skillJsonData.skill_SameCastNum[j]);
			}
			SkillQuality = skillJsonData.Skill_LV;
			ColorIndex = SkillQuality * 2 - 1;
		}
	}

	public Skill()
	{
		skill_ID = -1;
	}

	public void initStaticSkill(int id, int level, int max)
	{
		if (!StaticSkillJsonData.DataDict.ContainsKey(id))
		{
			throw new Exception($"功法表中不存在id为{id}的功法，请检查配表");
		}
		StaticSkillJsonData staticSkillJsonData = StaticSkillJsonData.DataDict[id];
		if (staticSkillJsonData != null)
		{
			IsStaticSkill = true;
			skill_Name = staticSkillJsonData.name;
			skill_ID = id;
			SkillID = staticSkillJsonData.Skill_ID;
			Skill_Lv = staticSkillJsonData.Skill_Lv;
			if (Skill_Lv < 1 || Skill_Lv > 5)
			{
				throw new Exception($"功法表中id为[{id}] 名字为[{skill_Name}]的功法出现了等级不正确异常，Skill_Lv必须在1-5之间，当前值为:{Skill_Lv}，请检查配表");
			}
			skill_Desc = staticSkillJsonData.descr;
			skill_level = level;
			Max_level = max;
			CoolDown = 10000f;
			CurCD = 0f;
			SkillQuality = staticSkillJsonData.Skill_LV;
			ColorIndex = SkillQuality * 2 - 1;
		}
	}

	public Skill Clone()
	{
		return MemberwiseClone() as Skill;
	}

	public SkillCanUseType CanUse(Entity _attaker, Entity _receiver, bool showError = true, string uuid = "")
	{
		Avatar avatar = (Avatar)_attaker;
		_ = (Avatar)_receiver;
		if (CurCD != 0f)
		{
			if (avatar.isPlayer() && showError)
			{
				UIPopTip.Inst.Pop(Tools.getStr("shangweilengque"));
			}
			return SkillCanUseType.尚未冷却不能使用;
		}
		if (avatar.HP <= 0 || avatar.OtherAvatar.HP <= 0)
		{
			return SkillCanUseType.角色死亡不能使用;
		}
		if (avatar.state == 1 || avatar.OtherAvatar.state == 1)
		{
			return SkillCanUseType.角色死亡不能使用;
		}
		if (avatar.state != 3)
		{
			return SkillCanUseType.非自己回合不能使用;
		}
		if (avatar.buffmag.HasBuffSeid(83))
		{
			foreach (List<int> item in avatar.buffmag.getBuffBySeid(83))
			{
				int value = BuffSeidJsonData83.DataDict[item[2]].value1;
				if (avatar.fightTemp.NowRoundUsedSkills.Count >= value && jsonData.instance.Buff[item[2]].CanRealized(avatar, null, item))
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("yidaodaozuida"));
					}
					return SkillCanUseType.超过最多使用次数不能使用;
				}
			}
		}
		if (avatar.isPlayer() && avatar.OtherAvatar.buffmag.HasBuffSeid(103))
		{
			List<List<int>> buffBySeid = avatar.OtherAvatar.buffmag.getBuffBySeid(103);
			_skillJsonData skillJsonData = _skillJsonData.DataDict[skill_ID];
			foreach (List<int> item2 in buffBySeid)
			{
				if (!jsonData.instance.Buff[item2[2]].CanRealized(avatar, null, item2))
				{
					continue;
				}
				Dictionary<int, int> nowCacheLingQiIntDict = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQiIntDict();
				bool flag = skillJsonData.skill_CastType.Contains(5);
				if (nowCacheLingQiIntDict.ContainsKey(5) && !flag)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop("魔气无法当做同系灵气使用");
					}
					return SkillCanUseType.魔气无法当做同系灵气不能使用;
				}
			}
		}
		foreach (int item3 in _skillJsonData.DataDict[skill_ID].seid)
		{
			if (item3 == 45 || item3 == 46 || item3 == 47)
			{
				JSONObject seidJson = getSeidJson(item3);
				int i = seidJson["value1"].I;
				bool flag2 = true;
				if (avatar.buffmag.HasBuff(i) && avatar.buffmag.getBuffByID(i)[0][1] >= seidJson["value2"].I)
				{
					flag2 = false;
				}
				if (flag2)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"));
					}
					return SkillCanUseType.Buff层数不足无法使用;
				}
			}
			if (item3 == 57)
			{
				JSONObject seidJson2 = getSeidJson(item3);
				JSONObject jSONObject = seidJson2["value1"];
				JSONObject jSONObject2 = seidJson2["value2"];
				for (int j = 0; j < jSONObject.Count; j++)
				{
					if (avatar.buffmag.HasBuff(jSONObject[j].I))
					{
						if (avatar.buffmag.getBuffByID(jSONObject[j].I)[0][1] < jSONObject2[j].I)
						{
							if (avatar.isPlayer() && showError)
							{
								UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"));
							}
							return SkillCanUseType.Buff层数不足无法使用;
						}
						continue;
					}
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"));
					}
					return SkillCanUseType.Buff层数不足无法使用;
				}
			}
			if (item3 == 93 && avatar.dunSu < avatar.OtherAvatar.dunSu)
			{
				if (avatar.isPlayer() && showError)
				{
					UIPopTip.Inst.Pop("遁速需大于敌人");
				}
				return SkillCanUseType.遁速不足无法使用;
			}
			if (item3 == 76)
			{
				bool flag3 = true;
				int i2 = getSeidJson(item3)["value1"].I;
				if (avatar.HP <= i2)
				{
					flag3 = false;
				}
				if (flag3)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("xieliangdiyu").Replace("{X}", i2.ToString()));
					}
					return SkillCanUseType.血量太高无法使用;
				}
			}
			if (item3 == 109)
			{
				bool flag4 = true;
				int i3 = getSeidJson(item3)["value1"].I;
				if (avatar.buffmag.HasXTypeBuff(i3))
				{
					flag4 = false;
				}
				if (flag4)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("Buffcengshubuzu"));
					}
					return SkillCanUseType.Buff层数不足无法使用;
				}
			}
			if (item3 == 111)
			{
				bool flag5 = true;
				int i4 = getSeidJson(item3)["value1"].I;
				if (avatar.isPlayer())
				{
					if (avatar.shouYuan - avatar.age - i4 > 0)
					{
						flag5 = false;
					}
				}
				else if (NpcJieSuanManager.inst.GetNpcShengYuTime(Tools.instance.MonstarID) - i4 > 0)
				{
					flag5 = false;
				}
				if (flag5)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop("寿元不足无法使用");
					}
					return SkillCanUseType.寿元不足无法使用;
				}
			}
			if (item3 == 140)
			{
				JSONObject jSONObject3 = getSeidJson(item3)["value1"];
				int count = jSONObject3.Count;
				int num = 0;
				for (int k = 0; k < count; k++)
				{
					if (avatar.buffmag.HasBuff(jSONObject3[k].I))
					{
						num++;
					}
				}
				if (num < count)
				{
					if (showError && avatar.isPlayer())
					{
						UIPopTip.Inst.Pop("需要的buff不足无法释放");
					}
					return SkillCanUseType.Buff种类不足无法使用;
				}
			}
			if (item3 == 145)
			{
				bool flag6 = true;
				int i5 = getSeidJson(item3)["value1"].I;
				if ((float)avatar.HP / (float)avatar.HP_Max * 100f <= (float)i5)
				{
					flag6 = false;
				}
				if (flag6)
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop(Tools.getStr("xieliangdiyu").Replace("{X}", i5.ToString()));
					}
					return SkillCanUseType.血量太高无法使用;
				}
			}
			if (item3 == 147)
			{
				JSONObject seidJson3 = getSeidJson(item3);
				int buffRoundByID = getTargetAvatar(147, _attaker as Avatar).buffmag.GetBuffRoundByID(seidJson3["value1"].I);
				if (!Tools.symbol(seidJson3["panduan"].str, buffRoundByID, seidJson3["value2"].I))
				{
					if (avatar.isPlayer() && showError)
					{
						UIPopTip.Inst.Pop("未满足释放条件");
					}
					return SkillCanUseType.Buff层数不足无法使用;
				}
			}
			if (item3 != 163)
			{
				continue;
			}
			bool flag7 = true;
			int i6 = getSeidJson(item3)["value1"].I;
			if (avatar.shengShi > i6)
			{
				flag7 = false;
			}
			if (flag7)
			{
				if (avatar.isPlayer() && showError)
				{
					UIPopTip.Inst.Pop($"神识大于{i6}才能使用");
				}
				return SkillCanUseType.神识不足无法使用;
			}
		}
		if (ItemAddSeid != null)
		{
			List<JSONObject> list = ItemAddSeid.list;
			if (list.Count > 1)
			{
				for (int l = 0; l < list.Count; l++)
				{
					if (list[l]["id"].I == 145 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list[l]["value1"].n)
					{
						if (avatar.isPlayer() && showError)
						{
							UIPopTip.Inst.Pop(string.Format("生命值{0}%以下才能使用", list[l]["value1"].n));
						}
						return SkillCanUseType.血量太高无法使用;
					}
				}
			}
		}
		foreach (int item4 in _skillJsonData.DataDict[skill_ID].seid)
		{
			if (item4 == 68 && avatar.fightTemp.NowRoundUsedSkills.Count > 0)
			{
				if (avatar.isPlayer() && showError)
				{
					UIPopTip.Inst.Pop(Tools.getStr("canNotUseSkill1"));
				}
				return SkillCanUseType.本回合使用过其他技能无法使用;
			}
		}
		foreach (KeyValuePair<int, int> item5 in getSkillCast(avatar))
		{
			if (avatar.cardMag.HasNoEnoughNum(item5.Key, item5.Value))
			{
				if (avatar.isPlayer() && showError)
				{
					UIPopTip.Inst.Pop(Tools.getStr("lingqikapaibuzu"));
				}
				return SkillCanUseType.灵气不足无法使用;
			}
		}
		List<int> list2 = getremoveCastNum(avatar);
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (KeyValuePair<int, int> item6 in skillSameCast)
		{
			bool flag8 = false;
			for (int m = 0; m < list2.Count; m++)
			{
				if (m != 5 && list2[m] - item6.Value >= 0 && !dictionary.ContainsKey(m))
				{
					dictionary.Add(m, item6.Value);
					flag8 = true;
					break;
				}
			}
			if (!flag8)
			{
				if (avatar.isPlayer() && showError)
				{
					UIPopTip.Inst.Pop(Tools.getStr("tongxilinqikapaibuzu"));
				}
				return SkillCanUseType.灵气不足无法使用;
			}
		}
		return SkillCanUseType.可以使用;
	}

	public List<int> getremoveCastNum(Entity _attaker)
	{
		Avatar avatar = (Avatar)_attaker;
		List<int> list = avatar.crystal.ToListInt32();
		foreach (KeyValuePair<int, int> item in getSkillCast(avatar))
		{
			if (!avatar.crystal.HasNoEnoughNum(item.Key, item.Value))
			{
				list[item.Key] = avatar.crystal[item.Key] - item.Value;
			}
		}
		return list;
	}

	public void useCrystal(Entity _attaker)
	{
		Avatar avatar = (Avatar)_attaker;
		_ = avatar.crystal;
		nowSkillUseCard.Clear();
		nowSkillIsChuFa.Clear();
		if (_attaker.isPlayer())
		{
			nowSkillUseCard = UIFightPanel.Inst.CacheLingQiController.GetNowCacheLingQiIntDict();
			{
				foreach (KeyValuePair<int, int> item in nowSkillUseCard)
				{
					avatar.UseCryStal(item.Key, item.Value);
				}
				return;
			}
		}
		foreach (KeyValuePair<int, int> item2 in getSkillCast(avatar))
		{
			avatar.UseCryStal(item2.Key, item2.Value);
			nowSkillUseCard[item2.Key] = item2.Value;
		}
		List<int> list = getremoveCastNum(avatar);
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (KeyValuePair<int, int> item3 in skillSameCast)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] - item3.Value >= 0 && !dictionary.ContainsKey(i))
				{
					dictionary.Add(i, item3.Value);
					avatar.UseCryStal(i, item3.Value);
					break;
				}
			}
		}
		foreach (KeyValuePair<int, int> item4 in dictionary)
		{
			if (!nowSkillUseCard.ContainsKey(item4.Key))
			{
				nowSkillUseCard[item4.Key] = 0;
			}
			nowSkillUseCard[item4.Key] += item4.Value;
		}
	}

	public int getNomelCastNum(Avatar attaker)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> item in getSkillCast(attaker))
		{
			num += item.Value;
		}
		return num;
	}

	public int GetSameCastNum(Avatar attaker)
	{
		int num = 0;
		foreach (KeyValuePair<int, int> item in skillSameCast)
		{
			num += item.Value;
		}
		return num;
	}

	public void ShowScroll(Entity _attaker, Entity _receiver, int damage)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		GameObject val = (GameObject)_attaker.renderObj;
		GameObject val2 = (GameObject)_receiver.renderObj;
		string text = "<color=#aeee7f>" + val.GetComponent<GameEntity>().entity_name + "</color>";
		string text2 = "<color=#f27a2b>" + val2.GetComponent<GameEntity>().entity_name + "</color>";
		string text3 = skill_Name.RemoveNumber();
		if (string.IsNullOrWhiteSpace(text3))
		{
			text3 = skill_Name;
		}
		string text4 = "<color=#f6c73c>" + text3 + "</color>";
		string text5 = "";
		text5 = ((damage > 0) ? (",造成<color=#ffec96>" + damage + "</color>点伤害") : ((damage >= 0) ? "。" : (",回复<color=#ffec96>" + damage + "</color>点生命")));
		string text6 = "";
		text6 = ((_attaker != _receiver) ? (text + "对" + text2 + "释放了" + text4 + text5) : (text + "释放了" + text4 + text5));
		UIFightPanel.Inst.FightJiLu.AddText(text6);
	}

	public Dictionary<int, int> GetSkillSameCast()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		foreach (KeyValuePair<int, int> item in nowSkillUseCard)
		{
			dictionary[item.Key] = item.Value;
		}
		foreach (KeyValuePair<int, int> item2 in skillCast)
		{
			dictionary[item2.Value] -= item2.Value;
		}
		return dictionary;
	}

	public bool setSeidNum(List<int> TempSeid, List<int> infoFlag, int _index)
	{
		if (infoFlag[4] > 0)
		{
			if (TempSeid.Contains(139))
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public void triggerStartSeid(List<int> SeidList, List<int> infoFlag, Entity _attaker, Entity _receiver, int type)
	{
		List<int> TempSeid = new List<int>();
		SeidList.ForEach(delegate(int aa)
		{
			TempSeid.Add(aa);
		});
		int num = 0;
		int num2 = -1;
		foreach (int item in TempSeid)
		{
			try
			{
				if (num2 <= 0)
				{
					goto IL_0064;
				}
				if (TempSeid.IndexOf(item) < num2)
				{
					continue;
				}
				infoFlag[2] = 0;
				num2 = -1;
				goto IL_0064;
				IL_0064:
				if (setSeidNum(TempSeid, infoFlag, num))
				{
					for (int i = 0; i < infoFlag[4]; i++)
					{
						realizeSeid(item, infoFlag, _attaker, _receiver, type);
					}
				}
				else
				{
					realizeSeid(item, infoFlag, _attaker, _receiver, type);
				}
				if (infoFlag[2] == 1)
				{
					if (TempSeid.Contains(117))
					{
						for (int j = TempSeid.IndexOf(item); j < TempSeid.Count; j++)
						{
							if (TempSeid[j] == 117)
							{
								num2 = j;
								break;
							}
						}
					}
					if (num2 < 0)
					{
						break;
					}
				}
				else if (_attaker.isPlayer())
				{
					if (RoundManager.instance.PlayerSkillCheck != null)
					{
						RoundManager.instance.PlayerSkillCheck.HasPassSeid.Add(item);
					}
				}
				else if (RoundManager.instance.NpcSkillCheck != null)
				{
					RoundManager.instance.NpcSkillCheck.HasPassSeid.Add(item);
				}
				goto IL_0213;
			}
			catch (Exception ex)
			{
				string text = "";
				for (int k = 0; k < infoFlag.Count; k++)
				{
					text += $" {infoFlag[k]}";
				}
				Debug.LogError((object)("检测到技能错误！错误 SkillID:" + skill_ID + " 技能特性:" + item + "额外数据：" + text));
				Debug.LogError((object)("异常信息:" + ex.Message + "\n异常位置:" + ex.StackTrace));
				Debug.LogError((object)$"{ex}");
				goto IL_0213;
			}
			IL_0213:
			num++;
		}
	}

	public void triggerBuffEndSeid(List<int> SeidList, List<int> infoFlag, Entity _attaker, Entity _receiver, int type)
	{
		List<int> TempSeid = new List<int>();
		SeidList.ForEach(delegate(int aa)
		{
			TempSeid.Add(aa);
		});
		int num = 0;
		foreach (int item in TempSeid)
		{
			try
			{
				if (infoFlag[2] == 1)
				{
					break;
				}
				if (setSeidNum(TempSeid, infoFlag, num))
				{
					for (int i = 0; i < infoFlag[4]; i++)
					{
						realizeBuffEndSeid(item, infoFlag, _attaker, _receiver, type);
					}
				}
				else
				{
					realizeBuffEndSeid(item, infoFlag, _attaker, _receiver, type);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("检测到技能错误！错误 SkillID:" + skill_ID + " 技能特性:" + item + "额外数据：" + infoFlag.ToString()));
				Debug.LogError((object)ex);
			}
			num++;
		}
	}

	public void triggerSkillFinalSeid(List<int> SeidList, List<int> infoFlag, Entity _attaker, Entity _receiver, int type)
	{
		List<int> TempSeid = new List<int>();
		SeidList.ForEach(delegate(int aa)
		{
			TempSeid.Add(aa);
		});
		int num = 0;
		foreach (int item in TempSeid)
		{
			try
			{
				if (setSeidNum(TempSeid, infoFlag, num))
				{
					for (int i = 0; i < infoFlag[4]; i++)
					{
						realizeFinalSeid(item, infoFlag, _attaker, _receiver, type);
					}
				}
				else
				{
					realizeFinalSeid(item, infoFlag, _attaker, _receiver, type);
				}
				if (infoFlag[2] == 1)
				{
					break;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)("检测到技能错误！错误 SkillID:" + skill_ID + " 技能特性:" + item + "额外数据：" + infoFlag.ToString()));
				Debug.LogError((object)ex);
			}
			num++;
		}
	}

	public static void SetSkillFlag(List<int> infoFlag, int damage, int skill_ID)
	{
		infoFlag.Add(damage);
		infoFlag.Add(skill_ID);
		infoFlag.Add(0);
		infoFlag.Add(0);
		infoFlag.Add(0);
	}

	public List<int> VirtualPutingSkill(Entity _attaker, Entity _receiver, int type = 0)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		NowSkillSeid.Clear();
		int damage = Damage;
		int damage2 = Damage;
		List<int> list = new List<int>();
		SetSkillFlag(list, damage, skill_ID);
		NowSkillSeid = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(skill_ID)]["seid"]);
		if (NowSkillSeid.Contains(999))
		{
			avatar2.spell.onBuffTickByType(46, list);
			avatar.spell.onBuffTickByType(9, list);
			avatar2.spell.onBuffTickByType(29, list);
			avatar.recvDamage(_attaker, avatar2, skill_ID, list[0], type);
			return list;
		}
		avatar.spell.onBuffTickByType(30, list);
		if (list[3] == 1)
		{
			List<int> list2 = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(skill_ID)]["seid"]);
			if (list2.Contains(29))
			{
				realizeSeid(29, list, _attaker, _receiver, type);
			}
			if (list2.Contains(5))
			{
				realizeSeid(5, list, _attaker, _receiver, type);
			}
			if (list2.Contains(6))
			{
				realizeSeid(6, list, _attaker, _receiver, type);
			}
			return list;
		}
		if (ItemAddSeid != null)
		{
			NowSkillSeid.Clear();
			NowSkillSeid.Add(29);
			List<JSONObject> list3 = ItemAddSeid.list;
			if (list3.Count > 1)
			{
				for (int i = 1; i < list3.Count; i++)
				{
					int i2 = list3[i]["id"].I;
					if (i2 == 145 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list3[i]["value1"].n)
					{
						UIPopTip.Inst.Pop(string.Format("生命值{0}%以下才能使用", list3[i]["value1"].n));
						return list;
					}
					if (i2 == 79)
					{
						for (int j = 0; j < list3[i]["value1"].Count; j++)
						{
							avatar2.spell.addDBuff(list3[i]["value1"][j].I, list3[i]["value2"][j].I);
						}
					}
					if (i2 == 80)
					{
						for (int k = 0; k < list3[i]["value1"].Count; k++)
						{
							avatar.spell.addDBuff(list3[i]["value1"][k].I, list3[i]["value2"][k].I);
						}
					}
					if (i2 == 106)
					{
						for (int l = 0; l < list3[i]["value1"].Count; l++)
						{
							avatar.recvDamage(avatar, avatar, skill_ID, -list3[i]["value1"][l].I);
						}
					}
					if (i2 == 11)
					{
						JSONObject jSONObject = list3[i]["AttackType"];
						int i3 = list3[i]["value1"].I;
						int i4 = list3[i]["value2"].I;
						int i5 = list3[i]["value3"].I;
						JSONObject arr = JSONObject.arr;
						arr.Add(999);
						_skillJsonData.DataDict[i5].AttackType = jSONObject.ToList();
						_skillJsonData.DataDict[i5].seid = arr.ToList();
						jsonData.instance.skillJsonData[i5.ToString()].SetField("AttackType", jSONObject);
						jsonData.instance.skillJsonData[i5.ToString()].SetField("seid", arr);
						Skill skill = new Skill(i5, avatar.level, 5);
						skill.Damage = i4;
						for (int m = 0; m < i3; m++)
						{
							skill.PutingSkill(avatar, avatar2, type);
						}
					}
				}
			}
		}
		triggerStartSeid(NowSkillSeid, list, _attaker, _receiver, type);
		if (damage > 0)
		{
			avatar.spell.onBuffTickByType(9, list);
			avatar2.spell.onBuffTickByType(29, list);
		}
		if (damage > 0)
		{
			avatar.spell.onBuffTickByType(37, list);
		}
		triggerBuffEndSeid(NowSkillSeid, list, _attaker, _receiver, type);
		avatar.spell.onBuffTickByType(47, list);
		damage = list[0];
		damage = avatar.recvDamage(_attaker, avatar2, skill_ID, damage, type);
		if (NowSkillSeid.Contains(11) && LateDamages != null)
		{
			foreach (LateDamage lateDamage in LateDamages)
			{
				List<int> list4 = new Skill(lateDamage.SkillId, avatar.level, 5)
				{
					Damage = lateDamage.Damage
				}.PutingSkill(avatar, avatar2, type);
				if (list4.Count > 0)
				{
					damage += list4[0];
				}
			}
			LateDamages = new List<LateDamage>();
		}
		list[0] = damage;
		if (damage2 > 0)
		{
			avatar.spell.onBuffTickByType(40, list);
		}
		triggerSkillFinalSeid(NowSkillSeid, list, _attaker, _receiver, type);
		if (damage > 0)
		{
			avatar2.OtherAvatar.spell.onBuffTickByType(33, list);
		}
		return list;
	}

	public List<int> PutingSkill(Entity _attaker, Entity _receiver, int type = 0)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		NowSkillSeid.Clear();
		RoundManager.instance.CurSkill = this;
		int damage = Damage;
		int damage2 = Damage;
		List<int> list = new List<int>();
		SetSkillFlag(list, damage, skill_ID);
		NowSkillSeid = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(skill_ID)]["seid"]);
		if (NowSkillSeid.Contains(999))
		{
			avatar2.spell.onBuffTickByType(46, list);
			avatar.spell.onBuffTickByType(9, list);
			avatar2.spell.onBuffTickByType(29, list);
			avatar.recvDamage(_attaker, avatar2, skill_ID, list[0], type);
			return list;
		}
		avatar.spell.onBuffTickByType(30, list);
		if (list[3] == 1)
		{
			List<int> list2 = Tools.JsonListToList(jsonData.instance.skillJsonData[string.Concat(skill_ID)]["seid"]);
			if (list2.Contains(29))
			{
				realizeSeid(29, list, _attaker, _receiver, type);
			}
			if (list2.Contains(5))
			{
				realizeSeid(5, list, _attaker, _receiver, type);
			}
			if (list2.Contains(6))
			{
				realizeSeid(6, list, _attaker, _receiver, type);
			}
			return list;
		}
		if (ItemAddSeid != null)
		{
			NowSkillSeid.Clear();
			NowSkillSeid.Add(29);
			List<JSONObject> list3 = ItemAddSeid.list;
			if (list3.Count > 1)
			{
				for (int i = 1; i < list3.Count; i++)
				{
					int i2 = list3[i]["id"].I;
					if (i2 == 145 && (float)avatar.HP / (float)avatar.HP_Max * 100f > list3[i]["value1"].n)
					{
						UIPopTip.Inst.Pop(string.Format("生命值{0}%以下才能使用", list3[i]["value1"].n));
						return list;
					}
					if (i2 == 79)
					{
						for (int j = 0; j < list3[i]["value1"].Count; j++)
						{
							avatar2.spell.addDBuff(list3[i]["value1"][j].I, list3[i]["value2"][j].I);
						}
					}
					if (i2 == 80)
					{
						for (int k = 0; k < list3[i]["value1"].Count; k++)
						{
							avatar.spell.addDBuff(list3[i]["value1"][k].I, list3[i]["value2"][k].I);
						}
					}
					if (i2 == 106)
					{
						for (int l = 0; l < list3[i]["value1"].Count; l++)
						{
							avatar.recvDamage(avatar, avatar, skill_ID, -list3[i]["value1"][l].I);
						}
					}
					if (i2 == 11)
					{
						JSONObject jSONObject = list3[i]["AttackType"];
						int i3 = list3[i]["value1"].I;
						int i4 = list3[i]["value2"].I;
						int i5 = list3[i]["value3"].I;
						JSONObject arr = JSONObject.arr;
						arr.Add(999);
						_skillJsonData.DataDict[i5].AttackType = jSONObject.ToList();
						_skillJsonData.DataDict[i5].seid = arr.ToList();
						jsonData.instance.skillJsonData[i5.ToString()].SetField("AttackType", jSONObject);
						jsonData.instance.skillJsonData[i5.ToString()].SetField("seid", arr);
						Skill skill = new Skill(i5, avatar.level, 5);
						skill.Damage = i4;
						for (int m = 0; m < i3; m++)
						{
							skill.PutingSkill(avatar, avatar2, type);
						}
					}
				}
			}
		}
		triggerStartSeid(NowSkillSeid, list, _attaker, _receiver, type);
		if (damage > 0)
		{
			avatar2.spell.onBuffTickByType(46, list);
			avatar.spell.onBuffTickByType(9, list);
			avatar2.spell.onBuffTickByType(29, list);
		}
		if (damage > 0)
		{
			avatar.spell.onBuffTickByType(37, list);
		}
		triggerBuffEndSeid(NowSkillSeid, list, _attaker, _receiver, type);
		avatar.spell.onBuffTickByType(47, list);
		damage = list[0];
		damage = avatar.recvDamage(_attaker, avatar2, skill_ID, damage, type);
		if (NowSkillSeid.Contains(11) && LateDamages != null)
		{
			foreach (LateDamage lateDamage in LateDamages)
			{
				List<int> list4 = new Skill(lateDamage.SkillId, avatar.level, 5)
				{
					Damage = lateDamage.Damage
				}.PutingSkill(avatar, avatar2, type);
				if (list4.Count > 0)
				{
					damage += list4[0];
				}
			}
			LateDamages = new List<LateDamage>();
		}
		list[0] = damage;
		if (damage2 > 0)
		{
			avatar.spell.onBuffTickByType(40, list);
		}
		if (avatar.spell.UseSkillLateDict != null && avatar.spell.UseSkillLateDict.Count > 0)
		{
			foreach (int key in avatar.spell.UseSkillLateDict.Keys)
			{
				avatar.spell.addBuff(key, avatar.spell.UseSkillLateDict[key]);
			}
			avatar.spell.UseSkillLateDict = new Dictionary<int, int>();
		}
		triggerSkillFinalSeid(NowSkillSeid, list, _attaker, _receiver, type);
		if (damage > 0)
		{
			avatar2.OtherAvatar.spell.onBuffTickByType(33, list);
		}
		return list;
	}

	public Avatar getTargetAvatar(int seid, Avatar attker)
	{
		if (getSeidJson(seid)["target"].I == 1)
		{
			return attker;
		}
		return attker.OtherAvatar;
	}

	public void SkillStartWait(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.fightTemp.WaitAttacker = attaker;
		attaker.fightTemp.WaitReceiver = receiver;
		attaker.fightTemp.WaitDamage = damage;
		attaker.fightTemp.WaitSkill = this;
		attaker.state = 5;
		bool flag = false;
		attaker.fightTemp.WaitSeid = new List<int>();
		foreach (JSONObject item in jsonData.instance.skillJsonData[string.Concat(skill_ID)]["seid"].list)
		{
			if (flag)
			{
				attaker.fightTemp.WaitSeid.Add(item.I);
			}
			if (item.I == seid)
			{
				flag = true;
			}
		}
		damage[2] = 1;
	}

	public void VirtualPuting(Entity _attaker, Entity _receiver, int type = 0, string uuid = "")
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		List<List<int>> list = new List<List<int>>();
		List<List<int>> list2 = new List<List<int>>();
		List<int> collection = new List<int>(avatar.UsedSkills);
		Dictionary<int, Dictionary<int, int>> dictionary = new Dictionary<int, Dictionary<int, int>>();
		Dictionary<int, Dictionary<int, int>> dictionary2 = new Dictionary<int, Dictionary<int, int>>();
		foreach (int key in avatar.BuffSeidFlag.Keys)
		{
			dictionary[key] = new Dictionary<int, int>(avatar.BuffSeidFlag[key]);
		}
		foreach (int key2 in avatar.OtherAvatar.BuffSeidFlag.Keys)
		{
			dictionary2[key2] = new Dictionary<int, int>(avatar.OtherAvatar.BuffSeidFlag[key2]);
		}
		foreach (List<int> item in avatar.bufflist)
		{
			List<int> list3 = new List<int>();
			foreach (int item2 in item)
			{
				list3.Add(item2);
			}
			list.Add(list3);
		}
		foreach (List<int> item3 in avatar.OtherAvatar.bufflist)
		{
			List<int> list4 = new List<int>();
			foreach (int item4 in item3)
			{
				list4.Add(item4);
			}
			list2.Add(list4);
		}
		List<card> list5 = new List<card>();
		foreach (card item5 in avatar.cardMag._cardlist)
		{
			list5.Add(item5);
		}
		List<card> list6 = new List<card>();
		foreach (card item6 in avatar.OtherAvatar.cardMag._cardlist)
		{
			list6.Add(item6);
		}
		int hP = avatar.HP;
		int hP2 = avatar.OtherAvatar.HP;
		if (CanUse(_attaker, _receiver, showError: true, uuid) == SkillCanUseType.可以使用)
		{
			useCrystal(_attaker);
			List<int> list7 = VirtualPutingSkill(_attaker, _receiver, type);
			avatar.spell.onBuffTickByType(8, list7);
			avatar.spell.onRemoveBuffByType(10);
			avatar.UsedSkills = new List<int>(collection);
			avatar2.spell.AutoRemoveBuff();
			avatar.spell.AutoRemoveBuff();
			RoundManager.instance.VirtualSkillDamage = list7[0];
			avatar.bufflist = list;
			avatar.OtherAvatar.bufflist = list2;
			avatar.cardMag._cardlist = list5;
			avatar.OtherAvatar.cardMag._cardlist = list6;
			avatar.HP = hP;
			avatar.OtherAvatar.HP = hP2;
			avatar.BuffSeidFlag = dictionary;
			avatar.OtherAvatar.BuffSeidFlag = dictionary2;
			CurCD = 0f;
			RoundManager.instance.eventChengeCrystal();
		}
	}

	public void Puting(Entity _attaker, Entity _receiver, int type = 0, string uuid = "")
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		Avatar attaker = (Avatar)_attaker;
		Avatar receiver = (Avatar)_receiver;
		attack = attaker;
		target = receiver;
		this.type = type;
		if (CanUse(_attaker, _receiver, showError: true, uuid) == SkillCanUseType.可以使用)
		{
			if (attaker.isPlayer())
			{
				RoundManager.instance.PlayerSkillCheck = new SkillCheck
				{
					SkillId = skill_ID
				};
			}
			else
			{
				RoundManager.instance.NpcSkillCheck = new SkillCheck
				{
					SkillId = skill_ID
				};
			}
			Queue<UnityAction> queue = new Queue<UnityAction>();
			UnityAction val = (UnityAction)delegate
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				((Component)((GameObject)attaker.renderObj).transform.GetChild(0)).GetComponent<Animator>().Play("Punch", -1, 0f);
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(val);
			RoundManager.EventFightTalk("UseSkill", new EventDelegate(delegate
			{
				Flowchart fightTalk = RoundManager.instance.FightTalk;
				fightTalk.SetBooleanVariable("attaker", attaker.isPlayer());
				fightTalk.SetBooleanVariable("receiver", receiver.isPlayer());
				fightTalk.SetIntegerVariable("SkillID", jsonData.instance.skillJsonData[skill_ID.ToString()]["Skill_ID"].I);
			}));
			Debug.Log((object)(attaker.name + "施放了" + skill_Name));
			UnityAction val2 = (UnityAction)delegate
			{
				//IL_0062: Unknown result type (might be due to invalid IL or missing references)
				string demage = skill_Name.Replace("1", "").Replace("2", "").Replace("3", "")
					.Replace("4", "")
					.Replace("5", "");
				((GameObject)attaker.renderObj).GetComponentInChildren<AvatarShowSkill>().setText(demage, attaker);
				YSFuncList.Ints.Continue();
			};
			queue.Enqueue(val2);
			YSFuncList.Ints.AddFunc(queue);
			RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能消耗;
			useCrystal(_attaker);
			UIFightPanel.Inst.CacheLingQiController.DestoryAllLingQi();
			UIFightPanel.Inst.PlayerLingQiController.ResetPlayerLingQiCount();
			RoundManager.instance.chengeCrystal();
			RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能后消耗;
			List<int> list = PutingSkill(_attaker, _receiver, type);
			attaker.spell.onBuffTickByType(8, list);
			attaker.fightTemp.UseSkill(skill_ID);
			attaker.spell.onRemoveBuffByType(10);
			receiver.spell.AutoRemoveBuff();
			attaker.spell.AutoRemoveBuff();
			RoundManager.instance.eventChengeCrystal();
			ShowScroll(_attaker, _receiver, list[0]);
			UIFightPanel.Inst.RefreshCD();
			RoundManager.instance.NowUseLingQiType = UseLingQiType.None;
		}
	}

	public int realizeSeid(int seid, List<int> damage, Entity _attaker, Entity _receiver, int type)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		string text = "realizeSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[5] { seid, damage, avatar, avatar2, type });
		}
		return damage[0];
	}

	public int realizeBuffEndSeid(int seid, List<int> damage, Entity _attaker, Entity _receiver, int type)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		string text = "realizeBuffEndSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[5] { seid, damage, avatar, avatar2, type });
		}
		return damage[0];
	}

	public int realizeFinalSeid(int seid, List<int> damage, Entity _attaker, Entity _receiver, int type)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		string text = "realizeFinalSeid" + seid;
		InitMethod(text);
		if (methodDict[text] != null)
		{
			methodDict[text].Invoke(this, new object[5] { seid, damage, avatar, avatar2, type });
		}
		return damage[0];
	}

	public bool CanRealizeSeid(Avatar attaker, Avatar receiver)
	{
		foreach (int item in _skillJsonData.DataDict[skill_ID].seid)
		{
			switch (item)
			{
			case 10:
				if (attaker.shengShi <= receiver.shengShi)
				{
					return false;
				}
				break;
			case 52:
				if (!receiver.buffmag.HasBuff((int)getSeidJson(item)["value1"].n))
				{
					return false;
				}
				break;
			case 53:
				if (getCrystalNum(receiver) != 0)
				{
					return false;
				}
				break;
			case 54:
				if (getCrystalNum(attaker) < (int)getSeidJson(item)["value1"].n)
				{
					return false;
				}
				break;
			case 56:
			{
				JSONObject seidJson = getSeidJson(item);
				if (attaker.buffmag.HasBuff(seidJson["value1"].I))
				{
					if (attaker.buffmag.getBuffByID(seidJson["value1"].I)[0][1] < seidJson["value2"].I)
					{
						return false;
					}
					break;
				}
				return false;
			}
			case 58:
				if (attaker.UsedSkills.Count > 0)
				{
					bool flag = true;
					int i = getSeidJson(item)["value1"].I;
					foreach (int item2 in _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType)
					{
						if (item2 == i)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return false;
					}
					break;
				}
				return false;
			case 78:
				if (attaker.crystal.getCardNum() != 0)
				{
					return false;
				}
				break;
			case 124:
				if (attaker.UsedSkills.Count > 0)
				{
					bool flag2 = true;
					int i2 = getSeidJson(item)["value1"].I;
					foreach (int item3 in _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType)
					{
						if (item3 == i2)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						return false;
					}
					break;
				}
				return false;
			case 160:
				if (attaker.OtherAvatar.buffmag.HasBuff(getSeidJson(item)["value1"].I))
				{
					return false;
				}
				break;
			}
		}
		return true;
	}

	public int getCrystalNum(Avatar attaker)
	{
		return attaker.crystal.getCardNum();
	}

	public void setSeidSkillFlag(Avatar attaker, int seid, int num)
	{
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(skill_ID, 0);
		}
		attaker.SkillSeidFlag[seid][skill_ID] = num;
	}

	public int GetSeidSkillFlag(Avatar attaker, int seid)
	{
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(skill_ID, 0);
		}
		return attaker.SkillSeidFlag[seid][skill_ID];
	}

	public void AddSeidSkillFlag(Avatar attaker, int seid, int Addnum)
	{
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(skill_ID, 0);
		}
		attaker.SkillSeidFlag[seid][skill_ID] += Addnum;
	}

	public void reduceBuff(Avatar Targ, int X, int Y)
	{
		Targ.buffmag.getBuffByID(X)[0][1] -= Y;
	}

	public int RemoveBuff(Avatar target, int BuffID)
	{
		return target.buffmag.RemoveBuff(BuffID);
	}

	public void createSkill(Avatar attaker, int CreateSkillID)
	{
		Skill skill = new Skill(CreateSkillID, attaker.level, 5);
		if (jsonData.instance.skillJsonData[string.Concat(skill.skill_ID)]["script"].str == "SkillAttack")
		{
			skill.PutingSkill(attaker, attaker.OtherAvatar);
		}
		else if (jsonData.instance.skillJsonData[string.Concat(skill.skill_ID)]["script"].str == "SkillSelf")
		{
			skill.PutingSkill(attaker, attaker);
		}
	}

	public JSONObject getSeidJson(int seid)
	{
		if (ItemAddSeid != null)
		{
			foreach (JSONObject item in ItemAddSeid.list)
			{
				if (item["id"].I == seid)
				{
					return item;
				}
			}
		}
		JSONObject result = null;
		if (seid < jsonData.instance.SkillSeidJsonData.Length)
		{
			JSONObject jSONObject = jsonData.instance.SkillSeidJsonData[seid];
			if (jSONObject.HasField(skill_ID.ToString()))
			{
				return jSONObject[skill_ID.ToString()];
			}
			Debug.LogError((object)$"获取技能seid数据失败，技能id:{skill_ID}，seid:{seid}，技能seid{seid}表中不存在id为{skill_ID}的数据，请检查配表");
		}
		else
		{
			Debug.LogError((object)$"获取技能seid数据失败，技能id:{skill_ID}，seid:{seid}，seid超出了jsonData.instance.SkillSeidJsonData.Length，请检查配表");
		}
		return result;
	}

	public void realizeBuffEndSeid1(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = (int)((float)damage[0] * getSeidJson(seid)["value1"].n);
		attaker.recvDamage(attaker, attaker, skill_ID, -num, type);
	}

	public void realizeSeid2(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		RoundManager.instance.RandomDrawCard(attaker, i);
		RoundManager.instance.chengeCrystal();
	}

	public void realizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		JSONObject seidJson = getSeidJson(seid);
		List<int> list = new List<int>();
		foreach (List<int> item in receiver.bufflist)
		{
			for (int i = 0; i < seidJson["value1"].list.Count; i++)
			{
				if (item[2] == seidJson["value1"][i].I)
				{
					for (int j = 0; j < seidJson["value2"][i].I; j++)
					{
						list.Add(num);
					}
					break;
				}
			}
			num++;
		}
		foreach (int item2 in list)
		{
			receiver.spell.onBuffTick(item2, new List<int> { 0 });
		}
	}

	public void realizeSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		for (int i = 0; i < seidJson["value1"].Count; i++)
		{
			int i2 = seidJson["value1"][i].I;
			int i3 = seidJson["value2"][i].I;
			if (i3 >= 100)
			{
				receiver.spell.addBuff(i2, i3);
				continue;
			}
			for (int j = 0; j < i3; j++)
			{
				receiver.spell.addDBuff(i2);
			}
		}
	}

	public void realizeSeid5(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(0, 1);
		}
		attaker.SkillSeidFlag[seid][skill_ID] = 1;
		CurCD = 50000f;
	}

	public void realizeSeid6(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		CurCD = 50000f;
	}

	public void realizeSeid7(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		for (int i = 0; i < seidJson["value1"].Count; i++)
		{
			int i2 = seidJson["value1"][i].I;
			for (int j = 0; j < seidJson["value2"][i].I; j++)
			{
				RoundManager.instance.DrawCard(attaker, i2);
			}
		}
	}

	public void realizeSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid9(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		int i = getSeidJson(seid)["value1"].I;
		foreach (List<int> item in receiver.bufflist)
		{
			if (item[2] == i)
			{
				for (int j = 0; j < item[1]; j++)
				{
					receiver.spell.onBuffTick(num);
				}
				break;
			}
			num++;
		}
	}

	public void realizeSeid10(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.shengShi <= attaker.OtherAvatar.shengShi)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid11(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		int i3 = seidJson["value3"].I;
		for (int j = 0; j < i; j++)
		{
			if (LateDamages == null)
			{
				LateDamages = new List<LateDamage>();
			}
			if (skill_ID == 12508)
			{
				int buffSum = attaker.buffmag.GetBuffSum(i3);
				LateDamages.Add(new LateDamage
				{
					SkillId = i2,
					Damage = buffSum
				});
			}
			else if (SkillID == 1170)
			{
				int damage2 = GlobalValue.Get(i3);
				LateDamages.Add(new LateDamage
				{
					SkillId = i2,
					Damage = damage2
				});
			}
			else
			{
				LateDamages.Add(new LateDamage
				{
					SkillId = i2,
					Damage = i3
				});
			}
		}
	}

	public void realizeSeid12(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		RoundManager.instance.removeCard(receiver, getSeidJson(seid)["value1"].I);
	}

	public void realizeSeid13(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		if (!receiver.SkillSeidFlag.ContainsKey(seid))
		{
			receiver.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
		}
		if (!receiver.SkillSeidFlag[seid].ContainsKey(i))
		{
			receiver.SkillSeidFlag[seid].Add(i, 0);
		}
		int i2 = seidJson["value2"].I;
		receiver.SkillSeidFlag[seid][i] += i2;
	}

	public void realizeSeid14(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid15(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		RoundManager.instance.removeCard(receiver, seidJson["value1"].I, seidJson["value2"].I);
	}

	public void realizeSeid16(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int listSum = RoundManager.instance.getListSum(receiver.crystal);
		int num = 0;
		num = (int)receiver.NowCard - listSum;
		RoundManager.instance.RandomDrawCard(attaker, num);
	}

	public void realizeSeid17(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		for (int j = 0; j < i; j++)
		{
			card card = RoundManager.instance.drawCard(attaker);
			if (i2 == card.cardType)
			{
				RoundManager.instance.DrawCard(attaker, card.cardType);
			}
		}
	}

	public void realizeSeid18(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		foreach (int item in attaker.crystal.ToListInt32())
		{
			for (int i = 0; i < item; i++)
			{
				RoundManager.instance.DrawCard(attaker, num);
			}
			num++;
		}
	}

	public void realizeSeid19(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int listSum = RoundManager.instance.getListSum(receiver.crystal);
		RoundManager.instance.removeCard(receiver, listSum);
	}

	public void realizeSeid20(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int listSum = RoundManager.instance.getListSum(receiver.crystal);
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		RoundManager.instance.removeCard(receiver, listSum);
		stopwatch.Stop();
		Debug.Log((object)$"移除耗时{stopwatch.ElapsedMilliseconds}ms");
		stopwatch.Reset();
		stopwatch.Start();
		RoundManager.instance.RandomDrawCard(attaker, listSum);
		stopwatch.Stop();
		Debug.Log((object)$"添加耗时{stopwatch.ElapsedMilliseconds}ms");
	}

	public void realizeSeid21(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		RoundManager.instance.removeCard(receiver, receiver.crystal[i], i);
	}

	public void realizeSeid22(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid23(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] += (int)(getSeidJson(seid)["value1"].n * (float)attaker.useSkillNum);
	}

	public void realizeSeid24(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(0, 1);
		}
		attaker.SkillSeidFlag[seid][0] = 1;
	}

	public void realizeSeid25(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.spell.addDBuff(5, attaker.HP_Max - attaker.HP);
	}

	public void realizeSeid26(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] = receiver.HP / 2;
		int i = getSeidJson(seid)["value1"].I;
		if (damage[0] > i)
		{
			damage[0] = i;
		}
	}

	public void realizeSeid27(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int listSum = RoundManager.instance.getListSum(attaker.crystal);
		damage[0] += listSum * getSeidJson(seid)["value1"].I;
	}

	public void realizeFinalSeid28(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = receiver.HP - damage[0] - receiver.HP_Max;
		if (num > 0)
		{
			receiver.spell.addDBuff(5, num);
		}
	}

	public void realizeSeid29(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		if (weaponuuid != null && weaponuuid != "")
		{
			if (RoundManager.instance.WeaponSkillList.ContainsKey(weaponuuid))
			{
				RoundManager.instance.WeaponSkillList[weaponuuid] = i;
			}
			else
			{
				RoundManager.instance.WeaponSkillList.Add(weaponuuid, i);
			}
			CurCD = 50000f;
			return;
		}
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(0, 1);
		}
		attaker.SkillSeidFlag[seid][skill_ID] = i;
		CurCD = 50000f;
	}

	public void realizeSeid30(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		int[] randomLingQiTypes = RoundManager.instance.GetRandomLingQiTypes(attaker, i);
		for (int j = 0; j < randomLingQiTypes.Length; j++)
		{
			if (randomLingQiTypes[j] > 0)
			{
				RoundManager.instance.DrawCardCreatSpritAndAddCrystal(attaker, j, randomLingQiTypes[j]);
			}
		}
		for (int k = 0; k < randomLingQiTypes.Length; k++)
		{
			if (randomLingQiTypes[k] > 0 && k != i2)
			{
				RoundManager.instance.RoundTimeAutoRemoveCard(attaker, k, randomLingQiTypes[k]);
			}
		}
	}

	public void realizeSeid31(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		for (int i = 0; i < seidJson["value1"].Count; i++)
		{
			int i2 = seidJson["value1"][i].I;
			int i3 = seidJson["value2"][i].I;
			if (SkillID == 221)
			{
				for (int j = 0; j < i2; j++)
				{
					attaker.spell.addDBuff(i3);
				}
			}
			else
			{
				attaker.spell.addBuff(i3, i2);
			}
		}
	}

	public void realizeSeid32(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value2"].I);
		if (buffByID.Count > 0)
		{
			int i = seidJson["value1"].I;
			if (buffByID[0][1] >= i)
			{
				buffByID[0][1] -= i;
			}
			else
			{
				buffByID[0][1] = 0;
			}
		}
	}

	public void realizeSeid33(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<List<int>> buffByID = attaker.buffmag.getBuffByID(seidJson["value2"].I);
		if (buffByID.Count > 0)
		{
			int i = seidJson["value1"].I;
			if (buffByID[0][1] >= i)
			{
				buffByID[0][1] -= i;
			}
			else
			{
				buffByID[0][1] = 0;
			}
		}
	}

	public void realizeBuffEndSeid34(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] *= getSeidJson(seid)["value1"].I;
	}

	public void realizeSeidFinal34(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid35(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] += attaker.NowRoundUsedCard.Count * getSeidJson(seid)["value1"].I;
	}

	public void realizeSeid36(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int value1 = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		int count = attaker.NowRoundUsedCard.FindAll((int i) => i == value1).Count;
		damage[0] += count * i2;
	}

	public void realizeSeid37(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int cardNum = attaker.crystal.getCardNum();
		attaker.recvDamage(attaker, attaker, skill_ID, -cardNum * getSeidJson(seid)["value1"].I, type);
	}

	public void realizeSeid38(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int num = attaker.crystal[seidJson["value1"].I];
		damage[0] += num * seidJson["value2"].I;
	}

	public void realizeSeid39(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int listSum = RoundManager.instance.getListSum(attaker.crystal);
		RoundManager.instance.removeCard(attaker, listSum);
		damage[0] += listSum * getSeidJson(seid)["value1"].I;
	}

	public void realizeSeid40(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		int num = attaker.crystal[i];
		RoundManager.instance.removeCard(attaker, num, i);
		damage[0] += num * i2;
	}

	public void realizeSeid41(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (!attaker.SkillSeidFlag.ContainsKey(seid))
		{
			attaker.SkillSeidFlag.Add(seid, new Dictionary<int, int>());
			attaker.SkillSeidFlag[seid].Add(0, 1);
		}
		if (!attaker.SkillSeidFlag[seid].ContainsKey(skill_ID))
		{
			attaker.SkillSeidFlag[seid][skill_ID] = 0;
		}
		attaker.SkillSeidFlag[seid][skill_ID]++;
		int num = damage[0];
		JSONObject seidJson = getSeidJson(seid);
		damage[0] += attaker.SkillSeidFlag[seid][skill_ID] / seidJson["value1"].I * seidJson["value2"].I;
		if (num > 0 && damage[0] < 0)
		{
			damage[0] = 0;
		}
		if (num < 0 && damage[0] > 0)
		{
			damage[0] = 0;
		}
	}

	public void realizeSeid42(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		if (receiver.buffmag.HasBuff(i))
		{
			List<List<int>> buffByID = receiver.buffmag.getBuffByID(i);
			damage[0] += buffByID[0][1] * i2;
		}
	}

	public void realizeSeid43(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		for (int i = 0; i < seidJson["value1"].list.Count; i++)
		{
			int i2 = seidJson["value1"][i].I;
			if (attaker.buffmag.HasBuff(i2))
			{
				List<List<int>> buffByID = attaker.buffmag.getBuffByID(i2);
				damage[0] += buffByID[0][1] * seidJson["value2"][i].I;
			}
		}
	}

	public void realizeSeid44(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		if (attaker.buffmag.HasBuff(i))
		{
			int num = attaker.buffmag.getBuffByID(i)[0][1] * i2;
			attaker.recvDamage(attaker, attaker, skill_ID, -num, type);
		}
	}

	public void realizeSeid45(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
		damage[0] += seidJson["value3"].I;
	}

	public void realizeSeid46(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
		attaker.recvDamage(attaker, attaker, skill_ID, -seidJson["value3"].I, type);
	}

	public void realizeSeid47(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		reduceBuff(attaker, seidJson["value1"].I, seidJson["value2"].I);
		createSkill(attaker, Tools.instance.getSkillKeyByID(seidJson["value3"].I, attaker));
	}

	public void realizeSeid48(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		if (attaker.buffmag.HasBuff(i))
		{
			int num = RemoveBuff(attaker, i);
			attaker.recvDamage(attaker, receiver, 10001 + seidJson["value3"].I, num * seidJson["value2"].I, type);
		}
	}

	public void realizeSeid49(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		foreach (JSONObject item in jSONObject.list)
		{
			_ = item;
			if (attaker.buffmag.HasBuff(jSONObject[num].I))
			{
				int num2 = RemoveBuff(attaker, jSONObject[num].I);
				damage[0] += num2 * seidJson["value2"][num].I;
			}
			num++;
		}
	}

	public void realizeSeid50(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		if (attaker.buffmag.HasBuff(i))
		{
			int num = RemoveBuff(attaker, i);
			attaker.recvDamage(attaker, attaker, skill_ID, -num * seidJson["value2"].I, type);
		}
	}

	public void realizeSeid51(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		foreach (JSONObject item in jSONObject.list)
		{
			_ = item;
			if (attaker.buffmag.HasBuff(jSONObject[num].I))
			{
				int num2 = RemoveBuff(attaker, jSONObject[num].I);
				int i = seidJson["value2"][num].I;
				int i2 = seidJson["value3"][num].I;
				receiver.spell.addBuff(i, i2 * num2);
			}
			num++;
		}
	}

	public void realizeSeid52(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (!receiver.buffmag.HasBuff(getSeidJson(seid)["value1"].I))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid53(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (getCrystalNum(receiver) != 0)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid54(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int crystalNum = getCrystalNum(attaker);
		int i = getSeidJson(seid)["value1"].I;
		if (crystalNum < i)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid55(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (RoundManager.instance.drawCard(attaker).cardType != getSeidJson(seid)["value1"].I)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid56(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		if (attaker.buffmag.HasBuff(i))
		{
			if (attaker.buffmag.getBuffByID(i)[0][1] < seidJson["value2"].I)
			{
				damage[2] = 1;
			}
		}
		else
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid57(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		for (int i = 0; i < seidJson["value1"].Count; i++)
		{
			reduceBuff(attaker, seidJson["value1"][i].I, seidJson["value2"][i].I);
		}
	}

	public void realizeSeid58(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.UsedSkills.Count > 0)
		{
			bool flag = true;
			List<int> attackType = _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType;
			int i = getSeidJson(seid)["value1"].I;
			foreach (int item in attackType)
			{
				if (item == i)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				damage[2] = 1;
			}
		}
		else
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid59(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.recvDamage(attaker, attaker, skill_ID, -attaker.HP_Max * (int)getSeidJson(seid)["value1"].n / 100, type);
	}

	public void realizeSeid60(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.recvDamage(attaker, attaker, skill_ID, -(attaker.HP_Max - attaker.HP) * (int)getSeidJson(seid)["value1"].n / 100, type);
	}

	public void realizeSeid61(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] += attaker.HP_Max * (int)getSeidJson(seid)["value1"].n / 100;
	}

	public void realizeSeid62(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		List<int> list = new List<int>();
		foreach (List<int> item in attaker.bufflist)
		{
			int key = item[2];
			if (_BuffJsonData.DataDict[key].bufftype == i)
			{
				list.Add(key);
			}
		}
		foreach (int item2 in list)
		{
			receiver.spell.onBuffTickById(item2, damage);
		}
	}

	public void realizeSeid63(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		foreach (List<int> item in attaker.bufflist)
		{
			int key = item[2];
			if (_BuffJsonData.DataDict[key].bufftype == i)
			{
				damage[0] += i2;
			}
		}
	}

	public void realizeSeid64(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.recvDamage(attaker, receiver, skill_ID, receiver.HP_Max * (int)getSeidJson(seid)["value1"].n / 100, type);
	}

	public void realizeSeid65(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid66(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] += (int)getSeidJson(seid)["value1"].n;
	}

	public void realizeSeid67(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		int i3 = seidJson["value3"].I;
		int i4 = seidJson["value4"].I;
		List<List<int>> buffByID = attaker.buffmag.getBuffByID(i);
		if (buffByID.Count > 0 && buffByID[0][1] >= i2)
		{
			int num = buffByID[0][1] / i2;
			for (int j = 0; j < num; j++)
			{
				receiver.spell.addDBuff(i3, i4);
			}
		}
	}

	public void realizeSeid68(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid69(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid70(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		RoundManager.instance.startRound(attaker);
	}

	public void realizeSeid71(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		attaker.setHP(attaker.HP - seidJson["value1"].I);
		attaker.spell.addDBuff(seidJson["value2"].I, seidJson["value3"].I);
	}

	public void realizeSeid72(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int listSum = RoundManager.instance.getListSum(receiver.crystal);
		RoundManager.instance.removeCard(receiver, listSum);
		RoundManager.instance.RandomDrawCard(attaker, listSum);
	}

	public void realizeSeid73(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		damage[0] += attaker.fightTemp.lastRoundDamage[1] / 2;
	}

	public void realizeSeid74(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		foreach (List<int> item in attaker.buffmag.getAllBuffByType(getSeidJson(seid)["value1"].I))
		{
			attaker.spell.removeBuff(item);
		}
	}

	public void realizeSeid75(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value1"].I);
		if (buffByID.Count == 0)
		{
			return;
		}
		int num = 0;
		int i = seidJson["value2"].I;
		num = ((buffByID[0][1] < i) ? buffByID[0][1] : i);
		int num2 = 0;
		foreach (List<int> item in receiver.bufflist)
		{
			if (item == buffByID[0])
			{
				for (int j = 0; j < num; j++)
				{
					receiver.spell.onBuffTick(num2, new List<int>());
					RoundManager.instance.DrawCard(attaker);
				}
				break;
			}
			num2++;
		}
	}

	public void realizeSeid77(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if ((int)((float)attaker.HP / (float)attaker.HP_Max * 100f) > getSeidJson(seid)["value1"].I)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid78(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.crystal.getCardNum() != 0)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid79(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		for (int i = 0; i < jSONObject.Count; i++)
		{
			receiver.spell.addBuff(jSONObject[i].I, jSONObject2[i].I);
		}
	}

	public void realizeSeid80(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		for (int i = 0; i < jSONObject2.Count; i++)
		{
			attaker.spell.addBuff(jSONObject[i].I, jSONObject2[i].I);
		}
	}

	public void realizeSeid81(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.cardMag.getCardNum();
		int i = getSeidJson(seid)["value1"].I;
		RoundManager.instance.removeCard(attaker, i);
	}

	public void realizeSeid82(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<List<int>> buffByID = receiver.buffmag.getBuffByID(seidJson["value1"].I);
		if (buffByID.Count > 0)
		{
			buffByID[0][1] = seidJson["value2"].I;
		}
	}

	public void realizeSeid83(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		foreach (JSONObject item in jSONObject.list)
		{
			_ = item;
			int i = jSONObject[num].I;
			int i2 = jSONObject2[num].I;
			List<List<int>> buffByID = attaker.buffmag.getBuffByID(i);
			if (buffByID.Count > 0)
			{
				buffByID[0][1] = i2;
			}
			else
			{
				attaker.spell.addDBuff(i, i2);
			}
			num++;
		}
	}

	public void realizeSeid84(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int num = ((attaker.shengShi > attaker.OtherAvatar.shengShi) ? (attaker.shengShi - attaker.OtherAvatar.shengShi) : 0);
		damage[0] += (int)((float)num / seidJson["value1"].n) * seidJson["value2"].I;
	}

	public void realizeSeid85(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int dunSu = attaker.dunSu;
		JSONObject seidJson = getSeidJson(seid);
		damage[0] += (int)((float)dunSu / seidJson["value1"].n) * seidJson["value2"].I;
	}

	public void realizeSeid86(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.recvDamage(attaker, receiver, skill_ID, receiver.HP * (int)getSeidJson(seid)["value1"].n / 100, type);
	}

	public void realizeSeid87(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		int i2 = getSeidJson(seid)["value2"].I;
		for (int j = 0; j < i; j++)
		{
			card randomCard = attaker.OtherAvatar.cardMag.getRandomCard();
			if (randomCard != null)
			{
				int cardType = randomCard.cardType;
				card card = attaker.OtherAvatar.cardMag.ChengCard(randomCard.cardType, i2);
				if (attaker.OtherAvatar.isPlayer() && card != null)
				{
					Debug.Log((object)$"将{(LingQiType)cardType}污染为{(LingQiType)i2}");
					UIFightPanel.Inst.PlayerLingQiController.SlotList[cardType].LingQiCount--;
					UIFightPanel.Inst.PlayerLingQiController.SlotList[i2].LingQiCount++;
				}
			}
		}
	}

	public void realizeSeid88(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		for (int j = 0; j < i; j++)
		{
			RoundManager.instance.DrawCard(receiver, i2);
		}
	}

	public void realizeSeid89(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (receiver.cardMag[(int)getSeidJson(seid)["value1"].n] <= 0)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid90(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int num = attaker.OtherAvatar.crystal[seidJson["value1"].I];
		damage[0] += num * seidJson["value2"].I;
	}

	public void realizeSeid91(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int value1 = seidJson["value1"].I;
		int count = attaker.UsedSkills.FindAll((int id) => _skillJsonData.DataDict[id].Skill_ID == value1).Count;
		damage[0] += count * seidJson["value2"].I;
	}

	public void realizeSeid92(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		RoundManager.instance.removeCard(attaker, seidJson["value1"].I, seidJson["value2"].I);
	}

	public void realizeSeid94(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<int> list = receiver.spell.addDBuff(seidJson["value1"].I);
		if (list != null)
		{
			list[1] = seidJson["value2"].I;
		}
	}

	public void realizeSeid95(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (!receiver.buffmag.HasXTypeBuff(getSeidJson(seid)["value1"].I))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid96(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		foreach (List<int> item in receiver.bufflist)
		{
			if (i == _BuffJsonData.DataDict[item[2]].bufftype)
			{
				item[1]--;
			}
		}
	}

	public void realizeSeid100(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int buffRoundByID = receiver.buffmag.GetBuffRoundByID(seidJson["value1"].I);
		string str = seidJson["panduan"].Str;
		if (!PanDuan(str, buffRoundByID, seidJson["value2"].I))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid101(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.setHP((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid102(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = 0;
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		foreach (List<int> item in attaker.bufflist)
		{
			for (int i = 0; i < jSONObject.list.Count; i++)
			{
				if (item[2] == jSONObject[i].I)
				{
					for (int j = 0; j < jSONObject2[i].I; j++)
					{
						attaker.spell.onBuffTick(num, new List<int> { 0 });
					}
					break;
				}
			}
			num++;
		}
	}

	public void realizeFinalSeid103(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		AddSeidSkillFlag(attaker, seid, damage[0]);
		int num = attaker.SkillSeidFlag[seid][skill_ID];
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int i2 = seidJson["value2"].I;
		int i3 = seidJson["value3"].I;
		if (num >= i)
		{
			int num2 = num / i;
			setSeidSkillFlag(attaker, seid, num % i);
			for (int j = 0; j < num2; j++)
			{
				receiver.spell.addDBuff(i2, i3);
			}
		}
	}

	public void realizeFinalSeid104(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		Tools.instance.ToolsStartCoroutine(ISeid104(seid));
	}

	public IEnumerator ISeid104(int seid)
	{
		yield return (object)new WaitForSeconds(0.01f);
		skillInit(getSeidJson(seid)["value1"].I, 0, 10);
	}

	public void realizeSeid105(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		int skillID = Tools.instance.getSkillKeyByID(getSeidJson(seid)["value1"].I, attaker);
		Skill skill = new Skill(skillID, 0, 10);
		List<int> _damage = new List<int>();
		Tools.AddQueue((UnityAction)delegate
		{
			RoundManager.instance.NowUseLingQiType = UseLingQiType.释放技能后消耗;
			string script = _skillJsonData.DataDict[skillID].script;
			if (script == "SkillAttack")
			{
				_damage = skill.PutingSkill(attaker, attaker.OtherAvatar);
			}
			else if (script == "SkillSelf")
			{
				_damage = skill.PutingSkill(attaker, attaker);
			}
			attaker.spell.onBuffTickByType(8, _damage);
			RoundManager.instance.NowUseLingQiType = UseLingQiType.None;
			YSFuncList.Ints.Continue();
		});
	}

	public void realizeSeid107(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		Avatar targetAvatar = getTargetAvatar(seid, attaker);
		int buffSum = targetAvatar.buffmag.GetBuffSum(getSeidJson(seid)["value1"].I);
		if (buffSum > 0)
		{
			targetAvatar.recvDamage(attaker, attaker, skill_ID, -buffSum, type);
		}
	}

	public void realizeSeid108(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int num = attaker.crystal[seidJson["value1"].I];
		attaker.spell.addBuff(seidJson["value2"].I, seidJson["value3"].I * num);
	}

	public void realizeSeid110(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		int num = attaker.HP * i / 100;
		attaker.setHP(attaker.HP - num);
		damage[0] += num;
	}

	public void realizeSeid111(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		if (attaker.isPlayer())
		{
			attaker.shouYuan -= (uint)i;
		}
		else if (NpcJieSuanManager.inst.isCanJieSuan && Tools.instance.MonstarID >= 20000)
		{
			NpcJieSuanManager.inst.npcSetField.AddNpcShouYuan(Tools.instance.MonstarID, -i);
		}
	}

	public void realizeSeid112(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		for (int i = 0; i < jSONObject.Count; i++)
		{
			receiver.spell.addBuff(jSONObject[i].I, jSONObject2[i].I);
		}
	}

	public void realizeSeid113(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		for (int i = 0; i < jSONObject2.Count; i++)
		{
			attaker.spell.addDBuff(jSONObject[i].I, jSONObject2[i].I);
		}
	}

	public void realizeSeid114(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		JSONObject jSONObject = seidJson["value1"];
		JSONObject jSONObject2 = seidJson["value2"];
		Avatar monstar = RoundManager.instance.GetMonstar();
		for (int i = 0; i < jSONObject.Count; i++)
		{
			monstar.spell.addBuff(jSONObject[i].I, jSONObject2[i].I);
		}
	}

	public void realizeSeid115(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.isPlayer())
		{
			if (RoundManager.instance.PlayerUseSkillList.Contains(skill_ID))
			{
				damage[2] = 1;
			}
			else
			{
				RoundManager.instance.PlayerUseSkillList.Add(skill_ID);
			}
		}
		else if (RoundManager.instance.NpcUseSkillList.Contains(skill_ID))
		{
			damage[2] = 1;
		}
		else
		{
			RoundManager.instance.NpcUseSkillList.Add(skill_ID);
		}
	}

	public void realizeSeid116(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int num = attaker.buffmag.GetBuffSum(seidJson["value1"].I) / seidJson["value2"].I;
		int num2 = num * seidJson["value3"].I;
		List<List<int>> buffByID = attaker.buffmag.getBuffByID(seidJson["value1"].I);
		if (buffByID.Count > 0)
		{
			RoundManager.instance.NowSkillUsedLingQiSum += num;
			buffByID[0][1] = 0;
			damage[0] += num2;
		}
	}

	public void realizeSeid118(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int cardNum = attaker.cardMag.getCardNum();
		RoundManager.instance.removeCard(attaker, cardNum);
		JSONObject seidJson = getSeidJson(seid);
		List<int> list = seidJson["value1"].ToList();
		List<int> list2 = seidJson["value2"].ToList();
		for (int i = 0; i < list.Count; i++)
		{
			attaker.spell.addBuff(list[i], list2[i] * cardNum);
		}
	}

	public void realizeSeid119(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int value = GlobalValue.Get(getSeidJson(seid)["value1"].I, "特性119");
		damage[0] = value;
	}

	public void realizeSeid120(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int i = seidJson["value1"].I;
		int num = GlobalValue.Get(seidJson["value2"].I, "特性120");
		attaker.spell.addBuff(i, num);
	}

	public void realizeSeid121(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid122(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int num = GlobalValue.Get(getSeidJson(seid)["value1"].I, "特性122");
		attaker.setHP(attaker.HP + num);
	}

	public void realizeSeid123(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		List<JSONObject> list = seidJson["value1"].list;
		List<JSONObject> list2 = seidJson["value2"].list;
		foreach (KeyValuePair<int, int> item in nowSkillUseCard)
		{
			if (item.Value > 0)
			{
				attaker.spell.addBuff(list[item.Key].I, list2[item.Key].I);
			}
		}
	}

	public void realizeSeid124(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.UsedSkills.Count > 0)
		{
			bool flag = true;
			List<int> attackType = _skillJsonData.DataDict[attaker.UsedSkills[attaker.UsedSkills.Count - 1]].AttackType;
			int i = getSeidJson(seid)["value1"].I;
			foreach (int item in attackType)
			{
				if (item == i)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				damage[2] = 1;
			}
		}
		else
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid140(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid141(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.jieyin.AddJinMai((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid142(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.jieyin.AddYiZhi((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid143(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.jieyin.AddHuaYing((int)getSeidJson(seid)["value1"].n);
	}

	public void realizeSeid144(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		attaker.jieyin.AddJinDanHP(i);
		if (i < 0)
		{
			attaker.spell.onBuffTickByType(39, new List<int>());
		}
	}

	public void realizeSeid146(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.ZhuJiJinDu += getSeidJson(seid)["value1"].I;
	}

	public void realizeSeid148(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		Avatar targetAvatar = getTargetAvatar(seid, attaker);
		int buffRoundByID = targetAvatar.buffmag.GetBuffRoundByID(seidJson["value1"].I);
		if (!Tools.symbol(seidJson["panduan"].str, buffRoundByID, targetAvatar.buffmag.GetBuffSum(seidJson["value2"].I)))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid149(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		Avatar targetAvatar = getTargetAvatar(seid, attaker);
		int buffRoundByID = targetAvatar.buffmag.GetBuffRoundByID(seidJson["value1"].I);
		if (!Tools.symbol(seidJson["panduan"].str, buffRoundByID, targetAvatar.buffmag.GetBuffSum(seidJson["value2"].I)))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid150(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		NowSkillSeid.Clear();
		List<int> flag1 = new List<int>();
		damage.ForEach(delegate(int aa)
		{
			flag1.Add(aa);
		});
		triggerStartSeid(new List<int> { seidJson["value1"].I }, flag1, attaker, receiver, type);
		if (flag1[2] == 1)
		{
			NowSkillSeid = Tools.JsonListToList(seidJson["value3"]);
			triggerStartSeid(NowSkillSeid, damage, attaker, receiver, type);
		}
		else
		{
			NowSkillSeid = Tools.JsonListToList(seidJson["value2"]);
			triggerStartSeid(NowSkillSeid, damage, attaker, receiver, type);
		}
	}

	public void realizeSeid151(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		realizeSeid150(seid, damage, attaker, receiver, type);
	}

	public void realizeSeid152(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		JSONObject seidJson = getSeidJson(seid);
		int buffHasYTime = getTargetAvatar(seid, attaker).buffmag.GetBuffHasYTime(seidJson["value1"].I, seidJson["value2"].I);
		if (buffHasYTime > 0)
		{
			damage[4] = buffHasYTime;
		}
		else
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid153(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		Avatar targetAvatar = getTargetAvatar(seid, attaker);
		JSONObject seidJson = getSeidJson(seid);
		int buffHasYTime = targetAvatar.buffmag.GetBuffHasYTime(seidJson["value1"].I, seidJson["value2"].I);
		if (buffHasYTime > 0)
		{
			RemoveBuff(targetAvatar, seidJson["value1"].I);
			damage[4] = buffHasYTime;
		}
		else
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid154(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid155(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (skillSameCast.Count <= 0)
		{
			return;
		}
		Dictionary<int, int> xiangSheng = Tools.GetXiangSheng();
		int num = 0;
		if (nowSkillUseCard.Count <= 1)
		{
			damage[2] = 1;
		}
		foreach (KeyValuePair<int, int> item in nowSkillUseCard)
		{
			if (nowSkillUseCard.ContainsKey(xiangSheng[item.Key]))
			{
				num++;
			}
		}
		if (num < nowSkillUseCard.Count - 1)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid156(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (skillSameCast.Count <= 0)
		{
			return;
		}
		Dictionary<int, int> xiangKe = Tools.GetXiangKe();
		int num = 0;
		if (nowSkillUseCard.Count <= 1)
		{
			damage[2] = 1;
		}
		foreach (KeyValuePair<int, int> item in nowSkillUseCard)
		{
			if (nowSkillUseCard.ContainsKey(xiangKe[item.Key]))
			{
				num++;
			}
		}
		if (num < nowSkillUseCard.Count - 1)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid157(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (skillSameCast.Count > 0 && nowSkillUseCard.Count != 1)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid158(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		NowSkillSeid.Clear();
		List<int> flag1 = new List<int>();
		damage.ForEach(delegate(int aa)
		{
			flag1.Add(aa);
		});
		JSONObject seidJson = getSeidJson(seid);
		triggerStartSeid(new List<int> { seidJson["value1"].I }, flag1, attaker, receiver, type);
		if (flag1[2] != 1)
		{
			NowSkillSeid = Tools.JsonListToList(seidJson["value2"]);
		}
		List<int> flag2 = new List<int>();
		damage.ForEach(delegate(int aa)
		{
			flag2.Add(aa);
		});
		triggerStartSeid(new List<int> { seidJson["value3"].I }, flag2, attaker, receiver, type);
		if (flag2[2] != 1)
		{
			seidJson["value4"].list.ForEach(delegate(JSONObject aa)
			{
				NowSkillSeid.Add(aa.I);
			});
		}
		triggerStartSeid(NowSkillSeid, damage, attaker, receiver, type);
	}

	public void realizeSeid159(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int buffTypeNum = getTargetAvatar(seid, attaker).buffmag.getBuffTypeNum(getSeidJson(seid)["value1"].I);
		if (buffTypeNum > 0)
		{
			damage[4] = buffTypeNum;
		}
		else
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid160(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.OtherAvatar.buffmag.HasBuff(getSeidJson(seid)["value1"].I))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid161(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (attaker.dunSu <= attaker.OtherAvatar.dunSu)
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid162(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		Avatar targetAvatar = getTargetAvatar(seid, attaker);
		JSONObject seidJson = getSeidJson(seid);
		int num = RemoveBuff(targetAvatar, seidJson["value1"].I);
		receiver.spell.addBuff(seidJson["value2"].I, num);
	}

	public void realizeSeid164(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["target"].I;
		string str = getSeidJson(seid)["panduan"].str;
		int i2 = getSeidJson(seid)["value1"].I;
		int statr = ((i == 1) ? attaker.shengShi : attaker.OtherAvatar.shengShi);
		if (!Tools.symbol(str, statr, i2))
		{
			damage[2] = 1;
		}
	}

	public void realizeSeid165(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (PlayerEx.GetGameDifficulty() == 游戏难度.极简)
		{
			attaker.ZhuJiJinDu += getSeidJson(seid)["value1"].I;
		}
	}

	public void realizeSeid166(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		List<ITEM_INFO> list = new List<ITEM_INFO>();
		List<ITEM_INFO> list2 = new List<ITEM_INFO>();
		int count = attaker.itemList.values.Count;
		for (int i = 0; i < count; i++)
		{
			ITEM_INFO iTEM_INFO = attaker.itemList.values[i];
			if (_ItemJsonData.DataDict.ContainsKey(iTEM_INFO.itemId))
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[iTEM_INFO.itemId];
				if (itemJsonData.type >= 0 && itemJsonData.type <= 2)
				{
					list.Add(iTEM_INFO);
				}
			}
		}
		count = attaker.equipItemList.values.Count;
		for (int j = 0; j < count; j++)
		{
			ITEM_INFO iTEM_INFO2 = attaker.equipItemList.values[j];
			if (_ItemJsonData.DataDict.ContainsKey(iTEM_INFO2.itemId))
			{
				_ItemJsonData itemJsonData2 = _ItemJsonData.DataDict[iTEM_INFO2.itemId];
				if (itemJsonData2.type >= 0 && itemJsonData2.type <= 2)
				{
					list.Add(iTEM_INFO2);
					list2.Add(iTEM_INFO2);
				}
			}
		}
		if (list.Count > 0)
		{
			ITEM_INFO randomOne = list.GetRandomOne();
			if (randomOne.itemId >= 18001 && randomOne.itemId <= 18010)
			{
				if (randomOne.Seid != null)
				{
					int key = 1;
					int num = 1;
					if (randomOne.Seid.HasField("quality"))
					{
						key = randomOne.Seid["quality"].I;
					}
					if (randomOne.Seid.HasField("QPingZhi"))
					{
						num = randomOne.Seid["QPingZhi"].I;
					}
					int num2 = seid166Dict[key][num - 1];
					attaker.spell.addBuff(5, num2);
				}
			}
			else if (_ItemJsonData.DataDict.ContainsKey(randomOne.itemId))
			{
				_ItemJsonData itemJsonData3 = _ItemJsonData.DataDict[randomOne.itemId];
				int num3 = seid166Dict[itemJsonData3.quality][itemJsonData3.typePinJie - 1];
				attaker.spell.addBuff(5, num3);
			}
			if (list2.Contains(randomOne))
			{
				attaker.removeEquipItem(randomOne.uuid);
			}
			else
			{
				attaker.removeItem(randomOne.uuid);
			}
		}
		else
		{
			Debug.Log((object)"Seid166:没有法宝用于销毁");
		}
	}

	public void realizeSeid170(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int i = getSeidJson(seid)["value1"].I;
		int cardNum = receiver.cardMag.getCardNum();
		damage[0] += (int)((float)(i * cardNum) / 100f);
	}

	public void realizeSeid171(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		int xinjin = PlayerEx.Player.xinjin;
		damage[0] += 10000 - 5 * xinjin;
	}

	public void realizeSeid172(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (!receiver.SkillSeidFlag.ContainsKey(13))
		{
			receiver.SkillSeidFlag.Add(13, new Dictionary<int, int>());
		}
		for (int i = 0; i < 5; i++)
		{
			receiver.SkillSeidFlag[13][i] = 20 - receiver.LingGeng[i];
		}
	}

	public void realizeSeid173(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
	}

	public void realizeSeid174(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (receiver.HP < receiver.HP_Max)
		{
			int i = getSeidJson(seid)["value1"].I;
			damage[0] = (int)((float)damage[0] * (1f + (float)i / 100f));
		}
	}

	public void realizeSeid175(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (TianJieManager.Inst.LeiJieIndex < 9)
		{
			int damage2 = TianJieLeiJieShangHai.DataDict[TianJieManager.Inst.LeiJieIndex + 1].Damage;
			attaker.spell.addBuff(3150, damage2);
			if (TianJieManager.Inst.NowLeiJie == "乾天劫")
			{
				attaker.spell.addBuff(3155, 1);
			}
			TianJieManager.Inst.EffectManager.PlayXuLi();
			TianJieManager.Inst.YiXuLi = true;
		}
	}

	public void realizeSeid176(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (TianJieManager.Inst.LeiJieIndex < 9)
		{
			string nowLeiJie = TianJieManager.Inst.NowLeiJie;
			int leiJieSkillID = TianJieLeiJieType.DataDict[nowLeiJie].SkillId;
			Debug.Log((object)$"释放雷劫 {nowLeiJie} 技能ID:{leiJieSkillID}");
			UIFightPanel.Inst.PlayerStatus.NoRefresh = true;
			TianJieEffectManager.Inst.PlayAttack(delegate
			{
				createSkill(attaker, Tools.instance.getSkillKeyByID(leiJieSkillID, attaker));
			});
			TianJieManager.Inst.YiXuLi = false;
		}
	}

	public void realizeSeid177(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		if (TianJieManager.Inst.LeiJieIndex >= 8)
		{
			return;
		}
		int num = TianJieManager.Inst.LeiJieIndex + 1;
		string text = TianJieManager.Inst.LeiJieList[num];
		for (int i = 0; i < 10; i++)
		{
			string text2 = TianJieManager.Inst.RollLeiJie(num);
			if (text2 != text)
			{
				TianJieManager.Inst.LeiJieList[num] = text2;
				Debug.Log((object)$"Skill特性177:将{num}号雷劫 {text}替换为{text2}");
				break;
			}
		}
	}

	public bool PanDuan(string fuhao, int value1, int value2)
	{
		switch (fuhao)
		{
		case "=":
			if (value1 == value2)
			{
				return true;
			}
			return false;
		case ">":
			if (value1 > value2)
			{
				return true;
			}
			return false;
		case "<":
			if (value1 < value2)
			{
				return true;
			}
			return false;
		default:
			return false;
		}
	}
}
