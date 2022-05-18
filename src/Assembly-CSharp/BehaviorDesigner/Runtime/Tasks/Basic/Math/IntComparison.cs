using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C0 RID: 5568
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs comparison between two integers: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class IntComparison : Conditional
	{
		// Token: 0x060082C8 RID: 33480 RVA: 0x002CE0C0 File Offset: 0x002CC2C0
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case IntComparison.Operation.LessThan:
				if (this.integer1.Value >= this.integer2.Value)
				{
					return 1;
				}
				return 2;
			case IntComparison.Operation.LessThanOrEqualTo:
				if (this.integer1.Value > this.integer2.Value)
				{
					return 1;
				}
				return 2;
			case IntComparison.Operation.EqualTo:
				if (this.integer1.Value != this.integer2.Value)
				{
					return 1;
				}
				return 2;
			case IntComparison.Operation.NotEqualTo:
				if (this.integer1.Value == this.integer2.Value)
				{
					return 1;
				}
				return 2;
			case IntComparison.Operation.GreaterThanOrEqualTo:
				if (this.integer1.Value < this.integer2.Value)
				{
					return 1;
				}
				return 2;
			case IntComparison.Operation.GreaterThan:
				if (this.integer1.Value <= this.integer2.Value)
				{
					return 1;
				}
				return 2;
			default:
				return 1;
			}
		}

		// Token: 0x060082C9 RID: 33481 RVA: 0x00059B80 File Offset: 0x00057D80
		public override void OnReset()
		{
			this.operation = IntComparison.Operation.LessThan;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
		}

		// Token: 0x04006F90 RID: 28560
		[Tooltip("The operation to perform")]
		public IntComparison.Operation operation;

		// Token: 0x04006F91 RID: 28561
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x04006F92 RID: 28562
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x020015C1 RID: 5569
		public enum Operation
		{
			// Token: 0x04006F94 RID: 28564
			LessThan,
			// Token: 0x04006F95 RID: 28565
			LessThanOrEqualTo,
			// Token: 0x04006F96 RID: 28566
			EqualTo,
			// Token: 0x04006F97 RID: 28567
			NotEqualTo,
			// Token: 0x04006F98 RID: 28568
			GreaterThanOrEqualTo,
			// Token: 0x04006F99 RID: 28569
			GreaterThan
		}
	}
}
