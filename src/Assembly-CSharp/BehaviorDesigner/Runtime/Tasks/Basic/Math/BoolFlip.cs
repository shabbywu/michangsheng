using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020010FB RID: 4347
	[TaskCategory("Basic/Math")]
	[TaskDescription("Flips the value of the bool.")]
	public class BoolFlip : Action
	{
		// Token: 0x060074B6 RID: 29878 RVA: 0x002B2E2C File Offset: 0x002B102C
		public override TaskStatus OnUpdate()
		{
			this.boolVariable.Value = !this.boolVariable.Value;
			return 2;
		}

		// Token: 0x060074B7 RID: 29879 RVA: 0x002B2E48 File Offset: 0x002B1048
		public override void OnReset()
		{
			this.boolVariable.Value = false;
		}

		// Token: 0x04006068 RID: 24680
		[Tooltip("The bool to flip the value of")]
		public SharedBool boolVariable;
	}
}
