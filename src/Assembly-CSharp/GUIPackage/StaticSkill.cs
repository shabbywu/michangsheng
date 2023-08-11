using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using KBEngine;
using UnityEngine;

namespace GUIPackage;

[Serializable]
public class StaticSkill
{
	public enum StaticSeidAll
	{
		StaticSEID2 = 2,
		StaticSEID3 = 3,
		StaticSEID5 = 5,
		StaticSEID6 = 6,
		StaticSEID7 = 7,
		StaticSEID8 = 8,
		StaticSEID9 = 9,
		StaticSEID10 = 10,
		StaticSEID11 = 11,
		StaticSEID12 = 12,
		StaticSEID13 = 13,
		StaticSEID14 = 14,
		StaticSEID15 = 15,
		StaticSEID16 = 16,
		StaticSEID17 = 17
	}

	public string skill_Name;

	public int skill_ID;

	public string skill_Desc;

	public Texture2D skill_Icon;

	public Texture2D SkillPingZhi;

	public Sprite skillIconSprite;

	public Sprite SkillPingZhiSprite;

	public int skill_level;

	public int Max_level;

	public float CoolDown;

	public float CurCD;

	public Dictionary<int, int> skillCast = new Dictionary<int, int>();

	public Dictionary<int, int> skillSameCast = new Dictionary<int, int>();

	public StaticSkill(int id, int level, int max)
	{
		JSONObject jSONObject = jsonData.instance.StaticSkillJsonData[string.Concat(id)];
		if (jSONObject != null)
		{
			string text = Regex.Unescape(jSONObject["name"].str);
			skill_Name = text;
			skill_ID = id;
			string text2 = Regex.Unescape(jSONObject["descr"].str);
			skill_Desc = text2;
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
			skill_level = level;
			Max_level = max;
			CoolDown = 10000f;
			CurCD = 0f;
		}
	}

	public StaticSkill()
	{
		skill_ID = -1;
	}

	public Skill Clone()
	{
		return MemberwiseClone() as Skill;
	}

	public virtual JSONObject getJsonData()
	{
		return jsonData.instance.StaticSkillJsonData;
	}

	public void PutingSkill(Entity _attaker, Entity _receiver, int type = 0)
	{
		List<int> flag = new List<int>();
		foreach (JSONObject item in getJsonData()[string.Concat(skill_ID)]["seid"].list)
		{
			realizeSeid((int)item.n, flag, _attaker, _receiver, type);
		}
	}

	public void putingStudySkill(Entity _attaker, Entity _receiver, int type = 0)
	{
		List<int> flag = new List<int>();
		foreach (JSONObject item in getJsonData()[string.Concat(skill_ID)]["seid"].list)
		{
			StudySkillSeid((int)item.n, flag, _attaker, _receiver, type);
		}
	}

