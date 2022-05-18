using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015CB RID: 5579
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a bool value")]
	public class SetBool : Action
	{
		// Token: 0x060082E3 RID: 33507 RVA: 0x00059D0E File Offset: 0x00057F0E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.boolValue.Value;
			return 2;
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x00059D27 File Offset: 0x00057F27
		public override void OnReset()
		{
			this.boolValue.Value = false;
			this.storeResult.Value = false;
		}

		// Token: 0x04006FB9 RID: 28601
		[Tooltip("The bool value to set")]
		public SharedBool boolValue;

		// Token: 0x04006FBA RID: 28602
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}
