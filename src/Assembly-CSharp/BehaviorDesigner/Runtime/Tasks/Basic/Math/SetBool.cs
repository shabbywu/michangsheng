using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x0200110C RID: 4364
	[TaskCategory("Basic/Math")]
	[TaskDescription("Sets a bool value")]
	public class SetBool : Action
	{
		// Token: 0x060074E9 RID: 29929 RVA: 0x002B37FB File Offset: 0x002B19FB
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.boolValue.Value;
			return 2;
		}

		// Token: 0x060074EA RID: 29930 RVA: 0x002B3814 File Offset: 0x002B1A14
		public override void OnReset()
		{
			this.boolValue.Value = false;
			this.storeResult.Value = false;
		}

		// Token: 0x04006096 RID: 24726
		[Tooltip("The bool value to set")]
		public SharedBool boolValue;

		// Token: 0x04006097 RID: 24727
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}
