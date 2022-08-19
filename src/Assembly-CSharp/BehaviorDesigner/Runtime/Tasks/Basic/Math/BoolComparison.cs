using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020010FA RID: 4346
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a comparison between two bools.")]
	public class BoolComparison : Conditional
	{
		// Token: 0x060074B3 RID: 29875 RVA: 0x002B2DF5 File Offset: 0x002B0FF5
		public override TaskStatus OnUpdate()
		{
			if (this.bool1.Value != this.bool2.Value)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060074B4 RID: 29876 RVA: 0x002B2E12 File Offset: 0x002B1012
		public override void OnReset()
		{
			this.bool1.Value = false;
			this.bool2.Value = false;
		}

		// Token: 0x04006066 RID: 24678
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x04006067 RID: 24679
		[Tooltip("The second bool")]
		public SharedBool bool2;
	}
}
