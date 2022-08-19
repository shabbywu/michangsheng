using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples
{
	// Token: 0x02000FB3 RID: 4019
	[TaskCategory("YS")]
	[TaskDescription("检测当前技能是否可用")]
	public class CheckSkillType : Conditional
	{
		// Token: 0x06006FF3 RID: 28659 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06006FF4 RID: 28660 RVA: 0x002A8A5F File Offset: 0x002A6C5F
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.currentSkillIndex = (base.Owner.GetVariable("NowSkill") as SharedInt);
		}

		// Token: 0x06006FF5 RID: 28661 RVA: 0x002A8A98 File Offset: 0x002A6C98
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

		// Token: 0x04005C68 RID: 23656
		[Tooltip("当前技能id")]
		public AI.skillWeight weight = AI.skillWeight.Circle;

		// Token: 0x04005C69 RID: 23657
		protected Avatar avatar;

		// Token: 0x04005C6A RID: 23658
		private SharedInt currentSkillIndex;
	}
}
