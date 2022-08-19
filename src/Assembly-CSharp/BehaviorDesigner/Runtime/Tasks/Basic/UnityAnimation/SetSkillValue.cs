using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011C0 RID: 4544
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class SetSkillValue : Action
	{
		// Token: 0x060077A8 RID: 30632 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x002B9498 File Offset: 0x002B7698
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
			this.selfBehavior = this.gameObject.GetComponent<Behavior>();
		}

		// Token: 0x060077AA RID: 30634 RVA: 0x002B94C8 File Offset: 0x002B76C8
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

		// Token: 0x060077AB RID: 30635 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006324 RID: 25380
		[Tooltip("The name of the animation")]
		public AI.skillWeight skillweith;

		// Token: 0x04006325 RID: 25381
		private Avatar avatar;

		// Token: 0x04006326 RID: 25382
		private Behavior selfBehavior;

		// Token: 0x04006327 RID: 25383
		private SharedInt tempWeith;
	}
}
