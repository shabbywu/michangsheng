using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001681 RID: 5761
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class SetNowSkill : Action
	{
		// Token: 0x060085AE RID: 34222 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x060085AF RID: 34223 RVA: 0x002D1290 File Offset: 0x002CF490
		public override void OnStart()
		{
			AvaterAddScript component = this.gameObject.GetComponent<AvaterAddScript>();
			this.avatar = (Avatar)component.entity;
			this.selfBehavior = this.gameObject.GetComponent<Behavior>();
		}

		// Token: 0x060085B0 RID: 34224 RVA: 0x002D12CC File Offset: 0x002CF4CC
		public override TaskStatus OnUpdate()
		{
			SharedInt nowSkill = this.NowSkill;
			int value = nowSkill.Value;
			nowSkill.Value = value + 1;
			if (this.avatar.skill.Count <= this.NowSkill.Value)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060085B1 RID: 34225 RVA: 0x0005CB56 File Offset: 0x0005AD56
		public override void OnReset()
		{
			this.NowSkill.Value = 0;
		}

		// Token: 0x0400724F RID: 29263
		[Tooltip("The name of the animation")]
		public SharedInt NowSkill;

		// Token: 0x04007250 RID: 29264
		private Avatar avatar;

		// Token: 0x04007251 RID: 29265
		private Behavior selfBehavior;

		// Token: 0x04007252 RID: 29266
		private SharedInt tempWeith;
	}
}
