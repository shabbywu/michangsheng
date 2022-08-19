using System;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x02001103 RID: 4355
	[TaskCategory("Basic/Math")]
	[TaskDescription("Performs comparison between two integers: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class IntComparison : Conditional
	{
		// Token: 0x060074CE RID: 29902 RVA: 0x002B3300 File Offset: 0x002B1500
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

		// Token: 0x060074CF RID: 29903 RVA: 0x002B33E0 File Offset: 0x002B15E0
		public override void OnReset()
		{
			this.operation = IntComparison.Operation.LessThan;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
		}

		// Token: 0x0400607C RID: 24700
		[Tooltip("The operation to perform")]
		public IntComparison.Operation operation;

		// Token: 0x0400607D RID: 24701
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x0400607E RID: 24702
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x02001736 RID: 5942
		public enum Operation
		{
			// Token: 0x0400756C RID: 30060
			LessThan,
			// Token: 0x0400756D RID: 30061
			LessThanOrEqualTo,
			// Token: 0x0400756E RID: 30062
			EqualTo,
			// Token: 0x0400756F RID: 30063
			NotEqualTo,
			// Token: 0x04007570 RID: 30064
			GreaterThanOrEqualTo,
			// Token: 0x04007571 RID: 30065
			GreaterThan
		}
	}
}
