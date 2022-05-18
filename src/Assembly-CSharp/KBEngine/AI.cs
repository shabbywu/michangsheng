using System;
using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime;
using JSONClass;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x0200100F RID: 4111
	public class AI
	{
		// Token: 0x0600623B RID: 25147 RVA: 0x000441B8 File Offset: 0x000423B8
		public AI(Entity avater)
		{
			this.entity = avater;
		}

		// Token: 0x0600623C RID: 25148 RVA: 0x00273638 File Offset: 0x00271838
		public virtual void think()
		{
			if (!((Avatar)this.entity).fightTemp.useAI)
			{
				return;
			}
			Avatar avatar = (Avatar)this.entity;
			if (avatar.state == 3)
			{
				((GameObject)avatar.renderObj).transform.GetComponent<BehaviorTree>().EnableBehavior();
			}
		}

		// Token: 0x0600623D RID: 25149 RVA: 0x0027368C File Offset: 0x0027188C
		public int getSkillWeight(int skillId)
		{
			JSONObject jsonobject = jsonData.instance.skillJsonData[string.Concat(skillId)];
			List<int> list = new List<int>();
			list.Add((int)jsonobject["Skill_Type"].n);
			if (FightAIData.DataDict.ContainsKey(skillId))
			{
				using (List<int>.Enumerator enumerator = FightAIData.DataDict[skillId].ShunXu.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int seid = enumerator.Current;
						this.realizeAIType(seid, skillId, list);
					}
					goto IL_EA;
				}
			}
			foreach (int num in jsonData.instance.AIJsonDate.Keys)
			{
				if (jsonData.instance.AIJsonDate[num].HasField(skillId.ToString()))
				{
					this.realizeAIType(num, skillId, list);
				}
			}
			IL_EA:
			return list[0];
		}

		// Token: 0x0600623E RID: 25150 RVA: 0x000441C7 File Offset: 0x000423C7
		public JSONObject getAIRealizID(int seid, int skillId)
		{
			return jsonData.instance.AIJsonDate[seid][skillId.ToString()];
		}

		// Token: 0x0600623F RID: 25151 RVA: 0x000441E5 File Offset: 0x000423E5
		public void setFlag(List<int> flag, int num)
		{
			if (num != 0)
			{
				flag[0] = num;
			}
		}

		// Token: 0x06006240 RID: 25152 RVA: 0x002737A8 File Offset: 0x002719A8
		public Avatar getAvatarByStr(JSONObject AIinfo)
		{
			Avatar result = null;
			if (AIinfo["panduan1"].str == "self")
			{
				result = (Avatar)this.entity;
			}
			else if (AIinfo["panduan1"].str == "other")
			{
				result = ((Avatar)this.entity).OtherAvatar;
			}
			else
			{
				Debug.LogError("AI表中" + AIinfo["id"].n + "技能（对象判定）字段错误");
			}
			return result;
		}

		// Token: 0x06006241 RID: 25153 RVA: 0x0027383C File Offset: 0x00271A3C
		public void setFlagByPanduan2(JSONObject AIinfo, int LeftNum, int rightNum, List<int> flag)
		{
			if (AIinfo["panduan2"].str == ">")
			{
				if (LeftNum > rightNum)
				{
					this.setFlag(flag, (int)AIinfo["Yes"].n);
					return;
				}
				this.setFlag(flag, (int)AIinfo["No"].n);
				return;
			}
			else
			{
				if (!(AIinfo["panduan2"].str == "<"))
				{
					Debug.LogError("AI表中" + AIinfo["id"].n + "技能（判断大小）字段错误");
					return;
				}
				if (LeftNum < rightNum)
				{
					this.setFlag(flag, (int)AIinfo["Yes"].n);
					return;
				}
				this.setFlag(flag, (int)AIinfo["No"].n);
				return;
			}
		}

		// Token: 0x06006242 RID: 25154 RVA: 0x0027391C File Offset: 0x00271B1C
		public void realizeAIType(int seid, int skillId, List<int> flag)
		{
			int i = 0;
			while (i < 500)
			{
				if (i == seid)
				{
					MethodInfo method = base.GetType().GetMethod("AIRealize" + seid);
					if (method != null)
					{
						method.Invoke(this, new object[]
						{
							seid,
							skillId,
							flag
						});
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06006243 RID: 25155 RVA: 0x00273988 File Offset: 0x00271B88
		public void AIRealize1(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			List<List<int>> buffByID = this.getAvatarByStr(airealizID).buffmag.getBuffByID((int)airealizID["value1"].n);
			int num = 0;
			foreach (List<int> list in buffByID)
			{
				num += list[1];
			}
			this.setFlagByPanduan2(airealizID, num, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x06006244 RID: 25156 RVA: 0x00273A20 File Offset: 0x00271C20
		public void AIRealize2(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int hp = this.getAvatarByStr(airealizID).HP;
			this.setFlagByPanduan2(airealizID, hp, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x00273A60 File Offset: 0x00271C60
		public void AIRealize3(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			if (avatar.UsedSkills.Count <= 0)
			{
				this.setFlag(flag, (int)airealizID["No"].n);
				return;
			}
			bool flag2 = false;
			using (List<JSONObject>.Enumerator enumerator = jsonData.instance.skillJsonData[avatar.UsedSkills[avatar.UsedSkills.Count - 1].ToString()]["AttackType"].list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((int)enumerator.Current.n == (int)airealizID["value1"].n)
					{
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				this.setFlag(flag, (int)airealizID["Yes"].n);
				return;
			}
			this.setFlag(flag, (int)airealizID["No"].n);
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x00273B74 File Offset: 0x00271D74
		public void AIRealize4(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int cardNum = this.getAvatarByStr(airealizID).crystal.getCardNum();
			this.setFlagByPanduan2(airealizID, cardNum, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x06006247 RID: 25159 RVA: 0x00273BB8 File Offset: 0x00271DB8
		public void AIRealize5(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatarByStr = this.getAvatarByStr(airealizID);
			if (avatarByStr.HP >= avatarByStr.HP_Max)
			{
				this.setFlag(flag, (int)airealizID["Yes"].n);
				return;
			}
			this.setFlag(flag, (int)airealizID["No"].n);
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x00273C18 File Offset: 0x00271E18
		public void AIRealize6(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			this.setFlagByPanduan2(airealizID, avatar.shengShi, avatar.OtherAvatar.shengShi, flag);
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x00273C54 File Offset: 0x00271E54
		public void AIRealize7(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatarByStr = this.getAvatarByStr(airealizID);
			int leftNum = avatarByStr.HP + avatarByStr.buffmag.getHuDun();
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x00273CA0 File Offset: 0x00271EA0
		public void AIRealize8(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).crystal[(int)airealizID["value1"].n];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x00273CF8 File Offset: 0x00271EF8
		public void AIRealize9(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).GetLingGeng[(int)airealizID["value1"].n];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x00273D50 File Offset: 0x00271F50
		public void AIRealize10(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			if ((long)avatar.cardMag.getCardNum() > (long)((ulong)avatar.NowCard))
			{
				this.setFlag(flag, (int)airealizID["Yes"].n);
				return;
			}
			this.setFlag(flag, (int)airealizID["No"].n);
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x00273DB8 File Offset: 0x00271FB8
		public void AIRealize11(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatarByStr = this.getAvatarByStr(airealizID);
			int num = 0;
			foreach (List<int> list in avatarByStr.bufflist)
			{
				if ((int)jsonData.instance.BuffJsonData[list[2].ToString()]["bufftype"].n == (int)airealizID["value1"].n)
				{
					num++;
				}
			}
			this.setFlagByPanduan2(airealizID, num, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00273E78 File Offset: 0x00272078
		public void AIRealize12(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).fightTemp.lastRoundDamage[1];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x00273EC0 File Offset: 0x002720C0
		public void AIRealize13(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).OtherAvatar.crystal[(int)airealizID["value1"].n];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x00273F1C File Offset: 0x0027211C
		public void AIRealize14(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatarByStr = this.getAvatarByStr(airealizID);
			int buffSum = avatarByStr.buffmag.GetBuffSum(airealizID["value1"].I);
			int buffSum2 = avatarByStr.buffmag.GetBuffSum(airealizID["value2"].I);
			this.setFlagByPanduan2(airealizID, buffSum, buffSum2, flag);
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00273F7C File Offset: 0x0027217C
		public void AIRealize15(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			int num = RoundManager.instance.StaticRoundNum;
			if (avatar.dunSu > avatar.OtherAvatar.dunSu)
			{
				num++;
			}
			int i = airealizID["value1"].I;
			this.setFlagByPanduan2(airealizID, num, i, flag);
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x00273FDC File Offset: 0x002721DC
		public void AIRealize16(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int count = ((Avatar)this.entity).fightTemp.NowRoundUsedSkills.Count;
			int i = airealizID["value1"].I;
			this.setFlagByPanduan2(airealizID, count, i, flag);
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x00274028 File Offset: 0x00272228
		public void AIRealize17(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			int fightType = (int)Tools.instance.monstarMag.FightType;
			if (airealizID["value1"].ToList().Contains(fightType))
			{
				this.setFlag(flag, (int)airealizID["Yes"].n);
				return;
			}
			this.setFlag(flag, (int)airealizID["No"].n);
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x002740A4 File Offset: 0x002722A4
		public void AIRealize30(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int buffSum = this.getAvatarByStr(airealizID).buffmag.GetBuffSum(airealizID["value1"].I);
			int i = airealizID["value2"].I;
			this.setFlagByPanduan2(airealizID, buffSum, i, flag);
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x002740F8 File Offset: 0x002722F8
		public void AIRealize31(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			RoundManager.instance.IsVirtual = true;
			avatar.spell.VirtualspellSkill(skillId, "");
			RoundManager.instance.IsVirtual = false;
			int virtualSkillDamage = RoundManager.instance.VirtualSkillDamage;
			int hp = avatar.OtherAvatar.HP;
			if (virtualSkillDamage >= hp)
			{
				this.setFlag(flag, (int)airealizID["Yes"].n);
				return;
			}
			this.setFlag(flag, (int)airealizID["No"].n);
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x002740A4 File Offset: 0x002722A4
		public void AIRealize32(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int buffSum = this.getAvatarByStr(airealizID).buffmag.GetBuffSum(airealizID["value1"].I);
			int i = airealizID["value2"].I;
			this.setFlagByPanduan2(airealizID, buffSum, i, flag);
		}

		// Token: 0x04005CD0 RID: 23760
		public Entity entity;

		// Token: 0x02001010 RID: 4112
		public enum entityState
		{
			// Token: 0x04005CD2 RID: 23762
			Dead = 1,
			// Token: 0x04005CD3 RID: 23763
			RoundEnd,
			// Token: 0x04005CD4 RID: 23764
			RoundStart,
			// Token: 0x04005CD5 RID: 23765
			GameStart,
			// Token: 0x04005CD6 RID: 23766
			WaitDoNext
		}

		// Token: 0x02001011 RID: 4113
		public enum skillWeight
		{
			// Token: 0x04005CD8 RID: 23768
			Circle = 1,
			// Token: 0x04005CD9 RID: 23769
			Draw,
			// Token: 0x04005CDA RID: 23770
			Buff,
			// Token: 0x04005CDB RID: 23771
			Attack,
			// Token: 0x04005CDC RID: 23772
			Defense,
			// Token: 0x04005CDD RID: 23773
			Other,
			// Token: 0x04005CDE RID: 23774
			Final = 20
		}
	}
}
