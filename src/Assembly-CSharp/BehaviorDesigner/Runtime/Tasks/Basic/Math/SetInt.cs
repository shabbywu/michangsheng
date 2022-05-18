using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015CD RID: 5581
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets an int value")]
	public class SetInt : Action
	{
		// Token: 0x060082E9 RID: 33513 RVA: 0x00059D7C File Offset: 0x00057F7C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.intValue.Value;
			return 2;
		}

		// Token: 0x060082EA RID: 33514 RVA: 0x00059D95 File Offset: 0x00057F95
		public override void OnReset()
		{
			this.intValue.Value = 0;
			this.storeResult.Value = 0;
		}

		// Token: 0x04006FBD RID: 28605
		[Tooltip("The int value to set")]
		public SharedInt intValue;

		// Token: 0x04006FBE RID: 28606
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}