	public void StudySkillSeid(int seid, List<int> flag, Entity _attaker, Entity _receiver, int type)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		_ = jsonData.instance.StaticSkillTypeJsonData[seid.ToString()]["value1"].n;
		MethodInfo method = GetType().GetMethod(getStudyMethodName() + seid);
		if (method != null)
		{
			method.Invoke(this, new object[5] { seid, flag, avatar, avatar2, type });
		}
	}

	public void Puting(Entity _attaker, Entity _receiver, int type = 0)
	{
		_ = (Avatar)_attaker;
		_ = (Avatar)_receiver;
		PutingSkill(_attaker, _receiver, type);
	}

	public static void resetSeid(Avatar attaker)
	{
		attaker.StaticSkillSeidFlag.Clear();
		foreach (SkillItem equipStaticSkill in attaker.equipStaticSkillList)
		{
			new StaticSkill(Tools.instance.getStaticSkillKeyByID(equipStaticSkill.itemId), 0, 5).Puting(attaker, attaker, 2);
		}
	}

	public virtual string getMethodName()
	{
		return "realizeSeid";
	}

	public virtual string getStudyMethodName()
	{
		return "StudtRealizeSeid";
	}

	public virtual Dictionary<int, Dictionary<int, int>> getSeidFlag(Avatar attaker)
	{
		return attaker.StaticSkillSeidFlag;
	}

	public virtual void realizeSeid(int seid, List<int> flag, Entity _attaker, Entity _receiver, int type)
	{
		Avatar avatar = (Avatar)_attaker;
		Avatar avatar2 = (Avatar)_receiver;
		if ((int)jsonData.instance.StaticSkillTypeJsonData[seid.ToString()]["value1"].n == type)
		{
			MethodInfo method = GetType().GetMethod(getMethodName() + seid);
			if (method != null)
			{
				method.Invoke(this, new object[5] { seid, flag, avatar, avatar2, type });
			}
		}
	}

	public void findKey(int seid, Avatar attaker)
	{
		if (!getSeidFlag(attaker).ContainsKey(seid))
		{
			getSeidFlag(attaker).Add(seid, new Dictionary<int, int>());
			getSeidFlag(attaker)[seid].Add(skill_ID, 0);
		}
	}

	public virtual JSONObject getSeidJson(int seid)
	{
		JSONObject jSONObject = jsonData.instance.StaticSkillSeidJsonData[seid][skill_ID.ToString()];
		if (jSONObject == null)
		{
			Debug.LogError((object)$"功法特性{seid}的配表中不存在技能{skill_ID}的数据，请检查配表");
		}
		return jSONObject;
	}

	public virtual void resetSeidFlag(int seid, Avatar attaker)
	{
		findKey(seid, attaker);
		if (!getSeidFlag(attaker)[seid].ContainsKey(skill_ID))
		{
			getSeidFlag(attaker)[seid].Add(skill_ID, 0);
		}
		getSeidFlag(attaker)[seid][skill_ID] += (int)getSeidJson(seid)["value1"].n;
	}

	public void setSeidFlag(int seid, int Num, Avatar attaker)
	{
		findKey(seid, attaker);
		if (!getSeidFlag(attaker)[seid].ContainsKey(skill_ID))
		{
			getSeidFlag(attaker)[seid].Add(skill_ID, 0);
		}
		getSeidFlag(attaker)[seid][skill_ID] = Num;
	}

	public Avatar getTargetAvatar(int seid, Avatar attker)
	{
		if (getSeidJson(seid)["target"].I == 1)
		{
			return attker;
		}
		return attker.OtherAvatar;
	}

	public void realizeSeid1(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		Avatar targetAvatar = getTargetAvatar(seid, attaker);
		for (int i = 0; i < getSeidJson(seid)["value1"].Count; i++)
		{
			int buffid = (int)getSeidJson(seid)["value1"][i].n;
			int time = (int)getSeidJson(seid)["value2"][i].n;
			targetAvatar.spell.addDBuff(buffid, time);
		}
	}

	public void realizeSeid2(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public virtual void realizeSeid3(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
		if (!attaker.isPlayer())
		{
			attaker.setHP(attaker.HP + (int)getSeidJson(seid)["value1"].n);
		}
	}

	public void realizeSeid4(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker.shouYuan += (uint)getSeidJson(seid)["value1"].n;
	}

	public void realizeSeid5(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		attaker._xinjin += getSeidJson(seid)["value1"].I;
	}

	public void realizeSeid6(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid7(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid8(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid10(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid11(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid12(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid13(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		setSeidFlag(seid, 1, attaker);
	}

	public void realizeSeid14(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		resetSeidFlag(seid, attaker);
	}

	public void realizeSeid15(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		setSeidFlag(seid, 1, attaker);
	}

	public void realizeSeid17(int seid, List<int> damage, Avatar attaker, Avatar receiver, int type)
	{
		setSeidFlag(seid, 1, attaker);
	}
}
