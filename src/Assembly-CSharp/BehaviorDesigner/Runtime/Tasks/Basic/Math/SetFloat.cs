using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015CC RID: 5580
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a float value")]
	public class SetFloat : Action
	{
		// Token: 0x060082E6 RID: 33510 RVA: 0x00059D41 File Offset: 0x00057F41
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.floatValue.Value;
			return 2;
		}

		// Token: 0x060082E7 RID: 33511 RVA: 0x00059D5A File Offset: 0x00057F5A
		public override void OnReset()
		{
			this.floatValue.Value = 0f;
			this.storeResult.Value = 0f;
		}

		// Token: 0x04006FBB RID: 28603
		[Tooltip("The float value to set")]
		public SharedFloat floatValue;

		// Token: 0x04006FBC RID: 28604
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}
