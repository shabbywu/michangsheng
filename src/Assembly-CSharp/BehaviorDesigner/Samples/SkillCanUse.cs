using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using KBEngine;

namespace BehaviorDesigner.Samples
{
	// Token: 0x0200146E RID: 5230
	[TaskCategory("YS")]
	[TaskDescription("检测当前技能是否可用")]
	public class SkillCanUse : Conditional
	{
		// Token: 0x06007DF9 RID: 32249 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x000552B7 File Offset: 0x000534B7
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x002C82B0 File Offset: 0x002C64B0
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

		// Token: 0x04006B66 RID: 27494
		[Tooltip("当前技能id")]
		public SharedInt currentSkillIndex;

		// Token: 0x04006B67 RID: 27495
		private Avatar avatar;
	}
}
