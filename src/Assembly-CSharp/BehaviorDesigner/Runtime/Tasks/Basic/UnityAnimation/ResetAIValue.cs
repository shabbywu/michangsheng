using System;
using KBEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001676 RID: 5750
	[TaskCategory("YS")]
	[TaskDescription("设置技能最优值")]
	public class ResetAIValue : Action
	{
		// Token: 0x06008577 RID: 34167 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnAwake()
		{
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x0005C992 File Offset: 0x0005AB92
		public override void OnStart()
		{
			this.avatar = (Avatar)this.gameObject.GetComponent<AvaterAddScript>().entity;
		}

		// Token: 0x06008579 RID: 34169 RVA: 0x0005C9AF File Offset: 0x0005ABAF
		public override TaskStatus OnUpdate()
		{
			this.NowSkill.Value = -1;
			this.skillID.Value = 0;
			this.skillWeight.Value = 999;
			return 2;
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x000042DD File Offset: 0x000024DD
		public override void OnReset()
		{
		}

		// Token: 0x04007236 RID: 29238
		public SharedInt NowSkill;

		// Token: 0x04007237 RID: 29239
		public SharedInt skillID;

		// Token: 0x04007238 RID: 29240
		public SharedInt skillWeight;

		// Token: 0x04007239 RID: 29241
		private Avatar avatar;

		// Token: 0x0400723A RID: 29242
		private Behavior selfBehavior;

		// Token: 0x0400723B RID: 29243
		private SharedInt tempWeith;
	}
}
