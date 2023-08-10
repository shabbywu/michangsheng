using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime;
using JSONClass;
using UnityEngine;

namespace KBEngine;

public class AI
{
	public enum entityState
	{
		Dead = 1,
		RoundEnd,
		RoundStart,
		GameStart,
		WaitDoNext
	}

	public enum skillWeight
	{
		Circle = 1,
		Draw = 2,
		Buff = 3,
		Attack = 4,
		Defense = 5,
		Other = 6,
		Final = 20
	}

	public Entity entity;

	public AI(Entity avater)
	{
		entity = avater;
	}

	public virtual void think()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (((Avatar)entity).fightTemp.useAI)
		{
			Avatar avatar = (Avatar)entity;
			if (avatar.state == 3)
			{
				((Behavior)((Component)((GameObject)avatar.renderObj).transform).GetComponent<BehaviorTree>()).EnableBehavior();
			}
		}
	}

	public int getSkillWeight(int skillId)
	{
		JSONObject jSONObject = jsonData.instance.skillJsonData[string.Concat(skillId)];
		List<int> list = new List<int>();
		list.Add((int)jSONObject["Skill_Type"].n);
		if (FightAIData.DataDict.ContainsKey(skillId))
		{
			foreach (int item in FightAIData.DataDict[skillId].ShunXu)
			{
				realizeAIType(item, skillId, list);
			}
		}
		else
		{
			foreach (int key in jsonData.instance.AIJsonDate.Keys)
			{
				if (jsonData.instance.AIJsonDate[key].HasField(skillId.ToString()))
				{
					realizeAIType(key, skillId, list);
				}
			}
		}
		return list[0];
	}

	public JSONObject getAIRealizID(int seid, int skillId)
	{
		return jsonData.instance.AIJsonDate[seid][skillId.ToString()];
	}

	public void setFlag(List<int> flag, int num)
	{
		if (num != 0)
		{
			flag[0] = num;
		}
	}

	public Avatar getAvatarByStr(JSONObject AIinfo)
	{
		Avatar result = null;
		if (AIinfo["panduan1"].str == "self")
		{
			result = (Avatar)entity;
		}
		else if (AIinfo["panduan1"].str == "other")
		{
			result = ((Avatar)entity).OtherAvatar;
		}
		else
		{
			Debug.LogError((object)("AI表中" + AIinfo["id"].n + "技能（对象判定）字段错误"));
		}
		return result;
	}

	public void setFlagByPanduan2(JSONObject AIinfo, int LeftNum, int rightNum, List<int> flag)
	{
		if (AIinfo["panduan2"].str == ">")
		{
			if (LeftNum > rightNum)
			{
				setFlag(flag, (int)AIinfo["Yes"].n);
			}
			else
			{
				setFlag(flag, (int)AIinfo["No"].n);
			}
		}
		else if (AIinfo["panduan2"].str == "<")
		{
			if (LeftNum < rightNum)
			{
				setFlag(flag, (int)AIinfo["Yes"].n);
			}
			else
			{
				setFlag(flag, (int)AIinfo["No"].n);
			}
		}
		else
		{
			Debug.LogError((object)("AI表中" + AIinfo["id"].n + "技能（判断大小）字段错误"));
		}
	}

	public void realizeAIType(int seid, int skillId, List<int> flag)
	{
		for (int i = 0; i < 500; i++)
		{
			if (i == seid)
			{
				MethodInfo method = GetType().GetMethod("AIRealize" + seid);
				if (method != null)
				{
					method.Invoke(this, new object[3] { seid, skillId, flag });
				}
				break;
			}
		}
	}

	public void AIRealize1(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		List<List<int>> buffByID = getAvatarByStr(aIRealizID).buffmag.getBuffByID((int)aIRealizID["value1"].n);
		int num = 0;
		foreach (List<int> item in buffByID)
		{
			num += item[1];
		}
		setFlagByPanduan2(aIRealizID, num, (int)aIRealizID["value2"].n, flag);
	}

	public void AIRealize2(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int hP = getAvatarByStr(aIRealizID).HP;
		setFlagByPanduan2(aIRealizID, hP, (int)aIRealizID["value1"].n, flag);
	}

	public void AIRealize3(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatar = (Avatar)entity;
		if (avatar.UsedSkills.Count <= 0)
		{
			setFlag(flag, (int)aIRealizID["No"].n);
			return;
		}
		bool flag2 = false;
		foreach (JSONObject item in jsonData.instance.skillJsonData[avatar.UsedSkills[avatar.UsedSkills.Count - 1].ToString()]["AttackType"].list)
		{
			if ((int)item.n == (int)aIRealizID["value1"].n)
			{
				flag2 = true;
			}
		}
		if (flag2)
		{
			setFlag(flag, (int)aIRealizID["Yes"].n);
		}
		else
		{
			setFlag(flag, (int)aIRealizID["No"].n);
		}
	}

	public void AIRealize4(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int cardNum = getAvatarByStr(aIRealizID).crystal.getCardNum();
		setFlagByPanduan2(aIRealizID, cardNum, (int)aIRealizID["value1"].n, flag);
	}

	public void AIRealize5(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatarByStr = getAvatarByStr(aIRealizID);
		if (avatarByStr.HP >= avatarByStr.HP_Max)
		{
			setFlag(flag, (int)aIRealizID["Yes"].n);
		}
		else
		{
			setFlag(flag, (int)aIRealizID["No"].n);
		}
	}

	public void AIRealize6(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatar = (Avatar)entity;
		setFlagByPanduan2(aIRealizID, avatar.shengShi, avatar.OtherAvatar.shengShi, flag);
	}

	public void AIRealize7(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatarByStr = getAvatarByStr(aIRealizID);
		int leftNum = avatarByStr.HP + avatarByStr.buffmag.getHuDun();
		setFlagByPanduan2(aIRealizID, leftNum, (int)aIRealizID["value1"].n, flag);
	}

	public void AIRealize8(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int leftNum = ((Avatar)entity).crystal[(int)aIRealizID["value1"].n];
		setFlagByPanduan2(aIRealizID, leftNum, (int)aIRealizID["value2"].n, flag);
	}

	public void AIRealize9(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int leftNum = ((Avatar)entity).GetLingGeng[(int)aIRealizID["value1"].n];
		setFlagByPanduan2(aIRealizID, leftNum, (int)aIRealizID["value2"].n, flag);
	}

	public void AIRealize10(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatar = (Avatar)entity;
		if (avatar.cardMag.getCardNum() > avatar.NowCard)
		{
			setFlag(flag, (int)aIRealizID["Yes"].n);
		}
		else
		{
			setFlag(flag, (int)aIRealizID["No"].n);
		}
	}

	public void AIRealize11(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatarByStr = getAvatarByStr(aIRealizID);
		int num = 0;
		foreach (List<int> item in avatarByStr.bufflist)
		{
			if ((int)jsonData.instance.BuffJsonData[item[2].ToString()]["bufftype"].n == (int)aIRealizID["value1"].n)
			{
				num++;
			}
		}
		setFlagByPanduan2(aIRealizID, num, (int)aIRealizID["value2"].n, flag);
	}

	public void AIRealize12(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int leftNum = ((Avatar)entity).fightTemp.lastRoundDamage[1];
		setFlagByPanduan2(aIRealizID, leftNum, (int)aIRealizID["value1"].n, flag);
	}

	public void AIRealize13(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int leftNum = ((Avatar)entity).OtherAvatar.crystal[(int)aIRealizID["value1"].n];
		setFlagByPanduan2(aIRealizID, leftNum, (int)aIRealizID["value2"].n, flag);
	}

	public void AIRealize14(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatarByStr = getAvatarByStr(aIRealizID);
		int buffSum = avatarByStr.buffmag.GetBuffSum(aIRealizID["value1"].I);
		int buffSum2 = avatarByStr.buffmag.GetBuffSum(aIRealizID["value2"].I);
		setFlagByPanduan2(aIRealizID, buffSum, buffSum2, flag);
	}

	public void AIRealize15(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatar = (Avatar)entity;
		int num = RoundManager.instance.StaticRoundNum;
		if (avatar.dunSu > avatar.OtherAvatar.dunSu)
		{
			num++;
		}
		int i = aIRealizID["value1"].I;
		setFlagByPanduan2(aIRealizID, num, i, flag);
	}

	public void AIRealize16(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int count = ((Avatar)entity).fightTemp.NowRoundUsedSkills.Count;
		int i = aIRealizID["value1"].I;
		setFlagByPanduan2(aIRealizID, count, i, flag);
	}

	public void AIRealize17(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		_ = (Avatar)entity;
		int fightType = (int)Tools.instance.monstarMag.FightType;
		if (aIRealizID["value1"].ToList().Contains(fightType))
		{
			setFlag(flag, (int)aIRealizID["Yes"].n);
		}
		else
		{
			setFlag(flag, (int)aIRealizID["No"].n);
		}
	}

	public void AIRealize30(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int buffSum = getAvatarByStr(aIRealizID).buffmag.GetBuffSum(aIRealizID["value1"].I);
		int i = aIRealizID["value2"].I;
		setFlagByPanduan2(aIRealizID, buffSum, i, flag);
	}

	public void AIRealize31(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		Avatar avatar = (Avatar)entity;
		RoundManager.instance.IsVirtual = true;
		avatar.spell.VirtualspellSkill(skillId);
		RoundManager.instance.IsVirtual = false;
		int virtualSkillDamage = RoundManager.instance.VirtualSkillDamage;
		int hP = avatar.OtherAvatar.HP;
		if (virtualSkillDamage >= hP)
		{
			setFlag(flag, (int)aIRealizID["Yes"].n);
		}
		else
		{
			setFlag(flag, (int)aIRealizID["No"].n);
		}
	}

	public void AIRealize32(int seid, int skillId, List<int> flag)
	{
		JSONObject aIRealizID = getAIRealizID(seid, skillId);
		int buffSum = getAvatarByStr(aIRealizID).buffmag.GetBuffSum(aIRealizID["value1"].I);
		int i = aIRealizID["value2"].I;
		setFlagByPanduan2(aIRealizID, buffSum, i, flag);
	}
}
