using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x0200110E RID: 4366
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets an int value")]
	public class SetInt : Action
	{
		// Token: 0x060074EF RID: 29935 RVA: 0x002B3869 File Offset: 0x002B1A69
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.intValue.Value;
			return 2;
		}

		// Token: 0x060074F0 RID: 29936 RVA: 0x002B3882 File Offset: 0x002B1A82
		public override void OnReset()
		{
			this.intValue.Value = 0;
			this.storeResult.Value = 0;
		}

		// Token: 0x0400609A RID: 24730
		[Tooltip("The int value to set")]
		public SharedInt intValue;

		// Token: 0x0400609B RID: 24731
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}
