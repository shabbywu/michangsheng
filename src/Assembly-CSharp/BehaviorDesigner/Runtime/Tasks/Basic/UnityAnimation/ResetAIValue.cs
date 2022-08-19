using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B4 RID: 4532
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class ResetAIValue : Action
	{
		// Token: 0x0600776C RID: 30572 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x0600776D RID: 30573 RVA: 0x002B910D File Offset: 0x002B730D
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x0600776E RID: 30574 RVA: 0x002B912A File Offset: 0x002B732A
		public override TaskStatus OnUpdate()
		{
			this.NowSkill.Value = -1;
			this.skillID.Value = 0;
			this.skillWeight.Value = 999;
			return 2;
		}

		// Token: 0x0600776F RID: 30575 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnReset()
		{
		}

		// Token: 0x04006307 RID: 25351
		public SharedInt NowSkill;

		// Token: 0x04006308 RID: 25352
		public SharedInt skillID;

		// Token: 0x04006309 RID: 25353
		public SharedInt skillWeight;

		// Token: 0x0400630A RID: 25354
		private Avatar avatar;

		// Token: 0x0400630B RID: 25355
		private Behavior selfBehavior;

		// Token: 0x0400630C RID: 25356
		private SharedInt tempWeith;
	}
}
