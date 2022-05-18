using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001682 RID: 5762
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class SetSkillValue : Action
	{
		// Token: 0x060085B3 RID: 34227 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x0005CB64 File Offset: 0x0005AD64
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.selfBehavior = this.gameObject.GetComponent<Behavior>();
		}

		// Token: 0x060085B5 RID: 34229 RVA: 0x002D1310 File Offset: 0x002CF510
		public override TaskStatus OnUpdate()
		{
			SharedVariable<int> sharedVariable = this.selfBehavior.GetVariable("optimalSkillID") as SharedInt;
			SharedVariable<int> sharedVariable2 = this.selfBehavior.GetVariable("optimalSkillWeight") as SharedInt;
			SharedInt sharedInt = this.selfBehavior.GetVariable("NowSkill") as SharedInt;
			int skillWeight = this.avatar.ai.getSkillWeight(this.avatar.skill[sharedInt.Value].skill_ID);
			sharedVariable2.Value = skillWeight;
			sharedVariable.Value = this.avatar.skill[sharedInt.Value].skill_ID;
			return 2;
		}

		// Token: 0x060085B6 RID: 34230 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007253 RID: 29267
		[Tooltip("The name of the animation")]
		public AI.skillWeight skillweith;

		// Token: 0x04007254 RID: 29268
		private Avatar avatar;

		// Token: 0x04007255 RID: 29269
		private Behavior selfBehavior;

		// Token: 0x04007256 RID: 29270
		private SharedInt tempWeith;
	}
}
