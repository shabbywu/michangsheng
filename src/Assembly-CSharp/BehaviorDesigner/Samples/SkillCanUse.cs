using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples
{
	// Token: 0x02000FB6 RID: 4022
	[TaskCategory("YS")]
	[TaskDescription("检测当前技能是否可用")]
	public class SkillCanUse : Conditional
	{
		// Token: 0x06006FFF RID: 28671 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x06007000 RID: 28672 RVA: 0x002A8BC3 File Offset: 0x002A6DC3
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06007001 RID: 28673 RVA: 0x002A8BE0 File Offset: 0x002A6DE0
		public override TaskStatus OnUpdate()
		{
			if (this.avatar.skill[(int)this.currentSkillIndex.GetValue()].skill_ID <= 0)
			{
				return 1;
			}
			if (this.avatar.skill[(int)this.currentSkillIndex.GetValue()].CanUse(this.avatar, this.avatar, true, "") == SkillCanUseType.可以使用)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x04005C6E RID: 23662
		[Tooltip("当前技能id")]
		public SharedInt currentSkillIndex;

		// Token: 0x04005C6F RID: 23663
		private Avatar avatar;
	}
}
