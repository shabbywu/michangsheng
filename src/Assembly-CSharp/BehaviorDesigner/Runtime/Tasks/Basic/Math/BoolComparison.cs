using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015B4 RID: 5556
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs a comparison between two bools.")]
	public class BoolComparison : Conditional
	{
		// Token: 0x060082AD RID: 33453 RVA: 0x000599CC File Offset: 0x00057BCC
		public override TaskStatus OnUpdate()
		{
			if (this.bool1.Value != this.bool2.Value)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x000599E9 File Offset: 0x00057BE9
		public override void OnReset()
		{
			this.bool1.Value = false;
			this.bool2.Value = false;
		}

		// Token: 0x04006F66 RID: 28518
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x04006F67 RID: 28519
		[Tooltip("The second bool")]
		public SharedBool bool2;
	}
}
