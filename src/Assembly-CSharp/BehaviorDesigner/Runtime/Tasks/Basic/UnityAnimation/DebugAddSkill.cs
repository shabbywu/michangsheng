using System;
using GUIPackage;
using KBEngine;
using YSGame.Fight;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001674 RID: 5748
	[TaskCategory("YS")]
	[TaskDescription("测试自动加载技能")]
	public class DebugAddSkill : Action
	{
		// Token: 0x0600856C RID: 34156 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x0600856D RID: 34157 RVA: 0x0005C941 File Offset: 0x0005AB41
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x0600856E RID: 34158 RVA: 0x002D0EE4 File Offset: 0x002CF0E4
		public override TaskStatus OnUpdate()
		{
			SharedInt sharedInt = this.skillID;
			int value = sharedInt.Value;
			sharedInt.Value = value + 1;
			if (!jsonData.instance.skillJsonData.HasField(this.skillID.Value.ToString()))
			{
				return 1;
			}
			Avatar avatar = this.avatar;
			GUIPackage.Skill skill = new GUIPackage.Skill(this.skillID.Value, 0, 10);
			avatar.skill.Add(skill);
			if (avatar.isPlayer())
			{
				foreach (UIFightSkillItem uifightSkillItem in UIFightPanel.Inst.FightSkills)
				{
					if (!uifightSkillItem.HasSkill)
					{
						uifightSkillItem.SetSkill(skill);
						break;
					}
				}
			}
			if (avatar.cardMag.getCardNum() < 30)
			{
				int num = 0;
				foreach (JSONObject jsonobject in jsonData.instance.skillJsonData[this.skillID.Value.ToString()]["skill_CastType"].list)
				{
					avatar.cardMag.addCard((int)jsonobject.n, (int)jsonData.instance.skillJsonData[this.skillID.Value.ToString()]["skill_Cast"][num].n);
					num++;
				}
				int num2 = 0;
				foreach (JSONObject jsonobject2 in jsonData.instance.skillJsonData[this.skillID.Value.ToString()]["skill_SameCastNum"].list)
				{
					avatar.cardMag.addCard(num2, (int)jsonobject2.n);
					num2++;
				}
			}
			return 2;
		}

		// Token: 0x0600856F RID: 34159 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x0400722E RID: 29230
		public SharedInt NowSkill;

		// Token: 0x0400722F RID: 29231
		public SharedInt skillID;

		// Token: 0x04007230 RID: 29232
		public SharedInt skillWeight;

		// Token: 0x04007231 RID: 29233
		private Avatar avatar;

		// Token: 0x04007232 RID: 29234
		private Behavior selfBehavior;

		// Token: 0x04007233 RID: 29235
		private SharedInt tempWeith;
	}
}
