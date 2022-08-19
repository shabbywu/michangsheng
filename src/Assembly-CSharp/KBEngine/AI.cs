using System;
using System.Collections.Generic;
using System.Reflection;
using BehaviorDesigner.Runtime;
using JSONClass;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C70 RID: 3184
	public class AI
	{
		// Token: 0x060057C3 RID: 22467 RVA: 0x0024746A File Offset: 0x0024566A
		public AI(Entity avater)
		{
			this.entity = avater;
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x0024747C File Offset: 0x0024567C
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

		// Token: 0x060057C5 RID: 22469 RVA: 0x002474D0 File Offset: 0x002456D0
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

		// Token: 0x060057C6 RID: 22470 RVA: 0x002475EC File Offset: 0x002457EC
		public JSONObject getAIRealizID(int seid, int skillId)
		{
			return jsonData.instance.AIJsonDate[seid][skillId.ToString()];
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x0024760A File Offset: 0x0024580A
		public void setFlag(List<int> flag, int num)
		{
			if (num != 0)
			{
				flag[0] = num;
			}
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x00247618 File Offset: 0x00245818
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

		// Token: 0x060057C9 RID: 22473 RVA: 0x002476AC File Offset: 0x002458AC
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

		// Token: 0x060057CA RID: 22474 RVA: 0x0024778C File Offset: 0x0024598C
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

		// Token: 0x060057CB RID: 22475 RVA: 0x002477F8 File Offset: 0x002459F8
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

		// Token: 0x060057CC RID: 22476 RVA: 0x00247890 File Offset: 0x00245A90
		public void AIRealize2(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int hp = this.getAvatarByStr(airealizID).HP;
			this.setFlagByPanduan2(airealizID, hp, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x002478D0 File Offset: 0x00245AD0
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

		// Token: 0x060057CE RID: 22478 RVA: 0x002479E4 File Offset: 0x00245BE4
		public void AIRealize4(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int cardNum = this.getAvatarByStr(airealizID).crystal.getCardNum();
			this.setFlagByPanduan2(airealizID, cardNum, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x00247A28 File Offset: 0x00245C28
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

		// Token: 0x060057D0 RID: 22480 RVA: 0x00247A88 File Offset: 0x00245C88
		public void AIRealize6(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatar = (Avatar)this.entity;
			this.setFlagByPanduan2(airealizID, avatar.shengShi, avatar.OtherAvatar.shengShi, flag);
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x00247AC4 File Offset: 0x00245CC4
		public void AIRealize7(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatarByStr = this.getAvatarByStr(airealizID);
			int leftNum = avatarByStr.HP + avatarByStr.buffmag.getHuDun();
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00247B10 File Offset: 0x00245D10
		public void AIRealize8(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).crystal[(int)airealizID["value1"].n];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00247B68 File Offset: 0x00245D68
		public void AIRealize9(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).GetLingGeng[(int)airealizID["value1"].n];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00247BC0 File Offset: 0x00245DC0
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

		// Token: 0x060057D5 RID: 22485 RVA: 0x00247C28 File Offset: 0x00245E28
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

		// Token: 0x060057D6 RID: 22486 RVA: 0x00247CE8 File Offset: 0x00245EE8
		public void AIRealize12(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).fightTemp.lastRoundDamage[1];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value1"].n, flag);
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00247D30 File Offset: 0x00245F30
		public void AIRealize13(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int leftNum = ((Avatar)this.entity).OtherAvatar.crystal[(int)airealizID["value1"].n];
			this.setFlagByPanduan2(airealizID, leftNum, (int)airealizID["value2"].n, flag);
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x00247D8C File Offset: 0x00245F8C
		public void AIRealize14(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			Avatar avatarByStr = this.getAvatarByStr(airealizID);
			int buffSum = avatarByStr.buffmag.GetBuffSum(airealizID["value1"].I);
			int buffSum2 = avatarByStr.buffmag.GetBuffSum(airealizID["value2"].I);
			this.setFlagByPanduan2(airealizID, buffSum, buffSum2, flag);
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00247DEC File Offset: 0x00245FEC
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

		// Token: 0x060057DA RID: 22490 RVA: 0x00247E4C File Offset: 0x0024604C
		public void AIRealize16(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int count = ((Avatar)this.entity).fightTemp.NowRoundUsedSkills.Count;
			int i = airealizID["value1"].I;
			this.setFlagByPanduan2(airealizID, count, i, flag);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00247E98 File Offset: 0x00246098
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

		// Token: 0x060057DC RID: 22492 RVA: 0x00247F14 File Offset: 0x00246114
		public void AIRealize30(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int buffSum = this.getAvatarByStr(airealizID).buffmag.GetBuffSum(airealizID["value1"].I);
			int i = airealizID["value2"].I;
			this.setFlagByPanduan2(airealizID, buffSum, i, flag);
		}

		// Token: 0x060057DD RID: 22493 RVA: 0x00247F68 File Offset: 0x00246168
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

		// Token: 0x060057DE RID: 22494 RVA: 0x00247FFC File Offset: 0x002461FC
		public void AIRealize32(int seid, int skillId, List<int> flag)
		{
			JSONObject airealizID = this.getAIRealizID(seid, skillId);
			int buffSum = this.getAvatarByStr(airealizID).buffmag.GetBuffSum(airealizID["value1"].I);
			int i = airealizID["value2"].I;
			this.setFlagByPanduan2(airealizID, buffSum, i, flag);
		}

		// Token: 0x040051F2 RID: 20978
		public Entity entity;

		// Token: 0x02001615 RID: 5653
		public enum entityState
		{
			// Token: 0x0400713B RID: 28987
			Dead = 1,
			// Token: 0x0400713C RID: 28988
			RoundEnd,
			// Token: 0x0400713D RID: 28989
			RoundStart,
			// Token: 0x0400713E RID: 28990
			GameStart,
			// Token: 0x0400713F RID: 28991
			WaitDoNext
		}

		// Token: 0x02001616 RID: 5654
		public enum skillWeight
		{
			// Token: 0x04007141 RID: 28993
			Circle = 1,
			// Token: 0x04007142 RID: 28994
			Draw,
			// Token: 0x04007143 RID: 28995
			Buff,
			// Token: 0x04007144 RID: 28996
			Attack,
			// Token: 0x04007145 RID: 28997
			Defense,
			// Token: 0x04007146 RID: 28998
			Other,
			// Token: 0x04007147 RID: 28999
			Final = 20
		}
	}
}
