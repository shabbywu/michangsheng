using System;
using GUIPackage;
using KBEngine;
using YSGame.Fight;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B2 RID: 4530
	[TaskCategory("YS")]
	[TaskDescription("测试自动加载技能")]
	public class DebugAddSkill : Action
	{
		// Token: 0x06007760 RID: 30560 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x002B8DDA File Offset: 0x002B6FDA
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06007762 RID: 30562 RVA: 0x002B8DF8 File Offset: 0x002B6FF8
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

		// Token: 0x06007763 RID: 30563 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x040062FF RID: 25343
		public SharedInt NowSkill;

		// Token: 0x04006300 RID: 25344
		public SharedInt skillID;

		// Token: 0x04006301 RID: 25345
		public SharedInt skillWeight;

		// Token: 0x04006302 RID: 25346
		private Avatar avatar;

		// Token: 0x04006303 RID: 25347
		private Behavior selfBehavior;

		// Token: 0x04006304 RID: 25348
		private SharedInt tempWeith;
	}
}
