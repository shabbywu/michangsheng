using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples
{
	// Token: 0x0200146B RID: 5227
	[TaskCategory("YS")]
	[TaskDescription("检测当前技能是否可用")]
	public class CheckSkillType : Conditional
	{
		// Token: 0x06007DED RID: 32237 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x00055226 File Offset: 0x00053426
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.currentSkillIndex = (base.Owner.GetVariable("NowSkill") as SharedInt);
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x002C81DC File Offset: 0x002C63DC
		public override TaskStatus OnUpdate()
		{
			int skill_ID = this.avatar.skill[(int)this.currentSkillIndex.GetValue()].skill_ID;
			SharedInt sharedInt = base.Owner.GetVariable("optimalSkillWeight") as SharedInt;
			if (this.avatar.ai.getSkillWeight(skill_ID) < (int)sharedInt.GetValue())
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x04006B60 RID: 27488
		[Tooltip("当前技能id")]
		public AI.skillWeight weight = AI.skillWeight.Circle;

		// Token: 0x04006B61 RID: 27489
		protected Avatar avatar;

		// Token: 0x04006B62 RID: 27490
		private SharedInt currentSkillIndex;
	}
}
