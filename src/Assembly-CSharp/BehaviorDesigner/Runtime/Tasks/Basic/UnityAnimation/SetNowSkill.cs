using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011BF RID: 4543
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class SetNowSkill : Action
	{
		// Token: 0x060077A3 RID: 30627 RVA: 0x00004095 File Offset: 0x00002295
		public override void OnAwake()
		{
		}

		// Token: 0x060077A4 RID: 30628 RVA: 0x002B940C File Offset: 0x002B760C
		public override void OnStart()
		{
			AvaterAddScript component = this.gameObject.GetComponent<AvaterAddScript>();
			this.avatar = (Avatar)component.entity;
			this.selfBehavior = this.gameObject.GetComponent<Behavior>();
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x002B9448 File Offset: 0x002B7648
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

		// Token: 0x060077A6 RID: 30630 RVA: 0x002B948A File Offset: 0x002B768A
		public override void OnReset()
		{
			this.NowSkill.Value = 0;
		}

		// Token: 0x04006320 RID: 25376
		[Tooltip("The name of the animation")]
		public SharedInt NowSkill;

		// Token: 0x04006321 RID: 25377
		private Avatar avatar;

		// Token: 0x04006322 RID: 25378
		private Behavior selfBehavior;

		// Token: 0x04006323 RID: 25379
		private SharedInt tempWeith;
	}
}
